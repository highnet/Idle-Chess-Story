using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Language { English, Spanish, NONE }
public class Translator : MonoBehaviour
{
    public UiController uiController;
    public Dropdown languagePicker;

    private string shoppingPhase_English = "Shopping Phase"; // string id : 44
    private string shoppingPhase_Spanish = "***"; // string id : 44

    private string fightPhase_English = "Fight Phase"; // string id : 45
    private string fightPhase_Spanish = "***"; // string id : 45

    private string waitPhase_English = "Wait"; // string id : 46
    private string waitPhase_Spanish = "***"; // string id : 46

    private string ability_name_fireball_English = "Fireball"; // string id : 96
    private string ability_name_fireball_Spanish = "***"; // string id : 96

    private string ability_name_apupself_English = "AttackPower UP Self"; // string id : 97
    private string ability_name_apupself_Spanish = "***"; // string id : 97

    private string ability_name_armorupself_English = "Armor UP Self"; // string id : 98
    private string ability_name_armorupself_Spanish = "***"; // string id : 98

    private string ability_name_retaliationupself_English = "Retalation UP Self"; // string id : 99
    private string ability_name_retaliationupself_Spanish = "***"; // string id : 99

    private string ability_name_heroicstrike_English = "Heroic Strike"; // string id : 100
    private string ability_name_heroicstrike_Spanish = "***"; // string id : 100

    private string ability_name_apdownother_English = "AttackPower DOWN Other"; // string id : 101
    private string ability_name_apdownother_Spanish = "***"; // string id : 101

    private string ability_name_armordownother_English = "Armor DOWN Other"; // string id : 102
    private string ability_name_armordownother_Spanish = "***"; // string id : 102

    private string ability_name_frostball_English = "Frostball"; // string id : 103
    private string ability_name_frostball_Spanish = "***"; // string id : 103

    private string ability_name_stab_English = "Stab"; // string id : 104
    private string ability_name_stab_Spanish = "***"; // string id : 104

    private string ability_name_stun_English = "Stun"; // string id : 105
    private string ability_name_stun_Spanish = "***"; // string id : 105

    private string ability_name_healfriend_English = "Heal Friend"; // string id : 106
    private string ability_name_healfriend_Spanish = "***"; // string id : 106

    private string ability_name_hpupself_English = "HP UP Self"; // string id : 107
    private string ability_name_hpupself_Spanish = "***"; // string id : 107

    private string ability_tooltip_fireball_English = "Deal damage equal to 200% of your spell power"; // string id : 108
    private string ability_tooltip_fireball_Spanish = "***"; // string id : 108

    private string ability_tooltip_apupself_English = "Increase your attack power for a duration of time;"; // string id : 109
    private string ability_tooltip_apupself_Spanish = "***"; // string id : 109

    private string ability_tooltip_armorupself_English = "Increase your armor for a duration of time"; // string id : 110
    private string ability_tooltip_armorupself_Spanish = "***"; // string id : 110

    private string ability_tooltip_retaliationupself_English = "Increases your retaliation for a duration of time"; // string id : 111
    private string ability_tooltip_retaliationupself_Spanish = "***"; // string id : 111

    private string ability_tooltip_heroicstrike_English = "Deal Damage equal to 190% of your Attack Power"; // string id : 112
    private string ability_tooltip_heroicstrike_Spanish = "***"; // string id : 112

    private string ability_tooltip_apdownother_English = "Reduce your target's Attackpower for a duration of time"; // string id : 113
    private string ability_tooltip_apdownother_Spanish = "***"; // string id : 113

    private string ability_tooltip_armordownother_English = "Increases your ARMOR for a duration of time"; // string id : 114
    private string ability_tooltip_armordownother_Spanish = "***"; // string id : 114

    private string ability_tooltip_frostball_English = "Deal damage equal to 150% of your spell power"; // string id : 115
    private string ability_tooltip_frostball_Spanish = "***"; // string id : 115

    private string ability_tooltip_stab_English = "Deal damage equal to 140% of your AttackPower"; // string id : 116
    private string ability_tooltip_stab_Spanish = "***"; // string id : 116

    private string ability_tooltip_stun_English = "Stun your target for a duration of time"; // string id : 117
    private string ability_tooltip_stun_Spanish = "***"; // string id : 117

    private string ability_tooltip_healfriend_English = "Heal a friend for 100% of your spell power"; // string id : 118 
    private string ability_tooltip_healfriend_Spanish = "***"; // string id : 118 

    private string ability_tooltip_hpupself_English = "Increases your HP for a duration of time"; // string id : 119
    private string ability_tooltip_hpupself_Spanish = "***"; // string id : 119

