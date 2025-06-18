using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
      public void Customization()
    {
        SceneManager.LoadSceneAsync("Customization");
    }
    public void PlayGame()
    {

        TMP_InputField playerName;
        GameObject playerNameError = GameObject.FindGameObjectWithTag("Player name error");

        playerName = GameObject.FindGameObjectWithTag("Player name input").GetComponent<TMP_InputField>();
        if (playerName.text == "")
        {
            Debug.Log("aa");
            playerNameError.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadSceneAsync("Game");
        }
        
        
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync("Main menu");
    }
    public void toScoreboard()
    {
        SceneManager.LoadSceneAsync("Scoreboard");
    }
}
