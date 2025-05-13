using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatReportBar : MonoBehaviour
{
    public CombatReport combatReport;
    public GameObject t1Star;
    public GameObject t2Star;
    public GameObject t3Star;
    public Text nameText;
    public Text dpsText;
    public Image bar;
    public float parseTime;
    public float totalDamage;
    public float topDamage;
    public GameObject visualScaler;
    public Color unitColor;

    private void Start()
    {
        /*
        int colorsRNG = UnityEngine.Random.Range(0, 5);
        if (colorsRNG == 0)
        {
            bar.color = Color.red;
        }
       else if (colorsRNG == 1)
        {
            bar.color = Color.blue;

        }
        else if (colorsRNG == 2)
        {
            bar.color = Color.gray;

        }
        else if (colorsRNG == 3)
        {
            bar.color = Color.green;

        }
        else if (colorsRNG == 4)
        {
            bar.color = Color.magenta;

        }
        else
        {
            bar.color = Color.black;
        }
        */

    }

    public void SetUIElements()
    {

        if (combatReport != null)
        {
        int tier = combatReport.npcTier;
        if (tier == 1)
        {
            t1Star.gameObject.SetActive(true);
        }
        if (tier == 2)
        {
                t1Star.gameObject.SetActive(true);
                t2Star.gameObject.SetActive(true);
        }
        if (tier == 3)
        {
            t1Star.gameObject.SetActive(true);
                t2Star.gameObject.SetActive(true);
                t3Star.gameObject.SetActive(true);
            }

        nameText.text = combatReport.npcName;
        parseTime = combatReport.parseTime;

         float TotalDamagePercentage = (combatReport.damageDealt / totalDamage) * 100f;
         float TopDamagePercentage = (combatReport.damageDealt / topDamage) * 100f;

            if (parseTime != 0)
            {
                dpsText.text = Mathf.Round(combatReport.damageDealt / parseTime).ToString() + " (" + Mathf.Round(TotalDamagePercentage) + "%)";
            }
          visualScaler.transform.localScale =  new Vector3(TopDamagePercentage / 100f, 1, 1) ;
            bar.color = unitColor;
        }


    }

}
