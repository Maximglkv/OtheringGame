using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private GameObject RythmMinigame;
    [SerializeField] private GameObject FeedMinigame;
    [SerializeField] private GameObject PillMinigame;
    [SerializeField] private GameObject Park_BG;
    [SerializeField] private GameObject Dark_BG;
    [SerializeField] private GameObject Neighbour_BG;
    [SerializeField] private GameObject Credits_BG;
    [SerializeField] private float typingSpeed = 0.04f;
    [SerializeField] private GameObject Volume;
    [SerializeField] public TextAsset IntroDialogue;
    private Animator layoutAnimator;
  //  public Fadeandstuff fader;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    private bool canContinueToNextLine = false;
    public bool dialogueIsPlaying { get; set; }

    private static DialogueManager instance;
    private Coroutine displayLineCoroutine;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get the layout animator
        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        DialogueManager.GetInstance().EnterDialogueMode(IntroDialogue);
    }

    private void Update()
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            Volume.SetActive(false);
            return;
            
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (canContinueToNextLine && currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }

        if (dialogueIsPlaying)
        {
            Volume.SetActive(true);
        }
        
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        
        
       
        currentStory.BindExternalFunction("RythmGame", () =>
        {
            RythmMinigame.SetActive(true);
            dialogueIsPlaying = true;


        });
        currentStory.BindExternalFunction("FeedGame", () =>
        {
            FeedMinigame.SetActive(true);
            dialogueIsPlaying = true;
        });
        currentStory.BindExternalFunction("PillGame", () =>
        {
            PillMinigame.SetActive(true);
            dialogueIsPlaying = true;
            Volume.SetActive(true);
        });
        currentStory.BindExternalFunction("Park_BG", () =>
        {
            Park_BG.SetActive(true);
            Neighbour_BG.SetActive(false);
            dialogueIsPlaying = true;


        });
        currentStory.BindExternalFunction("Neighbour_BG", () =>
        {
            Park_BG.SetActive(false);
            Neighbour_BG.SetActive(true);
            dialogueIsPlaying = true;


        });
        currentStory.BindExternalFunction("Credits_BG", () =>
        {
           // Park_BG.SetActive(false);
            Credits_BG.SetActive(true);
            dialogueIsPlaying = true;


        });
        currentStory.BindExternalFunction("Dark_BG", () =>
        {
            
            Dark_BG.SetActive(true);
            dialogueIsPlaying = true;


        });


        // reset portrait, layout, and speaker
        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        currentStory.UnbindExternalFunction("RythmGame");
        currentStory.UnbindExternalFunction("FeedGame");
        currentStory.UnbindExternalFunction("PillGame");
        currentStory.UnbindExternalFunction("Park_BG");
        currentStory.UnbindExternalFunction("Dark_BG");
        currentStory.UnbindExternalFunction("Neighbour_BG");
        currentStory.UnbindExternalFunction("Credits_BG");
        Volume.SetActive(true);
        Park_BG.SetActive(false);
        Neighbour_BG.SetActive(false);
        Dark_BG.SetActive(false);


        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            
            // set text for the current dialogue line
            string nextLine = currentStory.Continue();
            // handle tags
           
            
            if (nextLine.Equals("") && !currentStory.canContinue) 
            {
                StartCoroutine(ExitDialogueMode()); 
            }
            else
            {
                HandleTags(currentStory.currentTags);
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
            

        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }
    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        canContinueToNextLine = false;
        HideChoices();
        foreach (char letter in line.ToCharArray())
        {
            if (InputManager.GetInstance().GetSubmitPressed())
            {
                dialogueText.text = line;
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        // display choices, if any, for this dialogue line
        DisplayChoices();

        canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            InputManager.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script
            ContinueStory();
        }
        
    }

}