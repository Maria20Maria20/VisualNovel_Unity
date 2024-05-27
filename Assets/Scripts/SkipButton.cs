using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{
    private Button _button;

    private Dialogues _dialogues;
    void Start()
    {
        _dialogues = FindObjectOfType<Dialogues>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SkipDialog);
    }

    private void SkipDialog()
    {
        while (!_dialogues.choiceButtonsPanel.activeInHierarchy)
        {
            _dialogues.ContinueStory(_dialogues.choiceButtonsPanel.activeInHierarchy);
        }
    }
}
