using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Steamworks;

public class UiController : MonoBehaviour
{
    PlayerController playerController;
    BoardController boardController;
    ObjectPoolController objectPoolController;
    NpcController npcController;
    SessionLogger sessionLogger;
    DialogueManager dialogueManager;
    public GameObject hudCanvas;
    public GameObject hudCanvasTopBar;
    public GameObject hudCanvasBottomBar;
    public GameObject hudCanvasPlayerPanel;
    public GameObject hudCanvasShopPanel;
    public GameObject hudCanvasWizardPanel;
    public GameObject hudCanvasTribesPanel;
    public GameObject hudCanvasCurrentlySelectedUnitPanel;
    public GameObject hudCanvasReportDefeatPanel;
    public GameObject hudCanvasEscapePanel;
    public GameObject hudCanvasreportVictoryPanel;
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
    public Button shopUnitCapButton;
    public Button loadButton;
    public Button mainMenuButton;
    public Button forfeitButton;
    public Button closeEscapeMenuTabButton;
    public Image damageOverlay;
    public Text hudCanvasTopPanelPlayerCurrentHPCounterText;
    public Text hudCanvasTopPanelGoldCountText;
    public Text hudCanvasTopPanelUsernameText;
    public Text hudCanvasTopPanelGameStatusText;
    public Text hudCanvasTopPanelCurrentRoundText;
    public Text hudCanvasTopPanelDeployedUnitCountText;
    public Text hudCanvasShopCostToShuffleText;
    public Text hudCanvasShopUnitCapUpgradeText;
    public Image hudCanvasTopPanelSteamUserAvatar;
    public Text shopbutton1_hudCanvasShopCostBuyUnit;
    public Text shopbutton2_hudCanvasShopCostBuyUnit;
    public Text shopbutton3_hudCanvasShopCostBuyUnit;
    public Text shopbutton4_hudCanvasShopCostBuyUnit;
    public Text shopbutton5_hudCanvasShopCostBuyUnit;
    public Text shopbutton6_hudCanvasShopCostBuyUnit;
    public Text hudCanvasTribePanel_WizardCounter;
    public Text hudCanvasTribePanel_WarriorCounter;
    public Text hudCanvasTribePanel_UndeadCounter;
    public Text hudCanvasTribePanel_StructureCounter;
    public Text hudCanvasTribePanel_ElementalCounter;
    public Text hudCanvasTribePanel_BeastCounter;
    public Text hudCanvasTribePanel_AssassinCounter;
    public Text hudCanvasTribePanel_GuardianCounter;
    public Text intro_playerName;
    public Text intro_playerMMR;
    public Button intro_SettingsButton;
    public Dropdown wizard_difficultyPicker;
    public Button startGameButton;
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
    public Button resetSelectedTargetButton;
    public Button sellFriendlySelectedTargetButton;
    public Text shopMouseOverInfoText_NAME;
    public Text shopMouseOverInfoText_PRIMARYTRIBE;
    public Text shopMouseOverInfoText_SECONDARYTRIBE;
    public DynamicTribeIconVisualizer shopMouseOverInfoIcon_PRIMARYTRIBE_tribeIconVisualizer;
    public DynamicTribeIconVisualizer shopMouseOverInfoIcon_SECONDARYTRIBE_tribeIconVisualizer;
    public AudioListener mainAudioListener;
    public AudioSource hudCanvasAudioSource;
    public AudioClip startGameAudioClip;
    public AudioClip shopRefreshAudioClip;
    public AudioClip shopOpenAudioClip;
    public AudioClip shopClosedAudioClip;
    public AudioClip fightStartAudioClip;
    public AudioClip genericSucessAudioClip;
    public AudioClip genericButtonFailureAudioClip;
    public AudioClip buyUnitSuccessAudioClip;
    public AudioClip chessUnitReleaseAudioClip;
    public AudioClip chessUnitclickAudioClip;
    public AudioClip levelUpAudioClip;
    public Button muteAudioToggleButton;
    public Image audioMutedIndicatorIcon;
    public bool audioIsMuted;
    public float userVolume;
    public Slider volumeSlider;
    public GameObject escapeMenuForfeitConfirmationPromptPanel;
    public Button settingsButton;
    public Button exitGameButton;
    public Dropdown cameraModePicker;
    public Button CreditsButton;
    public GameObject creditsPanel;
    public GameObject helpPanel;
    public Button helpButton;
    public Button closeHelpPanelButton;
    public HelperTipContainer helperTipContainer;
    public Text helperText;
    public Button nextTipButton;
    public Button previousTipButton;
    public Text currentTipCounterText;
    public Toggle randomTipToggler;
    public Camera shopCam1;
    public Camera shopCam2;
    public Camera shopCam3;
    public Camera shopCam4;
    public Camera shopCam5;
    public Camera shopCam6;
    public Text reportDefeatPanel_TotalUnitsDeployedText;
    public Text reportDefeatPanel_MostTribesDeployedText;
    public DynamicTribeIconVisualizer reportDefeatPanel_MostTribeDeployedIconVisualizer;
    public Text reportVictoryPanel_TotalUnitsDeployedText;
    public Text reportVictoryPanel_MostTribesDeployedText;
    public DynamicTribeIconVisualizer reportVictoryPanel_MostTribeDeployedIconVisualizer;
    public GameObject ShopPanelTooltipSubPanel;
    public Camera mainCamera;
    public Image steamUserAvatar;
    public Leaderboard steamLeaderboard;
    public bool fetchSteamLeaderboardEntry = true;
    public bool fetchSteamLeaderboardTop100Entries = true;
    public GameObject LeaderboardEntriesContent;
    public GameObject dialoguePanel;
    public Text dialoguePanelSpeakerNameText;
    public Text dialoguePanelDialogueText;
    public Button continueDialogueButton;
    public DialogueTriggerSystem dialogueTriggerSystem;
    public GameObject leaderBoardPanel;
    public List<LeaderboardEntry> steamLeaderBoardTop100EntriesUIPrefabsList;
    public GameObject combatTimerPanel;
    public Text combatTimerPanelText;
    public Text versionText;
    public GameObject MousedOverSelectedItemTooltipPanel;
    public Text FocusedItemTooltipTextName;
    public Text FocusedItemTooltipTextStats;
    public GameObject CurrentlySelectedUnitInventoryPanel;
    public Image InventorySlot1Panel;
    public Image InventorySlot2Panel;
    public Image InventorySlot3Panel;
    public Image InventorySlot4Panel;
    public Item EmptyItem;
    private Vector3 tooltipOffsetVector;
    private int xOffsetTooltip = 800;
    private int yOffsetTooltip = 50;
    public CombatLogger combatLogger;

