using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public static class  Helper
{

    public static void Increment<T>(this Dictionary<T, int> dictionary, T key)
    {
        int count = 0;
        dictionary.TryGetValue(key, out count);
        dictionary[key] = count + 1;
        
    }

    public static void Decrement<T>(this Dictionary<T, int> dictionary, T key)
    {

        int count = 0;
        dictionary.TryGetValue(key, out count);
        dictionary[key] = count - 1;
    }
}


public class NPC : MonoBehaviour
{
    public NpcController npcController;
    public GameObject worldControl;
    public BoardController boardController;
    public PlayerController playerController;
    public UiController uiController;
    public List<GameObject> inventory;
    public List<GameObject> equipment;
    public GameObject currentInventorySelection;
    public GameObject currentTarget;
    public SpellbookController spellBookController;
    public Animator animator;
    //
    public float HP;
    public float MAXHP;
    public float BASE_MAXHP;
    public float HPRegeneration;
    public float CONCENTRATION;
    public float MAXCONCENTRATION;
    public float RETALIATION;
    public float BASE_RETALIATION;
    public float ARMOR;
    public float BASE_ARMOR;
    public float ATTACKPOWER;
    public float BASE_ATTACKPOWER;
    public float SPELLPOWER;
    public float BASE_SPELLPOWER;
    public int TIER;
    public int AttackDistance;
    public float actionTime;

    public Tribe PRIMARYTRIBE;
    public Tribe SECONDARYTRIBE;
    public Unit UNIT_TYPE;
    public Ability ABILITY;
    public DamageSource autoattack_DamageType;


    //
    public float baseGoldBountyReward;
    //
    public bool visualIdentifierGenerated = false;
    public bool isEnemy = false;
    public bool beingDragged;
    public bool isDying_SingleRunController = false;

    public Coroutine liveRoutine = null;

    //
    public GameObject occupyingTile;
    public NPC target;

    private VisualSideIdentifierCircleToggler visualSideIdentifierCircle;


    public void Awake()
    {
        FindWorldControllers();
        HP = BASE_MAXHP;
        MAXHP = BASE_MAXHP;
        ARMOR = BASE_ARMOR;
        ATTACKPOWER = BASE_ATTACKPOWER;
        SPELLPOWER = BASE_SPELLPOWER;
        RETALIATION = BASE_RETALIATION;
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartLiveRoutine();
    }

    public void StartLiveRoutine()
    {
        liveRoutine = StartCoroutine(Live(this.AttackDistance, this.autoattack_DamageType, this.ABILITY, this.actionTime));

    }

    private void Update()
    {
        if (target != null)
        {
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        }
    }

    

    public void recalculateArmorValue()
    {
        this.ARMOR = BASE_ARMOR;

        if (ARMOR < 0.01f)
        {
            ARMOR = 0.01f;
        }
    }

    public void recalculateHPValues()
    {
        this.HP = BASE_MAXHP;
        this.MAXHP = BASE_MAXHP;

    }
    public void recalculateAttackPowerValue()
    {
        this.ATTACKPOWER = BASE_ATTACKPOWER;
    }
    public void recalculateSpellPowerValue() {
        this.SPELLPOWER = BASE_SPELLPOWER;
    }

    public void recalculateRetaliationValue()
    {
        this.RETALIATION = BASE_RETALIATION;
    }


    public void GainConcentration(float concentrationToGain, HealSource concentrationSourceType)
    {
        if (concentrationToGain <= 0)
        {
            return;
        }

        if (concentrationSourceType == HealSource.Concentration_Gain)
        {
            this.CONCENTRATION += concentrationToGain;
        }

        if (this.CONCENTRATION > this.MAXCONCENTRATION)
        {
            this.CONCENTRATION = MAXCONCENTRATION;
        }
    }

    public void GainHP(float healsToHeal, HealSource healSourceType)
    {
        if (healsToHeal <= 0)
        {
            return;
        }

        this.HP = this.HP + healsToHeal;

        if (HP > MAXHP)
        {
            HP = MAXHP;
        }
    }



    