    private string tribe_assassin_English = "Assassin"; // string id : 120
    private string tribe_assassin_Spanish = "***"; // string id : 120

    private string tribe_Beast_English = "Beast"; // string id : 121
    private string tribe_Beast_Spanish = "***"; // string id : 121

    private string tribe_Elemental_English = "Elemental"; // string id : 122
    private string tribe_Elemental_Spanish = "***"; // string id : 122

    private string tribe_Guardian_English = "Guardian"; // string id : 123
    private string tribe_Guardian_Spanish = "***"; // string id : 123

    private string tribe_Structure_English = "Structure"; // string id : 124
    private string tribe_Structure_Spanish = "***"; // string id : 124

    private string tribe_Undead_English = "Undead"; // string id : 125
    private string tribe_Undead_Spanish = "***"; // string id : 125

    private string tribe_Warrior_English = "Warrior"; // string id : 126
    private string tribe_Warrior_Spanish = "***"; // string id : 126

    private string tribe_Wizard_English = "Wizard"; // string id : 127
    private string tribe_Wizard_Spanish = "***"; // string id : 127

    public string stat_ap_English = "Attack: "; // string id : 128
    public string stat_ap_Spanish = "***"; // string id : 128 

    public string stat_armor_English = "Armor: "; // string id : 129
    public string stat_armor_Spanish = "***"; // string id : 129

    public string stat_maxhp_English = "HP: "; // string id : 130
    public string stat_maxhp_Spanish = "***"; // string id : 130

    public string stat_retaliation_English = "Retaliation: "; // string id : 131
    public string stat_retaliation_Spanish = "***"; // string id : 131

    public string stat_sp_English = "Spell Power: "; // string id : 132
    public string stat_sp_Spanish = "***"; // string id : 132

    public string string_snippet_1_English = "You deployed: "; // string id : 141
    public string string_snippet_2_English = " unit(s)"; // string id : 142
    public string string_snippet_3_English = "Most deployed tribe: "; // string id : 143

    private void Start()
    {
        languagePicker.onValueChanged.RemoveAllListeners();
        languagePicker.onValueChanged.AddListener(delegate { SwapLanguage(true,Language.NONE); });
    }

