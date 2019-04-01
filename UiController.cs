using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UiController : MonoBehaviour
{
    PlayerController playerController;
    BoardController boardController;
    NpcController npcController;
    public PlayerProfiler playerProfiler;
    //
    public GameObject hudCanvas;
    public GameObject hudCanvasTopBar;
    public GameObject hudCanvasBottomBar;
    public GameObject hudCanvasPlayerPanel;
    public GameObject hudCanvasShopPanel;
    public GameObject hudCanvasWizardPanel;
    public GameObject hudCanvasTribesPanel;
    public GameObject hudCanvasCurrentlySelectedUnitPanel;
    public GameObject hudCanvasReportDefeatPanel;
    //
    public Button shopToggleButton;
    public Button tribesToggleButton;
    public Button fightButton;
    public Button shopButton1;
    public Button shopButton2;
    public Button shopButton3;
    public Button shopButton4;
    public Button shopButton5;
    public Button shopButton6;
    public Button shopRefreshButton;
    public Button saveButton;
    public Button loadButton;
    public Button gameOverButton;
    //
    public InputField wizardNewPlayerNameInputField;
    //
    public Image damageOverlay;
    //
    public Text hudCanvasTopPanelPlayerCurrentHPCounterText;
    //
    private Text hudCanvasTopPanelGoldCountText;
    private Text hudCanvasTopPanelUsernameText;
    private Text hudCanvasTopPanelGameStatusText;
    private Text hudCanvasTopPanelCurrentRoundText;
    private Text hudCanvasTopPanelDeployedUnitCountText;
    private Text hudCanvasShopCostToShuffleText;
    //
    public Text shopbutton1_hudCanvasShopCostBuyUnit;
    public Text shopbutton2_hudCanvasShopCostBuyUnit;
    public Text shopbutton3_hudCanvasShopCostBuyUnit;
    public Text shopbutton4_hudCanvasShopCostBuyUnit;
    public Text shopbutton5_hudCanvasShopCostBuyUnit;
    public Text shopbutton6_hudCanvasShopCostBuyUnit;
    //
    public Text hudCanvasTribePanel_WizardCounter;
    public Text hudCanvasTribePanel_WarriorCounter;
    public Text hudCanvasTribePanel_UndeadCounter;
    public Text hudCanvasTribePanel_StructureCounter;
    public Text hudCanvasTribePanel_ElementalCounter;
    public Text hudCanvasTribePanel_BeastCounter;
    public Text hudCanvasTribePanel_AssassinCounter;
    public Text hudCanvasTribePanel_GuardianCounter;
    //
    public Text wizard_playerName;
    public Text wizard_playerMMR;
    public Button startGameButton;
    //
    public Text selectedUnitPanel_InformationText_NAME;
    public Text selectedUnitPanel_InformationText_TIER;
    public Text selectedUnitPanel_InformationText_HP;
    public Text selectedUnitPanel_InformationText_CONCENTRATION;
    public Text selectedUnitPanel_InformationText_ARMORANDRETALIATION;
    public Text selectedUnitPanel_InformationText_ATTACKPOWER;
    public Text selectedUnitPanel_InformationText_SPELLPOWER;
    public DynamicTribeIconVisualizer selectedUnitPanel_primaryTribeIconVisualizer;
    public DynamicTribeIconVisualizer selectedUnitPanel_secondaryTribeIconVisualizer;
    public DynamicAbilityIconVisualizer selectedUnitPanel_abilityiconVisualizer;
    //
    public Button resetSelectedTargetButton;
    public Button sellFriendlySelectedTargetButton;
    //
    public Text shopMouseOverInfoText_NAME;
    public Text shopMouseOverInfoText_PRIMARYTRIBE;
    public Text shopMouseOverInfoText_SECONDARYTRIBE;
    public DynamicTribeIconVisualizer shopMouseOverInfoIcon_PRIMARYTRIBE_tribeIconVisualizer;
    public DynamicTribeIconVisualizer shopMouseOverInfoIcon_SECONDARYTRIBE_tribeIconVisualizer;
    //
    public Text reportdefeatPanel_reachedRoundText;
    public Text reportdefeatPanel_recordReachedRoundText;
    public Text reportdefeatPanel_mmrChange;
    public Text reportdefeatPanel_recordAchievedMMRText;
    public Text reportdefeatPanel_userNameText;
    public Text reportdefeatPanel_userMMR;
    //
    public AudioSource hudCanvasAudioSource;
    public AudioClip startGameAudioClip;
    public AudioClip shopRefreshAudioClip;
    public AudioClip shopOpenAudioClip;
    public AudioClip shopClosedAudioClip;
    public AudioClip fightStartAudioClip;
    public AudioClip genericButtonSucessAudioClip;
    public AudioClip genericButtonFailureAudioClip;
    public AudioClip buyUnitSuccessAudioClip;
    public AudioClip chessUnitReleaseAudioClip;
    public AudioClip chessUnitclickAudioClip;
    //

    private void Awake()
    {
        boardController = GetComponent<BoardController>();
        playerController = GetComponent<PlayerController>();
        npcController = GetComponent<NpcController>();
        playerProfiler = GetComponent<PlayerProfiler>();

        hudCanvas = GameObject.Find("HUDCanvas");
        hudCanvasTopBar = GameObject.Find("HUDCanvas/Top Bar");
        hudCanvasBottomBar = GameObject.Find("HUDCanvas/Bottom Bar");
        hudCanvasPlayerPanel = GameObject.Find("HUDCanvas/Player Panel");
        hudCanvasShopPanel = GameObject.Find("HUDCanvas/Player Panel/Shop Panel");
        hudCanvasTribesPanel = GameObject.Find("HUDCanvas/Player Panel/Tribes Panel");
        hudCanvasCurrentlySelectedUnitPanel = GameObject.Find("HUDCanvas/Player Panel/Currently Selected Unit Information Panel");
        hudCanvasTopPanelGoldCountText = GameObject.Find("HUDCanvas/Top Bar/Top Panel/Gold Count Text").GetComponent<Text>();
        hudCanvasTopPanelUsernameText = GameObject.Find("HUDCanvas/Top Bar/Top Panel/Username Text").GetComponent<Text>();
        hudCanvasTopPanelGameStatusText = GameObject.Find("HUDCanvas/Top Bar/Top Panel/Status Text").GetComponent<Text>();
        hudCanvasTopPanelCurrentRoundText = GameObject.Find("HUDCanvas/Top Bar/Top Panel/Round Text").GetComponent<Text>();
        hudCanvasTopPanelDeployedUnitCountText = GameObject.Find("HUDCanvas/Top Bar/Top Panel/Deployed Unit Count Text").GetComponent<Text>();
        hudCanvasShopCostToShuffleText = GameObject.Find("HUDCanvas/Player Panel/Shop Panel/Refresh Button/Gold Cost Text").GetComponent<Text>();
        gameOverButton.onClick.RemoveAllListeners();
        gameOverButton.onClick.AddListener(delegate { boardController.ChangeGameStatus("game over"); });

        resetSelectedTargetButton.onClick.RemoveAllListeners();
        resetSelectedTargetButton.onClick.AddListener(delegate { ResetSelectedTarget(); });

        sellFriendlySelectedTargetButton.onClick.RemoveAllListeners();
        sellFriendlySelectedTargetButton.onClick.AddListener(delegate { SellFriendlySelectedTarget(); });

        startGameButton.onClick.RemoveAllListeners();
        startGameButton.onClick.AddListener(delegate { StartGameTransitionPhase(); });

        saveButton.onClick.RemoveAllListeners();
        saveButton.onClick.AddListener(delegate { ChangeNameAndSaveToFile(); });

        loadButton.onClick.RemoveAllListeners();
        loadButton.onClick.AddListener(delegate { LoadFromSaveFile(); });

        shopToggleButton.onClick.RemoveAllListeners();
        shopToggleButton.onClick.AddListener(delegate { ToggleShopPanel(); });


        tribesToggleButton.onClick.RemoveAllListeners();
        tribesToggleButton.onClick.AddListener(delegate { ToggleTribePanel(); });

        fightButton.onClick.RemoveAllListeners();
        fightButton.onClick.AddListener(delegate { TryTransitionToFightPhase(); });

        shopButton1.onClick.RemoveAllListeners();
        shopButton1.onClick.AddListener(delegate { ShopButton1Clicked(); });

        shopButton2.onClick.RemoveAllListeners();
        shopButton2.onClick.AddListener(delegate { ShopButton2Clicked(); });

        shopButton3.onClick.RemoveAllListeners();
        shopButton3.onClick.AddListener(delegate { ShopButton3Clicked(); });

        shopButton4.onClick.RemoveAllListeners();
        shopButton4.onClick.AddListener(delegate { ShopButton4Clicked(); });

        shopButton5.onClick.RemoveAllListeners();
        shopButton5.onClick.AddListener(delegate { ShopButton5Clicked(); });

        shopButton6.onClick.RemoveAllListeners();
        shopButton6.onClick.AddListener(delegate { ShopButton6Clicked(); });

        shopRefreshButton.onClick.RemoveAllListeners();
        shopRefreshButton.onClick.AddListener(delegate { RefreshShop(); });

    }

    public void SellFriendlySelectedTarget() // sell the friendly selected target npc
    {
        if (npcController.allyList.Count > 1 && boardController.selectedObject != null && boardController.selectedObject.GetComponent<NPC>().isEnemy == false)
        {
            int goldReward;
            playerController.NPC_COST_DATA.TryGetValue(boardController.selectedObject.GetComponent<NPC>().UNIT_TYPE, out goldReward); // fetch the gold reward for the unit type
            if (boardController.selectedObject.GetComponent<NPC>().TIER == 2)
            {
                goldReward = (int)(goldReward * 2f); // gold bonus for selling a tier 2 unit
            }
            else if (boardController.selectedObject.GetComponent<NPC>().TIER == 3)
            {
                goldReward = (int)(goldReward * 8f); // gold bonus for selling a tier 3 unit
            }
            boardController.selectedObject.GetComponent<NPC>().RemoveFromBoard(true); // remove the sold npc from the board 
            playerController.SetPlayerGoldCount(playerController.playerGoldCount + (long)goldReward); // apply the gold reward to the player
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        } else
        {
            hudCanvasAudioSource.PlayOneShot(genericButtonFailureAudioClip);
        }
    }

    public void ResetSelectedTarget() // reset the selected target
    {
        boardController.selectedObject = null; // null it
    }

    void Update()
    {

        if (boardController.gameStatus != "report defeat" && boardController.selectedObject != null) // update the entire selected unit panel
        {
            hudCanvasCurrentlySelectedUnitPanel.SetActive(true);
            GameObject selectedObject = boardController.selectedObject;
            NPC selectedNPC = selectedObject.GetComponent<NPC>();
            string friendOrFoe = selectedNPC.isEnemy ? "Enemy " : "";
            selectedUnitPanel_InformationText_NAME.text = friendOrFoe + selectedObject.name;
            selectedUnitPanel_InformationText_ARMORANDRETALIATION.text = "Armor: " + selectedNPC.ARMOR + " / Retaliation: " + selectedNPC.RETALIATION;
            selectedUnitPanel_InformationText_ATTACKPOWER.text = "AP: " + selectedNPC.ATTACKPOWER;
            selectedUnitPanel_InformationText_CONCENTRATION.text = "CONCENTRATION: " + selectedNPC.CONCENTRATION + "/" + selectedNPC.MAXCONCENTRATION;
            selectedUnitPanel_InformationText_HP.text = "HP: " + selectedNPC.HP + "/" + selectedNPC.MAXHP;
            selectedUnitPanel_InformationText_SPELLPOWER.text = "SP: " + selectedNPC.SPELLPOWER;
            selectedUnitPanel_InformationText_TIER.text = "Level " + selectedNPC.TIER;
            selectedUnitPanel_primaryTribeIconVisualizer.SetImage(selectedNPC.PRIMARYTRIBE);
            selectedUnitPanel_secondaryTribeIconVisualizer.SetImage(selectedNPC.SECONDARYTRIBE);
            selectedUnitPanel_abilityiconVisualizer.SetImage(selectedNPC.ABILITY);
            sellFriendlySelectedTargetButton.gameObject.SetActive(!selectedNPC.isEnemy);

        }
        else
        {
            hudCanvasCurrentlySelectedUnitPanel.SetActive(false);

        }

        if (!boardController.gameStatus.Equals("shopping")) // take out ui elements outside that should not be outside of the shopping phase
        {
            shopToggleButton.gameObject.SetActive(false);
            hudCanvasShopPanel.SetActive(false);
            sellFriendlySelectedTargetButton.gameObject.SetActive(false);
            fightButton.gameObject.SetActive(false);
        } else
        {
            fightButton.gameObject.SetActive(true);
        }

    }

    public void ChangeNameAndSaveToFile()
    {
        string newName = wizardNewPlayerNameInputField.text;  // fetch name from input field
        hudCanvasAudioSource.PlayOneShot(genericButtonSucessAudioClip);

        if (newName != "" && newName != playerController.playerName) // check for valid name
        {
            playerController.playerName = newName; // update player name
            SaveToSaveFile(); // save to save file with new name
            wizardNewPlayerNameInputField.text = ""; // reset the input field
        }


    }

    public void StartGameTransitionPhase() // set up the ui for starting the game
    {
        LoadFromSaveFile(); // load from save file
        if (playerController.playerName != "") // dont allow to start game with an empty named save file
        {
            hudCanvasAudioSource.PlayOneShot(startGameAudioClip);
            if (wizardNewPlayerNameInputField.text != "")
            {
                ChangeNameAndSaveToFile(); // new name has been inputted so lets save anew to file
            }
            boardController.ChangeGameStatus("shopping", "Shopping Phase"); // set to shopping phae
            ChangeCurrentRoundDisplayText(boardController.currentGameRound); // update ui text element
            boardController.StartCoroutine("ProgressHealthRegeneration"); // begin health regeneration process
            boardController.StartCoroutine("ProgressConcentrationRegeneration"); // begin concentration regeneration process
            hudCanvasPlayerPanel.SetActive(true); // activate required ui elements
            hudCanvasShopPanel.SetActive(true);
            hudCanvasBottomBar.SetActive(true);
            shopToggleButton.gameObject.SetActive(true);
            hudCanvasTopBar.SetActive(true);
            hudCanvasWizardPanel.SetActive(false);
           

        }
        else
        {
            hudCanvasAudioSource.PlayOneShot(genericButtonFailureAudioClip);
        }


    }

    public void UpdateReportDefeatPanelScreen(int mmrChange)
    {
        reportdefeatPanel_mmrChange.text = "MMR Change: " + mmrChange;
        PlayerProfileSave pps = playerProfiler.LoadProfile(0);
        reportdefeatPanel_reachedRoundText.text = "Reached Round: " + pps.achievedRound;
        reportdefeatPanel_recordAchievedMMRText.text = "Record Max MMR: " + pps.highestAchievedMmr;
        reportdefeatPanel_recordReachedRoundText.text = "Record Reached Round: " + pps.highestAchievedRound;
        reportdefeatPanel_userMMR.text = "MMR: " + pps.mmr;
        reportdefeatPanel_userNameText.text = "" + pps.characterName;

    }

    public void LoadFromSaveFile()
    {
        PlayerProfileSave pps = playerProfiler.LoadProfile(0); // load profile from slot 0
        playerController.LoadProfileFromSave(pps); // load players profile
        ChangeCurrentPlayerUsernameDisplayText(playerController.playerName, playerController.playerMMR.ToString()); // update username display text
        wizard_playerName.text = playerController.playerName; // update username display text 
        wizard_playerMMR.text = "MMR: " + playerController.playerMMR.ToString(); // update mmr display text 

        if (playerController.playerMMR <= 0) // reset the user mmr if its negative or zero
        {
            playerController.playerMMR = 1500;
        }
    }

    public void SaveToSaveFile()
    {
        PlayerProfileSave pps = new PlayerProfileSave(); // create new save file
        pps.characterName = playerController.playerName; // set the save file parameters
        pps.mmr = playerController.playerMMR;
        pps.rank = "queen";
        pps.UserIconImageName = "polarbear";
        pps.achievedRound = boardController.currentGameRound;
        playerProfiler.SaveProfile(pps, 0); // save profile to slot 0
        LoadFromSaveFile(); // load the new profile in
    }

    public void UpdateAllShopButtons() // update all the shop buttons with the required information
    {

        hudCanvasShopPanel.SetActive(true);

        var gameObjects = GameObject.FindGameObjectsWithTag("thumbnail");
        int unitCost;
        GameObject spawned;

        foreach (GameObject go in gameObjects)
        {
            Destroy(go);
        }

        shopButton1.interactable = true;
        shopButton1.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI board Large  parchment");
        shopButton1.GetComponent<Thubmnail>().SourcePrefab = Resources.Load<GameObject>("3d_thumbnail_" + playerController.shoppingOptions[0]);
        playerController.NPC_COST_DATA.TryGetValue(playerController.shoppingOptions[0], out unitCost);
        shopbutton1_hudCanvasShopCostBuyUnit.text = unitCost.ToString();
        spawned = Instantiate(shopButton1.GetComponent<Thubmnail>().SourcePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        spawned.transform.SetParent(shopButton1.transform);
        spawned.transform.position = shopButton1.transform.position;
        spawned.transform.rotation = Camera.main.transform.rotation;
        spawned.transform.Rotate(0f, 180f, 0f, Space.Self);

        shopButton2.interactable = true;
        shopButton2.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI board Large  parchment");

        shopButton2.GetComponent<Thubmnail>().SourcePrefab = Resources.Load<GameObject>("3d_thumbnail_" + playerController.shoppingOptions[1]);
        playerController.NPC_COST_DATA.TryGetValue(playerController.shoppingOptions[1], out unitCost);
        shopbutton2_hudCanvasShopCostBuyUnit.text = unitCost.ToString();
        spawned = Instantiate(shopButton2.GetComponent<Thubmnail>().SourcePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        spawned.transform.SetParent(shopButton2.transform);
        spawned.transform.position = shopButton2.transform.position;
        spawned.transform.rotation = Camera.main.transform.rotation;
        spawned.transform.Rotate(0f, 180f, 0f, Space.Self);

        shopButton3.interactable = true;
        shopButton3.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI board Large  parchment");

        shopButton3.GetComponent<Thubmnail>().SourcePrefab = Resources.Load<GameObject>("3d_thumbnail_" + playerController.shoppingOptions[2]);
        playerController.NPC_COST_DATA.TryGetValue(playerController.shoppingOptions[2], out unitCost);
        shopbutton3_hudCanvasShopCostBuyUnit.text = unitCost.ToString();
        spawned = Instantiate(shopButton3.GetComponent<Thubmnail>().SourcePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        spawned.transform.SetParent(shopButton3.transform);
        spawned.transform.position = shopButton3.transform.position;
        spawned.transform.rotation = Camera.main.transform.rotation;
        spawned.transform.Rotate(0f, 180f, 0f, Space.Self);

        shopButton4.interactable = true;
        shopButton4.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI board Large  parchment");

        shopButton4.GetComponent<Thubmnail>().SourcePrefab = Resources.Load<GameObject>("3d_thumbnail_" + playerController.shoppingOptions[3]);
        playerController.NPC_COST_DATA.TryGetValue(playerController.shoppingOptions[3], out unitCost);
        shopbutton4_hudCanvasShopCostBuyUnit.text = unitCost.ToString();
        spawned = Instantiate(shopButton4.GetComponent<Thubmnail>().SourcePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        spawned.transform.SetParent(shopButton4.transform);
        spawned.transform.position = shopButton4.transform.position;
        spawned.transform.rotation = Camera.main.transform.rotation;
        spawned.transform.Rotate(0f, 180f, 0f, Space.Self);

        shopButton5.interactable = true;
        shopButton5.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI board Large  parchment");

        shopButton5.GetComponent<Thubmnail>().SourcePrefab = Resources.Load<GameObject>("3d_thumbnail_" + playerController.shoppingOptions[4]);
        playerController.NPC_COST_DATA.TryGetValue(playerController.shoppingOptions[4], out unitCost);
        shopbutton5_hudCanvasShopCostBuyUnit.text = unitCost.ToString();
        spawned = Instantiate(shopButton5.GetComponent<Thubmnail>().SourcePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        spawned.transform.SetParent(shopButton5.transform);
        spawned.transform.position = shopButton5.transform.position;
        spawned.transform.rotation = Camera.main.transform.rotation;
        spawned.transform.Rotate(0f, 180f, 0f, Space.Self);

        shopButton6.interactable = true;
        shopButton6.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI board Large  parchment");

        shopButton6.GetComponent<Thubmnail>().SourcePrefab = Resources.Load<GameObject>("3d_thumbnail_" + playerController.shoppingOptions[5]);
        playerController.NPC_COST_DATA.TryGetValue(playerController.shoppingOptions[5], out unitCost);
        shopbutton6_hudCanvasShopCostBuyUnit.text = unitCost.ToString();
        spawned = Instantiate(shopButton6.GetComponent<Thubmnail>().SourcePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        spawned.transform.SetParent(shopButton6.transform);
        spawned.transform.position = shopButton6.transform.position;
        spawned.transform.rotation = Camera.main.transform.rotation;
        spawned.transform.Rotate(0f, 180f, 0f, Space.Self);
    }

    void RefreshShop()
    {
        playerController.ShuffleNewShopingOptions(false);
    }

    void ShopButton1Clicked()
    {
        if (playerController.buyUnitFromShop(playerController.shoppingOptions[0]))
        {
            shopButton1.interactable = false;
            shopButton1.GetComponent<Image>().sprite = Resources.Load<Sprite>("bought unit icon");
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        }
    }

    void ShopButton2Clicked()
    {
        if (playerController.buyUnitFromShop(playerController.shoppingOptions[1]))
        {
            shopButton2.interactable = false;
            shopButton2.GetComponent<Image>().sprite = Resources.Load<Sprite>("bought unit icon");
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        }
    }

    void ShopButton3Clicked()
    {
        if (playerController.buyUnitFromShop(playerController.shoppingOptions[2]))
        {
            shopButton3.interactable = false;
            shopButton3.GetComponent<Image>().sprite = Resources.Load<Sprite>("bought unit icon");
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        }
 
    }

    void ShopButton4Clicked()
    {
        if (playerController.buyUnitFromShop(playerController.shoppingOptions[3]))

        {
            shopButton4.interactable = false;
            shopButton4.GetComponent<Image>().sprite = Resources.Load<Sprite>("bought unit icon");
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        }

    } 

    void ShopButton5Clicked()
    {
        if (playerController.buyUnitFromShop(playerController.shoppingOptions[4]))
        {
            shopButton5.interactable = false;
            shopButton5.GetComponent<Image>().sprite = Resources.Load<Sprite>("bought unit icon");
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        }
    }

    void ShopButton6Clicked()
    {
        if (playerController.buyUnitFromShop(playerController.shoppingOptions[5]))
        {
            shopButton6.interactable = false;
            shopButton6.GetComponent<Image>().sprite = Resources.Load<Sprite>("bought unit icon");
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        }


    }

    void ToggleShopPanel()
    {
        
        if ( hudCanvasShopPanel.activeSelf) // check if the shop panel is active
        {
          
            hudCanvasAudioSource.PlayOneShot(shopClosedAudioClip); // play the shop closing sound
        }
        else
        {
            hudCanvasAudioSource.PlayOneShot(shopOpenAudioClip); // play the shop opening sound

        }
        hudCanvasShopPanel.SetActive(!hudCanvasShopPanel.activeSelf); //toggle the active state
       
    }

    void ToggleTribePanel()
    {
        hudCanvasAudioSource.PlayOneShot(genericButtonSucessAudioClip);
        hudCanvasTribesPanel.SetActive(!hudCanvasTribesPanel.activeSelf);
    }

    IEnumerator SmoothRoundFightPhaseTransition()
    {

        yield return new WaitForSeconds(2);
        if (boardController.gameStatus != "report defeat"){
        boardController.ChangeGameStatus("Fight");

        }
    }

    void TryTransitionToFightPhase()
    {

        if (boardController.gameStatus != "Fight" && boardController.gameStatus != "report defeat" && boardController.gameStatus == "shopping" && playerController.currentlyDeployedUnits > 0) // check if we are ready to proceed to combat
        {
            boardController.ChangeGameStatus("Wait"); // smooth wait transition
            npcController.allyListBackup = new List<NPC>();
            boardController.SpawnEnemyUnitsRound_Balanced(); // spawn balanced enemies
            for (int i = npcController.deployedAllyList.Count - 1; i >= 0; i--) // copy the human player unit deployment so we can restore it after combat
            {
                if (npcController.deployedAllyList[i] != null)
                {
                    npcController.deployedAllyList[i].enabled = false;
                    GameObject obj = Instantiate(npcController.deployedAllyList[i].transform.parent.gameObject); // make a copy of our units and store them out of sight so we can restore them after combat
                    npcController.allyListBackup.Add(obj.GetComponentInChildren<NPC>());
                    obj.transform.position = Vector3.up * 1000;
                    obj.name = npcController.deployedAllyList[i].transform.parent.gameObject.name;
                    npcController.deployedAllyList[i].enabled = true;

                }
            }
            hudCanvasAudioSource.PlayOneShot(fightStartAudioClip);
            StartCoroutine(SmoothRoundFightPhaseTransition()); // finally we can transition to combat phase.
        }
        else
        {
            hudCanvasAudioSource.PlayOneShot(genericButtonFailureAudioClip);
        }

    }

    public void SpawnFloatingCombatText(NPC TargetDestinationNPC,DamageReport dmgReport,DisplayMode displayMode)
    {
        if (TargetDestinationNPC != null)
        {
            GameObject fct = (GameObject)Instantiate(Resources.Load("Floating Combat Text"), TargetDestinationNPC.transform.position, Quaternion.identity);
            FloatingCombatText floatingCombatTextScript = fct.GetComponentInChildren<FloatingCombatText>();
            floatingCombatTextScript.displayMode = displayMode;
            floatingCombatTextScript.dmgReport = dmgReport;
            UnityEngine.Object.Destroy(fct, 1f);
        }
    }

    public void RefreshDeployedTribesCounter()
    {

        int count = 0;

        playerController.deployedTribesCounter.TryGetValue(Tribe.Wizard, out count);
        hudCanvasTribePanel_WizardCounter.text = count.ToString();

        playerController.deployedTribesCounter.TryGetValue(Tribe.Warrior, out count);
        hudCanvasTribePanel_WarriorCounter.text = count.ToString(); ;

        playerController.deployedTribesCounter.TryGetValue(Tribe.Undead, out count);
        hudCanvasTribePanel_UndeadCounter.text = count.ToString(); ;

        playerController.deployedTribesCounter.TryGetValue(Tribe.Structure, out count);
        hudCanvasTribePanel_StructureCounter.text = count.ToString(); ;

        playerController.deployedTribesCounter.TryGetValue(Tribe.Elemental, out count);
        hudCanvasTribePanel_ElementalCounter.text = count.ToString(); ;

        playerController.deployedTribesCounter.TryGetValue(Tribe.Beast, out count);
        hudCanvasTribePanel_BeastCounter.text = count.ToString(); ;

        playerController.deployedTribesCounter.TryGetValue(Tribe.Assassin, out count);
        hudCanvasTribePanel_AssassinCounter.text = count.ToString(); ;

        playerController.deployedTribesCounter.TryGetValue(Tribe.Guardian, out count);
        hudCanvasTribePanel_GuardianCounter.text = count.ToString(); ;

    }

    public void ChangeDeployedUnitCountDisplayText(int _currentlyDeployedUnits, int _maxDeployedUnitsLimit)
    {
        hudCanvasTopPanelDeployedUnitCountText.text = _currentlyDeployedUnits + "/" + _maxDeployedUnitsLimit;
    }

    public void ChangeCurrentRoundDisplayText(int _roundCounter)
    {
        if (hudCanvasTopPanelCurrentRoundText != null)
        {
            hudCanvasTopPanelCurrentRoundText.text = "Round " + _roundCounter;
        }
    }

    public void ChangeHPPlayerDisplayText(int _currentHP, int _maxHP)
    {
        hudCanvasTopPanelPlayerCurrentHPCounterText.text = _currentHP.ToString() + "/" + _maxHP.ToString();
    }

    public IEnumerator FadeOut(float cycleTime) //fade out the damage overlay
    {
        yield return new WaitForSeconds(cycleTime);
        Color fixedColor = damageOverlay.color;
        fixedColor.a = 1;
        damageOverlay.color = fixedColor;
        damageOverlay.CrossFadeAlpha(1, 0f, true);
        damageOverlay.CrossFadeAlpha(0f, cycleTime, false);
    }

    public void DamageOveray_Animation_CycleFadeInFadeOut(float cycleTime) // begin the fade in fade out cycle of the damage overlay
    {
        Color fixedColor = damageOverlay.color;
        fixedColor.a = 1;
        damageOverlay.color = fixedColor;
        damageOverlay.CrossFadeAlpha(0f, 0f, true);
        damageOverlay.CrossFadeAlpha(1, cycleTime, false);
        StartCoroutine(FadeOut(cycleTime));

    }

    public void ChangeGameStatusDisplayText(string str)
    {
        if (hudCanvasTopPanelGameStatusText != null)
        {
            hudCanvasTopPanelGameStatusText.text = str;
        }

    }

    public void ChangeCostToShuffleShopDisplayText(string str)
    {
        hudCanvasShopCostToShuffleText.text = str;
    }

    public void ChangeCurrentPlayerGoldCountDisplayText(string str)
    {
        hudCanvasTopPanelGoldCountText.text = str;
    }

    public void ChangeCurrentPlayerUsernameDisplayText(string nameString, string mmrString)
    {
        hudCanvasTopPanelUsernameText.text = nameString + "(" + mmrString + ")";
    }
}