    public void CheckConcentrationGain_And_Guardian3Bonus(NPC damageDealer, int guardiansCount, int enemy_GuardiansCount)
    {
        float concentrationGain = playerController.constant_OnHit_BaseConcentrationGain;
        if (damageDealer.isEnemy == false && guardiansCount >= 3)
        {
            concentrationGain *= playerController.coefficient_Guardian_3_OnHit_ConcentrationGainBonusMultiplier; // FRIENDLY  GUARDIAN TRIBE 3 * CONCENTRATION GAIN PER HIT BONUS
        }

       else if (damageDealer.isEnemy == true && enemy_GuardiansCount >= 3)
        {
            concentrationGain *= playerController.coefficient_Guardian_3_OnHit_ConcentrationGainBonusMultiplier;  // ENEMY GUARDIAN TRIBE 3 * BONUS CONCENTRATION ON HITI 
        }

  
        damageDealer.GainConcentration(concentrationGain, HealSource.Concentration_Gain);
    }
    public bool CheckRollMiss_And_Assassin6Bonus(bool DamageDealerIsEnemy, int assassinsCount, int enemy_AssassinsCount, int rollMiss)
    {

        if (DamageDealerIsEnemy == true && assassinsCount >= 6)
        {
            rollMiss = UnityEngine.Random.Range(playerController.constant_Miss_RngRollLowerBound, playerController.constant_Assassin_6_MissBonus_RngRollBonusUpperBound); // FRIENDLY Assassin Tribe 6 * BUFFED EVASION
        }

       else if (DamageDealerIsEnemy == false && enemy_AssassinsCount >= 6)
        {
            rollMiss = UnityEngine.Random.Range(playerController.constant_Miss_RngRollLowerBound, playerController.constant_Assassin_6_MissBonus_RngRollBonusUpperBound); // ENEMY Assassin Tribe 6 * BUFFED EVASION
        }

        if (rollMiss == 0) // hard coded explicit 1% miss chance (careful with changing rollMiss bounds)
        {
            return false;
        } else
        {
            return true;
        }
    }

    public float CheckUndead6Bonus(bool DamageDealerIsEnemy, int undeadCount, int enemy_UndeadCount)
    {
        if (DamageDealerIsEnemy == false && undeadCount >= 6)
        {
            return playerController.coefficient_Undead_6_SpellPowerBonus_OnHitBonusSpellPower;
        }
        else if (DamageDealerIsEnemy == true && enemy_UndeadCount >= 6)
        {
            return playerController.coefficient_Undead_6_SpellPowerBonus_OnHitBonusSpellPower;
        }
        else { return 1; }
    }

    public float CalculateCriticalDamage_And_Beast3Bonus(bool DamageDealerIsEnemy, int beastsCount, int enemy_beastsCount, int rollCriticalHit)
    {
        if (DamageDealerIsEnemy == false && beastsCount >= 3)
        {
            rollCriticalHit = UnityEngine.Random.Range(playerController.constant_Beast_3_CritBonus_RngRollBonusLowerBound, playerController.constant_Crit_RngRollBaseUpperBound); // FRIENDLY Beast Tribe 3 * BUFFED CRITICAL CHANCE
        }

      else  if (DamageDealerIsEnemy == true && enemy_beastsCount >= 3)
        {
            rollCriticalHit = UnityEngine.Random.Range(playerController.constant_Beast_3_CritBonus_RngRollBonusLowerBound, playerController.constant_Crit_RngRollBaseUpperBound); // ENEMY Beast Tribe 3 * BUFFED CRITICAL CHANCE
        }

        if (rollCriticalHit >= playerController.constant_Crit_RngRollBaseMinimumRequiredLuck) // Regular Base Critical Chance for All units.
        {
            return playerController.coefficient_Crit_OnCritBonusDamageMultiplier;  // critical value scalar coefficient

        }

        return 1;
    }

    public float CheckWarrior6Bonus(bool DamageDealerIsEnemy, int warriorsCount, int enemy_WarriorsCount)
    {
        if (DamageDealerIsEnemy == false && warriorsCount >= 6)
        {
            return playerController.coefficient_Warrior_6_OnHitBonusPhysicalDamageMultiplier;  // FRIENDLY Warrior Tribe 6 * BUFFED PHYSICAL DAMAGE
        }

      else  if (DamageDealerIsEnemy == true && enemy_WarriorsCount >= 6)
        {
             return playerController.coefficient_Warrior_6_OnHitBonusPhysicalDamageMultiplier;  // ENEMY Warrior Tribe 6 * BUFFED PHYSICAL DAMAGE
        }

        return 1;
    }