    public string TranslateStringSnippet(string text, Language language)
    {
        string str = "";
        if (language == Language.English)
        {
            return text;
        } else if (language == Language.Spanish)
        {
            if (text.Equals("You deployed: "))
            {
                return "***";
            } else if (text.Equals(" unit(s)"))
            {
                return "***";
            }else if(text.Equals("Most deployed tribe: "))
            {
                return "***";
            }
        }
        return str;
    }
    public string TranslateAbilityName(Ability ability, Language language)
    {
        string str = "";

        if (language == Language.English)
        {

            if (ability == Ability.Fireball)
            {
                return ability_name_fireball_English;
            }
            else if (ability == Ability.AP_UP_Self)
            {
                return ability_name_apupself_English;
            }
            else if (ability == Ability.Armor_UP_Self)
            {
                return ability_name_armorupself_English;
            }
            else if (ability == Ability.MaxHP_Up_Self)
            {
                return ability_name_hpupself_English;
            }
            else if (ability == Ability.Retaliation_UP_Self)
            {
                return ability_name_retaliationupself_English;
            }
            else if (ability == Ability.HeroicStrike)
            {
                return ability_name_heroicstrike_English;
            }
            else if (ability == Ability.AP_DOWN_OTHER)
            {
                return ability_name_apdownother_English;
            }
            else if (ability == Ability.ARMOR_DOWN_OTHER)
            {
                return ability_name_armordownother_English;
            }
            else if (ability == Ability.FrostBall)
            {
                return ability_name_frostball_English;
            }
            else if (ability == Ability.Stab)
            {
                return ability_name_stab_English;
            }
            else if (ability == Ability.Stun)
            {
                return ability_name_stun_English;
            }
            else if (ability == Ability.HealFriend)
            {
                return ability_name_healfriend_English;
            }
            else
            {
                return "";
            }
        }
        else if (language == Language.Spanish)
        {
            if (ability == Ability.Fireball)
            {
                return ability_name_fireball_Spanish;
            }
            else if (ability == Ability.AP_UP_Self)
            {
                return ability_name_apupself_Spanish;
            }
            else if (ability == Ability.Armor_UP_Self)
            {
                return ability_name_armorupself_Spanish;
            }
            else if (ability == Ability.MaxHP_Up_Self)
            {
                return ability_name_hpupself_Spanish;
            }
            else if (ability == Ability.Retaliation_UP_Self)
            {
                return ability_name_retaliationupself_Spanish;
            }
            else if (ability == Ability.HeroicStrike)
            {
                return ability_name_heroicstrike_Spanish;
            }
            else if (ability == Ability.AP_DOWN_OTHER)
            {
                return ability_name_apdownother_Spanish;
            }
            else if (ability == Ability.ARMOR_DOWN_OTHER)
            {
                return ability_name_armordownother_Spanish;
            }
            else if (ability == Ability.FrostBall)
            {
                return ability_name_frostball_Spanish;
            }
            else if (ability == Ability.Stab)
            {
                return ability_name_stab_Spanish;
            }
            else if (ability == Ability.Stun)
            {
                return ability_name_stun_Spanish;
            }
            else if (ability == Ability.HealFriend)
            {
                return ability_name_healfriend_Spanish;
            }
            else
            {
                return "";
            }
        }
        return str;
    }
    public string TranslateAbilityTooltip(Ability ability, Language language)
    {
        string str = "";

        if (language == Language.English)
        {
            if (ability == Ability.Fireball)
            {
                return ability_tooltip_fireball_English;
            }
            else if (ability == Ability.AP_UP_Self)
            {
                return ability_tooltip_apupself_English;
            }
            else if (ability == Ability.Armor_UP_Self)
            {
                return ability_tooltip_armorupself_English;
            }
            else if (ability == Ability.MaxHP_Up_Self)
            {
                return ability_tooltip_hpupself_English;
            }
            else if (ability == Ability.Retaliation_UP_Self)
            {
                return ability_tooltip_retaliationupself_English;
            }
            else if (ability == Ability.HeroicStrike)
            {
                return ability_tooltip_heroicstrike_English;
            }
            else if (ability == Ability.AP_DOWN_OTHER)
            {
                return ability_tooltip_apdownother_English;
            }
            else if (ability == Ability.ARMOR_DOWN_OTHER)
            {
                return ability_tooltip_armordownother_English;
            }
            else if (ability == Ability.FrostBall)
            {
                return ability_tooltip_frostball_English;
            }
            else if (ability == Ability.Stab)
            {
                return ability_tooltip_stab_English;
            }
            else if (ability == Ability.Stun)
            {
                return ability_tooltip_stun_English;
            }
            else if (ability == Ability.HealFriend)
            {
                return ability_tooltip_healfriend_English;
            }
            else
            {
                return  "";
            }
        }
        else if (language == Language.Spanish)
        {
            if (ability == Ability.Fireball)
            {
                return ability_tooltip_fireball_Spanish;
            }
            else if (ability == Ability.AP_UP_Self)
            {
                return ability_tooltip_apupself_Spanish;
            }
            else if (ability == Ability.Armor_UP_Self)
            {
                return ability_tooltip_armorupself_Spanish;
            }
            else if (ability == Ability.MaxHP_Up_Self)
            {
                return ability_tooltip_hpupself_Spanish;
            }
            else if (ability == Ability.Retaliation_UP_Self)
            {
                return ability_tooltip_retaliationupself_Spanish;
            }
            else if (ability == Ability.HeroicStrike)
            {
                return ability_tooltip_heroicstrike_Spanish;
            }
            else if (ability == Ability.AP_DOWN_OTHER)
            {
                return ability_tooltip_apdownother_Spanish;
            }
            else if (ability == Ability.ARMOR_DOWN_OTHER)
            {
                return ability_tooltip_armordownother_Spanish;
            }
            else if (ability == Ability.FrostBall)
            {
                return ability_tooltip_frostball_Spanish;
            }
            else if (ability == Ability.Stab)
            {
                return ability_tooltip_stab_Spanish;
            }
            else if (ability == Ability.Stun)
            {
                return ability_tooltip_stun_Spanish;
            }
            else if (ability == Ability.HealFriend)
            {
                return ability_tooltip_healfriend_Spanish;
            }
            else
            {
                return "";
            }
        }
        return str;
    }
    public string TranslateGameStatus(GameStatus status, Language language)
    {
        string str = "";

        if (language == Language.English)
        {
            if (status == GameStatus.Shopping)
            {
                return shoppingPhase_English;
            } else if (status == GameStatus.Fight)
            {
                return fightPhase_English;
            } else if (status == GameStatus.Wait)
            {
                return waitPhase_English;
            }
        } else if (language == Language.Spanish)
        {
            if (status == GameStatus.Shopping)
            {
                return shoppingPhase_Spanish;
            }
            else if (status == GameStatus.Fight)
            {
                return fightPhase_Spanish;
            }
            else if (status == GameStatus.Wait)
            {
                return waitPhase_Spanish;
            }
        }

        return str;
    }
    public string TranslateTribe(Tribe tribe, Language language)
    {
        string str = "";
        if (language == Language.English)
        {
            if (tribe == Tribe.Assassin)
            {
                return tribe_assassin_English;
            } else if (tribe == Tribe.Beast)
            {
                return tribe_Beast_English;
            }
            else if (tribe == Tribe.Elemental)
            {
                return tribe_Elemental_English;
            }
            else if (tribe == Tribe.Guardian)
            {
                return tribe_Guardian_English;
            }
            else if (tribe == Tribe.Structure)
            {
                return tribe_Structure_English;
            }
            else if (tribe == Tribe.Undead)
            {
                return tribe_Undead_English;
            }
            else if (tribe == Tribe.Warrior)
            {
                return tribe_Warrior_English;
            }
            else if (tribe == Tribe.Wizard)
            {
                return tribe_Wizard_English;
            }

        } else if (language == Language.Spanish)
        {
            if (tribe == Tribe.Assassin)
            {
                return tribe_assassin_Spanish;
            }
            else if (tribe == Tribe.Beast)
            {
                return tribe_Beast_Spanish;
            }
            else if (tribe == Tribe.Elemental)
            {
                return tribe_Elemental_Spanish;
            }
            else if (tribe == Tribe.Guardian)
            {
                return tribe_Guardian_Spanish;
            }
            else if (tribe == Tribe.Structure)
            {
                return tribe_Structure_Spanish;
            }
            else if (tribe == Tribe.Undead)
            {
                return tribe_Undead_Spanish;
            }
            else if (tribe == Tribe.Warrior)
            {
                return tribe_Warrior_Spanish;
            }
            else if (tribe == Tribe.Wizard)
            {
                return tribe_Wizard_Spanish;
            }
        }


        return str;
    }
    public string TranslateStat(Stat stat, Language language)
    {
        string str = "";
        if (language == Language.English)
        {
            if (stat == Stat.AP)
            {
                return stat_ap_English;

            } else if (stat == Stat.ARMOR)
            {
                return stat_armor_English;  
            } else if (stat == Stat.MAXHP)
            {
                return stat_maxhp_English;
            } else if ( stat == Stat.RETALIATION)
            {
                return stat_retaliation_English;
            } else if (stat == Stat.SP)
            {
                return stat_sp_English;
            }
        }else if (language == Language.Spanish)
        {
            if (stat == Stat.AP)
            {
                return stat_ap_Spanish;
            }
            else if (stat == Stat.ARMOR)
            {
                return stat_armor_Spanish;
            }
            else if (stat == Stat.MAXHP)
            {
                return stat_maxhp_Spanish;
            }
            else if (stat == Stat.RETALIATION)
            {
                return stat_retaliation_Spanish;
            }
            else if (stat == Stat.SP)
            {
                return stat_sp_Spanish;
            }
        }
        return str;
    }

