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
        greenManager.CompareAllSlotItems();
        
    }

}
