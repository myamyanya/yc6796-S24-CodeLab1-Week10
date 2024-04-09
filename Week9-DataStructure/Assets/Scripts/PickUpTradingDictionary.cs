using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTradingDictionary : MonoBehaviour
{
    // Create the dictionary for storing resources
    public Dictionary<string, int> resourcesOwned = new Dictionary<string, int>();
    // Create the dictionary for recording resources values
    private Dictionary<string, int> resourcesValue = new Dictionary<string, int>();
    public int totalValue = 0;
    
    // Create the dictionary for storing products being traded
    private Dictionary<string, int> productsOwned = new Dictionary<string, int>();
    
    // Making the script into singleton so that data can be carried over scenes
    public static PickUpTradingDictionary instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Reset resources values
        resourcesValue.Add("Coin", 100);
        resourcesValue.Add("Berry", 10);
        resourcesValue.Add("Orange", 5);
        resourcesValue.Add("Leaf", 1);
    }

    // If the player collides with an interactive item, pick it up
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Coin":
                //Debug.Log("Coin + 1");
                AddResource(other.gameObject.tag, 1, other.gameObject);
                break;
            case "Berry":
                AddResource(other.gameObject.tag, 3, other.gameObject);
                break;
            case "Orange":
                AddResource(other.gameObject.tag, 2, other.gameObject);
                break;
            case "Leaf":
                AddResource(other.gameObject.tag, 1, other.gameObject);
                break;
        }
    }

    // Adding resource to the dictionary
    private void AddResource(string resourceName, int amt, GameObject item)
    {
        if (resourcesOwned.ContainsKey(resourceName))
        {
            resourcesOwned[resourceName] = resourcesOwned[resourceName] + amt;
        }
        else
        {
            resourcesOwned.Add(resourceName, amt);
        }
        
        Debug.Log(resourceName + ": " + resourcesOwned[resourceName]);
        
        // Destroy after picked up
        Destroy(item);
    }
    
    // Sell all the resources in exchange for money
    public void SellResources()
    {
        Debug.Log("Try to sell something...");

        // Checking the resources values
        foreach (KeyValuePair<string, int> item in resourcesOwned)
        {
            string itemName = item.Key;
            int itemAmt = item.Value;

            if (resourcesValue.ContainsKey(itemName))
            {
                int itemValue = resourcesValue[itemName];
                totalValue += itemValue * itemAmt;
            }
        }
        
        Debug.Log("Total value of the inventory: " + totalValue);
        
        GameManager.instance.income.text = "Your total income is " + totalValue + " !!";
    }
}
