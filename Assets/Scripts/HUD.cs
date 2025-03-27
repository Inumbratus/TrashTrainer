using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    //Vars
    public GameManager gameManager;
    public TMP_Text scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (gameManager != null)
        {
            scoreText.text = "Score: " + gameManager.Score;
        }
    }
}