    private void Awake()
    {
        GetWorldControllers();
        BindButtons();
        steamUserAvatar.sprite = Sprite.Create(GetSteamAvatar(),new Rect(new Vector2(0,0),new Vector2(64,64)),new Vector2(64, 64));
        hudCanvasTopPanelSteamUserAvatar.sprite = Sprite.Create(GetSteamAvatar(), new Rect(new Vector2(0, 0), new Vector2(64, 64)), new Vector2(64, 64));
        helperTipContainer = GetComponent<HelperTipContainer>();
        RefreshHelperTips();
        AudioListener.volume = userVolume;
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            intro_playerName.text = name;
            hudCanvasTopPanelUsernameText.text = name;
            versionText.text = "Steam Build: " +  SteamApps.GetAppBuildId().ToString();
        }
    

        steamLeaderBoardTop100EntriesUIPrefabsList = new List<LeaderboardEntry>();
        for (int i = 0; i < 100; i++) // prepare the leaderboard top 10 entry display prefab objects
        {
           GameObject leaderBoardEntry = Instantiate(Resources.Load("Leaderboard Entry"), LeaderboardEntriesContent.transform,false) as GameObject;
            int rank = 1 + i;
            leaderBoardEntry.GetComponent<LeaderboardEntry>().rank = rank;
            leaderBoardEntry.GetComponent<LeaderboardEntry>().rankText.text = rank + ")";
            steamLeaderBoardTop100EntriesUIPrefabsList.Add(leaderBoardEntry.GetComponent<LeaderboardEntry>());

        }

