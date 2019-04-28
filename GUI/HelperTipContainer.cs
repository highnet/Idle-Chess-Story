using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperTipContainer : MonoBehaviour
{

    public string[] helperTips;
    private int totalNumberOfTips = 35;
    public string currentTip;
    public int currentTipIndex = 0;

    void Awake()
    {
        helperTips = new string[totalNumberOfTips];
        helperTips[0] = "Idle Chess Story is a rewarding rougelike strategy game!";
        helperTips[1] = "Players take on enemy AI Players in a gauntlet where the goal is to reach the highest amount of rounds possible";
        helperTips[2] = "Creatures from all over the Galaxy make their way to play on the annual idle chess tournament";
        helperTips[3] = "In Idle Chess Story You take control of a team of chess units with special abilities to win over your opponents and increase your ranking";
        helperTips[4] = "During the shopping phase, you chose what units you want to buy and place on the chess board";
        helperTips[5] = "During the fighting phase, your units fight automatically against enemy units to the death";
        helperTips[6] = "The player with the last unit surviving wins the duel and is rewarded a hefty gold bounty";
        helperTips[7] = "Careful with losing all your units!Reach 0 HP and the game is over";
        helperTips[8] = "Every unit belongs to two tribes. Gather enough units of the same tribe and unlock powerful bonuses for your allies";
        helperTips[9] = "Having 3 or 6 units of the same tribe is enough to unlock theiir respective tribal bonuses";
        helperTips[10] = "Use your gold wisely on units of the same type to quickly attain these bonuses to take control over your enemy";
        helperTips[11] = "Work your way up the leaderboard rankings and earn all the steam achievements!";
        helperTips[12] = "Your ranking reflects on your performance. Increase your knowledge of the game by mastering a tribe and crushing your opponents";
        helperTips[13] = "Units strive to gain concentration, once they reach their max concentration, they release their inner energy, shifting the outcome of the battle";
        helperTips[14] = "Abilities take maximum concentration to cast and reset concentration to zero.";
        helperTips[15] = "Units regen concentration slowly over time";
        helperTips[16] = "Units regen concentration fastest from their auto attacks";
        helperTips[17] = "Abilities rapidly shift the outcome of combat.";
        helperTips[18] = "Idle Chess Story features Steam leaderboard and achievements.";
        helperTips[19] = "There are 3 difficulties to try. Harder difficulties earn you more MMR";
        helperTips[20] = "MMR stands for matchmaking rating and is a system used to rank your performance";
        helperTips[21] = "Make sure to try the different camera modes!";
        helperTips[22] = "Is the sound too loud or annoying for you? Lower it or mute it in the settings menu";
        helperTips[23] = "The settings menu can be accessed by pressing the escape key on your keyboard";
        helperTips[24] = "Remember, ICS is an indie game made by 2 people. Independent game development take a long time.";
        helperTips[25] = "Follow us on Facebook https://www.facebook.com/Idle-Chess-Story-420264688708246/";
        helperTips[26] = "Read the Tribe Panel to gain an understanding of tribal bonuses";
        helperTips[27] = "Use the shop refresh button to gain new shopping options";
        helperTips[28] = "Use the shop +1 unit button to gain a new unit slot";
        helperTips[29] = "Your unit slots gradually increase as the rounds progress";
        helperTips[30] = "Your progress won't be saved until you fully complete a round (Rougelike)";
        helperTips[31] = "Combine 3 units of the same type to level them up";
        helperTips[32] = "Units can be level 1, level 2, or level 3";
        helperTips[33] = "Every unit has an inventory to store items that you give them.";
        helperTips[34] = "Enemy units may randomly drop items on the floor for you to pick up.";
        RefreshTip();
    }

    public void RandomTip()
    {
        int previousTip = currentTipIndex;
        while (currentTipIndex == previousTip)
        {
            currentTipIndex = UnityEngine.Random.Range(0, totalNumberOfTips);
        }
        RefreshTip();
    }

    public void NextTip()
    {
        currentTipIndex++;
        if (currentTipIndex == totalNumberOfTips)
        {
            currentTipIndex = 0;
        }
        RefreshTip();
    }

  public void PreviousTip()
    {
        currentTipIndex--;
        if (currentTipIndex < 0)
        {
            currentTipIndex = totalNumberOfTips - 1;
        }
        RefreshTip();
    }

   public void RefreshTip()
    {
        currentTip = helperTips[currentTipIndex];
    }

}
