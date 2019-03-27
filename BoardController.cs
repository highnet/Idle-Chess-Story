using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class BoardController : MonoBehaviour
{
    NpcController npcController;
    UiController uiController;
    PlayerController playerController;
    //
    public GameObject[,] chessBoard;
    public GameObject[] reserveBoard;
    public List<GameObject> unitsList;
    public GameObject selectedObject;
    public GameObject tilePrefab;
    //

    public bool testMode = false;

    public string gameStatus;
    public int currentGameRound = 1;


    private void Awake()
    {
        npcController = GetComponent<NpcController>();
        uiController = GetComponent<UiController>();
        playerController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {

        ChangeGameStatus("initializing");
        CreateBoardTiles();
        ChangeGameStatus("awaitingWizardConfirmation", "confirm settings"); // TODO: add pre game settings wizard / start menu
        uiController.LoadFromSaveFile();


    }

    private void FixedUpdate()
    {

    }
    void CreateBoardTiles()
    {
        chessBoard = new GameObject[9, 8];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 8; j++)
            {

                GameObject spawn = GameObject.Instantiate(tilePrefab, new Vector3(i == 8 ? i - 3.000f : i - 3.435f, 0, 3.435f - j), Quaternion.Euler(0, -90, 0));
                spawn.gameObject.name = "Chess Tile[" + i + "]" + "[" + j + "]";
                spawn.transform.parent = this.gameObject.transform;
                spawn.GetComponent<TileBehaviour>().i = i;
                spawn.GetComponent<TileBehaviour>().j = j;


                chessBoard[i, j] = spawn;
            }
        }
    }

    public void SpawnEnemyUnitsRound_Balanced()
    {
        if (!testMode)
        {
            Tribe randomTribe;
            randomTribe = (Tribe)Enum.GetValues(typeof(Tribe)).GetValue((int)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Tribe)).Length));
            bool doneTrying = false;
            while (doneTrying == false)
            {
                randomTribe = (Tribe)Enum.GetValues(typeof(Tribe)).GetValue((int)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Tribe)).Length));
                if (randomTribe == Tribe.Structure)
                {
                    doneTrying = false;
                }
                else
                {
                    doneTrying = true;
                }
            }

            List<Unit> spawnList;
            playerController.TRIBAL_UNIT_DATA.TryGetValue(randomTribe, out spawnList);


            int tier1_amount = 0;
            int tier2_amount = 0;
            int tier3_amount = 0;


            foreach (NPC npc in npcController.deployedAllyList)
            {
                if (npc.TIER == 1)
                {
                    tier1_amount++;
                }
                if (npc.TIER == 2)
                {
                    tier2_amount++;
                }
                if (npc.TIER == 3)
                {
                    tier3_amount++;
                }
            }

            Debug.Log("Counting up your units t1:t2:t3 ->" + tier1_amount + ":" + tier2_amount + ":" + tier3_amount);

            playerController.ReInitializeEnemyActiveTribesCounter();
            for (int i = 0; i < tier1_amount; i++)
            {
                int randomI = UnityEngine.Random.Range(0, 4);
                int randomJ = UnityEngine.Random.Range(0, 8);
                Unit randomUnit = spawnList.ToArray()[UnityEngine.Random.Range(0, spawnList.ToArray().Length)];

                GameObject spawnedEnemy = TrySpawnUnit(randomI, randomJ, randomUnit, true, false,1);
                if (spawnedEnemy != null)
                {
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE);
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
                }
             

            }

            for (int i = 0; i < tier2_amount; i++)
            {
                int randomI = UnityEngine.Random.Range(0, 4);
                int randomJ = UnityEngine.Random.Range(0, 8);
                Unit randomUnit = spawnList.ToArray()[UnityEngine.Random.Range(0, spawnList.ToArray().Length)];

                GameObject spawnedEnemy = TrySpawnUnit(randomI, randomJ, randomUnit, true,false,2);
                if (spawnedEnemy != null)
                {
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE);
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
                 
                }

               
            }

            for (int i = 0; i < tier3_amount; i++)
            {
                int randomI = UnityEngine.Random.Range(0, 4);
                int randomJ = UnityEngine.Random.Range(0, 8);
                Unit randomUnit = spawnList.ToArray()[UnityEngine.Random.Range(0, spawnList.ToArray().Length)];

                GameObject spawnedEnemy = TrySpawnUnit(randomI, randomJ, randomUnit, true,false,3);
                if (spawnedEnemy != null)
                {
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE);
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
    
                }


            }

        }
    
        else
        {
            GameObject spawnedEnemy = TrySpawnDummy(2, 4, true);
            if (spawnedEnemy != null)
            {
                Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE);
                Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
            }

        }

    }

    public void SpawnEnemyUnitsRound_Scaling(int roundCounter) // Spawns enemy units at the start of each round
    {

        if (!testMode)
        {
            Tribe randomTribe;
            randomTribe = (Tribe)Enum.GetValues(typeof(Tribe)).GetValue((int)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Tribe)).Length));


            bool doneTrying = false;
            while (doneTrying == false)
            {
                randomTribe = (Tribe)Enum.GetValues(typeof(Tribe)).GetValue((int)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Tribe)).Length));
                if (randomTribe == Tribe.Structure)
                {
                    doneTrying = false;
                }
                else
                {
                    doneTrying = true;
                }
            }

            List<Unit> spawnList;
            playerController.TRIBAL_UNIT_DATA.TryGetValue(randomTribe, out spawnList);

            int amountOfUnitsToSpawn = 1 + roundCounter;
       //    Debug.Log("Round: " + roundCounter + ". Spawning " + amountOfUnitsToSpawn + " Units, of type: " + randomTribe);
            playerController.ReInitializeEnemyActiveTribesCounter();
            for (int i = 0; i < amountOfUnitsToSpawn; i++)
            {
                int randomI = UnityEngine.Random.Range(0, 4);
                int randomJ = UnityEngine.Random.Range(0, 8);
                Unit randomUnit = spawnList.ToArray()[UnityEngine.Random.Range(0, spawnList.ToArray().Length)];

                GameObject spawnedEnemy = TrySpawnUnit(randomI, randomJ, randomUnit, true,true,1);
                if (spawnedEnemy != null)
                {
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE);
                    Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
                }

            }

        } else
        {
            GameObject spawnedEnemy = TrySpawnDummy(2,4,true);
            if (spawnedEnemy != null)
            {
                Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().PRIMARYTRIBE);
                Helper.Increment<Tribe>(playerController.enemyActiveTribesCounter, spawnedEnemy.GetComponentInChildren<NPC>().SECONDARYTRIBE);
            }

        }



    }


    public GameObject TrySpawnUnit(int i, int j, Unit unitToSpawn, bool isEnemySpawn,bool doRandomLevelup,int tierOverride)
    {

        Debug.Log("Attempting to spawn a unit [i=" + i + ",j=" + j + ",name:" + unitToSpawn + ",isenemy:" + isEnemySpawn + "]");
        string prefabName = Enum.GetName(typeof(Unit), unitToSpawn);
        GameObject tile = chessBoard[i, j];

        if (tile.GetComponent<TileBehaviour>().occupyingUnit != null)
        {
            Debug.Log("attempted to spawn on a occupied tile");
            if (isEnemySpawn &&  tile.GetComponent<TileBehaviour>().occupyingUnit.GetComponentInChildren<NPC>().TIER == 1)
            {
                tile.GetComponent<TileBehaviour>().occupyingUnit.GetComponentInChildren<NPC>().ApplyTier2Upgrades();
            } else if (isEnemySpawn && tile.GetComponent<TileBehaviour>().occupyingUnit.GetComponentInChildren<NPC>().TIER == 2)
            {
                tile.GetComponent<TileBehaviour>().occupyingUnit.GetComponentInChildren<NPC>().ApplyTier3Upgrades();
            }
         
            return null;
        }

        Quaternion spawnRotation = new Quaternion();

        if (isEnemySpawn)
        {
            spawnRotation = Quaternion.Euler(0, 90, 0);

        }
        else
        {
            spawnRotation = Quaternion.Euler(0, -90, 0);
        }

        GameObject go = (GameObject)GameObject.Instantiate(Resources.Load(prefabName), tile.transform.position, spawnRotation);
        go.GetComponentInChildren<NPC>().isEnemy = isEnemySpawn;
        tile.GetComponent<TileBehaviour>().occupyingUnit = go;
        go.GetComponentInChildren<NPC>().occupyingTile = tile;
        go.GetComponentInChildren<NPC>().PrepareNPC3DHud();
        if (isEnemySpawn && doRandomLevelup)
        {
            go.GetComponentInChildren<NPC>().RandomLevelUpEnemy();
        }

        if (tierOverride == 2)
        {
            go.GetComponentInChildren<NPC>().ApplyTier2Upgrades();
        }
        else if (tierOverride == 3)
            {
                go.GetComponentInChildren<NPC>().ApplyTier2Upgrades();
                go.GetComponentInChildren<NPC>().ApplyTier3Upgrades();
            }

        npcController.UpdateNpcList();

        return go;
    }

    public GameObject TrySpawnDummy(int i ,int j, bool isEnemy)
    {
        GameObject tile = chessBoard[i, j];

        if (tile.GetComponent<TileBehaviour>().occupyingUnit != null)
        {
            return null;
        }

        Quaternion spawnRotation = new Quaternion();

        if (isEnemy)
        {
            spawnRotation = Quaternion.Euler(0, 90, 0);

        }
        else
        {
            spawnRotation = Quaternion.Euler(0, -90, 0);
        }

        GameObject go = (GameObject)GameObject.Instantiate(Resources.Load("Dummy"), tile.transform.position, spawnRotation);
        go.GetComponentInChildren<NPC>().isEnemy = isEnemy;
        tile.GetComponent<TileBehaviour>().occupyingUnit = go;
        go.GetComponentInChildren<NPC>().occupyingTile = tile;
        go.GetComponentInChildren<NPC>().PrepareNPC3DHud();
        npcController.UpdateNpcList();

        return go;
    }



    public void ChangeGameStatus(string developerStatus, string userDisplayText)
    {
        gameStatus = developerStatus;
        uiController.ChangeGameStatusDisplayText(userDisplayText);
    }

    public void ChangeGameStatus(string status)
    {
        gameStatus = status;
        uiController.ChangeGameStatusDisplayText(status);
    }

    public void ChangeCurrentRound(int round)
    {
        currentGameRound = round;
        uiController.ChangeCurrentRoundDisplayText(currentGameRound);
    }

    IEnumerator ProgressConcentrationRegeneration()
    {
        for (; ; )
        { // TODO: Implement NPC concentration Regeneration Field. 1f 1f 1f 1f
            if (gameStatus.Equals("Fight"))
            {
                int guardiansCount = 0;
                playerController.deployedTribesCounter.TryGetValue(Tribe.Guardian, out guardiansCount);

                int enemy_GuardiansCount = 0;
                playerController.enemyActiveTribesCounter.TryGetValue(Tribe.Guardian, out enemy_GuardiansCount);

                foreach (NPC nPC in npcController.allyList)
                {
                    if (nPC.occupyingTile.GetComponent<TileBehaviour>().i != 8)
                    {
                        if (guardiansCount >= 6)
                        {
                            nPC.GainConcentration(playerController.constant_Concentration_BaseTickRegeneration + (playerController.coefficient_Guardian_6_ConcentrationRegenBonus_RegenPerTickBonusMultiplier * 1f), HealSource.Concentration_Gain); //FRIENDLY Guardian Tribe 6 * Buffed concentration regen

                        }
                        else
                        {
                            nPC.GainConcentration(playerController.constant_Concentration_BaseTickRegeneration + 1f, HealSource.Concentration_Gain); // Regular Friendly Regen
                        }
                    }

       
                }

                foreach (NPC nPC in npcController.enemyList)
                {
                    if (enemy_GuardiansCount >= 6)
                    {
                        nPC.GainConcentration(playerController.constant_Concentration_BaseTickRegeneration + (playerController.coefficient_Guardian_6_ConcentrationRegenBonus_RegenPerTickBonusMultiplier * 1f), HealSource.Concentration_Gain); //ENEMY Guardian Tribe 6 * Buffed concentration regen
                    }
                    else
                    {
                        nPC.GainConcentration(playerController.constant_Concentration_BaseTickRegeneration + 1f, HealSource.Concentration_Gain);// Regular enemy Regen
                    }
                }

            }
            yield return new WaitForSeconds(playerController.constant_Concentration_RegenerationIntervalSeconds);
        }
    }

    IEnumerator ProgressHealthRegeneration()
    {
        for (; ; )
        {
            if (gameStatus.Equals("Fight"))
            {
                int warriorsCount = 0;
                playerController.deployedTribesCounter.TryGetValue(Tribe.Warrior, out warriorsCount);

                int enemy_WarriorsCount = 0;
                playerController.enemyActiveTribesCounter.TryGetValue(Tribe.Warrior, out enemy_WarriorsCount);

                foreach (NPC nPC in npcController.allyList)
                {

                    if (warriorsCount >= 3) // FRIENDLY TRIBAL BONUS CHECK 3* WARRIORS FOR EXTRA REGEN
                    {
                        nPC.GainHP(playerController.constant_HP_BaseTickRegeneration + (playerController.coefficient_Warrior_3_HPRegenBonus_RegenPerTickBonusMultiplier * nPC.HPRegeneration), HealSource.HP_Regeneration); // FRIENDLY Warrior Tribe 3* BUFFED HP REGEN

                    }
                    else
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
                    else
                    {
                        nPC.GainHP(playerController.constant_HP_BaseTickRegeneration + nPC.HPRegeneration, HealSource.HP_Regeneration);  // ENEMY regular hp regen
                    }

                }

            }
            yield return new WaitForSeconds(playerController.constant_HP_RegenerationIntervalSeconds);
        }

    }




    // Update is called once per frame
    void LateUpdate()
    {
        if (gameStatus.Equals("game over"))
        {
            TransitionToGameOverPhase();
        }

        if (gameStatus.Equals("Fight") && (npcController.deployedAllyList.Count == 0 || npcController.enemyList.Count == 0))
        {
            TransitionToShoppingPhase(); // transition to shopping phase
        }

    }
    IEnumerator SmoothEndCombatRoundTransition()
    {

        yield return new WaitForSeconds(2);

        float goldReward = playerController.playerGoldCount * 1.1f;
        if (npcController.enemyList.Count == 0)
        {
            playerController.SetPlayerGoldCount((long)goldReward + 5 * currentGameRound);
        }
        else
        {
            playerController.SetPlayerGoldCount((long)goldReward);
        }

        int i;

        for (i = npcController.enemyList.Count - 1; i >= 0; i--)
        {
            if (npcController.enemyList[i] != null)
            {
                npcController.enemyList[i].RemoveFromBoard(true);
            }
        }

        for (i = npcController.deployedAllyList.Count - 1; i >= 0; i--)
        {
            if (npcController.deployedAllyList[i] != null)
            {
                npcController.deployedAllyList[i].RemoveFromBoard(true);
            }
        }


        for (i = npcController.allyListBackup.Count - 1; i >= 0; i--)
        {
            if (npcController.allyListBackup[i] != null)
            {
                npcController.allyListBackup[i].gameObject.transform.parent.position = npcController.allyListBackup[i].occupyingTile.transform.position;
                npcController.allyListBackup[i].enabled = true;
                chessBoard[npcController.allyListBackup[i].occupyingTile.GetComponent<TileBehaviour>().i, npcController.allyListBackup[i].occupyingTile.GetComponent<TileBehaviour>().j].GetComponent<TileBehaviour>().occupyingUnit = npcController.allyListBackup[i].gameObject;
                Helper.Increment<Tribe>(playerController.deployedTribesCounter, npcController.allyListBackup[i].PRIMARYTRIBE);
                Helper.Increment<Tribe>(playerController.deployedTribesCounter, npcController.allyListBackup[i].SECONDARYTRIBE);
                uiController.RefreshDeployedTribesCounter();
                playerController.SetCurrentlyDeployedUnits(playerController.currentlyDeployedUnits + 1);
                npcController.deployedAllyList.Add(npcController.allyListBackup[i]);
                selectedObject = npcController.deployedAllyList.ToArray()[0].gameObject;

            }
        }

        playerController.SetMaxDeployedUnitsLimit(playerController.maxDeployedUnitsLimit + 1);
        ChangeCurrentRound(currentGameRound + 1);
        playerController.ShuffleNewShopingOptions(true);
        ChangeGameStatus("shopping");
    }

    public void TransitionToGameOverPhase()
    {
        uiController.LoadFromSaveFile();
        SceneManager.LoadScene("highnet auto chess");
    }



    public void TransitionToShoppingPhase()
    {
        ChangeGameStatus("wait", "Combat Over");
        StartCoroutine(SmoothEndCombatRoundTransition());




    }

}
