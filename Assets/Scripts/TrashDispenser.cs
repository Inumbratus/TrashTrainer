using UnityEngine;

public class TrashDispenser : MonoBehaviour
{
    //Vars
    public GameObject[] trashPrefabs;
    public GameManager gameManager;

    public float spawnDelay = 1.5f;

    public GameObject spawnPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //Used for testing
        //SpawnTrash();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTrash()
    {
        Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Length)], spawnPos.transform.position, Random.rotation);
        //Increase spawn speed every time score reaches a multiple of 5, could use an expansion to reduce speed if score lowers
        if(gameManager.Score > 1 && gameManager.Score % 5 == 0)
        {
            spawnDelay -= 0.15f;
            spawnDelay = Mathf.Clamp(spawnDelay, 1.0f, 3.0f);
            Debug.Log("Difficulty Increased");
        }
        Invoke("SpawnTrash", spawnDelay);
    }
}
