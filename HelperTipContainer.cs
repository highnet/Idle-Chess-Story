using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperTipContainer : MonoBehaviour
{

    public string[] helperTips;
    public int totalNumberOfTips = 18;
    public string currentTip;
    public int currentTipIndex = 0;

    void Awake()
    {
        helperTips = new string[totalNumberOfTips];
        helperTips[0] = "Highnet Auto Chess is a rewarding strategy game!";
        helperTips[1] = "Players take on enemy AI Players in a gauntlet where the goal is to reach the highest amount of rounds possible";
        helperTips[2] = "Creatures from all over the Galaxy make their way to play on Highnet's annual auto chess tournament";
        helperTips[3] = "In Highnet Auto Chess You take control of a team of chess units with special abilities to win over your opponents and increase your rank";
        helperTips[4] = "During the shopping phase, you chose what units you want to buy and place on the chess board";
        helperTips[5] = "During the fighting phase, your units fight automatically against enemy units to the death";
        helperTips[6] = "The player with the last unit surviving wins the duel and is rewarded a hefty gold bounty";
        helperTips[7] = "Careful with losing too many units! For every unit that dies you lose 1 HP. Reach 0 HP and the game is over";
        helperTips[8] = "Every unit belongs to two tribes. Gather enough units of the same tribe and unlock powerful bonuses for your allies";
        helperTips[9] = "Having 3, 6 or 9 units of the same tribe is enough to unlock the respective tribal bonuses";
        helperTips[10] = "Use your gold wisely on units of the same type to quickly attain these bonuses to take control over your enemy";
        helperTips[11] = "You begin as a mere Pawn and you must work your way up the ranks of Knight, Bishop, Rook and Queen to finally reach the prestigious rank of King!";
        helperTips[12] = "Your ranking reflects on your performance. Increase your knowledge of the game by mastering a tribe and crushing your opponents";
        helperTips[13] = "Units strive to gain concentration, once they reach their max concentration, they release their inner energy, shifting the outcome of the battle";
        helperTips[14] = "Abilities take maximum concentration to cast and reset concentration to zero.";
        helperTips[15] = "Units regen concentration slowly over time";
        helperTips[16] = "Units regen concentration fastest from their auto attacks (think life steal but mana)";
        helperTips[17] = "Abilities rapidly shift the outcome of combat. Higher tiered units have scaled up power on their abilities.";

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
