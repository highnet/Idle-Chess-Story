﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStatus { Initializing, Fight, AwaitingWizardConfirmation, Wait, Shopping, ReportDefeat, GameOver, ReportVictory }

public class BoardController : MonoBehaviour
{
    NpcController npcController;
    UiController uiController;
    PlayerController playerController;
    SessionLogger sessionLogger;
    Translator translator;
    public GameObject[,] chessBoard;
    public GameObject[] reserveBoard;
    public List<GameObject> unitsList;
    public GameObject selectedNPC;
    public AssignableItemDrop selectedItemDrop;
    public GameObject tilePrefab;
    public MainCamera mainCameraController;
    public bool testDummyMode = false;
    public bool testItemDrops = false;
    public GameStatus gameStatus;
    public int currentGameRound = 1;
    public GameObject friendlySideIndicatorPlane;
    public GameObject rainSystem;
    public AmbienceSound ambienceSound;
    public Leaderboard steamLeaderboard;
    public Achievements steamAchievements;
    public float constant_TimeAllowedForCombatBeforeForceStop;
    public float combatRoundTimer = 0f;
    public List<GameObject> DroppedItemList;
    public Camera mainCamera;
    public NPC mousedOverNPC;
    public Item FocusedItem;

    private void Awake()
    {
        npcController = GetComponent<NpcController>(); //fetch world controllers
        uiController = GetComponent<UiController>();
        playerController = GetComponent<PlayerController>();
        sessionLogger = GetComponent<SessionLogger>();
        translator = GetComponent<Translator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeGameStatus(GameStatus.Initializing); // set initalizing status
        CreateBoardTiles(); // create the tile 9x8 array board
        ChangeGameStatus(GameStatus.AwaitingWizardConfirmation); // set to settings wizard status
        FocusedItem = new Item(ItemName.NO_ITEM);
    }

    void CreateBoardTiles()
    {
        chessBoard = new GameObject[9, 8]; // initialize the chessboard array
        for (int i = 0; i < 9; i++) // 9 rows
        {
            for (int j = 0; j < 8; j++) // 8 collumns
            {

                GameObject spawn = GameObject.Instantiate(tilePrefab, new Vector3(i == 8 ? i - 2.85f : i - 3.435f, 0, 3.435f - j), Quaternion.Euler(0, -90, 0)); // create a tile in the right position
                spawn.gameObject.name = "Chess Tile[" + i + "]" + "[" + j + "]"; // name the tile gameobject
                spawn.transform.parent = this.gameObject.transform; //set the tile parent to the game board
                spawn.GetComponent<TileBehaviour>().i = i; // store the i index inside the tile 
                spawn.GetComponent<TileBehaviour>().j = j;// store the j index inside the tile 
                chessBoard[i, j] = spawn; // store the tile inside the chessboard array
            }
        }
    }


