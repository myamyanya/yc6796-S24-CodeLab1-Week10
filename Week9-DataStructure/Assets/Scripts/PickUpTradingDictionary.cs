using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTradingDictionary : MonoBehaviour
{
    // Create the dictionary for storing resources
    public Dictionary<string, int> resourcesOwned = new Dictionary<string, int>();
    
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

    // If the player collides with an interactive item, pick it up
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Coin":
                //Debug.Log("Coin + 1");
                AddResource(other.gameObject.tag, 10, other.gameObject);
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
}
