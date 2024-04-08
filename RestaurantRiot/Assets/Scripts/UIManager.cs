using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI burgerCountText;
    [SerializeField] TextMeshProUGUI timeRemaining;
    [SerializeField] TextMeshProUGUI targetGoalText;
    [SerializeField] TextMeshProUGUI initialTargetGoalText;
    [SerializeField] TextMeshProUGUI tipsText;
    [SerializeField] TextMeshProUGUI causeText;
    [SerializeField] TextMeshProUGUI remainingTimeStat;

    string[] tips = { "Throw food at customers chasing you to stun them", "Pick up pink tokens to gain an extra 15 seconds", "Pick up burgers to gain an additional food item", "Shut doors behind you to slow down enemy customers" };

    FireProjectiles fireProjectilesScript;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        fireProjectilesScript = FindAnyObjectByType<FireProjectiles>();
        gameManager = FindAnyObjectByType<GameManager>();

        initialTargetGoalText.text = "Feed " + gameManager.customersToFeed + " customers in " + gameManager.timeRemaining + " seconds";

        int tipsIndex = Random.Range(0, tips.Length);
        tipsText.text = tips[tipsIndex];

    }

    // Update is called once per frame
    void Update()
    {
        float roundedTime =  Mathf.Round(gameManager.timeRemaining);
        burgerCountText.text = fireProjectilesScript.foodItems.ToString();
        timeRemaining.text = roundedTime.ToString() + " s";
        targetGoalText.text = gameManager.customersFed + " / " + gameManager.customersToFeed;
        causeText.text = gameManager.gameOverCauses[gameManager.gameOverCausesIndex].ToString();
        remainingTimeStat.text = "TIME REMAINING: " +  roundedTime.ToString() + " s";
    }
}