    public float CalculateDamageTaken(float damageToDeal,NPC damageDealer,DamageSource damageSourceType)
    {
        /// PRE-DAMAGE-SOURCE-FORK Preparation
        if (damageToDeal <= 0)
        {
            return 0;
        }

        int beastsCount = 0;
        playerController.deployedTribesCounter.TryGetValue(Tribe.Beast, out beastsCount);

        int enemy_beastsCount = 0;
        playerController.enemyActiveTribesCounter.TryGetValue(Tribe.Beast, out enemy_beastsCount);

        int warriorsCount = 0;
        playerController.deployedTribesCounter.TryGetValue(Tribe.Warrior, out warriorsCount);

        int enemy_WarriorsCount = 0;
        playerController.enemyActiveTribesCounter.TryGetValue(Tribe.Warrior, out enemy_WarriorsCount);

        int elementalsCount = 0;
        playerController.deployedTribesCounter.TryGetValue(Tribe.Elemental, out elementalsCount);

        int enemy_ElementalsCount = 0;
        playerController.enemyActiveTribesCounter.TryGetValue(Tribe.Elemental, out enemy_ElementalsCount);

        int wizardsCount = 0;
        playerController.deployedTribesCounter.TryGetValue(Tribe.Wizard, out wizardsCount);

        int enemy_WizardsCount = 0;
        playerController.enemyActiveTribesCounter.TryGetValue(Tribe.Wizard, out enemy_WizardsCount);

        int assassinsCount = 0;
        playerController.deployedTribesCounter.TryGetValue(Tribe.Assassin, out assassinsCount);

        int enemy_AssassinsCount = 0;
        playerController.enemyActiveTribesCounter.TryGetValue(Tribe.Assassin, out enemy_AssassinsCount);

        int undeadCount = 0;
        playerController.deployedTribesCounter.TryGetValue(Tribe.Undead, out undeadCount);

        int enemy_UndeadCount = 0;
        playerController.enemyActiveTribesCounter.TryGetValue(Tribe.Undead, out enemy_UndeadCount);

        int structureCount = 0;
        playerController.deployedTribesCounter.TryGetValue(Tribe.Structure, out structureCount);

        int enemy_StructureCount = 0;
        playerController.enemyActiveTribesCounter.TryGetValue(Tribe.Structure, out enemy_StructureCount);

        int guardiansCount = 0;
        playerController.deployedTribesCounter.TryGetValue(Tribe.Guardian, out guardiansCount);

        int enemy_GuardiansCount = 0;
        playerController.enemyActiveTribesCounter.TryGetValue(Tribe.Guardian, out enemy_GuardiansCount);

        int rollMiss = UnityEngine.Random.Range(playerController.constant_Miss_RngRollLowerBound, playerController.constant_Miss_RngRollUpperBound);
        int rollCriticalHit = UnityEngine.Random.Range(playerController.constant_Crit_RngRollBaseLowerBound, playerController.constant_Crit_RngRollBaseUpperBound);
        bool DamageDealerIsEnemy = damageDealer.isEnemy;
        float netRetaliation = this.RETALIATION;
        float netArmor = this.ARMOR;

        if (CheckRollMiss_And_Assassin6Bonus(DamageDealerIsEnemy,assassinsCount,enemy_AssassinsCount,rollMiss) == false)
        {
            return 0;
        } 

        if (damageSourceType == DamageSource.Magical_Ability || damageSourceType == DamageSource.Physical_Ability)
        {
            damageToDeal *= CheckUndead6Bonus(DamageDealerIsEnemy,undeadCount,enemy_UndeadCount);

        }

        if (damageSourceType == DamageSource.MagicalDamage_AutoAttack || damageSourceType == DamageSource.PhysicalDamage_AutoAttack)
        {
            CheckConcentrationGain_And_Guardian3Bonus(damageDealer,guardiansCount,enemy_GuardiansCount);
        }

        damageToDeal *= CalculateCriticalDamage_And_Beast3Bonus(DamageDealerIsEnemy,beastsCount,enemy_beastsCount,rollCriticalHit);

        /// DAMAGE-SOURCE-FORK
        if (damageSourceType == DamageSource.PhysicalDamage_AutoAttack || damageSourceType == DamageSource.Physical_Ability)
        {
            damageToDeal *= CheckWarrior6Bonus(DamageDealerIsEnemy,warriorsCount,enemy_WarriorsCount);
            damageToDeal *= CheckGuardian6Bonus(DamageDealerIsEnemy, guardiansCount, enemy_GuardiansCount, rollMiss);
            if (damageToDeal <= 0)
            {
                return 0; // guardian 6 dampened check
            }

        }

        else if (damageSourceType == DamageSource.MagicalDamage_AutoAttack || damageSourceType == DamageSource.Magical_Ability)
        {

            damageToDeal *= CalculateWizard3Bonus(DamageDealerIsEnemy,wizardsCount,enemy_WizardsCount);
            damageToDeal *= CheckWizard6Bonus(DamageDealerIsEnemy, wizardsCount, enemy_WizardsCount, rollMiss);
            if (damageToDeal <= 0)
            {
                return 0; //wizard 6 dampened check
            }

        }

      
        damageToDeal *= CheckElemental6Bonus(DamageDealerIsEnemy, elementalsCount, enemy_ElementalsCount);
        netArmor *= CalculateStructure6Bonus(DamageDealerIsEnemy,structureCount,enemy_StructureCount);
        netRetaliation *= CalculateStructure3Bonus(DamageDealerIsEnemy, structureCount, enemy_StructureCount);
        netArmor /= CheckUndead3Bonus(DamageDealerIsEnemy, undeadCount, enemy_UndeadCount);
        damageToDeal -= (netArmor);
        if (damageSourceType == DamageSource.MagicalDamage_AutoAttack || damageSourceType == DamageSource.PhysicalDamage_AutoAttack)
        {
            LifeSteal_CheckBeast6Bonus(DamageDealerIsEnemy, beastsCount, enemy_beastsCount, damageDealer, damageToDeal);
            damageDealer.HP -= damageToDeal * (netRetaliation / playerController.coefficient_DamageFormula_OnDamage_RetaliationEfficiencyPenalty);
        }
        return damageToDeal;         
    }

