using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuntimeItemAssigner : MonoBehaviour
{
    public InventoryItem inventoryItemPrefab;

    void Start()
    {
        // Assign a sprite to each tagged object
        AssignItemToSlot("pig 2", 1);
        AssignItemToSlot("Check Item 2", 2);
        AssignItemToSlot("Check Item 3", 3);
        AssignItemToSlot("Check Item 4", 4);
    }

    void AssignItemToSlot(string tag, int id)
    {
        Item newItem = ScriptableObject.CreateInstance<Item>();
        newItem.IDNumba = id; // Assign necessary properties
        InventoryItem newInventoryItem = Instantiate(inventoryItemPrefab);

        // Set the newItem to the item field of the InventoryItem
        newInventoryItem.item = newItem;
        GameObject parentObject = GameObject.FindWithTag(tag);
    
        if (parentObject != null)
        {
            newInventoryItem.transform.SetParent(parentObject.transform, false);  

            // Disable the Image component
            Image imageComponent = newInventoryItem.GetComponentInChildren<Image>();
            if (imageComponent != null)
            {
                imageComponent.enabled = false;
            }
        }

    }
}