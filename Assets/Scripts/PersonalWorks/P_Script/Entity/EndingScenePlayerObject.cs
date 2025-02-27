using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScenePlayerObject : MonoBehaviour
{
    public List<Sprite> playerImgs;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer.sprite = playerImgs[GameManager.Instance.playerOrder];

        PlayerPrefs.SetInt("Clear", 1);
    }
}
