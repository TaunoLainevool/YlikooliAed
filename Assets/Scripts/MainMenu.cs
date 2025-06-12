using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Customization()
    {
        SceneManager.LoadSceneAsync("Customization");
    }
    public void PlayGame() { 
        SceneManager.LoadSceneAsync("Game");
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync("Main menu");
    }
}