    public void SwapLanguage(bool takeLanguageFromLanguagePicker, Language languageOverride)
    {

        Language languageToSwapTo = Language.English;

        if (takeLanguageFromLanguagePicker)
        {

        if (languagePicker.value == 0)
        {
            languageToSwapTo = Language.English;
        } else if (languagePicker.value == 1)
        {
            languageToSwapTo = Language.Spanish;
        }
        }
        else if (languageOverride != Language.NONE)
        {
            languageToSwapTo = languageOverride;
        }
        uiController.currentLanguage = languageToSwapTo;

        if (languageToSwapTo == Language.English)
        {
            // 
            uiController.languageText.text = "Language:"; // string id : 1
            uiController.profileText.text = "Profile:"; // string id : 2
            uiController.startGameText.text = "Start Game"; // string id : 3
            uiController.leaderboardText.text = "Leaderboard"; // string id : 4
            uiController.settingsText.text = "Settings"; // string id : 5
            uiController.newsText0.text = "We want to personally thank everyone that has played the game in early access so far! \n We are working hard to add new features and make the game play experience more enjoyable."; // string id : 6
            uiController.newsText1.text = "----------------------------- \n Patch notes for ((TBD: RELEASE DATE)) \n Added Spanish localization. \n ----------------------------- \n To read all patch notes check our news page on Steam!"; // string id : 7
            uiController.creditsText.text = "Credits"; // string id : 8
            uiController.newsText.text = "NEWS"; // string id : 9
            uiController.difficultyText.text = "Difficulty:"; // string id : 10
            uiController.difficultyPicker.options[0].text = "Easy"; // string id : 11
            uiController.difficultyPicker.options[1].text = "Medium"; // string id : 12
            uiController.difficultyPicker.options[2].text = "Hard"; // string id : 13
            uiController.difficultyDropdownText.text = uiController.difficultyPicker.options[uiController.difficultyPicker.value].text; // set current difficulty label
            uiController.idleChessStoryLogoText.text = "Idle Chess Story"; ; // string id : 14
            //
            uiController.leaderboard2Text.text = "Leaderboard"; // string id : 15
            uiController.nameText.text = "Name"; // string id : 16
            uiController.ratingText.text = "Rating"; // string id : 17
            uiController.rankText.text = "Rank"; // string id : 18
            uiController.eloText.text = "*ELO Rating is calculated according to the FIDE Chess Rating System"; // string id : 19
            //
            uiController.settings2Text.text = "Settings"; // string id : 20
            uiController.volumeText.text = "Volume"; // string id : 21
            uiController.muteSoundText.text = "Mute Sound"; // string id : 22 
            uiController.muteMusicText.text = "Mute Music"; // string id : 23
            uiController.cameraModeText.text = "Camera Mode:"; // string id : 24
            uiController.cameraModePicker.options[0].text = "Normal";  // string id : 25
            uiController.cameraModePicker.options[1].text = "Follow";  // string id : 26
            uiController.cameraModeDropdownText.text = uiController.cameraModePicker.options[uiController.cameraModePicker.value].text; // set current camera mode label
            uiController.restartText.text = "Restart"; // string id : 27
            uiController.exitGameText.text = "Exit Game"; // string id : 28
            uiController.endGameConfirmationText.text = "Are you sure you want to end this game?"; // string id : 29
            uiController.forfeitText.text = "Forfeit"; // string id : 30
            uiController.cancelText.text = "Cancel"; // string id : 31
            //
            uiController.creditsTitleText.text = "Credits"; // string id : 32
            uiController.leadDevelopersText.text = "Lead Developers:"; // string id : 33
            uiController.musicText.text = "Music:"; // string id : 34
            uiController.localizationText.text = "Localization"; // string id : 35
            uiController.spanishText.text = "Spanish:"; // string id : 36
            //
            uiController.tribesText.text = "Tribes"; // string id : 37
            uiController.shopText.text = "Shop"; // string id : 38
            uiController.fightText.text = "Fight"; // string id : 39
            uiController.settings3Text.text = "Settings"; // string id : 40
            uiController.helpText.text = "Help"; // string id : 41
            //
            uiController.CurrentRoundText.text = "Round "; // string id : 42
            uiController.steamBuildVersionText.text = "Steam Build: "; // string id : 43
            //
            uiController.helperTipContainer.helperTips[0] = "Idle Chess Story is a rewarding rougelike strategy game!"; // string id : 47
            uiController.helperTipContainer.helperTips[1] = "Players take on enemy AI Players in a gauntlet where the goal is to reach the highest amount of rounds possible"; // string id : 48
            uiController.helperTipContainer.helperTips[2] = "Creatures from all over the Galaxy make their way to play on the annual idle chess tournament"; // string id : 49
            uiController.helperTipContainer.helperTips[3] = "In Idle Chess Story You take control of a team of chess units with special abilities to win over your opponents and increase your ranking"; // string id : 50
            uiController.helperTipContainer.helperTips[4] = "During the shopping phase, you chose what units you want to buy and place on the chess board"; // string id : 51
            uiController.helperTipContainer.helperTips[5] = "During the fighting phase, your units fight automatically against enemy units to the death"; // string id : 52
            uiController.helperTipContainer.helperTips[6] = "The player with the last unit surviving wins the duel and is rewarded a hefty gold bounty"; // string id : 53
            uiController.helperTipContainer.helperTips[7] = "Careful with losing all your units!Reach 0 HP and the game is over"; // string id : 54
            uiController.helperTipContainer.helperTips[8] = "Every unit belongs to two tribes. Gather enough units of the same tribe and unlock powerful bonuses for your allies"; // string id : 55
            uiController.helperTipContainer.helperTips[9] = "Having 3 or 6 units of the same tribe is enough to unlock theiir respective tribal bonuses";  // string id : 56
            uiController.helperTipContainer.helperTips[10] = "Use your gold wisely on units of the same type to quickly attain these bonuses to take control over your enemy"; // string id : 57
            uiController.helperTipContainer.helperTips[11] = "Work your way up the leaderboard rankings and earn all the steam achievements!"; // string id : 58
            uiController.helperTipContainer.helperTips[12] = "Your ranking reflects on your performance. Increase your knowledge of the game by mastering a tribe and crushing your opponents"; // string id : 59
            uiController.helperTipContainer.helperTips[13] = "Units strive to gain concentration, once they reach their max concentration, they release their inner energy, shifting the outcome of the battle"; // string id : 60
            uiController.helperTipContainer.helperTips[14] = "Abilities take maximum concentration to cast and reset concentration to zero."; // string id : 61
            uiController.helperTipContainer.helperTips[15] = "Units regen concentration slowly over time"; // string id : 62
            uiController.helperTipContainer.helperTips[16] = "Units regen concentration fastest from their auto attacks"; // string id : 63
            uiController.helperTipContainer.helperTips[17] = "Abilities rapidly shift the outcome of combat."; // string id : 64
            uiController.helperTipContainer.helperTips[18] = "Idle Chess Story features Steam leaderboard and achievements."; // string id : 65
            uiController.helperTipContainer.helperTips[19] = "There are 3 difficulties to try. Harder difficulties earn you more MMR"; // string id : 66
            uiController.helperTipContainer.helperTips[20] = "MMR stands for matchmaking rating and is a system used to rank your performance"; // string id : 67
            uiController.helperTipContainer.helperTips[21] = "Make sure to try the different camera modes!"; // string id : 68 
            uiController.helperTipContainer.helperTips[22] = "Is the sound too loud or annoying for you? Lower it or mute it in the settings menu"; // string id : 69
            uiController.helperTipContainer.helperTips[23] = "The settings menu can be accessed by pressing the escape key on your keyboard"; // string id : 70
            uiController.helperTipContainer.helperTips[24] = "Remember, ICS is an indie game made by 2 people. Independent game development take a long time."; // string id : 71
            uiController.helperTipContainer.helperTips[25] = "Follow us on Facebook https://www.facebook.com/Idle-Chess-Story-420264688708246/"; // string id : 72
            uiController.helperTipContainer.helperTips[26] = "Read the Tribe Panel to gain an understanding of tribal bonuses"; // string id : 73
            uiController.helperTipContainer.helperTips[27] = "Use the shop refresh button to gain new shopping options"; // string id : 74
            uiController.helperTipContainer.helperTips[28] = "Use the shop +1 unit button to gain a new unit slot"; // string id : 75
            uiController.helperTipContainer.helperTips[29] = "Your unit slots gradually increase as the rounds progress"; // string id : 76
            uiController.helperTipContainer.helperTips[30] = "Your progress won't be saved until you fully complete a round (Rougelike)"; // string id : 77
            uiController.helperTipContainer.helperTips[31] = "Combine 3 units of the same type to level them up";// string id : 78
            uiController.helperTipContainer.helperTips[32] = "Units can be level 1, level 2, or level 3"; // string id : 79
            uiController.helperTipContainer.helperTips[33] = "Every unit has an inventory to store items that you give them."; // string id : 80
            uiController.helperTipContainer.helperTips[34] = "Enemy units may randomly drop items on the floor for you to pick up."; // string id : 81
            //
            uiController.assassinTooltipText.text = "Assassins \n (3)Honor Among Thieves \n + Earn more Gold Per Kill \n (6)Black Ops \n + Increased Evasion on your Units"; // string id : 82
            uiController.beastTooltipText.text = "Beasts \n (3)Call of the Pack \n + Increased Critical Chance \n (6)Frenzied Inspiration \n + Units gain Lifesteal ability"; // string id : 83
            uiController.elementalTooltipText.text = "Elementals \n (3)Naturalist \n + Bonus Concentration Gain \n (6)Unlimited Power \n + Increased Physical and Magical dmg"; // string id : 84
            uiController.guardianTooltipText.text = "Guardians \n (3) Absorption \n + Dampen Physical Damage \n (6)Divine Brilliance \n + Bonus Concentration Regeneration"; // string id : 85
            uiController.structureTooltipText.text = "Beasts \n (3)Call of the Pack \n + Increased Critical Chance \n (6)Frenzied Inspiration \n + Units gain Lifesteal ability"; // string id : 86
            uiController.undeadTooltipText.text = "Undeads \n (3)Evil Ways \n - Reduced Enemy Armor \n (6)Dark Bargain \n + Increased Spell Damage"; // string id : 87
            uiController.warriorTooltipText.text = "Warriors \n (3)First Aid \n + Increased HP Regeneration \n (6)Barbaric Strength \n + Increased Physical Damage"; // string id : 88
            uiController.wizardTooltipText.text = "Wizards \n (3)Intellectual Advantage \n + Increased Magical Damage \n (6)Mastery of the Arts \n + Reduce Incoming Magical Dmg"; // string id : 89
            //
            uiController.selectedUnitPanel_InformationText_ARMOR.text = "Armor: "; // string id : 90
            uiController.selectedUnitPanel_InformationText_RETALIATION.text = "/ Retaliation: "; // string id : 91
            uiController.selectedUnitPanel_InformationText_CONCENTRATION.text = "Concentration: "; // string id : 92
            uiController.selectedUnitPanel_InformationText_TIER.text = "Level:"; // string id : 93 
            uiController.selectedUnitPanel_AbilityText.text = "Ability"; // string id : 94
            uiController.selectedUnitPanel_TribesText.text = "Tribes"; // string id : 95
            //
            uiController.randomTipText.text = "Random Tip"; // string id : 133
            uiController.help2Text.text = "Help"; // string id : 134
            //
            uiController.combatLogText.text = "Combat Log"; // string id : 135
            uiController.DPSText.text = "DPS"; // string id : 136
            //
            uiController.performanceReportText.text = "Performance Report:"; // string id : 137
            uiController.performanceReport2Text.text = "Performance Report:"; // string id : 138
            uiController.mainMenuText.text = "Main Menu"; // string id : 139
            uiController.mainMenu2Text.text = "Main Menu"; // string id : 140

        }
        else if (languageToSwapTo == Language.Spanish)
        {
         // 
            uiController.languageText.text = "Idioma:"; // string id : 1
            uiController.profileText.text = "Perfil:"; // string id : 2
            uiController.startGameText.text = "Iniciar juego"; // string id : 3
            uiController.leaderboardText.text = "Clasificación"; // string id : 4
            uiController.settingsText.text = "Configurar"; // string id : 5
            uiController.newsText0.text = "Muchas gracias a todas las personas que han jugado durante la etapa de acceso temprano. \n Estamos trabajando con la idea de agregar nuevas funciones para que el juego sea más divertido."; // string id : 6
            uiController.newsText1.text = "----------------------------- \n Patch notes for ((TBD: RELEASE DATE)) \n Added Spanish localization. \n ----------------------------- \n Puedes leer todas las notas en nuestra página de noticias en Steam"; // string id : 7
            uiController.creditsText.text = "Créditos"; // string id : 8
            uiController.newsText.text = "NUEVO"; // string id : 9
            uiController.difficultyText.text = "Dificultad:"; // string id : 10
            uiController.difficultyPicker.options[0].text = "Fácil"; // string id : 11
            uiController.difficultyPicker.options[1].text = "Media"; // string id : 12
            uiController.difficultyPicker.options[2].text = "Difícil"; // string id : 13
            uiController.difficultyDropdownText.text = uiController.difficultyPicker.options[uiController.difficultyPicker.value].text; // set current difficulty label
            uiController.idleChessStoryLogoText.text = "Idle Chess Story"; ; // string id : 14
            //
            uiController.leaderboard2Text.text = "***"; // string id : 15
            uiController.nameText.text = "***"; // string id : 16
            uiController.ratingText.text = "***"; // string id : 17
            uiController.rankText.text = "***"; // string id : 18
            uiController.eloText.text = "***"; // string id : 19
            //
            uiController.settings2Text.text = "***"; // string id : 20
            uiController.volumeText.text = "***"; // string id : 21
            uiController.muteSoundText.text = "***"; // string id : 22 
            uiController.muteMusicText.text = "***"; // string id : 23
            uiController.cameraModeText.text = "***"; // string id : 24
            uiController.cameraModePicker.options[0].text = "***";  // string id : 25
            uiController.cameraModePicker.options[1].text = "***";  // string id : 26
            uiController.cameraModeDropdownText.text = uiController.cameraModePicker.options[uiController.cameraModePicker.value].text; // set current camera mode label
            uiController.restartText.text = "***"; // string id : 27
            uiController.exitGameText.text = "***"; // string id : 28
            uiController.endGameConfirmationText.text = "***"; // string id : 29
            uiController.forfeitText.text = "***"; // string id : 30
            uiController.cancelText.text = "***"; // string id : 31
            //
            uiController.creditsTitleText.text = "***"; // string id : 32
            uiController.leadDevelopersText.text = "***"; // string id : 33
            uiController.musicText.text = "***"; // string id : 34
            uiController.localizationText.text = "***"; // string id : 35
            uiController.spanishText.text = "***"; // string id : 36
            //
            uiController.tribesText.text = "***"; // string id : 37
            uiController.shopText.text = "***"; // string id : 38
            uiController.fightText.text = "***"; // string id : 39
            uiController.settings3Text.text = "***"; // string id : 40
            uiController.helpText.text = "***"; // string id : 41
            //
            uiController.CurrentRoundText.text = "***"; // string id : 42
            uiController.steamBuildVersionText.text = "***"; // string id : 43
            //
            uiController.helperTipContainer.helperTips[0] = "***"; // string id : 47
            uiController.helperTipContainer.helperTips[1] = "***"; // string id : 48
            uiController.helperTipContainer.helperTips[2] = "***"; // string id : 49
            uiController.helperTipContainer.helperTips[3] = "***"; // string id : 50
            uiController.helperTipContainer.helperTips[4] = "***"; // string id : 51
            uiController.helperTipContainer.helperTips[5] = "***"; // string id : 52
            uiController.helperTipContainer.helperTips[6] = "***"; // string id : 53
            uiController.helperTipContainer.helperTips[7] = "***"; // string id : 54
            uiController.helperTipContainer.helperTips[8] = "***"; // string id : 55
            uiController.helperTipContainer.helperTips[9] = "***"; // string id : 56
            uiController.helperTipContainer.helperTips[10] = "***";// string id : 57
            uiController.helperTipContainer.helperTips[11] = "***"; // string id : 58
            uiController.helperTipContainer.helperTips[12] = "***"; // string id : 59
            uiController.helperTipContainer.helperTips[13] = "***"; // string id : 60
            uiController.helperTipContainer.helperTips[14] = "***"; // string id : 61
            uiController.helperTipContainer.helperTips[15] = "***"; // string id : 62
            uiController.helperTipContainer.helperTips[16] = "***"; // string id : 63
            uiController.helperTipContainer.helperTips[17] = "***"; // string id : 64
            uiController.helperTipContainer.helperTips[18] = "***"; // string id : 65
            uiController.helperTipContainer.helperTips[19] = "***"; // string id : 66
            uiController.helperTipContainer.helperTips[20] = "***"; // string id : 67
            uiController.helperTipContainer.helperTips[21] = "***"; // string id : 68
            uiController.helperTipContainer.helperTips[22] = "***"; // string id : 69
            uiController.helperTipContainer.helperTips[23] = "***"; // string id : 70
            uiController.helperTipContainer.helperTips[24] = "***"; // string id : 71
            uiController.helperTipContainer.helperTips[25] = "***"; // string id : 72
            uiController.helperTipContainer.helperTips[26] = "***"; // string id : 73
            uiController.helperTipContainer.helperTips[27] = "***"; // string id : 74 
            uiController.helperTipContainer.helperTips[28] = "***"; // string id : 75
            uiController.helperTipContainer.helperTips[29] = "***"; // string id : 76
            uiController.helperTipContainer.helperTips[30] = "***"; // string id : 77
            uiController.helperTipContainer.helperTips[31] = "***"; // string id : 78
            uiController.helperTipContainer.helperTips[32] = "***"; // string id : 79
            uiController.helperTipContainer.helperTips[33] = "***"; // string id : 80
            uiController.helperTipContainer.helperTips[34] = "***"; // string id : 81
            //
            uiController.assassinTooltipText.text = "***"; // string id : 82
            uiController.beastTooltipText.text = "***"; // string id : 83
            uiController.elementalTooltipText.text = "***"; // string id : 84
            uiController.guardianTooltipText.text = "***"; // string id : 85
            uiController.structureTooltipText.text = "***"; // string id : 86
            uiController.undeadTooltipText.text = "***"; // string id : 87
            uiController.warriorTooltipText.text = "***"; // string id : 88
            uiController.wizardTooltipText.text = "***"; // string id : 89
            //
            uiController.selectedUnitPanel_InformationText_ARMOR.text = "***"; // string id : 90
            uiController.selectedUnitPanel_InformationText_RETALIATION.text = "***"; // string id : 91
            uiController.selectedUnitPanel_InformationText_CONCENTRATION.text = "***"; // string id : 92
            uiController.selectedUnitPanel_InformationText_TIER.text = "***"; // string id : 93
            uiController.selectedUnitPanel_AbilityText.text = "***"; // string id : 94
            uiController.selectedUnitPanel_TribesText.text = "***"; // string id : 95
            //
            uiController.randomTipText.text = "***"; // string id : 133
            uiController.help2Text.text = "***"; // string id : 134
            //
            uiController.combatLogText.text = "***"; // string id : 135
            uiController.DPSText.text = "***"; // string id : 136
                                               //
            uiController.performanceReportText.text = "***"; // string id : 137
            uiController.performanceReport2Text.text = "***"; // string id : 138
            uiController.mainMenuText.text = "***"; // string id : 139
            uiController.mainMenu2Text.text = "***"; // string id : 140
        }
    }
}
