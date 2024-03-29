﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MovesCounter movesCounter;
    public int nextScene;
    public GameObject levelFailedPanel;
    
    public GameObject pausePanel;
    public GameObject[] gamebuttons;
    public Text lvlNo;
    
    public GameObject levelWonPanel;
    public GameObject tryAgainPanel;
    public Text coins;

    public Text coinsEarnedInthisLevel;
    private int _coins;
    public void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        LevelNo();
    }
    private void Update()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        if (Input.GetKeyDown(KeyCode.Escape)) 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void WintoNextLevel()
    {
        SceneManager.LoadScene(nextScene);
        //if (nextScene > PlayerPrefs.GetInt("levelAt"))
        if(nextScene > SaveSystem.LevelAt)
        {
            //PlayerPrefs.SetInt("levelAt", nextScene);
            SaveSystem.LevelAt = nextScene;
        }
        SaveSystem.SavePlayer();
    }
    
    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LevelFailed()
    {
        Time.timeScale = 0f;
        levelFailedPanel.SetActive(true);
        movesCounter.moves.gameObject.SetActive(false);
        gamebuttons[0].SetActive(false);
        gamebuttons[1].SetActive(false);
        gamebuttons[2].SetActive(false);
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LevelNo()
    {
        lvlNo.text = "Level\n" + (nextScene - 1);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        tryAgainPanel.SetActive(false);
        movesCounter.moves.gameObject.SetActive(true);
        gamebuttons[0].SetActive(true);
        gamebuttons[1].SetActive(true);
        gamebuttons[2].SetActive(true);
    }

    public void LevelWon()
    {
        Time.timeScale = 0f;
        levelWonPanel.SetActive(true);
        movesCounter.moves.gameObject.SetActive(false);
        gamebuttons[0].SetActive(false);
        gamebuttons[1].SetActive(false);
        gamebuttons[2].SetActive(false);
        CoinsCounter();
    }

    public void TryAgain()
    {
        Time.timeScale = 0f;
        tryAgainPanel.SetActive(true);
        movesCounter.moves.gameObject.SetActive(false);
        gamebuttons[0].SetActive(false);
        gamebuttons[1].SetActive(false);
        gamebuttons[2].SetActive(false);
    }

    private void CoinsCounter()
    {
        
        if (movesCounter.movesLeft > 15)
        {
            _coins = 200;
            SaveSystem.Coins += _coins;
            coinsEarnedInthisLevel.text = 200.ToString();
        } else
        {
            _coins = 200;
            SaveSystem.Coins += _coins;
            coinsEarnedInthisLevel.text = 100.ToString();
        }
        SaveSystem.SavePlayer();
        coins.text = SaveSystem.Coins.ToString();


    }
}

