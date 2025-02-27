using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectOption : MonoBehaviour
{
    public Toggle secondPlayerToggle;
    public TextMeshProUGUI hide;

    public void Start()
    {
        if (PlayerPrefs.HasKey("Clear"))
        {
            var clear = PlayerPrefs.GetInt("Clear"); // 0이면 클리어 못함
            if (clear > 0)
            {
                secondPlayerToggle.interactable = true;
                hide.gameObject.SetActive(false);
            }
        }
    }

    public void OnClickPlayer1(bool isSelect)
    {
        if (isSelect)
        {
            GameManager.Instance.playerOrder = 0;
        }
    }

    public void OnClickPlayer2(bool isSelect)
    {
        if (isSelect)
        {
            GameManager.Instance.playerOrder = 1;
        }
    }
}
