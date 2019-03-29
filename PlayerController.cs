using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//  Debug.Log(string.Join(", ", (playerController.enemyActiveTribesCounter.Select(pair => $"{pair.Key} => {pair.Value}"))));
//  Debug.Log(string.Join(", ", (playerController.deployedTribesCounter.Select(pair => $"{pair.Key} => {pair.Value}"))));

public enum Unit {Grenadier, RogueSpy,Chomper,RatWarrior, Tiger, Orbling, Skeleton, AxeGuard, DeadWizard, Cactus, BabyDragon, BigLizard, RockGolem, MageTower, ShamanTotem, Spitter, GoblinZerk, Zombie,Tree,KnightTower,CursedTomb,WarriorTent,AngelStatue,DemonLord}
public enum Tribe {Warrior, Beast, Elemental, Undead, Wizard, Assassin, Guardian, Structure }
public enum DamageSource {PhysicalDamage_AutoAttack, MagicalDamage_AutoAttack, PureDamage, Physical_Ability, Magical_Ability,NOTHING}
public enum HealSource { HP_Regeneration, AOEHeal, Heal, Concentration_Gain,NOTHING}
public enum Ability {Fireball,AP_UP_Self,MaxHP_Up_Self,Retaliation_UP_Self,Armor_UP_Self,NOTHING,HeroicStrike,AP_DOWN_OTHER,ARMOR_DOWN_OTHER,FrostBall,Stab,Stun,HealFriend}


public class PlayerController : MonoBehaviour
{
     UiController uiController;
     BoardController boardController;
     NpcController npcController;
     PlayerProfiler playerProfiler;
  
    //
    public string playerName;
    public int playerMMR;

    public long playerGoldCount;
    public int playerGlobalUnitCap;
    public int currentPlayerUnits;
    public int maxDeployedUnitsLimit;
    public int currentlyDeployedUnits;
    public List<NPC> playerOwnedNpcs;
    public int timesRefreshed;
    //
    public int currentPlayerHealth;
    public int maxPlayerHealth;
    int costToShuffleShop;
    public Dictionary<Unit, int> NPC_COST_DATA;
    public Dictionary<Tribe, List<Unit>> TRIBAL_UNIT_DATA;
    public Dictionary<Tribe, int> deployedTribesCounter;
    public Dictionary<Tribe, int> enemyActiveTribesCounter;
 
    public Unit[] shoppingOptions;
    //
   

    // Game balance constants and coefficients
    public int constant_Crit_RngRollBaseLowerBound;
    public int constant_Crit_RngRollBaseUpperBound;
    public int constant_Crit_RngRollBaseMinimumRequiredLuck;
    public float coefficient_Crit_OnCritBonusDamageMultiplier;
    public int constant_Miss_RngRollLowerBound;
    public int constant_Miss_RngRollUpperBound;
    public float coefficient_Warrior_6_OnHitBonusPhysicalDamageMultiplier;
    public int constant_Beast_3_CritBonus_RngRollBonusLowerBound;
    public int constant_Assassin_6_MissBonus_RngRollBonusUpperBound;
    public float coefficient_Elemental_6_OnHitBonusDamageMultiplier;
    public float coefficient_Guardian_6_DampenBonus_OnIncomingPhysicalDamageMultiplier;
    public int constant_Guardian_6_DampenBonus_RngRollMinimumRequiredRoll;
    public float coefficient_Undead_3_ArmorPierceBonus_OnHitBonusPierceMultiplier;
    public float coefficient_Undead_6_SpellPowerBonus_OnHitBonusSpellPower;
    public float coefficient_Guardian_6_ConcentrationRegenBonus_RegenPerTickBonusMultiplier;
    public float coefficient_Warrior_3_HPRegenBonus_RegenPerTickBonusMultiplier;
    public float coefficient_Structure_6_ArmorBonus_OnIncomingDamageArmorMultiplier;
    public float coefficient_Structure_3_RetaliationBonus_OnIncomingDamageRetaliationMultiplier;
    public int coefficient_DamageFormula_OnDamage_RetaliationEfficiencyPenalty;
    public float coefficient_Beast_6_LifeStealBonus_OnDamage_LifeStealEfficiencyMultiplier;
    public float coefficient_Wizard_3_OnHitBonusMagicalDamageMultiplier;
    public float coefficient_Wizard_6_DampenBonus_OnIncomingMagicalDamageMultiplier;
    public int constant_Wizard_6_DampenBonus_RngRollMinimumRequiredRoll;
    public float constant_Concentration_BaseTickRegeneration;
    public float constant_Concentration_RegenerationIntervalSeconds;
    public float constant_HP_CONCENTRATIONPerSecond;
    public float constant_HP_BaseTickRegeneration;
    public float constant_HP_RegenerationIntervalSeconds;
    public float constant_HP_RegenPerSecond;
    public float coefficient_Assassin_3_OnKillBonusGoldMultiplier;

