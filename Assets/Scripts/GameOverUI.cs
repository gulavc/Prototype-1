using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

    public GameManager gameManager;

    public Text textField;

    public string winText, loseText;
	
    public void Restart()
    {
        gameManager.RestartGame();
        this.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver(bool win)
    {
        this.gameObject.SetActive(true);
        textField.text = win ? winText : loseText;
    }

    public void Hide(bool value)
    {
        this.gameObject.SetActive(!value);
    }

}