        EmptyItem = new Item(ItemName.NO_ITEM);
        tooltipOffsetVector = new Vector3(xOffsetTooltip, yOffsetTooltip, 0);
    }

    public void GetWorldControllers()
    {
        boardController = GetComponent<BoardController>();
        playerController = GetComponent<PlayerController>();
        npcController = GetComponent<NpcController>();
        sessionLogger = GetComponent<SessionLogger>();
        combatLogger = GetComponent<CombatLogger>();
        dialogueManager = GetComponent<DialogueManager>();
        objectPoolController = GetComponent<ObjectPoolController>();
        hudCanvas = GameObject.Find("HUDCanvas");

    }

    public Texture2D GetSteamAvatar()
    {
        CSteamID user = SteamUser.GetSteamID();
        int FriendAvatar = SteamFriends.GetMediumFriendAvatar(user);
        uint ImageWidth;
        uint ImageHeight;
        bool success = SteamUtils.GetImageSize(FriendAvatar, out ImageWidth, out ImageHeight);

        if (success && ImageWidth > 0 && ImageHeight > 0)
        {
            byte[] Image = new byte[ImageWidth * ImageHeight * 4];
            Texture2D returnTexture = new Texture2D((int)ImageWidth, (int)ImageHeight, TextureFormat.RGBA32, false, true);
            success = SteamUtils.GetImageRGBA(FriendAvatar, Image, (int)(ImageWidth * ImageHeight * 4));
            
            if (success)
            {
                returnTexture.LoadRawTextureData(Image);
                returnTexture.Apply();
            }
            return returnTexture;
        }
        else
        {
            Debug.LogError("Couldn't get avatar.");
            return new Texture2D(0, 0);
        }
    }

    public void BindButtons()
    {
        previousTipButton.onClick.RemoveAllListeners();
        previousTipButton.onClick.AddListener(delegate { PreviousTip(); });

        nextTipButton.onClick.RemoveAllListeners();
        nextTipButton.onClick.AddListener(delegate { NextTip(); });

        mainMenuButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.AddListener(delegate { boardController.TransitionToGameOverPhase(); });

        closeHelpPanelButton.onClick.RemoveAllListeners();
        closeHelpPanelButton.onClick.AddListener(delegate { ToggleHelpPanel(); });

        helpButton.onClick.RemoveAllListeners();
        helpButton.onClick.AddListener(delegate { ToggleHelpPanel(); });

        CreditsButton.onClick.RemoveAllListeners();
        CreditsButton.onClick.AddListener(delegate { ToggleCreditsPanel(); });

        cameraModePicker.onValueChanged.RemoveAllListeners();
        cameraModePicker.onValueChanged.AddListener(delegate { PickCameraMode(); });

        exitGameButton.onClick.RemoveAllListeners();
        exitGameButton.onClick.AddListener(delegate { ExitGame(); });

        intro_SettingsButton.onClick.RemoveAllListeners();
        intro_SettingsButton.onClick.AddListener(delegate { toggleEscapeMenu(); });

        settingsButton.onClick.RemoveAllListeners();
        settingsButton.onClick.AddListener(delegate { toggleEscapeMenu(); });

        muteAudioToggleButton.onClick.RemoveAllListeners();
        muteAudioToggleButton.onClick.AddListener(delegate { ToggleAudioListener(); });

        volumeSlider.onValueChanged.RemoveAllListeners();
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(volumeSlider.value); });

        resetSelectedTargetButton.onClick.RemoveAllListeners();
        resetSelectedTargetButton.onClick.AddListener(delegate { ResetSelectedTarget(); });


        resetSelectedTargetButton.onClick.RemoveAllListeners();
        resetSelectedTargetButton.onClick.AddListener(delegate { ResetSelectedTarget(); });

        sellFriendlySelectedTargetButton.onClick.RemoveAllListeners();
        sellFriendlySelectedTargetButton.onClick.AddListener(delegate { SellFriendlySelectedTarget(); });

        startGameButton.onClick.RemoveAllListeners();
        startGameButton.onClick.AddListener(delegate { StartGameTransitionPhase(); });

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

        forfeitButton.onClick.RemoveAllListeners();
        forfeitButton.onClick.AddListener(delegate { ForfeitButtonClicked(); });

        closeEscapeMenuTabButton.onClick.RemoveAllListeners();
        closeEscapeMenuTabButton.onClick.AddListener(delegate { toggleEscapeMenu(); });

        shopUnitCapButton.onClick.RemoveAllListeners();
        shopUnitCapButton.onClick.AddListener(delegate { UpgradeUnitCap(); });

    }

    public void ToggleLeaderboardPanel()
    {
        leaderBoardPanel.gameObject.SetActive(!leaderBoardPanel.gameObject.activeSelf);
    }


    public void RefreshHelperTips()
    {
        helperText.text = helperTipContainer.currentTip;
        currentTipCounterText.text = "Tip: " + (helperTipContainer.currentTipIndex+1) + "/" + helperTipContainer.helperTips.Length;
    }

    public void PreviousTip()
    {
        if (randomTipToggler.isOn)
        {
            helperTipContainer.RandomTip();
        }
        else
        {
            helperTipContainer.PreviousTip();
        }
        helperText.text = helperTipContainer.currentTip;
        hudCanvasAudioSource.PlayOneShot(genericSucessAudioClip);
        RefreshHelperTips();
    }
    public void NextTip()
    {
        if (randomTipToggler.isOn)
        {
            helperTipContainer.RandomTip();
        }
        else
        {
            helperTipContainer.NextTip();
        }
        helperText.text = helperTipContainer.currentTip;
        hudCanvasAudioSource.PlayOneShot(genericSucessAudioClip);
        RefreshHelperTips();
    }

    public void ToggleHelpPanel()
    {
        helpPanel.SetActive(!helpPanel.activeSelf);
        if (helpPanel.activeSelf)
        {
            RefreshHelperTips();
        }
        hudCanvasAudioSource.PlayOneShot(genericSucessAudioClip);
    }

    public void ToggleCreditsPanel()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
        hudCanvasAudioSource.PlayOneShot(genericSucessAudioClip);
    }

    public void ExitGame()
    {
    

      Application.Quit();

    }

    public void PickCameraMode()
    {
        if (cameraModePicker.value == 0)
        {
            boardController.mainCameraController.cameraMode = "Normal";
        } else if (cameraModePicker.value == 1)
        {
            boardController.mainCameraController.cameraMode = "Follow";
        }
    }

    public void SetVolume(float volume)
    {
        if (audioIsMuted)
        {
            audioMutedIndicatorIcon.sprite = Resources.Load<Sprite>("audio icon");
            audioIsMuted = false;
        }
        AudioListener.volume = volume;
        userVolume = volume;
    }
    public void ToggleAudioListener()
    {
        if (!audioIsMuted)
        {
            audioIsMuted = true;
            audioMutedIndicatorIcon.sprite = Resources.Load<Sprite>("audio muted icon");
            AudioListener.volume = 0;
        } else
        {
            audioIsMuted = false;
            audioMutedIndicatorIcon.sprite = Resources.Load<Sprite>("audio icon");
            SetVolume(userVolume);
        }
     
    }

    public void SellFriendlySelectedTarget() // sell the friendly selected target npc
    {
        if (npcController.allyList.Count > 1 && boardController.selectedNPC != null && boardController.selectedNPC.GetComponent<NPC>().isEnemy == false)
        {
            int goldReward = (int) boardController.selectedNPC.GetComponent<NPC>().baseGoldBountyReward;
            if (boardController.selectedNPC.GetComponent<NPC>().TIER == 2)
            {
                goldReward = (int)(goldReward * 2f); // gold bonus for selling a tier 2 unit
            }
            else if (boardController.selectedNPC.GetComponent<NPC>().TIER == 3)
            {
                goldReward = (int)(goldReward * 8f); // gold bonus for selling a tier 3 unit
            }
            int yOffset = 0;
            foreach(Item item in boardController.selectedNPC.GetComponent<NPC>().Inventory)
            {
                boardController.selectedNPC.GetComponent<NPC>().GenerateSpecificLootDrop(item.ItemName,yOffset);
                yOffset += 1;
            }
            boardController.selectedNPC.GetComponent<NPC>().RemoveFromBoard(true); // remove the sold npc from the board 
            playerController.SetPlayerGoldCount(playerController.playerGoldCount + (long)goldReward); // apply the gold reward to the player
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        } else
        {
            hudCanvasAudioSource.PlayOneShot(genericButtonFailureAudioClip);
        }
    }

    public void ResetSelectedTarget() // reset the selected target
    {
        boardController.selectedNPC = null; // null it
    }

    public void UpdateSelectedUnitUI(GameObject selectedObject, NPC selectedNPC)
    {
        string friendOrFoe = selectedNPC.isEnemy ? "Enemy " : "";
        selectedUnitPanel_InformationText_NAME.text = friendOrFoe + selectedObject.name;
        selectedUnitPanel_InformationText_ARMORANDRETALIATION.text = "Armor: " + Mathf.Round(selectedNPC.ARMOR) + " / Retaliation: " + Mathf.Round(selectedNPC.RETALIATION);
        selectedUnitPanel_InformationText_ATTACKPOWER.text = Mathf.Round(selectedNPC.ATTACKPOWER).ToString();
        selectedUnitPanel_InformationText_CONCENTRATION.text = "Concentration: " + Mathf.Round(selectedNPC.CONCENTRATION) + "/" + Mathf.Round(selectedNPC.MAXCONCENTRATION);
        selectedUnitPanel_InformationText_HP.text = Mathf.Round(selectedNPC.HP) + "/" + Mathf.Round(selectedNPC.MAXHP);
        selectedUnitPanel_InformationText_SPELLPOWER.text = Mathf.Round(selectedNPC.SPELLPOWER).ToString();
        selectedUnitPanel_InformationText_TIER.text = "Level " + selectedNPC.TIER;

        if (selectedNPC.Inventory != null)
        {
            InventorySlot1Panel.GetComponent<ItemThumbnail>().shownItem = EmptyItem;
            InventorySlot2Panel.GetComponent<ItemThumbnail>().shownItem = EmptyItem;
            InventorySlot3Panel.GetComponent<ItemThumbnail>().shownItem = EmptyItem;
            InventorySlot4Panel.GetComponent<ItemThumbnail>().shownItem = EmptyItem;
            if (selectedNPC.Inventory.Count >= 1)
            {
                InventorySlot1Panel.GetComponent<ItemThumbnail>().shownItem = selectedNPC.Inventory[0];
            }
            if (selectedNPC.Inventory.Count >= 2)
            {
                InventorySlot2Panel.GetComponent<ItemThumbnail>().shownItem = selectedNPC.Inventory[1];
            }
            if (selectedNPC.Inventory.Count >= 3)
            {
                InventorySlot3Panel.GetComponent<ItemThumbnail>().shownItem = selectedNPC.Inventory[2];
            }
            if (selectedNPC.Inventory.Count >= 4)
            {
                InventorySlot4Panel.GetComponent<ItemThumbnail>().shownItem = selectedNPC.Inventory[3];
            }
        }
        InventorySlot1Panel.GetComponent<ItemThumbnail>().SetImage();
        InventorySlot2Panel.GetComponent<ItemThumbnail>().SetImage();
        InventorySlot3Panel.GetComponent<ItemThumbnail>().SetImage();
        InventorySlot4Panel.GetComponent<ItemThumbnail>().SetImage();
    }

    public void UpdateFocusedItemTooltip(Item item)
    {
        FocusedItemTooltipTextName.text = item.ItemName.ToString() + " (" + item.ItemRarity.ToString() + ")";
        if (item.ItemRarity.Equals(ItemRarity.Trash))
        {
            FocusedItemTooltipTextName.color = Color.gray;
        }
        else if (item.ItemRarity.Equals(ItemRarity.Common))
        {
            FocusedItemTooltipTextName.color = Color.green;
        }
        else if (item.ItemRarity.Equals(ItemRarity.Rare))
        {
            FocusedItemTooltipTextName.color = Color.blue;
        }
        else if (item.ItemRarity.Equals(ItemRarity.Artifact))
        {
            FocusedItemTooltipTextName.color = new Color(190, 65, 0); //orange
        }
    
        FocusedItemTooltipTextStats.text = item.Tooltip;
    }

    public void SyncronizeToSteamLeaderBoardIfNeeded()
    {
        // try fetch the players leaderboard score one time
        if (fetchSteamLeaderboardEntry && steamLeaderboard.foundLeaderboard && steamLeaderboard.downloadedLeaderboard && !steamLeaderboard.downLoadingUserEntry)
        {
            int fetchedMMR = steamLeaderboard.ReadDownloadedUserLeaderboardEntry(); // read user mmr
            intro_playerMMR.text = fetchedMMR.ToString(); // syncronize the ui with the fetched value
            playerController.playerMMR = fetchedMMR;
            fetchSteamLeaderboardEntry = false;
        }

        // try fetch the top 10 leaderboard scores one time
        if (fetchSteamLeaderboardTop100Entries && steamLeaderboard.foundLeaderboard && steamLeaderboard.downloadedLeaderboard && !steamLeaderboard.downLoadingTop100Entries)
        {
            steamLeaderboard.top100Entries = steamLeaderboard.ReadDownloadedTop100Entries(); // read top 10 entries
            for (int i = 0; i < steamLeaderboard.top100Entries.Length; i++)
            {
                LeaderboardEntry leaderboardEntryPrefab = steamLeaderBoardTop100EntriesUIPrefabsList.ToArray()[i]; // syncronize the ui with the fetched values
                LeaderEntry top100Entry = steamLeaderboard.top100Entries[i];
                leaderboardEntryPrefab.rank = top100Entry.globalRank;
                leaderboardEntryPrefab.mmrText.text = top100Entry.score.ToString();
                leaderboardEntryPrefab.nameText.text = SteamFriends.GetFriendPersonaName(top100Entry.id);
                leaderboardEntryPrefab.rankText.text = top100Entry.globalRank.ToString() + ")";
                leaderboardEntryPrefab.steamAvatar.sprite = top100Entry.avatar;
                if (top100Entry.globalRank == 1 || top100Entry.globalRank == 2 || top100Entry.globalRank == 3 )
                {
                    leaderboardEntryPrefab.trophyIcon.gameObject.SetActive(true);
                    leaderboardEntryPrefab.trophyIcon.sprite = top100Entry.trophyIcon;
                } 
            }
            fetchSteamLeaderboardTop100Entries = false;
        }
    }

    public void PrepareUIAccordingly()
    {
        if (boardController.gameStatus != GameStatus.ReportVictory && boardController.gameStatus != GameStatus.ReportDefeat && boardController.FocusedItem.ItemName != EmptyItem.ItemName) // update the inventory mousedover tooltip
        {
                MousedOverSelectedItemTooltipPanel.gameObject.SetActive(true);
            RectTransform rectTrans = MousedOverSelectedItemTooltipPanel.GetComponent<RectTransform>();
            rectTrans.position = Input.mousePosition + tooltipOffsetVector;
        }
        else
        {
            MousedOverSelectedItemTooltipPanel.gameObject.SetActive(false);
        }
     
        if (boardController.gameStatus != GameStatus.ReportVictory && boardController.gameStatus != GameStatus.ReportDefeat && boardController.selectedNPC != null) // update the entire selected unit panel
        {
            GameObject selectedObject = boardController.selectedNPC;
            NPC selectedNPC = selectedObject.GetComponent<NPC>();
            hudCanvasCurrentlySelectedUnitPanel.SetActive(true);
            if (!boardController.selectedNPC.GetComponent<NPC>().isCreep && !selectedNPC.isCreep && !selectedNPC.isBoss)
            {
                CurrentlySelectedUnitInventoryPanel.SetActive(true);
            }
            UpdateSelectedUnitUI(selectedObject, selectedNPC);
         

            if (selectedNPC.UNIT_TYPE != Unit.RobotCreep)
            {
                selectedUnitPanel_primaryTribeIconVisualizer.SetImage(selectedNPC.PRIMARYTRIBE, true);
                selectedUnitPanel_secondaryTribeIconVisualizer.SetImage(selectedNPC.SECONDARYTRIBE, true);
            }
            else
            {
                selectedUnitPanel_primaryTribeIconVisualizer.SetImage(selectedNPC.PRIMARYTRIBE, false);
                selectedUnitPanel_secondaryTribeIconVisualizer.SetImage(selectedNPC.SECONDARYTRIBE, false);
            }

            selectedUnitPanel_abilityiconVisualizer.SetImage(selectedNPC.ABILITY);
            sellFriendlySelectedTargetButton.gameObject.SetActive(!selectedNPC.isEnemy);

        }
        else
        {
            hudCanvasCurrentlySelectedUnitPanel.SetActive(false);
            CurrentlySelectedUnitInventoryPanel.SetActive(false);
        }

        if (!boardController.gameStatus.Equals(GameStatus.Shopping)) // take out ui elements outside that should not be outside of the shopping phase
        {
            shopToggleButton.gameObject.SetActive(false);
            ShopPanelTooltipSubPanel.SetActive(false);
            hudCanvasShopPanel.SetActive(false);
            sellFriendlySelectedTargetButton.gameObject.SetActive(false);
            fightButton.gameObject.SetActive(false);

        }
        else
        {
            fightButton.gameObject.SetActive(true);

        }
    }

    public void ListenForEscapeMenuToggle()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleEscapeMenu();
        }
    }

    void Update()
    {
        SyncronizeToSteamLeaderBoardIfNeeded();
        PrepareUIAccordingly();
        ListenForEscapeMenuToggle();
  
    }

    public void toggleEscapeMenu()
    {
            hudCanvasEscapePanel.SetActive(!hudCanvasEscapePanel.activeSelf);
        ShopPanelTooltipSubPanel.SetActive(false);
        hudCanvasShopPanel.SetActive(false);
    }


    public void StartGameTransitionPhase() // set up the ui for starting the game
    {

            hudCanvasAudioSource.PlayOneShot(startGameAudioClip);
            boardController.ChangeGameStatus(GameStatus.Shopping, "Shopping Phase"); // set to shopping phae
            ChangeCurrentRoundDisplayText(boardController.currentGameRound); // update ui text element
            boardController.StartCoroutine("ProgressHealthRegeneration"); // begin health regeneration process
            boardController.StartCoroutine("ProgressConcentrationRegeneration"); // begin concentration regeneration process
            hudCanvasPlayerPanel.SetActive(true); // activate required ui elements
            hudCanvasBottomBar.SetActive(true);
            shopToggleButton.gameObject.SetActive(true);
            hudCanvasTopBar.SetActive(true);
            hudCanvasWizardPanel.SetActive(false);
        if (playerController.newPlayer)
        {
            dialogueTriggerSystem.tutorialDialogue1.TriggerDialogue();
        }
       

        if (wizard_difficultyPicker.value == 0)
        {
            playerController.enemyMMR = 1200;
        } else if (wizard_difficultyPicker.value == 1)
        {
            playerController.enemyMMR = 1600;
        } else
        {
            playerController.enemyMMR = 1900;
        }

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

        int cameraDistance = 40;

        shopButton1.interactable = true;
        shopButton1.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI board Large  parchment");
        shopButton1.GetComponent<Thubmnail>().SourcePrefab = Resources.Load<GameObject>("3d_thumbnail_" + playerController.shoppingOptions[0]);
        playerController.NPC_COST_DATA.TryGetValue(playerController.shoppingOptions[0], out unitCost);
        shopbutton1_hudCanvasShopCostBuyUnit.text = unitCost.ToString();
        spawned = Instantiate(shopButton1.GetComponent<Thubmnail>().SourcePrefab, shopCam1.transform.position + (cameraDistance * shopCam1.transform.forward), Quaternion.identity);
        spawned.transform.Rotate(0f, 180f, 0f, Space.Self);
        spawned.transform.SetParent(shopButton1.transform);
        shopButton1.GetComponent<Thubmnail>().SpawnedAssociatedNPC = spawned;

        shopButton2.interactable = true;
        shopButton2.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI board Large  parchment");
        shopButton2.GetComponent<Thubmnail>().SourcePrefab = Resources.Load<GameObject>("3d_thumbnail_" + playerController.shoppingOptions[1]);
        playerController.NPC_COST_DATA.TryGetValue(playerController.shoppingOptions[1], out unitCost);
        shopbutton2_hudCanvasShopCostBuyUnit.text = unitCost.ToString();
        spawned = Instantiate(shopButton2.GetComponent<Thubmnail>().SourcePrefab, shopCam2.transform.position + (cameraDistance * shopCam2.transform.forward), Quaternion.identity);
        spawned.transform.Rotate(0f, 180f, 0f, Space.Self);
        spawned.transform.SetParent(shopButton2.transform);
        shopButton2.GetComponent<Thubmnail>().SpawnedAssociatedNPC = spawned;

        shopButton3.interactable = true;
        shopButton3.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI board Large  parchment");
        shopButton3.GetComponent<Thubmnail>().SourcePrefab = Resources.Load<GameObject>("3d_thumbnail_" + playerController.shoppingOptions[2]);
        playerController.NPC_COST_DATA.TryGetValue(playerController.shoppingOptions[2], out unitCost);
        shopbutton3_hudCanvasShopCostBuyUnit.text = unitCost.ToString();
        spawned = Instantiate(shopButton3.GetComponent<Thubmnail>().SourcePrefab, shopCam3.transform.position + (cameraDistance * shopCam3.transform.forward), Quaternion.identity);
        spawned.transform.Rotate(0f, 180f, 0f, Space.Self);
        spawned.transform.SetParent(shopButton3.transform);
        shopButton3.GetComponent<Thubmnail>().SpawnedAssociatedNPC = spawned;

        shopButton4.interactable = true;
        shopButton4.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI board Large  parchment");
        shopButton4.GetComponent<Thubmnail>().SourcePrefab = Resources.Load<GameObject>("3d_thumbnail_" + playerController.shoppingOptions[3]);
        playerController.NPC_COST_DATA.TryGetValue(playerController.shoppingOptions[3], out unitCost);
        shopbutton4_hudCanvasShopCostBuyUnit.text = unitCost.ToString();
        spawned = Instantiate(shopButton4.GetComponent<Thubmnail>().SourcePrefab, shopCam4.transform.position + (cameraDistance * shopCam4.transform.forward), Quaternion.identity);
        spawned.transform.Rotate(0f, 180f, 0f, Space.Self);
        spawned.transform.SetParent(shopButton4.transform);
        shopButton4.GetComponent<Thubmnail>().SpawnedAssociatedNPC = spawned;

        shopButton5.interactable = true;
        shopButton5.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI board Large  parchment");
        shopButton5.GetComponent<Thubmnail>().SourcePrefab = Resources.Load<GameObject>("3d_thumbnail_" + playerController.shoppingOptions[4]);
        playerController.NPC_COST_DATA.TryGetValue(playerController.shoppingOptions[4], out unitCost);
        shopbutton5_hudCanvasShopCostBuyUnit.text = unitCost.ToString();
        spawned = Instantiate(shopButton5.GetComponent<Thubmnail>().SourcePrefab, shopCam5.transform.position + (cameraDistance * shopCam5.transform.forward), Quaternion.identity);
        spawned.transform.Rotate(0f, 180f, 0f, Space.Self);
        spawned.transform.SetParent(shopButton5.transform);
        shopButton5.GetComponent<Thubmnail>().SpawnedAssociatedNPC = spawned;

        shopButton6.interactable = true;
        shopButton6.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI board Large  parchment");
        shopButton6.GetComponent<Thubmnail>().SourcePrefab = Resources.Load<GameObject>("3d_thumbnail_" + playerController.shoppingOptions[5]);
        playerController.NPC_COST_DATA.TryGetValue(playerController.shoppingOptions[5], out unitCost);
        shopbutton6_hudCanvasShopCostBuyUnit.text = unitCost.ToString();
        spawned = Instantiate(shopButton6.GetComponent<Thubmnail>().SourcePrefab, shopCam6.transform.position + (cameraDistance * shopCam6.transform.forward), Quaternion.identity);
        spawned.transform.Rotate(0f, 180f, 0f, Space.Self);
        spawned.transform.SetParent(shopButton6.transform);
        shopButton6.GetComponent<Thubmnail>().SpawnedAssociatedNPC = spawned;
    }

    void RefreshShop()
    {
        playerController.ShuffleNewShopingOptions(false);
    }

    void UpgradeUnitCap()
    {
        playerController.UpgradeUnitCapWithGold();
    }

    void ShopButton1Clicked()
    {
        if (playerController.BuyUnitFromShop(playerController.shoppingOptions[0]))
        {
            shopButton1.interactable = false;
            shopButton1.GetComponent<Image>().sprite = Resources.Load<Sprite>("bought unit icon");
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        }
    }

    void ShopButton2Clicked()
    {
        if (playerController.BuyUnitFromShop(playerController.shoppingOptions[1]))
        {
            shopButton2.interactable = false;
            shopButton2.GetComponent<Image>().sprite = Resources.Load<Sprite>("bought unit icon");
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        }
    }

    void ShopButton3Clicked()
    {
        if (playerController.BuyUnitFromShop(playerController.shoppingOptions[2]))
        {
            shopButton3.interactable = false;
            shopButton3.GetComponent<Image>().sprite = Resources.Load<Sprite>("bought unit icon");
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        }
 
    }

    void ShopButton4Clicked()
    {
        if (playerController.BuyUnitFromShop(playerController.shoppingOptions[3]))

        {
            shopButton4.interactable = false;
            shopButton4.GetComponent<Image>().sprite = Resources.Load<Sprite>("bought unit icon");
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        }

    } 

    void ShopButton5Clicked()
    {
        if (playerController.BuyUnitFromShop(playerController.shoppingOptions[4]))
        {
            shopButton5.interactable = false;
            shopButton5.GetComponent<Image>().sprite = Resources.Load<Sprite>("bought unit icon");
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        }
    }

    void ShopButton6Clicked()
    {
        if (playerController.BuyUnitFromShop(playerController.shoppingOptions[5]))
        {
            shopButton6.interactable = false;
            shopButton6.GetComponent<Image>().sprite = Resources.Load<Sprite>("bought unit icon");
            hudCanvasAudioSource.PlayOneShot(buyUnitSuccessAudioClip);
        }


    }

    void ForfeitButtonClicked()
    {
        escapeMenuForfeitConfirmationPromptPanel.SetActive(true);
    }


    void ToggleShopPanel()
    {
        
        if (hudCanvasShopPanel.activeSelf) // check if the shop panel is active
        {
            ShopPanelTooltipSubPanel.SetActive(false);
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
        hudCanvasAudioSource.PlayOneShot(genericSucessAudioClip);
        hudCanvasTribesPanel.SetActive(!hudCanvasTribesPanel.activeSelf);
    }

    IEnumerator SmoothRoundFightPhaseTransition()
    {

        yield return new WaitForSeconds(2);
        if (boardController.gameStatus != GameStatus.ReportDefeat || boardController.gameStatus != GameStatus.ReportVictory){
        boardController.ChangeGameStatus(GameStatus.Fight);
        }
    }

    void TryTransitionToFightPhase()
    {
        if (boardController.gameStatus != GameStatus.Fight && boardController.gameStatus != GameStatus.ReportDefeat && boardController.gameStatus == GameStatus.Shopping && playerController.currentlyDeployedUnits > 0) // check if we are ready to proceed to combat
        {
            boardController.ChangeGameStatus(GameStatus.Wait); // smooth wait transition
            npcController.allyListBackup = new List<NPC>();
            boardController.SpawnEnemyUnitsRound_Balanced(); // spawn balanced enemies

            if (playerController.newPlayer && boardController.currentGameRound == 1)
            {
                dialogueTriggerSystem.tutorialDialogue2.TriggerDialogue();
            }
            else if (playerController.newPlayer && boardController.currentGameRound == 3)
            {
                dialogueTriggerSystem.tutorialDialogue3.TriggerDialogue();
            }
            else if (boardController.currentGameRound == 6)
            {
                dialogueTriggerSystem.bossDialogue1.TriggerDialogue();
            }
            else if (boardController.currentGameRound == 12)
            {
                dialogueTriggerSystem.bossDialogue2.TriggerDialogue();
            }
            else if (boardController.currentGameRound == 18)
            {
                dialogueTriggerSystem.bossDialogue3.TriggerDialogue();
            }

            if (UnityEngine.Random.Range(0,7) == 1)
            {
                boardController.rainSystem.SetActive(!boardController.rainSystem.activeSelf);
                if (boardController.rainSystem.activeSelf)
                {
                    boardController.ambienceSound.PlayRainLoop();
                } else
                {
                    boardController.ambienceSound.StopPlaying();
                }
            }

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

            if (UnityEngine.Random.Range(0,2) == 0 && npcController.deployedAllyList[i].battleCry_SoundClip != null)
                    { 
                        npcController.deployedAllyList[i].npcAudioSource.PlayOneShot(npcController.deployedAllyList[i].battleCry_SoundClip);
                    }
                }
            }

            foreach(NPC npc in npcController.deployedAllyList)
            {
                sessionLogger.IncrementTribesDeployedToFightCounters(npc);
            }
            foreach (GameObject lootDrop in boardController.DroppedItemList) {
                GameObject.Destroy(lootDrop);
            }

            sessionLogger.unitsDeployedToFight += npcController.deployedAllyList.Count;
            combatTimerPanel.gameObject.SetActive(true); // toggle the combat timer panel

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
            GameObject fct = objectPoolController.InstantiateFloatingCombatText(TargetDestinationNPC);
            FloatingCombatText floatingCombatTextScript = fct.GetComponentInChildren<FloatingCombatText>();
            floatingCombatTextScript.displayMode = displayMode;
            floatingCombatTextScript.dmgReport = dmgReport;
            floatingCombatTextScript.Init();
            objectPoolController.DestroyFloatingCombatTextAfterSeconds(1f,fct);
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

    public void ChangeCostToUnitCapUpgradeDisplayText(string str)
    {
        hudCanvasShopUnitCapUpgradeText.text = str;
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
