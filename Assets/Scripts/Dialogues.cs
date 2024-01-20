using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Dialogues : MonoBehaviour
{
    private Story _currentStory;
    private TextAsset _inkJson;

    private GameObject _dialoguePanel;
    private TextMeshProUGUI _dialogueText;
    private TextMeshProUGUI _nameText;

    [HideInInspector] public GameObject _choiceButtonsPanel;
    private GameObject _choiceButton;
    private List<TextMeshProUGUI> _choicesText = new();
    private List<Character> characters = new();
    private float multiplier = 1.1f;

    public bool DialogPlay { get; private set; }
    [Inject]
    public void Construct(DialoguesInstaller dialoguesInstaller)
    {
        _inkJson = dialoguesInstaller.inkJson;
        _dialoguePanel = dialoguesInstaller.dialoguePanel;
        _dialogueText = dialoguesInstaller.dialogueText;
        _nameText = dialoguesInstaller.nameText;
        _choiceButtonsPanel = dialoguesInstaller.choiceButtonsPanel;
        _choiceButton = dialoguesInstaller.choiceButton;
    }
    private void Awake()
    {
        _currentStory = new Story(_inkJson.text);
    }
    void Start()
    {
        foreach (var character in FindObjectsOfType<Character>())
        {
            characters.Add(character);
        }
        StartDialogue();
    }
    public void StartDialogue()
    {
        DialogPlay = true;
        _dialoguePanel.SetActive(true);
        ContinueStory();
    }
    public void ContinueStory(bool choiceBefore = false)
    {
        if (_currentStory.canContinue)
        {
            ShowDialogue();
            ShowChoiceButtons();
        }
        else if(!choiceBefore)
        {
            ExitDialogue();
        }
    }
    private void ShowDialogue()
    {
        _dialogueText.text = _currentStory.Continue();
        _nameText.text = (string)_currentStory.variablesState["characterName"];
        var index = characters.FindIndex(character => character.characterName.Contains(_nameText.text));
        characters[index].ChangeEmotion((int)_currentStory.variablesState["characterExpression"]);
        ChangeCharacterScale(index);
    }
    private void ChangeCharacterScale(int indexCharacter)
    {
        if (indexCharacter>=0)
        {
            foreach (var character in characters)
            {
                if (character!=characters[indexCharacter])
                {
                    character.ResetScale();
                }
                else if(character.DefaultScale == character.transform.localScale)
                {
                    character.ChangeScale(multiplier);
                }
            }
        }
        else
        {
            characters.ForEach(character => character.ResetScale());
        }
    }
    private void ShowChoiceButtons()
    {
        List<Choice> currentChoices = _currentStory.currentChoices;
        _choiceButtonsPanel.SetActive(currentChoices.Count != 0);
        if (currentChoices.Count<=0){return;}
        _choiceButtonsPanel.transform.Cast<Transform>().ToList().ForEach(child => Destroy(child.gameObject));
        _choicesText.Clear();
        for (int i = 0; i < currentChoices.Count; i++)
        {
            GameObject choice = Instantiate(_choiceButton);
            choice.GetComponent<ButtonAction>().index = i;
            choice.transform.SetParent(_choiceButtonsPanel.transform);

            TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = currentChoices[i].text;
            _choicesText.Add(choiceText);
        }
    }
    public void ChoiceButtonAction(int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory(true);
    }
    private void ExitDialogue()
    {
        DialogPlay = false;
        _dialoguePanel.SetActive(false);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex<=SceneManager.sceneCount)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

}
