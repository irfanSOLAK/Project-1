using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour
{
    public Text infoText;
    private void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public void Game1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void Game2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

    }

    public void Game3()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Game1Info()
    {
        infoText.text = "Complete the parkour. Avoid obstacles.";
    }

    public void Game2Info()
    {
        infoText.text = "Paint the wall that appears on the screen. You can see the painted percentage at the top of the screen.";
    }

    public void Game3Info()
    {
        infoText.text = "The game has same mechanics as game 1. You can enjoy the game with extra players.";
    }

    public void EmptyText()
    {
        infoText.text = "";
    }
}