    public float CheckWizard6Bonus(bool DamageDealerIsEnemy, int wizardsCount, int enemy_WizardsCount, int rollMiss)
    {

        if (DamageDealerIsEnemy == true && wizardsCount >= 6)
        {
            if (rollMiss <= playerController.constant_Wizard_6_DampenBonus_RngRollMinimumRequiredRoll)
            {
                return 0;
            }
            return playerController.coefficient_Wizard_6_DampenBonus_OnIncomingMagicalDamageMultiplier;

        }

        else if (DamageDealerIsEnemy == false && enemy_WizardsCount >= 6)
        {
            if (rollMiss <= playerController.constant_Wizard_6_DampenBonus_RngRollMinimumRequiredRoll)
            {
                return 0;
            }
            return playerController.coefficient_Wizard_6_DampenBonus_OnIncomingMagicalDamageMultiplier;

        }
        return 1;
    }


    public float CalculateWizard3Bonus(bool DamageDealerIsEnemy, int wizardsCount, int enemy_WizardsCount)
    {
        if (DamageDealerIsEnemy == false && wizardsCount >= 3)
        {
            return playerController.coefficient_Wizard_3_OnHitBonusMagicalDamageMultiplier; // wizard bonus 3

        }

       else if (DamageDealerIsEnemy == true && enemy_WizardsCount >= 3)
        {
            return playerController.coefficient_Wizard_3_OnHitBonusMagicalDamageMultiplier; // wizard bonus 3
        }

        return 1;
    }

    public float CalculateStructure3Bonus(bool DamageDealerIsEnemy,int structureCount, int enemy_StructureCount)
    {

        if (DamageDealerIsEnemy == true && structureCount >= 3)
        {
            return  playerController.coefficient_Structure_3_RetaliationBonus_OnIncomingDamageRetaliationMultiplier; // FRIENDLY Structure Tribe 3 * FRIENDLY RETALIATION BUFFED
        }

        else if (DamageDealerIsEnemy == false && enemy_StructureCount >= 3)
        {
            return playerController.coefficient_Structure_3_RetaliationBonus_OnIncomingDamageRetaliationMultiplier; // ENEMY Structure Tribe 3 * ENEMY RETALIATION BUFFED
        }
        return 1;
    }

    public float CheckUndead3Bonus(bool DamageDealerIsEnemy, int undeadCount, int enemy_UndeadCount)
    {
        if (DamageDealerIsEnemy == false && undeadCount >= 3)
        {
            return playerController.coefficient_Undead_3_ArmorPierceBonus_OnHitBonusPierceMultiplier; // FRIENDLY Undead Tribe 3 *  ARMOR DEBUFFED
        }

       else if (DamageDealerIsEnemy == true && enemy_UndeadCount >= 3)
        {
            return playerController.coefficient_Undead_3_ArmorPierceBonus_OnHitBonusPierceMultiplier; // ENEMY Undead Tribe 3 *  ARMOR DEBUFFED
        }

        return 1;
    }

    public float CheckGuardian6Bonus(bool DamageDealerIsEnemy, int guardiansCount, int enemy_GuardiansCount ,int rollMiss)
    {
        if (DamageDealerIsEnemy == true && guardiansCount >= 6) // friendly Guardian triibe 6 bonus
        {
            rollMiss = UnityEngine.Random.Range(playerController.constant_Miss_RngRollLowerBound, playerController.constant_Miss_RngRollUpperBound);
            if (rollMiss <= playerController.constant_Guardian_6_DampenBonus_RngRollMinimumRequiredRoll)
            {
                return 0;
            }
            return playerController.coefficient_Guardian_6_DampenBonus_OnIncomingPhysicalDamageMultiplier;

        }

        else if (DamageDealerIsEnemy == false && enemy_GuardiansCount >= 6) // enemy Guardian triibe 6 bonus
        {
            rollMiss = UnityEngine.Random.Range(playerController.constant_Miss_RngRollLowerBound, playerController.constant_Miss_RngRollUpperBound);
            if (rollMiss <= playerController.constant_Guardian_6_DampenBonus_RngRollMinimumRequiredRoll)
            {
                return 0;
            }
            return playerController.coefficient_Guardian_6_DampenBonus_OnIncomingPhysicalDamageMultiplier;
    
        }
        return 1;
    }

    public float CheckElemental6Bonus(bool DamageDealerIsEnemy, int elementalsCount, int enemy_ElementalsCount)
    {
        if (DamageDealerIsEnemy == false && elementalsCount >= 6)
        {
            return playerController.coefficient_Elemental_6_OnHitBonusDamageMultiplier; // FRIENDLY Elemental Tribe  * 6 BUFFED PHYSICAL DAMAGE
        }

        else if (DamageDealerIsEnemy == true && enemy_ElementalsCount >= 6)
        {
            return  playerController.coefficient_Elemental_6_OnHitBonusDamageMultiplier; // // ENEMY Elemental Tribe  * 6 BUFFED PHYSICAL DAMAGE
        }
        return 1;
    }

    public void ReduceConcentration(float DMG)
    {

        CONCENTRATION = CONCENTRATION - DMG;
        if (CONCENTRATION <= 0)
        {
            CONCENTRATION = 0;
        }

        if (CONCENTRATION > MAXCONCENTRATION)
        {
            CONCENTRATION = MAXCONCENTRATION;
        }
    }

