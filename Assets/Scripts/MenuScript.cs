using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    //Vars
    public TMP_Text titleText;
    public GameObject htpPanel;
    private Color colorTarget;
    float elapsedTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorTarget = new Color(Random.value, Random.value, Random.value);
        //Unlock Cursor to game and Show it
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(titleText != null)
        {
            if(titleText.color == colorTarget)
            {
                colorTarget = new Color(Random.value, Random.value, Random.value);
                elapsedTime = 0f;
            }
            elapsedTime += Time.deltaTime * 0.5f;
            titleText.color = Color.Lerp(titleText.color, colorTarget, elapsedTime);
        }
    }

    void PlayGame()
    {
        SceneManager.LoadScene("TrashRoom");
    }

    void HowToPlay()
    {
        htpPanel.SetActive(!htpPanel.activeInHierarchy);
    }

    void OptionsMenu()
    {
        
    }

    void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
