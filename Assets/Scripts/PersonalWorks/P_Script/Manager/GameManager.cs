using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingletonDontDestroy<GameManager>
{
    public List<string> RoundSceneList;
    public string startScene;
    public string gameOverScene;
    public string bossIntroScene;
    // public string endingScene;

    public int currentRoundIndex = 0;

    public List<EnemySpawnData> enemySpawnDatas;
    public (int left, int right) mouseSkill = (4, 1);
    public List<PlayerController> playerControllers;
    public int selectPlayer = 0;
    public int playerGold = 0;

    public void Start()
    {
        if (PlayerPrefs.HasKey("Gold"))
        {
            playerGold = 0;
        }
    }

    public void CreatePlayer()
    {

    }

    public void NextRound()
    {
        currentRoundIndex += 1;
        StartRound(currentRoundIndex);
    }

    public void StartRound(int _round)
    {
        currentRoundIndex = _round;
        if (RoundSceneList.Count > currentRoundIndex)
        {
            SceneManager.LoadScene(RoundSceneList[_round]);
        }
        else
        {
            SceneManager.LoadScene(bossIntroScene);
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