    public float CalculateStructure6Bonus(bool DamageDealerIsEnemy, int structureCount, int enemy_StructureCount)
    {

        if (DamageDealerIsEnemy == true && structureCount >= 6)
        {
            return playerController.coefficient_Structure_6_ArmorBonus_OnIncomingDamageArmorMultiplier; // FRIENDLY Structure Tribe 6 * ARMOR BUFFED
        }

        else if (DamageDealerIsEnemy == false && enemy_StructureCount >= 6)
        {
            return playerController.coefficient_Structure_6_ArmorBonus_OnIncomingDamageArmorMultiplier; // ENEMY Structure Tribe 6 * ARMOR BUFFED
        }
        return 1;
    }

    public void ReduceRetaliation(float amount)
    {
        if (RETALIATION > 0)
        {
            RETALIATION = RETALIATION - amount;
        }

        if (RETALIATION < 0)
        {
            RETALIATION = 0;
        }
    }

    public void RemoveFromBoard(bool isNotCombatRelatedDeath)
    {
        if (isEnemy && !isDying_SingleRunController) // your unit dies
        {
            isDying_SingleRunController = true;
            Helper.Decrement<Tribe>(playerController.enemyActiveTribesCounter, this.PRIMARYTRIBE);
            Helper.Decrement<Tribe>(playerController.enemyActiveTribesCounter, this.SECONDARYTRIBE);
            npcController.enemyList.Remove(gameObject.GetComponent<NPC>());

            long goldBountyReward = (long)this.baseGoldBountyReward + (long) Random.Range(0, Mathf.Pow(2, boardController.currentGameRound)); // TODO: Find me a curve and coefficients

            int assassinsCount = 0;
            playerController.deployedTribesCounter.TryGetValue(Tribe.Assassin, out assassinsCount);

            if (assassinsCount >= 3)
            {
                float bonusGold = goldBountyReward * playerController.coefficient_Assassin_3_OnKillBonusGoldMultiplier;
                goldBountyReward += (long)bonusGold;
            }

            if (!isNotCombatRelatedDeath)
            {
            //    Debug.Log("Rewarding you " + goldBountyReward + " gold for killing: " + this.name);
                playerController.SetPlayerGoldCount(playerController.playerGoldCount + goldBountyReward);
            }
            npcController.npcList.Remove(gameObject.GetComponent<NPC>());
            gameObject.GetComponent<NPC>().occupyingTile.GetComponent<TileBehaviour>().occupyingUnit = null;
            Object.DestroyImmediate(transform.parent.gameObject);

        } else if (!isEnemy && !isDying_SingleRunController) // enemy unit dies
        {
      //      Debug.Log("Your unit died");
            isDying_SingleRunController = true;
            npcController.allyList.Remove(gameObject.GetComponent<NPC>());
            npcController.deployedAllyList.Remove(gameObject.GetComponent<NPC>());


            playerController.currentPlayerUnits--;

            if (occupyingTile.GetComponent<TileBehaviour>().i != 8)
            {
                Helper.Decrement<Tribe>(playerController.deployedTribesCounter, this.PRIMARYTRIBE);
                Helper.Decrement<Tribe>(playerController.deployedTribesCounter, this.SECONDARYTRIBE);
                uiController.RefreshDeployedTribesCounter();
                playerController.SetCurrentlyDeployedUnits(playerController.currentlyDeployedUnits - 1);
            }

            if (!isNotCombatRelatedDeath)
            {
                playerController.SetCurrentHP(playerController.currentPlayerHealth - 1);
            }
            npcController.npcList.Remove(gameObject.GetComponent<NPC>());
            gameObject.GetComponent<NPC>().occupyingTile.GetComponent<TileBehaviour>().occupyingUnit = null;
            Object.DestroyImmediate(transform.parent.gameObject);
        }
    }

    public void PrepareNPC3DHud()
    {

        if (!visualIdentifierGenerated)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("VisualSideIdentifierCircle"), this.transform.position, this.transform.rotation);
            go.transform.SetParent(this.gameObject.transform);

            this.visualSideIdentifierCircle = go.GetComponent<VisualSideIdentifierCircleToggler>();

            if (isEnemy)
            {
                visualSideIdentifierCircle.ActivateRedCircle();
            }
            else
            {
                visualSideIdentifierCircle.ActivateGreenCircle();
            }

            go = (GameObject)Instantiate(Resources.Load("HealthBar"), this.transform.position, this.transform.rotation);
            go.transform.SetParent(this.gameObject.transform);
            if (this.ABILITY == Ability.NOTHING)
            {
                go.GetComponent<HealthBarController>().emptyConcentrationBar.SetActive(false);
            }
          

