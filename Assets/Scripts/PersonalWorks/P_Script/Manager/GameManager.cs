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
    public (int left, int right) mouseSkill = (3, 1);
    public List<GameObject> playerControllers;
    public int playerOrder = 1;
    public int playerGold = 0;
    private PlayerController feildPlayer = null;

    public void Start()
    {
        if (PlayerPrefs.HasKey("PlayerGold"))
        {
            playerGold = PlayerPrefs.GetInt("PlayerGold");
        }
        if (PlayerPrefs.HasKey("PlayerOrder"))
        {
            playerOrder = PlayerPrefs.GetInt("PlayerOrder");
        }
    }

    public PlayerController GetPlayer()
    {
        if (feildPlayer == null)
        {
            feildPlayer = CreatePlayer().GetComponent<PlayerController>();
        }
        return feildPlayer;
    }
    private GameObject CreatePlayer()
    {
        var position = new Vector2(0, 0);
        if (RoundSceneList.Count == currentRoundIndex) position = new Vector2(-4, 0);
        var playerController = Instantiate(playerControllers[playerOrder], position, Quaternion.identity);
        return playerController;
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