using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIPromptPanel : MonoBehaviour
{
    public Button ConfirmButton;
    public Button CancelButton;

    // Start is called before the first frame update
    void Start()
    {
        ConfirmButton.onClick.RemoveAllListeners();
        ConfirmButton.onClick.AddListener(delegate { Confirm(); });

        CancelButton.onClick.RemoveAllListeners();
        CancelButton.onClick.AddListener(delegate { Cancel(); });
    }

    void Confirm()
    {
        GameObject.Find("World Controller").GetComponent<UiController>().CalculateMMRAndRestartScene();

    }

    void Cancel()
    {
        this.gameObject.SetActive(false);
    }

}
