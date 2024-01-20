using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    public int index;
    private Button _button;
    private Dialogues _dialogues;
    private UnityAction _clickAction;
    void Start()
    {
        _button = GetComponent<Button>();
        _dialogues = FindObjectOfType<Dialogues>();
        _clickAction = new UnityAction(() => _dialogues.ChoiceButtonAction(index));
        _button.onClick.AddListener(_clickAction);
    }
}
