using UnityEngine;

public enum BinType
{
    Landfill,     // == 0
    Recycling,     // == 1
    Organic,     // == 2
    Other     // == 3
}

public class BinScript : MonoBehaviour
{
    //vars
    public BinType binType;
    public GameManager gameManager;
    public bool isBin = true;
    
    private void UpdateBin()
    {
        Material material = GetComponent<Renderer>().materials[2];
        switch (binType)
        {
            case BinType.Landfill:
                material.color = Color.red;
                break;
            case BinType.Recycling:
                material.color = Color.yellow;
                break;
            case BinType.Organic:
                material.color = Color.green;
                break;
            case BinType.Other:
                material.color = Color.white;
                break;
            default:
                material.color = Color.black;
                break;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if(isBin)
        {
            UpdateBin();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + " item has entered the " + binType + " bin");
        //PlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (other.tag == binType.ToString())
        {
            gameManager.Score++;
        }
        else
        {
            gameManager.Score--;
        }
        //if(other == player)
        //{
        //    player.heldObj = null;
        //}
        //DestroyImmediate(other.GetComponent<Rigidbody>());
        Destroy(other.gameObject);
        //Debug.Log("Score is now " + gameManager.Score);
    }
}
