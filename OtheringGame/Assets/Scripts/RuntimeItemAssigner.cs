using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuntimeItemAssigner : MonoBehaviour
{
    public InventoryItem inventoryItemPrefab;
    //public Sprite[] itemSprites; // Array to hold sprites for each ID
    public Image[] itemImages; // Array to hold Image components for each ID


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
        if (id <= 0 || id > itemImages.Length)
        {
            Debug.LogError("Invalid ID or Sprite not assigned for ID: " + id);
            return;
        }

        Item newItem = ScriptableObject.CreateInstance<Item>();
        newItem.IDNumba = id; // Assign necessary properties
        InventoryItem newInventoryItem = Instantiate(inventoryItemPrefab);

        // Set the newItem to the item field of the InventoryItem
        newInventoryItem.item = newItem;
        GameObject parentObject = GameObject.FindWithTag(tag);
        Image assignedImage = itemImages[id - 1]; // Assuming IDs start from 1
        if (parentObject != null)
        {
            // Set the newInventoryItem as a child of the parentObject
            newInventoryItem.transform.SetParent(parentObject.transform, false);

            Image imageComponent = parentObject.GetComponentInChildren<Image>();
            if (imageComponent != null)
            {
                // Set the sprite of the parentObject's Image component to match the assigned Image
                imageComponent.sprite = assignedImage.sprite;
            }
            
        }

    }
}
