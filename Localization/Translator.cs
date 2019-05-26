using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Language { English, Spanish, NONE }
public class Translator : MonoBehaviour
{
    public UiController uiController;
    public Dropdown languagePicker;

    private void Start()
    {
        languagePicker.onValueChanged.RemoveAllListeners();
        languagePicker.onValueChanged.AddListener(delegate { SwapLanguage(true,Language.NONE); });
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
            // <MAIN MENU ELEMENTS ENGLISH>
            uiController.languageSelectedText.text = "Language:"; // string id : 1
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
        }
        else if (languageToSwapTo == Language.Spanish)
        {
            // <MAIN MENU ELEMENTS SPANISH>
            uiController.languageSelectedText.text = "***"; // string id: 1
            uiController.profileText.text = "***"; // string id: 2
            uiController.startGameText.text = "***"; // string id: 3
            uiController.leaderboardText.text = "***"; // string id : 4
            uiController.settingsText.text = "***"; // string id : 5
            uiController.newsText0.text = "***"; // string id : 6
            uiController.newsText1.text = "***"; // string id : 7
            uiController.creditsText.text = "***"; // string id : 8
            uiController.newsText.text = "***"; // string id : 9
            uiController.difficultyText.text = "***"; // string id : 10
            uiController.difficultyPicker.options[0].text = "***"; // string id : 11
            uiController.difficultyPicker.options[1].text = "***"; // string id : 12
            uiController.difficultyPicker.options[2].text = "***"; // string id : 13
            uiController.difficultyDropdownText.text = uiController.difficultyPicker.options[uiController.difficultyPicker.value].text; // set current difficulty label
            uiController.idleChessStoryLogoText.text = "***"; ; // string id : 14
            //
        }
    }
}
