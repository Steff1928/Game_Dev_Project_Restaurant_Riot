using SojaExiles;
using System.Collections;
using TMPro;
using UnityEngine;

// Contains general UI Logic
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
        timePickUpNotification.enabled = false; // Disable timePickUpNotification by default

        // Find the associated scripts
        fireProjectilesScript = FindAnyObjectByType<FireProjectiles>();
        gameManager = FindAnyObjectByType<GameManager>();
        playerMovementScript = FindAnyObjectByType<PlayerMovement>();

        // Assign the initialTargetGoal text to the customerToFeed and timeRemaining variables from the Game Manager
        initialTargetGoalText.text = "Feed " + gameManager.customersToFeed + " customers in " + gameManager.timeRemaining + " seconds";

        // Randomise a tip to display on startup
        int tipsIndex = Random.Range(0, tips.Length);
        tipsText.text = tips[tipsIndex];

    }

    // Update is called once per frame
    void Update()
    {
        // Assign the number of food items left to a text object
        foodItemCountText.text = fireProjectilesScript.foodItems.ToString();

        // Round timeRemaining and display it in text
        float roundedTime = Mathf.Round(gameManager.timeRemaining);
        timeRemaining.text = roundedTime.ToString() + " s";

        // Assign the number of customers fed and customers to feed to a text object
        targetGoalText.text = gameManager.customersFed + " / " + gameManager.customersToFeed;

        // Assign the associated cause of game over state using gameOverCausesIndex to a text object
        causeText.text = gameManager.gameOverCauses[gameManager.gameOverCausesIndex].ToString();

        // Assign the remaining time to a text object which is displayed later
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
