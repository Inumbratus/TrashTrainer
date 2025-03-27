using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    //Vars
    public GameManager gameManager;
    public TrashDispenser trashDispenser;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        trashDispenser = GameObject.FindGameObjectWithTag("Respawn").GetComponent<TrashDispenser>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && gameManager != null)
        {
            gameManager.gameStarted = true;
            trashDispenser.SpawnTrash();
        }
    }
}
