using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Variables for each UI element requiring a reference
    [SerializeField] TextMeshProUGUI foodItemCountText;
    [SerializeField] TextMeshProUGUI timeRemaining;
    [SerializeField] TextMeshProUGUI targetGoalText;
    [SerializeField] TextMeshProUGUI initialTargetGoalText;
    [SerializeField] TextMeshProUGUI tipsText;
    [SerializeField] TextMeshProUGUI causeText;
    [SerializeField] TextMeshProUGUI remainingTimeStat;
    [SerializeField] TextMeshProUGUI timePickUpNotification;

    // String array to randomise tips
    string[] tips = { "Throw food at customers chasing you to stun them", "Pick up pink tokens to gain an extra 10 seconds", "Pick up burgers to gain an additional food item", "Shut doors behind you to slow down enemy customers" };

    // Script references
    FireProjectiles fireProjectilesScript;
    GameManager gameManager;
    PlayerMovement playerMovementScript;

    IEnumerator coroutineRef; // Global variable to store a coroutine for better management

    // Start is called before the first frame update
    void Start()
    {
        timePickUpNotification.enabled = false;

        fireProjectilesScript = FindAnyObjectByType<FireProjectiles>();
        gameManager = FindAnyObjectByType<GameManager>();
        playerMovementScript = FindAnyObjectByType<PlayerMovement>();

        initialTargetGoalText.text = "Feed " + gameManager.customersToFeed + " customers in " + gameManager.timeRemaining + " seconds";

        int tipsIndex = Random.Range(0, tips.Length);
        tipsText.text = tips[tipsIndex];

    }

    // Update is called once per frame
    void Update()
    {
        float roundedTime =  Mathf.Round(gameManager.timeRemaining);
        foodItemCountText.text = fireProjectilesScript.foodItems.ToString();
        timeRemaining.text = roundedTime.ToString() + " s";
        targetGoalText.text = gameManager.customersFed + " / " + gameManager.customersToFeed;
        causeText.text = gameManager.gameOverCauses[gameManager.gameOverCausesIndex].ToString();

        remainingTimeStat.text = "TIME REMAINING: " +  roundedTime.ToString() + " s";

        // Change text colour based on time remaining and food items left

        if (gameManager.timeRemaining <= 30)
        {
            timeRemaining.color = Color.yellow;
        } 
        else if (gameManager.timeRemaining > 30)
        {
            timeRemaining.color = Color.white;
        }

        if (gameManager.timeRemaining <= 10)
        {
            timeRemaining.color = Color.red;
        } 

        if (fireProjectilesScript.foodItems <= 5)
        {
            foodItemCountText.color = Color.yellow;
        } 
        else if (fireProjectilesScript.foodItems > 5)
        {
            foodItemCountText.color = Color.white;
        }

        if (fireProjectilesScript.foodItems <= 0)
        {
            foodItemCountText.color = Color.red;
        }
    }

    public void DisplayTimeCollected()
    {
        // If another time pickup is collected before the coroutine is fully executed, stop it
        if (coroutineRef != null) 
        {
            StopCoroutine(coroutineRef);
        }
        // Display a text notification letting the player know a time pickup was collected
        timePickUpNotification.enabled = true;
        timePickUpNotification.text = "+ " + playerMovementScript.timeIncrease.ToString() + " s";
        // Store the coroutine as a reference to the global variable and start it
        coroutineRef = HideTimeCollected();
        StartCoroutine(coroutineRef);
    }

    // After some time, hide the notification
    IEnumerator HideTimeCollected()
    {
        yield return new WaitForSeconds(2);
        timePickUpNotification.enabled = false;
    }
}