    public float coefficient_Guardian_3_OnHit_ConcentrationGainBonusMultiplier;
    public float constant_OnHit_BaseConcentrationGain;

    private void Awake()
    {
        costToShuffleShop = 1;
        uiController = GetComponent<UiController>();
        boardController = GetComponent<BoardController>();
        npcController = GetComponent<NpcController>();
        playerProfiler = GetComponent<PlayerProfiler>();
        currentPlayerHealth = maxPlayerHealth;
    }

    public void ShuffleNewShopingOptions(bool isFreeShuffle) // shuffle new shopping options
    {
   

        if (isFreeShuffle || (costToShuffleShop <= playerGoldCount))
        {
            for (int i = 0; i < 6; i++)
            {
                shoppingOptions[i] = (Unit)Enum.GetValues(typeof(Unit)).GetValue((int)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Unit)).Length));
            }

            if (!isFreeShuffle)
            {
                SetPlayerGoldCount(playerGoldCount - costToShuffleShop);
                costToShuffleShop += timesRefreshed++;
            }

            uiController.shopToggleButton.gameObject.SetActive(true);
            uiController.UpdateAllShopButtons();
            uiController.ChangeCostToShuffleShopDisplayText(costToShuffleShop.ToString());
            uiController.hudCanvasAudioSource.PlayOneShot(uiController.shopRefreshAudioClip);
        }

    }

    public void InitializeShoppingOptions() // initalize shopping options
    {
        shoppingOptions = new Unit[6];
        ShuffleNewShopingOptions(true);
    }
    // Start is called before the first frame update
    void Start()
    {

        timesRefreshed = 0;


    NPC_COST_DATA = new Dictionary<Unit, int> // public enum Unit {Grenadier, RogueSpy,Chomper,RatWarrior, Tiger, Orbling, Skeleton, AxeGuard, DeadWizard, Cactus, BabyDragon, BigLizard, RockGolem, MageTower, ShamanTotem, Spitter, GoblinZerk, Zombie,Tree,KnightTower,CursedTomb,WarriorTent,AngelStatue,DemonLord}


    {
             { Unit.AngelStatue, 10 },
             { Unit.AxeGuard , 10 },
             { Unit.BabyDragon  , 10 },
             { Unit.BigLizard  , 10 },
             { Unit.Cactus , 10 },
             { Unit.Chomper , 10 },
             { Unit.CursedTomb , 10 },
             { Unit.DeadWizard , 10 },
             { Unit.DemonLord , 10 },
             { Unit.GoblinZerk , 10 },
             { Unit.Grenadier , 10 },
             { Unit.KnightTower , 10 },
             { Unit.MageTower , 10 },
             { Unit.Orbling , 10 },
             { Unit.RatWarrior , 10 },
             { Unit.RockGolem , 10 },
             { Unit.RogueSpy , 10 },
             { Unit.ShamanTotem , 10 },
             { Unit.Skeleton , 10 },
             { Unit.Spitter , 10 },
             { Unit.Tiger , 10 },
             { Unit.Tree , 10 },
             { Unit.WarriorTent , 10 },
             { Unit.Zombie , 10 },



    };

        List<Unit> assassinList = new List<Unit>();
        assassinList.Add(Unit.Tiger);
        assassinList.Add(Unit.Cactus);
        assassinList.Add(Unit.Grenadier);
        assassinList.Add(Unit.RogueSpy);
        assassinList.Add(Unit.Spitter);
        assassinList.Add(Unit.Zombie);

        List<Unit> beastList = new List<Unit>();
        beastList.Add(Unit.BabyDragon);
        beastList.Add(Unit.RatWarrior);
        beastList.Add(Unit.Tiger);
        beastList.Add(Unit.BigLizard);
        beastList.Add(Unit.Chomper);
        beastList.Add(Unit.Spitter);


        List<Unit> elementalList = new List<Unit>();
        elementalList.Add(Unit.RockGolem);
        elementalList.Add(Unit.Orbling);
        elementalList.Add(Unit.Cactus);
        elementalList.Add(Unit.BigLizard);
        elementalList.Add(Unit.ShamanTotem);
        elementalList.Add(Unit.Tree);

        List<Unit> guardianList = new List<Unit>();
        guardianList.Add(Unit.AxeGuard);
        guardianList.Add(Unit.Grenadier);
        guardianList.Add(Unit.GoblinZerk);
        guardianList.Add(Unit.KnightTower);
        guardianList.Add(Unit.DemonLord);
        guardianList.Add(Unit.AngelStatue);

        List<Unit> structureList = new List<Unit>();
        structureList.Add(Unit.MageTower);
        structureList.Add(Unit.ShamanTotem);
        structureList.Add(Unit.Tree);
        structureList.Add(Unit.KnightTower);
        structureList.Add(Unit.CursedTomb);
        structureList.Add(Unit.WarriorTent);
        structureList.Add(Unit.AngelStatue);


        List<Unit> undeadList = new List<Unit>();
        undeadList.Add(Unit.Skeleton);
        undeadList.Add(Unit.DeadWizard);
        undeadList.Add(Unit.Zombie);
        undeadList.Add(Unit.CursedTomb);
        undeadList.Add(Unit.DemonLord);


        List<Unit> warriorList = new List<Unit>();
        warriorList.Add(Unit.RockGolem);
        warriorList.Add(Unit.RatWarrior);
        warriorList.Add(Unit.Skeleton);
        warriorList.Add(Unit.AxeGuard);
        warriorList.Add(Unit.RogueSpy);
        warriorList.Add(Unit.GoblinZerk);
        warriorList.Add(Unit.WarriorTent);

        List<Unit> wizardList = new List<Unit>();
        wizardList.Add(Unit.BabyDragon);
        wizardList.Add(Unit.Orbling);
        wizardList.Add(Unit.DeadWizard);
        wizardList.Add(Unit.MageTower);
        wizardList.Add(Unit.Chomper);

        TRIBAL_UNIT_DATA = new Dictionary<Tribe, List<Unit>> // public enum Unit {RatWarrior, Tiger, Orbling, Skeleton,AxeGuard,DeadWizard,Cactus,BabyDragon,BigLizard,RockGolem,MageTower,ShamanTotem} --- public enum Tribe { Warrior, Beast, Elemental, Undead, Wizard, Assassin, Guardian, Structure }
        {
            {Tribe.Assassin, assassinList},
            {Tribe.Beast, beastList},
             {Tribe.Elemental, elementalList},
            {Tribe.Guardian, guardianList},
            {Tribe.Structure,structureList },
            {Tribe.Undead, undeadList},
            {Tribe.Warrior, warriorList},
             {Tribe.Wizard,wizardList },
        };

        constant_HP_RegenPerSecond = constant_HP_BaseTickRegeneration / constant_HP_RegenerationIntervalSeconds;
        constant_HP_CONCENTRATIONPerSecond = constant_Concentration_BaseTickRegeneration / constant_Concentration_RegenerationIntervalSeconds;

        InitializeDeployedTribesCounter();
        ReInitializeEnemyActiveTribesCounter();
        InitializeShoppingOptions();
        SetPlayerGoldCount(playerGoldCount);
        playerOwnedNpcs = new List<NPC>();
        uiController.ChangeCurrentPlayerUsernameDisplayText(playerName,playerMMR.ToString());
        SetCurrentlyDeployedUnits(0);

        uiController.ChangeHPPlayerDisplayText(this.currentPlayerHealth, this.maxPlayerHealth);
    }

    public void ReInitializeEnemyActiveTribesCounter()
    {
        enemyActiveTribesCounter = new Dictionary<Tribe, int>();
        var TribesArray = Enum.GetValues(typeof(Tribe));
        for (int i = 0; i < TribesArray.Length; i++)
        {
   
            enemyActiveTribesCounter.Add((Tribe)TribesArray.GetValue(i), 0);
        }
    }

    void InitializeDeployedTribesCounter()
    {
        deployedTribesCounter = new Dictionary<Tribe, int>();
        var TribesArray = Enum.GetValues(typeof(Tribe));
        for (int i = 0; i < TribesArray.Length; i++)
        {
            deployedTribesCounter.Add((Tribe)TribesArray.GetValue(i), 0);
        
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if ((boardController.gameStatus == "Fight" || boardController.gameStatus == "shopping") && currentPlayerHealth <= 0) // check lose condition
        {

            int mmrChange = 0;

            int currentGameRound = boardController.currentGameRound;
            if (currentGameRound < 10)
            {
                mmrChange -= 25;
            }
            else if (currentGameRound >= 10)
            {
                mmrChange += (25 + (currentGameRound / playerMMR % 100));
            }
            playerMMR += mmrChange;

            boardController.ChangeGameStatus("report defeat");
            uiController.hudCanvasReportDefeatPanel.gameObject.SetActive(true);
            uiController.hudCanvasTribesPanel.gameObject.SetActive(false);
            uiController.hudCanvasTopBar.gameObject.SetActive(false);
            uiController.hudCanvasBottomBar.gameObject.SetActive(false);
            uiController.hudCanvasShopPanel.gameObject.SetActive(false);
            uiController.SaveToSaveFile();
            uiController.UpdateReportDefeatPanelScreen(mmrChange);

        }

    }

    public void LoadProfileFromSave(PlayerProfileSave pps)
    {
        playerName = pps.characterName;
        playerMMR = pps.mmr;
    }




    public bool buyUnitFromShop(Unit unitToBuy) // buy a unit from the shop
    {

        int unitCost = 0;
        NPC_COST_DATA.TryGetValue(unitToBuy, out unitCost);
        if (unitCost <= playerGoldCount)
        {
            if (SpawnFriendlyNPC(unitToBuy))
            {
                return true;
            }
        }
        return false;
    }


    public bool SpawnFriendlyNPC(Unit unitToSpawn) // spawn a friendly npc in the reserve board if there is place
    {
        string prefabName = Enum.GetName(typeof(Unit), unitToSpawn);

        if (currentPlayerUnits < playerGlobalUnitCap)
        {
            bool doneSearching = false;
            bool reserveHasEmptySlot = false;
            int i = 0;
            int j = 0;

            for (j = 0; j < 8; j++)
            {
                if (boardController.chessBoard[8, j].GetComponent<TileBehaviour>().occupyingUnit == null)
                {
                    reserveHasEmptySlot = true;
                }
            }

            if (reserveHasEmptySlot)
            {

                while (!doneSearching)
                {
                    for (i = 0; i < 8; i++)
                    {
                        if (boardController.chessBoard[8, i].GetComponent<TileBehaviour>().occupyingUnit == null)
                        {
                            doneSearching = true;
                            break;
                        }
                    }
                }

                GameObject tile = boardController.chessBoard[8, i];

                GameObject go = (GameObject)GameObject.Instantiate(Resources.Load(prefabName), tile.transform.position, Quaternion.Euler(0, -90, 0));

                int unitCost = 0;
                NPC_COST_DATA.TryGetValue(unitToSpawn, out unitCost);

                tile.GetComponent<TileBehaviour>().occupyingUnit = go;
                go.GetComponentInChildren<NPC>().occupyingTile = tile;
                go.GetComponentInChildren<NPC>().PrepareNPC3DHud();
                npcController.UpdateNpcList();
                SetPlayerGoldCount(playerGoldCount - unitCost);
                currentPlayerUnits = currentPlayerUnits + 1;
                return true;
            }
        }
        return false;
    }



    public void SetCurrentHP(int amount)
    {
        currentPlayerHealth = amount;
        uiController.ChangeHPPlayerDisplayText(currentPlayerHealth,maxPlayerHealth);
        uiController.DamageOveray_Animation_CycleFadeInFadeOut(0.5f);
    }

    public void SetMaxHP(int amount)
    {
        maxPlayerHealth = amount;
        uiController.ChangeHPPlayerDisplayText(currentPlayerHealth, maxPlayerHealth);

    }



    public void SetMaxDeployedUnitsLimit(int amount)
    {
        maxDeployedUnitsLimit = amount;
        uiController.ChangeDeployedUnitCountDisplayText(currentlyDeployedUnits, maxDeployedUnitsLimit);
    }

    public void SetCurrentlyDeployedUnits(int amount)
    {
        currentlyDeployedUnits = amount;
        uiController.ChangeDeployedUnitCountDisplayText(currentlyDeployedUnits, maxDeployedUnitsLimit);
    }

    public void SetPlayerUsername(string name)
    {
        playerName = name;
        uiController.ChangeCurrentPlayerUsernameDisplayText(playerName, playerMMR.ToString());
    }

    public void SetPlayerGoldCount(long amount)
    {

        playerGoldCount = amount;
        uiController.ChangeCurrentPlayerGoldCountDisplayText(playerGoldCount.ToString());
    }
}
