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

    FireProjectiles fireProjectilesScript;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        fireProjectilesScript = FindAnyObjectByType<FireProjectiles>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float roundedTime =  Mathf.Round(gameManager.timeRemaining);
        burgerCountText.text = fireProjectilesScript.foodItems.ToString();
        timeRemaining.text = roundedTime.ToString() + " s";
        targetGoalText.text = gameManager.customersFed + " / " + gameManager.customersToFeed;
    }
}
