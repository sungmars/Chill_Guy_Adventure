using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingletonDontDestroy<GameManager>
{
    public List<string> RoundSceneList;
    public string startScene;
    public string gameOverScene;
    public string endingScene;

    private int currentRoundIndex = 0;

    public void Start()
    {

    }

    public void NextRound()
    {
        currentRoundIndex += 1;
        StartRound(currentRoundIndex);
    }

    public void StartRound(int _round)
    {
        if (RoundSceneList.Count < _round)
        {
            SceneManager.LoadScene(RoundSceneList[_round]);
        }
        else
        {
            SceneManager.LoadScene(endingScene);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(gameOverScene);
    }

    public void StartGame()
    {
        StartRound(0);
    }
}