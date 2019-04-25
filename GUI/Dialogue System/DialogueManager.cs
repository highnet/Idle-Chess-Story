using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public UiController uic;
     Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        uic = GetComponent<UiController>();
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        uic.dialoguePanel.gameObject.SetActive(true);
        uic.hudCanvasAudioSource.PlayOneShot(uic.genericButtonSucessAudioClip);
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            this.sentences.Enqueue(sentence);
        }
        uic.dialoguePanelSpeakerNameText.text = dialogue.speakerName;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        uic.hudCanvasAudioSource.PlayOneShot(uic.genericButtonSucessAudioClip);

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        } else
        {
            uic.dialoguePanelDialogueText.text = sentences.Dequeue();
        }
    }

    public void EndDialogue()
    {
        uic.dialoguePanel.gameObject.SetActive(false);
    }


}
