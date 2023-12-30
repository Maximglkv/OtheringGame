using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenManager : MonoBehaviour
{
    // Method to compare two items based on tags
    private bool CompareTwoSlotItems(string tag1, string tag2)
    {
        GameObject slot1 = GameObject.FindWithTag(tag1);
        GameObject slot2 = GameObject.FindWithTag(tag2);

        if (slot1 == null || slot2 == null)
        {
            Debug.LogError($"One or both of the slots for tags {tag1} and {tag2} are null.");
            return false;
        }

        InventoryItem itemInSlot1 = slot1.GetComponentInChildren<InventoryItem>();
        InventoryItem itemInSlot2 = slot2.GetComponentInChildren<InventoryItem>();

        if (itemInSlot1.item == null || itemInSlot2.item == null)
        {
            Debug.LogError($"One or both of the InventoryItems for tags {tag1} and {tag2} are null or their item fields are not set.");
            return false;
        }

        return itemInSlot1.item.IDNumba == itemInSlot2.item.IDNumba;
    }

    // Method to compare all pairs of items based on an array of tags
    public bool CompareAllSlotItems()
{
    // Array of tag pairs for comparison
    string[,] tagPairs = new string[,]
    {
        { "pig 2", "pig" },
        { "Check Item 2", "InvenSlot2" },
        { "Check Item 3", "InvenSlot3" },
        { "Check Item 4", "InvenSlot4" }
    };

    for (int i = 0; i < tagPairs.GetLength(0); i++)
    {
        string tag1 = tagPairs[i, 0];
        string tag2 = tagPairs[i, 1];

        bool match = CompareTwoSlotItems(tag1, tag2);
        Debug.Log($"Comparison between slots '{tag1}' and '{tag2}': {match}");

        // If any pair doesn't match, return false
        if (!match)
        {
            Debug.Log("Not all items match.");
            return false;
        }
    }

    // If the loop completes without returning false, all items match
    Debug.Log("All items match.");
    return true;
}
}