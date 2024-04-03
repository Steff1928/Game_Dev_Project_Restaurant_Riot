using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI burgerCountText;
    FireProjectiles fireProjectilesScript;
    // Start is called before the first frame update
    void Start()
    {
        fireProjectilesScript = FindAnyObjectByType<FireProjectiles>();
    }

    // Update is called once per frame
    void Update()
    {
        burgerCountText.text = fireProjectilesScript.foodItems.ToString();
    }
}
