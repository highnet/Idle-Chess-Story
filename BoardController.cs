using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public enum GameStatus {Initializing,Fight,AwaitingWizardConfirmation,Wait,Shopping,ReportDefeat,GameOver }

public class BoardController : MonoBehaviour
{
    NpcController npcController;
    UiController uiController;
    PlayerController playerController;
    SessionLogger sessionLogger;
    //
    public GameObject[,] chessBoard;
    public GameObject[] reserveBoard;
    public List<GameObject> unitsList;
    public GameObject selectedObject;
    public GameObject tilePrefab;
    //
    public MainCamera mainCameraController;

    public bool testDummyMode = false;

    public GameStatus gameStatus;
    public int currentGameRound = 1;

    public GameObject friendlySideIndicatorPlane;
    public GameObject rainSystem;
    public AmbienceSound ambienceSound;

    public Leaderboard steamLeaderboard;

    private void Awake()
    {
        npcController = GetComponent<NpcController>(); //fetch world controllers
        uiController = GetComponent<UiController>();
        playerController = GetComponent<PlayerController>();
        sessionLogger = GetComponent<SessionLogger>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeGameStatus(GameStatus.Initializing); // set initalizing status
        CreateBoardTiles(); // create the tile 9x8 array board
        ChangeGameStatus(GameStatus.AwaitingWizardConfirmation, "confirm settings"); // set to settings wizard status

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
       
            if (currentGameRound  == 1 || currentGameRound == 2  || currentGameRound == 5 ||  currentGameRound == 7 || currentGameRound == 11 || currentGameRound == 15 || currentGameRound == 17)
            {
                playerController.ReInitializeEnemyActiveTribesCounter(); // reset the enemy tribe counter
                for(int i = 0; i < 2 + (currentGameRound/2); i++)
                {
                    int randomI = UnityEngine.Random.Range(0, 4); // find a random i coordinate
                    int randomJ = UnityEngine.Random.Range(0, 8); // find a random j coordinate
                    TrySpawnUnit(randomI, randomJ, Unit.RobotCreep, true, 0,true); // spawn creep
                }

            }

            else  if (currentGameRound == 18) // boss round 
            {
                playerController.ReInitializeEnemyActiveTribesCounter(); // reset the enemy tribe counter
                GameObject spawnedEnemy = TrySpawnUnit(2, 4, Unit.Engineer, true, 0,true); // spawn engineer
                if (spawnedEnemy != null) // this can be used to test enemy tribe bonuses
                {
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE); //
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
                }
            }
            else if (currentGameRound == 12) // boss round 
            {
                playerController.ReInitializeEnemyActiveTribesCounter(); // reset the enemy tribe counter
                GameObject spawnedEnemy = TrySpawnUnit(2, 4, Unit.AlienSoldier, true, 0,true); // spawn alien soldier
                if (spawnedEnemy != null) // this can be used to test enemy tribe bonuses
                {
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE); //
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
                }
            }
            else if (currentGameRound == 6) // boss round 
            {
                playerController.ReInitializeEnemyActiveTribesCounter(); // reset the enemy tribe counter
                GameObject spawnedEnemy = TrySpawnUnit(2,4,Unit.Eyebat,true,0,true); // spawn eyebat
                if (spawnedEnemy != null) // this can be used to test enemy tribe bonuses
                {
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE); //
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
                }
            }
            else // normal round
            {
             float spawningBudget = playerController.sessionLogger.goldRewarded;

                int currentDificulty = uiController.wizard_difficultyPicker.value;
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

                if (currentDificulty == 0)
                {
                    enemySpawnCount -= 1;
                }
                else if (currentDificulty == 2)
                {
                    enemySpawnCount += 1;
                }


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

                bool skippedOne = false;
                bool skippedOneTierThree = false;

                foreach (NPC npc in npcController.enemyList)
                {
                    if (!skippedOne)
                    {
                        skippedOne = true;
                        continue;
                    }
                    playerController.NPC_COST_DATA.TryGetValue(npc.UNIT_TYPE, out int unitCost);
                    float t2UpgradeCost = 3 * unitCost;
                    float t3UpgradeCost = 3 * t2UpgradeCost;
                    if (spawningBudget >= t3UpgradeCost)
                    {
                        npc.ApplyTier2Upgrades();
                        if (!skippedOneTierThree)
                        {
                            npc.ApplyTier2Upgrades();
                            spawningBudget -= t2UpgradeCost;
                            skippedOneTierThree = true;
                            continue;
                        }
                        npc.ApplyTier3Upgrades();
                        spawningBudget -= t3UpgradeCost;
                    } else if (spawningBudget >= t2UpgradeCost)
                    {
                        npc.ApplyTier2Upgrades();
                        spawningBudget -= t2UpgradeCost;
                    }

                }

                }
            }
        
    
        else // if in test mode
        {
            playerController.ReInitializeEnemyActiveTribesCounter(); // reset the enemy tribe counter
            GameObject spawnedEnemy = TrySpawnDummy(2, 4, true); // spawn dummy
            if (spawnedEnemy != null) // this can be used to test enemy tribe bonuses
            {
                Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE); //
                Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
            }

        }

    }

    public GameObject TrySpawnUnit(int i, int j, Unit unitToSpawn, bool isEnemySpawn,int tierOverride,bool isCreep) // try to spawn a unit and return it
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

    public GameObject TrySpawnDummy(int i ,int j, bool isEnemy) // try spawn a dummy
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



    public void ChangeGameStatus(GameStatus developerStatus, string userDisplayText) // change game status and display string status (overload)
    {
        gameStatus = developerStatus;
        uiController.ChangeGameStatusDisplayText(userDisplayText);
    }

    public void ChangeGameStatus(GameStatus status) // change game status and display string status
    {
        gameStatus = status;
        uiController.ChangeGameStatusDisplayText(status.ToString());
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

                    }
                    else // we dont have warrior 3 bonus
                    {
                        nPC.GainHP(playerController.constant_HP_BaseTickRegeneration + nPC.HPRegeneration, HealSource.HP_Regeneration); // FRIENDLY regular hp regen
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
   
        yield return new WaitForSeconds(4); // wait 2 seconds
        float goldReward = playerController.playerGoldCount; // fetch the player's current gold
        if (npcController.enemyList.Count == 0) // combat VICTORY
        {
            sessionLogger.calculateFIDEMMRChange(true, (float)playerController.playerMMR, (float)playerController.enemyMMR,playerController.FIDE_KFactor++);

            if (npcController.allyList.Count != 0)
            {
                foreach (NPC npc in npcController.allyList)
                {
                    if (npc.cheering_SoundClip != null && UnityEngine.Random.Range(0,2) == 1)
                   {
                        npc.npcAudioSource.PlayOneShot(npc.cheering_SoundClip);
                   }
                }
            }

                goldReward *= 1.10f;

            if (goldReward < 2)
            {
                goldReward = 2;
            }
            goldReward = (float) Math.Round(goldReward, 0, MidpointRounding.AwayFromZero);
            float netGoldReward = goldReward - playerController.playerGoldCount;
            playerController.sessionLogger.goldRewarded += (long) netGoldReward;
            playerController.SetPlayerGoldCount((long)goldReward); // reward bonus gold
        }
        else // combat DEFEAT
        {
            sessionLogger.calculateFIDEMMRChange(false,(float) playerController.playerMMR,(float) playerController.enemyMMR,playerController.FIDE_KFactor++);

                goldReward *= 1.05f;

            if (goldReward < 2)
            {
                goldReward = 2;
            }
            float netGoldReward = goldReward - playerController.playerGoldCount;
            playerController.sessionLogger.goldRewarded += (long)netGoldReward;
            playerController.SetPlayerGoldCount((long)goldReward); // reward DEFEAT bonus gold

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
            } else if (isBossRound)
            {
                playerController.SetCurrentHP(playerController.currentPlayerHealth - 50);
            } else
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
                playerController.SetCurrentlyDeployedUnits(playerController.currentlyDeployedUnits + 1);;
                npcController.deployedAllyList.Add(npcController.allyListBackup[i]);
                npcController.allyList.Add(npcController.allyListBackup[i]);
                selectedObject = npcController.deployedAllyList.ToArray()[0].gameObject;

            }
        }

        if (gameStatus != GameStatus.ReportDefeat || gameStatus != GameStatus.GameOver)
        {
            ChangeCurrentRound(currentGameRound + 1);
        }
    
        if (currentGameRound % 3 == 0) // only once every 3 rounds
        {
            playerController.SetMaxDeployedUnitsLimit(playerController.maxDeployedUnitsLimit + 1); // increment the max deployed unit limit
        }
        playerController.ShuffleNewShopingOptions(true); // shuffle the player shop for free with new options to buy
     
        if (gameStatus != GameStatus.ReportDefeat)
        {
            ChangeGameStatus(GameStatus.Shopping); // finally, change the game status to shopping phase
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
        uiController.reportDefeatPanel_TotalUnitsDeployedText.text = "You deployed: " + sessionLogger.unitsDeployedToFight.ToString() + " unit(s)";
        uiController.reportDefeatPanel_MostTribesDeployedText.text = "Most deployed tribe: " + sessionLogger.mostDeployedTribeAmount + " " + sessionLogger.mostDeployedTribe.ToString();
        uiController.reportDefeatPanel_MostTribeDeployedIconVisualizer.SetImage(sessionLogger.mostDeployedTribe,true);
        steamLeaderboard.SetLeaderBoardScore(playerController.playerMMR + (int) sessionLogger.mmrChange);
        ChangeGameStatus(GameStatus.ReportDefeat);
    }
    public void TransitionToGameOverPhase()
    {
        SceneManager.LoadScene("highnet auto chess");
    }



    public void TransitionToShoppingPhase()
    {
        ChangeGameStatus(GameStatus.Wait, "Combat Over");
        StartCoroutine(SmoothEndCombatRoundTransition());

    }

}