    public void SpawnEnemyUnitsRound_Balanced() // copy the human players board and deply a similar team
    {
        if (!testDummyMode) // test mode spawns dummies instead of real units
        {

            if (currentGameRound == 1 || currentGameRound == 2 || currentGameRound == 5 || currentGameRound == 7 || currentGameRound == 11 || currentGameRound == 15 || currentGameRound == 19)
            {
                playerController.ReInitializeEnemyActiveTribesCounter(); // reset the enemy tribe counter
                for (int i = 0; i < 2 + (currentGameRound / 2); i++)
                {
                    int randomI = UnityEngine.Random.Range(0, 4); // find a random i coordinate
                    int randomJ = UnityEngine.Random.Range(0, 8); // find a random j coordinate
                    TrySpawnUnit(randomI, randomJ, Unit.RobotCreep, true, 0, true); // spawn creep
                }
            }

            else if (currentGameRound == 20) // boss round 
            {
                GameObject spawnedEnemy = NewMethod();
                if (spawnedEnemy != null) // this can be used to test enemy tribe bonuses
                {
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE); //
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
                }
            }
            else if (currentGameRound == 12) // boss round 
            {
                playerController.ReInitializeEnemyActiveTribesCounter(); // reset the enemy tribe counter
                GameObject spawnedEnemy = TrySpawnUnit(2, 4, Unit.AlienSoldier, true, 0, true); // spawn alien soldier
                if (spawnedEnemy != null) // this can be used to test enemy tribe bonuses
                {
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE); //
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
                }
            }
            else if (currentGameRound == 6) // boss round 
            {
                playerController.ReInitializeEnemyActiveTribesCounter(); // reset the enemy tribe counter
                GameObject spawnedEnemy = TrySpawnUnit(2, 4, Unit.Eyebat, true, 0, true); // spawn eyebat
                if (spawnedEnemy != null) // this can be used to test enemy tribe bonuses
                {
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE); //
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
                }
            }
            else // normal round
            {
                float spawningBudget = playerController.sessionLogger.goldRewarded;

                int currentDificulty = uiController.difficultyPicker.value;
                Debug.Log("difficulty: " + currentDificulty);
                if (currentDificulty == 0)
                {
                    spawningBudget *= 0.5f;
                }
                if (currentDificulty == 1)
                {
                    spawningBudget *= 0.6f;
                }
                else if (currentDificulty == 2)
                {
                    spawningBudget *= 0.7f;
                }
                if (spawningBudget < 6)
                {
                    spawningBudget = 6;
                }


                Tribe randomTribe;
                randomTribe = (Tribe)Enum.GetValues(typeof(Tribe)).GetValue((int)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Tribe)).Length)); // get a random tribe
                bool doneTrying = false;
                while (doneTrying == false) // we dont want the tribe to spawn to be structures only at the moment
                {
                    randomTribe = (Tribe)Enum.GetValues(typeof(Tribe)).GetValue((int)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Tribe)).Length)); // if we got structure tribe, try again until we dont
                    if (randomTribe == Tribe.Structure)
                    {
                        doneTrying = false; // go agane
                    }
                    else
                    {
                        doneTrying = true; // we gucci
                    }
                }

                List<Unit> spawnList;
                playerController.TRIBAL_UNIT_DATA.TryGetValue(randomTribe, out spawnList); // get a list of all units of our random tribe

                playerController.ReInitializeEnemyActiveTribesCounter(); // reset the enemy tribe counter

                int enemySpawnCount = playerController.maxDeployedUnitsLimit;
                int bonusSpawns = 0;
                if (currentGameRound <= 5)
                {
                    if (currentDificulty == 0)
                    {
                        bonusSpawns = UnityEngine.Random.Range(0, 2);
                    }
                    else if (currentDificulty == 1)
                    {
                        bonusSpawns = UnityEngine.Random.Range(0, 3);

                    }
                    else if (currentDificulty == 2)
                    {
                        bonusSpawns = UnityEngine.Random.Range(0, 3);
                    }
                }
                else if (currentGameRound <= 10)
                {
                    if (currentDificulty == 0)
                    {
                        bonusSpawns = UnityEngine.Random.Range(0, 3);
                    }
                    else if (currentDificulty == 1)
                    {
                        bonusSpawns = UnityEngine.Random.Range(0, 3);

                    }
                    else if (currentDificulty == 2)
                    {
                        bonusSpawns = UnityEngine.Random.Range(0, 4);
                    }
                }
                else if (currentGameRound <= 15)
                {
                    if (currentDificulty == 0)
                    {
                        bonusSpawns = UnityEngine.Random.Range(0, 4);
                    }
                    else if (currentDificulty == 1)
                    {
                        bonusSpawns = UnityEngine.Random.Range(0, 4);

                    }
                    else if (currentDificulty == 2)
                    {
                        bonusSpawns = UnityEngine.Random.Range(0, 5);
                    }
                }


                enemySpawnCount += bonusSpawns;
                Debug.Log("bonus spawns: " + bonusSpawns);
                Debug.Log("spawn count: " + enemySpawnCount);


                for (int i = 0; i < enemySpawnCount; i++) // spawn tier 1 units
                {
                    int randomI = UnityEngine.Random.Range(0, 4); // find a random i coordinate
                    int randomJ = UnityEngine.Random.Range(0, 8); // find a random j coordinate
                    Unit randomUnit = spawnList.ToArray()[UnityEngine.Random.Range(0, spawnList.ToArray().Length)]; // get a random unit from the list of units
                    playerController.NPC_COST_DATA.TryGetValue(randomUnit, out int unitCost);
                    if (spawningBudget >= unitCost)
                    {
                        GameObject spawnedEnemy = TrySpawnUnit(randomI, randomJ, randomUnit, true, 1, false); // try to spawn the unit
                        if (spawnedEnemy != null) // if we spawned a real unit
                        {
                            Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE); // increment the enemy tribe counter (primary tribe)
                            Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE); // increment the enemy tribe counter (secondary tribe)
                        }
                        spawningBudget -= unitCost;
                    }
                }

                bool skippedFirstLevelup = false;
                bool skippedOneTierThreeLevelup = false;

                foreach (NPC npc in npcController.enemyList) // give NPCs levelups
                {
                    if (!skippedFirstLevelup)
                    {
                        skippedFirstLevelup = true;
                        continue;
                    }
                    playerController.NPC_COST_DATA.TryGetValue(npc.UNIT_TYPE, out int unitCost);
                    float t2UpgradeCost = 3 * unitCost;
                    float t3UpgradeCost = 3 * t2UpgradeCost;
                    if (spawningBudget >= t3UpgradeCost)
                    {
                        npc.ApplyTier2Upgrades();
                        if (!skippedOneTierThreeLevelup)
                        {
                            npc.ApplyTier2Upgrades();
                            spawningBudget -= t2UpgradeCost;
                            skippedOneTierThreeLevelup = true;
                            continue;
                        }
                        npc.ApplyTier3Upgrades();
                        spawningBudget -= t3UpgradeCost;
                    }
                    else if (spawningBudget >= t2UpgradeCost)
                    {
                        npc.ApplyTier2Upgrades();
                        spawningBudget -= t2UpgradeCost;
                    }
                }
                int itemsAllowedBudget = 0;
                if (uiController.difficultyPicker.value == 0)
                {
                    itemsAllowedBudget = sessionLogger.itemDropsEarned + UnityEngine.Random.Range(-1, 2);
                }
                else if (uiController.difficultyPicker.value == 1)
                {
                    itemsAllowedBudget = sessionLogger.itemDropsEarned + UnityEngine.Random.Range(0, 2);
                }
                else if (uiController.difficultyPicker.value == 2)
                {
                    itemsAllowedBudget = sessionLogger.itemDropsEarned + UnityEngine.Random.Range(0, 2);
                }
                int[] itemDistribution = new int[npcController.enemyList.Count];

                for(int l = 0; l < itemsAllowedBudget; l++)
                {
                    int randomIndex = UnityEngine.Random.Range(0, itemDistribution.Length);
                    if (itemDistribution[randomIndex] < 4)
                    {
                        itemDistribution[randomIndex]++;
                    }
                }
                
                for(int i = 0; i < npcController.enemyList.Count; i++)
                {
                    for (int j = 0; j < itemDistribution[i]; j++)
                    {
                        if (npcController.enemyList[i].Inventory.Count < 4)
                        {
                        int RarityRoll = UnityEngine.Random.Range(0, 101);
                        ItemRarity rolledRarity = ItemRarity.Trash;
                        if (RarityRoll >= 60 && RarityRoll < 90)
                        {
                            rolledRarity = ItemRarity.Common;
                        }
                        else if (RarityRoll >= 90 && RarityRoll < 99)
                        {
                            rolledRarity = ItemRarity.Rare;
                        }
                        else if (RarityRoll >= 99)
                        {
                            rolledRarity = ItemRarity.Artifact;
                        }
                        List<ItemName> possibleDrops;
                        playerController.ITEM_RARITY_DATA.TryGetValue(rolledRarity, out possibleDrops); // get a list of all units of our possible item drops
                        int possibleDropIndex = UnityEngine.Random.Range(0, possibleDrops.Count);
                        ItemName itemName = possibleDrops[possibleDropIndex];
                        Item rngItem = new Item(itemName);
                        npcController.enemyList[i].Inventory.Add(rngItem);
                        }
                    }
                }
                /*
                bool skippedFirstItemAssignation = false;

                foreach (NPC npc in npcController.enemyList) // give NPCs items
                {
                    if (!skippedFirstItemAssignation)
                    {
                        skippedFirstItemAssignation = true;
                        continue;
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        if (itemsAllowedBudget > 0)
                        {
                            int RarityRoll = UnityEngine.Random.Range(0, 101);
                            ItemRarity rolledRarity = ItemRarity.Trash;
                            if (RarityRoll >= 60 && RarityRoll < 90)
                            {
                                rolledRarity = ItemRarity.Common;
                            }
                            else if (RarityRoll >= 90 && RarityRoll < 99)
                            {
                                rolledRarity = ItemRarity.Rare;
                            }
                            else if (RarityRoll >= 99)
                            {
                                rolledRarity = ItemRarity.Artifact;
                            }
                            List<ItemName> possibleDrops;
                            playerController.ITEM_RARITY_DATA.TryGetValue(rolledRarity, out possibleDrops); // get a list of all units of our possible item drops
                            int possibleDropIndex = UnityEngine.Random.Range(0, possibleDrops.Count);
                            ItemName itemName = possibleDrops[possibleDropIndex];
                            Item rngItem = new Item(itemName);
                            npc.Inventory.Add(rngItem);
                            itemsAllowedBudget--;
                        }
                        else
                        {
                            break;
                        }
                        npc.RecalculateInventoryItemValues();
                    }
                }
                */
            }
        }


        else // if in test mode
        {
            playerController.ReInitializeEnemyActiveTribesCounter(); // reset the enemy tribe counter
            GameObject spawnedEnemy = TrySpawnDummy(3, 5, true); // spawn dummy
            if (spawnedEnemy != null) // this can be used to test enemy tribe bonuses
            {
                Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE); //
                Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
            }

        }

    }

    private GameObject NewMethod()
    {
        playerController.ReInitializeEnemyActiveTribesCounter(); // reset the enemy tribe counter
        GameObject spawnedEnemy = TrySpawnUnit(2, 4, Unit.Engineer, true, 0, true); // spawn engineer
        return spawnedEnemy;
    }

    public GameObject TrySpawnUnit(int i, int j, Unit unitToSpawn, bool isEnemySpawn, int tierOverride, bool isCreep) // try to spawn a unit and return it
    {

        string prefabName = Enum.GetName(typeof(Unit), unitToSpawn); //get the units name as defined in the enum
        GameObject tile = chessBoard[i, j]; // get the tile we want to spawn on

        if (tile.GetComponent<TileBehaviour>().occupyingUnit != null) // tile is occupied
        {
            return null; // tile occupied is occupied so we break the function.
        }

        Quaternion spawnRotation = new Quaternion();

        if (isEnemySpawn)
        {
            spawnRotation = Quaternion.Euler(0, 90, 0); // fix the default spawn rotation for enemy
        }
        else
        {
            spawnRotation = Quaternion.Euler(0, -90, 0); //fix the default spawn rotation for friendly
        }

        GameObject spawnedNPC = (GameObject)GameObject.Instantiate(Resources.Load(prefabName), tile.transform.position, spawnRotation); // finally spawn the object
        spawnedNPC.GetComponentInChildren<NPC>().isEnemy = isEnemySpawn; //initialize the object
        tile.GetComponent<TileBehaviour>().occupyingUnit = spawnedNPC; //init
        spawnedNPC.GetComponentInChildren<NPC>().occupyingTile = tile;//init
        spawnedNPC.GetComponentInChildren<NPC>().PrepareNPC3DHud(); //init


        if (tierOverride == 2) //force spawn override tier to tier2
        {
            spawnedNPC.GetComponentInChildren<NPC>().ApplyTier2Upgrades(); //apply t2 upgrades
        }
        else if (tierOverride == 3) //force spawn override tier to tier3
        {
            spawnedNPC.GetComponentInChildren<NPC>().ApplyTier2Upgrades(); //apply t2 upgrades
            spawnedNPC.GetComponentInChildren<NPC>().ApplyTier3Upgrades(); //also apply the t3 upgrades
        }

        npcController.UpdateNpcList(); // update the NPC lists with the new NPC

        return spawnedNPC; // return the created object
    }

    public GameObject TrySpawnDummy(int i, int j, bool isEnemy) // try spawn a dummy
    {
        GameObject tile = chessBoard[i, j]; // get the tile we want to spawn on

        if (tile.GetComponent<TileBehaviour>().occupyingUnit != null) // if its an occupied tile break out of the function
        {
            return null;
        }

        Quaternion spawnRotation = new Quaternion();

        if (isEnemy)
        {
            spawnRotation = Quaternion.Euler(0, 90, 0); // fix the default spawn rotation

        }
        else
        {
            spawnRotation = Quaternion.Euler(0, -90, 0); // fix the default spawn rotation 
        }

        GameObject spawnedDummy = (GameObject)GameObject.Instantiate(Resources.Load("Dummy"), tile.transform.position, spawnRotation); // spawn the dummy
        spawnedDummy.GetComponentInChildren<NPC>().isEnemy = isEnemy; // initialize the dummy
        tile.GetComponent<TileBehaviour>().occupyingUnit = spawnedDummy; //init
        spawnedDummy.GetComponentInChildren<NPC>().occupyingTile = tile; //init
        spawnedDummy.GetComponentInChildren<NPC>().PrepareNPC3DHud(); //init
        npcController.UpdateNpcList(); // update the npc list with the new dummy

        return spawnedDummy; // return the dummy
    }

    public void ChangeGameStatus(GameStatus status) // change game status and display string status
    {
        gameStatus = status;
        uiController.UpdateGameStatusText(status);

    }

    public void ChangeCurrentRound(int round) // change the current game round
    {
        currentGameRound = round;
        uiController.ChangeCurrentRoundDisplayText(currentGameRound);
    }

    IEnumerator ProgressConcentrationRegeneration() // progress the global concentration regeneration coroutine
    {
        for (; ; )
        {
            if (gameStatus.Equals(GameStatus.Fight)) // only during fight phase
            {
                int guardiansCount = 0;
                playerController.deployedTribesCounter.TryGetValue(Tribe.Guardian, out guardiansCount); // get the friendly count of guardians

                int enemy_GuardiansCount = 0;
                playerController.enemyActiveTribesCounter.TryGetValue(Tribe.Guardian, out enemy_GuardiansCount);// get the enemy count of guardians

                foreach (NPC nPC in npcController.allyList) // iterate through the friendly ally list
                {
                    if (nPC.occupyingTile.GetComponent<TileBehaviour>().i != 8) // only consider deployed units and not reserve board units
                    {
                        if (guardiansCount >= 6) // we have guardian 6 bonus
                        {
                            nPC.GainConcentration(playerController.constant_Concentration_BaseTickRegeneration + (playerController.coefficient_Guardian_6_ConcentrationRegenBonus_RegenPerTickBonusMultiplier * 1f), HealSource.Concentration_Gain); //FRIENDLY Guardian Tribe 6 * Buffed concentration regen

                        }
                        else // we dont have guardian 6 bonus
                        {
                            nPC.GainConcentration(playerController.constant_Concentration_BaseTickRegeneration + 1f, HealSource.Concentration_Gain); // Regular Friendly Regen
                        }
                    }
                }

                foreach (NPC nPC in npcController.enemyList) // do the iteration but with enemy ally list
                {
                    if (enemy_GuardiansCount >= 6) // enemy has guardian 6 bonus
                    {
                        nPC.GainConcentration(playerController.constant_Concentration_BaseTickRegeneration + (playerController.coefficient_Guardian_6_ConcentrationRegenBonus_RegenPerTickBonusMultiplier * 1f), HealSource.Concentration_Gain); //ENEMY Guardian Tribe 6 * Buffed concentration regen
                    }
                    else // enemy does not have guardian 6 bonus
                    {
                        nPC.GainConcentration(playerController.constant_Concentration_BaseTickRegeneration + 1f, HealSource.Concentration_Gain);// Regular enemy Regen
                    }
                }

            }
            yield return new WaitForSeconds(playerController.constant_Concentration_RegenerationIntervalSeconds); // wait until next interval 
        }
    }

    IEnumerator ProgressHealthRegeneration()
    {
        for (; ; )
        {
            if (gameStatus.Equals(GameStatus.Fight)) // only do this during fight phase 
            {
                int warriorsCount = 0;
                playerController.deployedTribesCounter.TryGetValue(Tribe.Warrior, out warriorsCount); // check friendly warrior count

                int enemy_WarriorsCount = 0;
                playerController.enemyActiveTribesCounter.TryGetValue(Tribe.Warrior, out enemy_WarriorsCount); // check enemy friendly warrior count

                foreach (NPC nPC in npcController.allyList)
                {

                    if (warriorsCount >= 3) // FRIENDLY TRIBAL BONUS CHECK 3* WARRIORS FOR EXTRA REGEN
                    {
                        nPC.GainHP(playerController.constant_HP_BaseTickRegeneration + (playerController.coefficient_Warrior_3_HPRegenBonus_RegenPerTickBonusMultiplier * nPC.HPRegeneration), HealSource.HP_Regeneration); // FRIENDLY Warrior Tribe 3* BUFFED HP REGEN
                        float regened = playerController.constant_HP_BaseTickRegeneration + (playerController.coefficient_Warrior_3_HPRegenBonus_RegenPerTickBonusMultiplier * nPC.HPRegeneration);
                    }
                    else // we dont have warrior 3 bonus
                    {
                        nPC.GainHP(playerController.constant_HP_BaseTickRegeneration + nPC.HPRegeneration, HealSource.HP_Regeneration); // FRIENDLY regular hp regen
                        float regened = playerController.constant_HP_BaseTickRegeneration + nPC.HPRegeneration;
                    }
                }

                foreach (NPC nPC in npcController.enemyList)
                {
                    if (enemy_WarriorsCount >= 3) // ENEMY TRIBAL BONUS CHECK 3* WARRIORS FOR EXTRA REGEN
                    {
                        nPC.GainHP(playerController.constant_HP_BaseTickRegeneration + (playerController.coefficient_Warrior_3_HPRegenBonus_RegenPerTickBonusMultiplier * nPC.HPRegeneration), HealSource.HP_Regeneration);
                    }
                    else // enemy does not have warrior 3 bonus
                    {
                        nPC.GainHP(playerController.constant_HP_BaseTickRegeneration + nPC.HPRegeneration, HealSource.HP_Regeneration);  // ENEMY regular hp regen
                    }

                }

            }
            yield return new WaitForSeconds(playerController.constant_HP_RegenerationIntervalSeconds); // wait until next health regen interval
        }

    }

    private void Update()
    {
        if (gameStatus.Equals(GameStatus.Fight))
        {
            combatRoundTimer += Time.deltaTime;
            uiController.combatTimerPanelText.text = (constant_TimeAllowedForCombatBeforeForceStop - Mathf.Round(combatRoundTimer)).ToString();
            if (combatRoundTimer >= constant_TimeAllowedForCombatBeforeForceStop)
            {

                TransitionToShoppingPhase();
            }
        }
    }

    // Update is called once per frame
    void LateUpdate() // at the end of every frame check if game is over or if combat is over
    {
        if (gameStatus.Equals(GameStatus.GameOver)) // *GG* game over conditon *GG*
        {
            TransitionToGameOverPhase(); // transition to the end of game phase
        }
        else if (gameStatus.Equals(GameStatus.Fight) && (npcController.deployedAllyList.Count == 0 || npcController.enemyList.Count == 0)) // combat is over
        {
            TransitionToShoppingPhase(); // transition to shopping phase
        }

    }
    IEnumerator SmoothEndCombatRoundTransition() // smoothly end combat 
    {
        uiController.combatLogger.parseTime = combatRoundTimer;
        combatRoundTimer = 0;
        uiController.combatTimerPanel.gameObject.SetActive(false);
        uiController.EnableCombatReportPanel();
        List<CombatReport> combatReports = uiController.combatLogger.combatReports;

        foreach (CombatReport combatReport in combatReports)
        {
            if (combatReport.dirtyParseTime)
            {
                combatReport.parseTime = uiController.combatLogger.parseTime;
            }
        }


        GameObject combatReportBarPrefab = (GameObject)Resources.Load("Combat Report Bar");
        foreach (NPC npc in npcController.allyList)
        {
            if (!npc.isEnemy && !npc.isCreep && !npc.isBoss && npc.combatReport_DamageDoneThisRound != 0)
            {
                uiController.combatLogger.AddCombatReport(new CombatReport(npc.UNIT_TYPE.ToString(), npc.TIER, npc.combatReport_DamageDoneThisRound, uiController.combatLogger.parseTime, false, npc.combatReport_barUnitColor));
                npc.combatReport_DamageDoneThisRound = 0;
            }
        }
        uiController.combatLogger.ResetCombatReportBars();
        combatReports = uiController.combatLogger.combatReports;

        List<CombatReport> sortedCombatReports = combatReports.OrderByDescending(o => o.damageDealt).ToList();

        float totalDamageDone = 0;
        foreach (CombatReport combatReport in sortedCombatReports)
        {
            totalDamageDone += combatReport.damageDealt;
        }
        foreach (CombatReport combatReport in sortedCombatReports)
        {
            GameObject go = Instantiate(combatReportBarPrefab, uiController.combatReportEntries.transform);
            CombatReportBar combatReportBar = go.GetComponent<CombatReportBar>();
            combatReportBar.combatReport = combatReport;
            combatReportBar.totalDamage = totalDamageDone;
            combatReportBar.unitColor = combatReport.unitColor;
            if (sortedCombatReports.Count >= 0)
            {
                combatReportBar.topDamage = sortedCombatReports[0].damageDealt;

            }
            combatReportBar.SetUIElements();
            uiController.combatLogger.combatReportBars.Add(go);
        }

        uiController.combatLogger.ResetCombatLog();

        if (npcController.enemyList.Count == 0) // combat victory
        {
            if (npcController.allyList.Count != 0)
            {
                foreach (NPC npc in npcController.deployedAllyList)
                {

                    if (npc.cheering_SoundClip != null && UnityEngine.Random.Range(0, 2) == 1)
                    {
                        npc.npcAudioSource.PlayOneShot(npc.cheering_SoundClip);
                    }
                }

            }
        }

        yield return new WaitForSeconds(4); // wait 2 seconds
        float goldCount = playerController.playerGoldCount; // fetch the player's current gold
        playerController.enemyMMR += 5;
        if (npcController.enemyList.Count == 0) // combat VICTORY
        {
            steamAchievements.IncrementRoundsWon(1);

            sessionLogger.CalculateFIDEMMRChange(true, (float)playerController.playerMMR, (float)playerController.enemyMMR, playerController.FIDE_KFactor);

            goldCount *= 1.11f;
            goldCount += 1;

            if (goldCount < 2)
            {
                goldCount = 2;
            }
            goldCount = (float)Math.Round(goldCount, 0, MidpointRounding.AwayFromZero);
            float netGoldReward = goldCount - playerController.playerGoldCount;
            playerController.sessionLogger.goldRewarded += (long)netGoldReward;
            playerController.SetPlayerGoldCount((long)goldCount); // reward bonus gold
        }
        else // combat DEFEAT
        {

            sessionLogger.CalculateFIDEMMRChange(false, (float)playerController.playerMMR, (float)playerController.enemyMMR, playerController.FIDE_KFactor);

            goldCount *= 1.06f;
            goldCount += 1;

            if (goldCount < 2)
            {
                goldCount = 2;
            }
            float netGoldReward = goldCount - playerController.playerGoldCount;
            playerController.sessionLogger.goldRewarded += (long)netGoldReward;
            playerController.SetPlayerGoldCount((long)goldCount); // reward DEFEAT bonus gold

            bool isCreepRound = false;
            bool isBossRound = false;
            foreach (NPC npc in npcController.enemyList)
            {
                if (npc.isBoss) { isBossRound = true; }
                else if (npc.isCreep) { isCreepRound = true; }
            }
            if (isCreepRound)
            {
                playerController.SetCurrentHP(playerController.currentPlayerHealth - 5);
            }
            else if (isBossRound)
            {
                playerController.SetCurrentHP(playerController.currentPlayerHealth - 50);
            }
            else
            {
                playerController.SetCurrentHP(playerController.currentPlayerHealth - 10);
            }

        }



        uiController.hudCanvasAudioSource.PlayOneShot(uiController.shopRefreshAudioClip);

        int i;

        for (i = npcController.enemyList.Count - 1; i >= 0; i--) // clean up the board from any enemy units
        {
            if (npcController.enemyList[i] != null)
            {
                npcController.enemyList[i].RemoveFromBoard(true);
            }
        }

        for (i = npcController.deployedAllyList.Count - 1; i >= 0; i--) // clean up the board from any ally units
        {
            if (npcController.deployedAllyList[i] != null)
            {
                npcController.deployedAllyList[i].RemoveFromBoard(true);
            }
        }


        for (i = npcController.allyListBackup.Count - 1; i >= 0; i--) // put back all the pre-combat clones into the board
        {
            if (npcController.allyListBackup[i] != null)
            {
                npcController.allyListBackup[i].gameObject.transform.parent.position = npcController.allyListBackup[i].occupyingTile.transform.position;
                npcController.allyListBackup[i].enabled = true;
                chessBoard[npcController.allyListBackup[i].occupyingTile.GetComponent<TileBehaviour>().i, npcController.allyListBackup[i].occupyingTile.GetComponent<TileBehaviour>().j].GetComponent<TileBehaviour>().occupyingUnit = npcController.allyListBackup[i].gameObject;
                Helper.Increment<Tribe>(playerController.deployedTribesCounter, npcController.allyListBackup[i].PRIMARYTRIBE);
                Helper.Increment<Tribe>(playerController.deployedTribesCounter, npcController.allyListBackup[i].SECONDARYTRIBE);
                uiController.RefreshDeployedTribesCounter();
                playerController.SetCurrentlyDeployedUnits(playerController.currentlyDeployedUnits + 1); ;
                npcController.deployedAllyList.Add(npcController.allyListBackup[i]);
                npcController.allyList.Add(npcController.allyListBackup[i]);
                selectedNPC = npcController.deployedAllyList.ToArray()[0].gameObject;


            }
        }

        if (gameStatus != GameStatus.ReportDefeat || gameStatus != GameStatus.GameOver || gameStatus != GameStatus.ReportVictory)
        {
            ChangeCurrentRound(currentGameRound + 1);
        }

        playerController.ShuffleNewShopingOptions(true); // shuffle the player shop for free with new options to buy

        if (gameStatus != GameStatus.ReportDefeat || gameStatus != GameStatus.ReportVictory)
        {
            ChangeGameStatus(GameStatus.Shopping); // finally, change the game status to shopping phase
        }

        if (currentGameRound % 3 == 0) // only once every 3 rounds
        {
            playerController.SetMaxDeployedUnitsLimit(playerController.maxDeployedUnitsLimit + 1); // increment the max deployed unit limit
            playerController.costToUpgradeUnitCap = 10 * playerController.maxDeployedUnitsLimit;
            uiController.ChangeCostToUnitCapUpgradeDisplayText((10 * playerController.maxDeployedUnitsLimit).ToString());
        }
    }

    public void QuickForfeitAndRestart()
    {
        if ((int)sessionLogger.mmrChange < 0)
        {
            steamLeaderboard.SetLeaderBoardScore(playerController.playerMMR + (int)sessionLogger.mmrChange);
        }

        TransitionToGameOverPhase();
    }


    public void TransitionToReportDefeatPhase()
    {
        uiController.hudCanvasReportDefeatPanel.gameObject.SetActive(true);
        uiController.hudCanvasTribesPanel.gameObject.SetActive(false);
        uiController.hudCanvasTopBar.gameObject.SetActive(false);
        uiController.hudCanvasBottomBar.gameObject.SetActive(false);
        uiController.ShopPanelTooltipSubPanel.SetActive(false);
        uiController.hudCanvasShopPanel.gameObject.SetActive(false);
        sessionLogger.CalculateMaxDeployedTribe();
        uiController.reportDefeatPanel_MostTribeDeployedIconVisualizer.SetImage(sessionLogger.mostDeployedTribe, true);
        steamLeaderboard.SetLeaderBoardScore(playerController.playerMMR + (int)sessionLogger.mmrChange);
        steamAchievements.IncrementDeployedTribesStats(sessionLogger.TribesDeployedToFightTracker);
        steamAchievements.IncrementGoldEarnedStats(sessionLogger.goldRewarded);
        ChangeGameStatus(GameStatus.ReportDefeat);
    }

    public void TransitionToReportVictoryPhase()
    {
        uiController.hudCanvasreportVictoryPanel.gameObject.SetActive(true);
        uiController.hudCanvasTribesPanel.gameObject.SetActive(false);
        uiController.hudCanvasTopBar.gameObject.SetActive(false);
        uiController.hudCanvasBottomBar.gameObject.SetActive(false);
        uiController.ShopPanelTooltipSubPanel.SetActive(false);
        uiController.hudCanvasShopPanel.gameObject.SetActive(false);
        sessionLogger.CalculateMaxDeployedTribe();
        uiController.reportVictoryPanel_MostTribeDeployedIconVisualizer.SetImage(sessionLogger.mostDeployedTribe, true);
        steamLeaderboard.SetLeaderBoardScore(playerController.playerMMR + (int)sessionLogger.mmrChange);
        steamAchievements.IncrementGamesWon();
        steamAchievements.IncrementDeployedTribesStats(sessionLogger.TribesDeployedToFightTracker);
        steamAchievements.IncrementGoldEarnedStats(sessionLogger.goldRewarded);
        ChangeGameStatus(GameStatus.ReportVictory);
    }

    public void TransitionToGameOverPhase()
    {
        SceneManager.LoadScene("highnet auto chess");
    }



    public void TransitionToShoppingPhase()
    {
        ChangeGameStatus(GameStatus.Wait);
        StartCoroutine(SmoothEndCombatRoundTransition());

    }

}