            visualIdentifierGenerated = true;
        }
    }

    public void FindWorldControllers()
    {
        worldControl = GameObject.FindGameObjectWithTag("Controller");
        npcController = worldControl.GetComponent<NpcController>();
        boardController = worldControl.GetComponent<BoardController>();
        playerController = worldControl.GetComponent<PlayerController>();
        uiController = worldControl.GetComponent<UiController>();
        spellBookController = this.GetComponent<SpellbookController>();
    }

    void OnMouseDown()
    {


        if (boardController.selectedObject != this.gameObject)
        {
            boardController.selectedObject = this.gameObject;
        }
        else if (boardController.gameStatus.Equals("shopping"))
        {
            this.beingDragged = true;
        }

    }

    private void OnMouseDrag()
    {
        if (beingDragged && !isEnemy)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("ChessTile"))) {
                this.gameObject.GetComponentsInParent<Transform>()[1].position = hit.point + Vector3.up * 2;
            }
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("ChessBoard")))
            {
                this.gameObject.GetComponentsInParent<Transform>()[1].position = hit.point + Vector3.up * 2;
            }

            worldControl.GetComponent<LineRenderer>().enabled = true;
            Vector3[] positions = new Vector3[2];
            positions[0] = this.transform.position;
            positions[1] = this.transform.position + Vector3.down * 2;
            worldControl.GetComponent<LineRenderer>().SetPositions(positions);
        }
    }

    private void OnMouseUp()
    {

        if (isEnemy) { beingDragged = false; }
        if (beingDragged)
        {



            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("ChessTile")))
            {
                if (hit.collider.GetComponent<TileBehaviour>().i >= 4)
                {
                    moveToEmptyTile(hit.collider.GetComponent<TileBehaviour>().i, hit.collider.GetComponent<TileBehaviour>().j);
                }

            }

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("ChessBoard")))
            {
                    this.gameObject.GetComponentsInParent<Transform>()[1].position = this.occupyingTile.transform.position;
            }


            beingDragged = false;
            worldControl.GetComponent<LineRenderer>().enabled = false;
        }
    }


    public void LifeSteal_CheckBeast6Bonus(bool DamageDealerIsEnemy, int beastsCount, int enemy_beastsCount, NPC damageDealer, float damageToDeal)
    {
        if (DamageDealerIsEnemy == false && beastsCount >= 6)
        {
            damageDealer.HP += playerController.coefficient_Beast_6_LifeStealBonus_OnDamage_LifeStealEfficiencyMultiplier * damageToDeal; // FRIENDLY Beast Tribe 6 * LIFESTEAL
        }

        else if (DamageDealerIsEnemy == true && enemy_beastsCount >= 6)
        {
            damageDealer.HP += playerController.coefficient_Beast_6_LifeStealBonus_OnDamage_LifeStealEfficiencyMultiplier * damageToDeal; // FRIENDLY Beast Tribe 6 * LIFESTEAL

        }
    }


    public void moveToEmptyTile(int i, int j)
    {
        if (((i < 8 && i >= 0 && j < 8 && j >= 0) && !boardController.gameStatus.Equals("shopping")) || ((i < 9 && i >= 0 && j < 8 && j >= 0) && boardController.gameStatus.Equals("shopping"))) {
            if (boardController.chessBoard != null && boardController.chessBoard[i, j].GetComponent<TileBehaviour>().occupyingUnit == null)
            {
                if (this.occupyingTile.GetComponent<TileBehaviour>().i == 8 && boardController.chessBoard[i, j].GetComponent<TileBehaviour>().i != 8) // swapping from reserve board into the chess board
                {
                    playerController.SetCurrentlyDeployedUnits(playerController.currentlyDeployedUnits + 1);
                    npcController.deployedAllyList.Add(this);
                    Helper.Increment<Tribe>(playerController.deployedTribesCounter, this.PRIMARYTRIBE);
                    Helper.Increment<Tribe>(playerController.deployedTribesCounter, this.SECONDARYTRIBE);
                    uiController.RefreshDeployedTribesCounter();
                    LevelUpFriendly();
                }

                if (this.occupyingTile.GetComponent<TileBehaviour>().i != 8 && boardController.chessBoard[i, j].GetComponent<TileBehaviour>().i == 8) // swapping from chesss board into the reserve board
                {
                    playerController.SetCurrentlyDeployedUnits(playerController.currentlyDeployedUnits - 1);
                    npcController.deployedAllyList.Remove(this);
                    Helper.Decrement<Tribe>(playerController.deployedTribesCounter, this.PRIMARYTRIBE);
                    Helper.Decrement<Tribe>(playerController.deployedTribesCounter, this.SECONDARYTRIBE);
                    uiController.RefreshDeployedTribesCounter();
                }

                if (playerController.currentlyDeployedUnits <= playerController.maxDeployedUnitsLimit) {
                    this.occupyingTile.gameObject.GetComponent<TileBehaviour>().occupyingUnit = null;
                    this.occupyingTile = boardController.chessBoard[i, j];
                    boardController.chessBoard[i, j].GetComponent<TileBehaviour>().occupyingUnit = this.gameObject;
                    this.gameObject.GetComponentsInParent<Transform>()[1].position = boardController.chessBoard[i, j].GetComponent<TileBehaviour>().transform.position;
                } else
                {
                    playerController.SetCurrentlyDeployedUnits(playerController.currentlyDeployedUnits - 1);                     
                    npcController.deployedAllyList.Remove(this);
                    Helper.Decrement<Tribe>(playerController.deployedTribesCounter, this.PRIMARYTRIBE);
                    Helper.Decrement<Tribe>(playerController.deployedTribesCounter, this.SECONDARYTRIBE);
                    uiController.RefreshDeployedTribesCounter();
                }
            } 
        }
    }

    public void ApplyTier2Upgrades()
    {
        this.TIER = 2;
        if (PRIMARYTRIBE.Equals(Tribe.Warrior))
        {
            this.BASE_MAXHP += 100;
            recalculateHPValues();
        }
        else if (PRIMARYTRIBE.Equals(Tribe.Wizard))
        {
            this.BASE_SPELLPOWER += 50;
            recalculateSpellPowerValue();
        }
        else if (PRIMARYTRIBE.Equals(Tribe.Undead))
        {
            this.BASE_RETALIATION += 5;
            recalculateRetaliationValue();
        }
        else if (PRIMARYTRIBE.Equals(Tribe.Structure))
        {
            this.BASE_MAXHP += 100;
            recalculateHPValues();
        }
        else if (PRIMARYTRIBE.Equals(Tribe.Guardian))
        {
            this.BASE_ARMOR += 5;
            this.recalculateArmorValue();
        }
        else if (PRIMARYTRIBE.Equals(Tribe.Elemental))
        {
            this.BASE_SPELLPOWER += 25;
            this.BASE_ATTACKPOWER += 25;
            recalculateSpellPowerValue();
            recalculateAttackPowerValue();
        }
        else if (PRIMARYTRIBE.Equals(Tribe.Beast))
        {
            this.BASE_ATTACKPOWER += 25;
            this.BASE_RETALIATION += 2;
            recalculateAttackPowerValue();
            recalculateRetaliationValue();
        }
        else if (PRIMARYTRIBE.Equals(Tribe.Assassin))
        {
            this.BASE_ATTACKPOWER += 100;
            recalculateAttackPowerValue();
        }
    }
    public void ApplyTier3Upgrades()
    {
        this.TIER = 3;
        if (PRIMARYTRIBE.Equals(Tribe.Warrior))
        {
            this.BASE_MAXHP += 125;
            recalculateHPValues();
        }
        else if (PRIMARYTRIBE.Equals(Tribe.Wizard))
        {
            this.BASE_SPELLPOWER += 55;
            recalculateSpellPowerValue();

        }
        else if (PRIMARYTRIBE.Equals(Tribe.Undead))
        {
            this.BASE_RETALIATION += 7;
            recalculateRetaliationValue();
        }
        else if (PRIMARYTRIBE.Equals(Tribe.Structure))
        {
            this.BASE_MAXHP += 100;
            recalculateHPValues();
        }
        else if (PRIMARYTRIBE.Equals(Tribe.Guardian))
        {
            this.BASE_ARMOR += 5;
            this.recalculateArmorValue();
        }
        else if (PRIMARYTRIBE.Equals(Tribe.Elemental))
        {
            this.BASE_SPELLPOWER += 25;
            this.BASE_ATTACKPOWER += 25;
            recalculateSpellPowerValue();
            recalculateAttackPowerValue();
        }
        else if (PRIMARYTRIBE.Equals(Tribe.Beast))
        {
            this.BASE_ATTACKPOWER += 25;
            this.BASE_RETALIATION += 2;
            recalculateAttackPowerValue();
            recalculateRetaliationValue();
        }
        else if (PRIMARYTRIBE.Equals(Tribe.Assassin))
        {
            this.BASE_ATTACKPOWER += 100;
            recalculateAttackPowerValue();
        }

    }

    public void ReduceArmor(float amountToReduce)
    {

        this.ARMOR -= amountToReduce;
    }

    public void ReduceAttackPower(float amountToReduce){
        this.ATTACKPOWER -= amountToReduce;
    }

    public void RandomLevelUpEnemy()
    {

          int levelUpRNG = Random.Range(0, 101);

        if (levelUpRNG >= 50 && levelUpRNG <= 70)
        {

            ApplyTier2Upgrades();

        } else if(levelUpRNG > 70)
        {
            ApplyTier2Upgrades();
            ApplyTier3Upgrades();
        }
  
    }

    public bool LevelUpFriendly()
    {
        int counter = 0;
     
            NPC firstNPC = null;
            NPC secondNPC = null;
      

        foreach (NPC npc in npcController.deployedAllyList)
        {
            if (npc.gameObject.name.Equals(this.gameObject.name) && this != npc && TIER < 3)
            {
                if (npc.TIER == this.TIER)
                {
                    counter += 1;

                    if (counter == 1 && firstNPC == null)
                    {
                        firstNPC = npc;
                    } else if (counter ==2 && secondNPC == null)
                    {
                        secondNPC = npc;
                    }
                }

                if (counter == 2)
                {
                    if (TIER == 1)
                    {
                        ApplyTier2Upgrades();
                        TIER = 2;
                        firstNPC.RemoveFromBoard(true);
                        secondNPC.RemoveFromBoard(true);
                        this.LevelUpFriendly();
                        return true;
                    }
                    else if (TIER == 2)
                    {
                        ApplyTier3Upgrades();
                        TIER = 3;
                        firstNPC.RemoveFromBoard(true);
                        secondNPC.RemoveFromBoard(true);
                        this.LevelUpFriendly();
                        return true;
                    }
                    else if (TIER == 3)
                    {
                        return false;
                    }

                }
            }

        }
            return false;
    }

    public void TakePureDamage(float dmgToTake)
    {
        if (dmgToTake < 0)
        {
            dmgToTake = 0;
        }
        this.HP -= dmgToTake;
     
    if (HP <= 0)
    {
    this.HP = 0;
    this.RemoveFromBoard(false);
    }


    if (this.HP > this.MAXHP)
    {
    this.HP = this.MAXHP;
    }



    }




    public IEnumerator Live(int attackDistance,DamageSource autoAttack_DamageType,Ability abilityToUse,float actionTime)
    {
        for (; ; )
        {
           
       
            if (boardController.gameStatus.Equals("Fight") && occupyingTile.GetComponent<TileBehaviour>().i != 8) {

                float distance = 0;
                float minDistance = 9000f;

                if (!isEnemy)
                {
                    foreach (NPC npc in npcController.enemyList)
                    {
                        distance = Vector3.Distance(this.transform.position, npc.transform.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            target = npc;
                        }
                    }
                } else
                {
                    foreach (NPC npc in npcController.allyList)
                    {
                        distance = Vector3.Distance(this.transform.position, npc.transform.position);
                        if (distance < minDistance && npc.occupyingTile.GetComponent<TileBehaviour>().i != 8)
                        {
                            minDistance = distance;
                            target = npc;
                        }
                    }
                }

                if (target != null)
                {

                    transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                    distance = Vector3.Distance(this.transform.position, target.transform.position);

                    if (this.ABILITY != Ability.NOTHING && CONCENTRATION >= MAXCONCENTRATION)
                    {
                  //      Debug.Log(this.name + ": Concentration [" + CONCENTRATION + "/" + MAXCONCENTRATION + "] Casting Ability: " + this.ABILITY.ToString());
                       
                        if (this.spellBookController != null && target != null)
                        {
                            spellBookController.CastSpell(this, target, this.ABILITY, this.SPELLPOWER,this.ATTACKPOWER);
                        }
                    }
                    else if (distance < attackDistance)
                    {
                        float damageToDeal;

                        if (this.autoattack_DamageType == DamageSource.PhysicalDamage_AutoAttack) // meleee auto attack
                        {
                            damageToDeal = target.CalculateDamageTaken(this.ATTACKPOWER, this, autoAttack_DamageType);
                            uiController.SpawnFloatingCombatText(target, damageToDeal,DamageSource.PhysicalDamage_AutoAttack,this.isEnemy,HealSource.NOTHING);
                            target.TakePureDamage(damageToDeal);
                        }

                        else if (this.autoattack_DamageType == DamageSource.MagicalDamage_AutoAttack) // magical auto attack
                        {
                            damageToDeal = target.CalculateDamageTaken(ATTACKPOWER, this, DamageSource.MagicalDamage_AutoAttack);
                            GameObject aap = (GameObject)Instantiate(Resources.Load("Auto Attack Projectile"), this.transform.position, Quaternion.identity);
                            aap.GetComponentInChildren<MagicalAutoAttackProjectile>().SetCaster(this);
                            aap.GetComponentInChildren<MagicalAutoAttackProjectile>().SetTarget(target);
                            aap.GetComponentInChildren<MagicalAutoAttackProjectile>().SetDamageToDeal(damageToDeal);

                        }

                        if (animator != null)
                        {
                            animator.Play("Attack");
                        }
                    }
                    else
                    {
                  
                        int deltaI = 0;
                        int deltaJ = 0;

                        if (target.occupyingTile.GetComponent<TileBehaviour>().i < occupyingTile.GetComponent<TileBehaviour>().i)
                        {
                            deltaI = -1;
                        }
                        else if (target.occupyingTile.GetComponent<TileBehaviour>().i > occupyingTile.GetComponent<TileBehaviour>().i)
                        {
                            deltaI = 1;
                        }

                        if (target.occupyingTile.GetComponent<TileBehaviour>().j < occupyingTile.GetComponent<TileBehaviour>().j)
                        {
                            deltaJ = -1;
                        }
                        else if (target.occupyingTile.GetComponent<TileBehaviour>().j > occupyingTile.GetComponent<TileBehaviour>().j)
                        {
                            deltaJ = 1;
                        }
                        moveToEmptyTile(occupyingTile.GetComponent<TileBehaviour>().i + deltaI, occupyingTile.GetComponent<TileBehaviour>().j + deltaJ);
                    }

                }
            }
            yield return new WaitForSeconds(actionTime);
        }
    }
    
}

