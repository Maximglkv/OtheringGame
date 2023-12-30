using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Checker : MonoBehaviour
{
    public GreenManager greenManager;

    public GameObject incorrect;
    public GameObject correct;
    
    public void Comparison()
    {
        bool result = greenManager.CompareAllSlotItems();
        
        if(result)
        {
            correct.SetActive(false);
        }

        else 
        {
            incorrect.SetActive(true);
        }
    }

}
