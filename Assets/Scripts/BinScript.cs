using UnityEngine;

//Enum for choosing bin type
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
    
    //Update bin lid color
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
        //Only update bin visuals if it is a standard bin, this is so the disposal at the end of the dispenser doesn't throw errors
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
        //Add or remove score based on whether or not trash was properly disposed of
        if (other.tag == binType.ToString())
        {
            gameManager.Score++;
        }
        else
        {
            gameManager.Score--;
        }
        //Prevent Negative score
        gameManager.Score = Mathf.Clamp(gameManager.Score, 0, 999);
        //Destroy trash after it's been used for scoring
        Destroy(other.gameObject);
    }
}
