using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Status
{
    Beginning,
    Playing,
    End
}

public class GameManager : MonoBehaviour
{
    // For checking the status of the game
    public Status status;
    private bool isEnd = false;
    
    // For checking the end state
    public GameObject endPoint;
    
    // Making GM into a singleton
    public static GameManager instance;
    
    // Wiring the UI
    public TextMeshProUGUI display;
    public Button sellButton;

    public TextMeshProUGUI income;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // For displaying the resources 
        if (SceneManager.GetActiveScene().name != "End")
        {
            if (PickUpTradingDictionary.instance.resourcesOwned != null)
            {
                display.text = "You have ... \n";

                foreach (KeyValuePair<string, int> keyValuePair in PickUpTradingDictionary.instance.resourcesOwned)
                {
                    display.text += "\n" + keyValuePair.Key + " : " +
                                    PickUpTradingDictionary.instance.resourcesOwned[keyValuePair.Key];
                }
            }
            else
            {
                // Don't show inventory if it is empty
                display.text = " ";
            }

            // Hide the sell button and money UI display
            sellButton.gameObject.SetActive(false);
            income.text = " ";
        }
        else
        {
            // Hide inventory
            display.text = " ";
        }
        
        // If the game reaches the end stage, end the game
        if (status == Status.End && !isEnd)
        {
            EndGame();
            isEnd = true;
        }
    }

    void EndGame()
    {
        SceneManager.LoadScene("End");
        
        // Show sell button and money UI
        sellButton.gameObject.SetActive(true);
        sellButton.GetComponent<Button>().enabled = true;
    }
}
