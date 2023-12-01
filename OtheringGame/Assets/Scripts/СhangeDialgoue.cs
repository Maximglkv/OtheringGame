using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СhangeDialgoue : MonoBehaviour
{
    public DialogeTrigger dialogeTrigger;
    public TextAsset newDialogue;

    public void ChangeDialogue()
    {
        dialogeTrigger.inkJSON = newDialogue;
    }
}
