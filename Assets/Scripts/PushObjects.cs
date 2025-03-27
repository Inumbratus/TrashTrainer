using UnityEngine;

public class PushObjects : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        PushItem(other.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        PushItem(other.gameObject);
    }

    void PushItem(GameObject other)
    {
        if(other.transform.parent == null)
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * Time.deltaTime * 350.0f);
        }
        //Debug.Log("Attempting to push " + other);
    }
}
