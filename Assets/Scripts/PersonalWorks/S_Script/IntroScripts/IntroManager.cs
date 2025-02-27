using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    IntroBossController introBossController;

    IntroCameraController introCameraController;

    [SerializeField]
    GameObject tempPlayer;

    [SerializeField]
    GameObject boss;

    [SerializeField]
    SpriteRenderer top;

    [SerializeField]
    SpriteRenderer bottom;


    private void Awake()
    {
        introBossController = GameObject.FindObjectOfType<IntroBossController>(true);
        introCameraController = GameObject.FindObjectOfType<IntroCameraController>();
        tempPlayer.GetComponent<PlayerInput>().enabled = true;
    }

    private void Start()
    {
        StartCoroutine(introCameraController.MoveCamera(tempPlayer, boss, introBossController.BossIn()));
        StartCoroutine(CameaSetTop());
        StartCoroutine(CameaSetBottom());
    }

    public IEnumerator CameaSetTop()
    {
        float y = 4.3f;
        float tempY;
        while (top.transform.position.y > y)
        {
            tempY = Mathf.Lerp(top.transform.position.y, y, Time.deltaTime);
            top.transform.position = new Vector2(top.transform.position.x, tempY);
            yield return new WaitForSeconds(0f);
        }
    }

    public IEnumerator CameaSetBottom()
    {
        float y = -4.3f;
        float tempY;
        while (bottom.transform.position.y < y)
        {
            tempY = Mathf.Lerp(bottom.transform.position.y, y, Time.deltaTime);
            bottom.transform.position = new Vector2(bottom.transform.position.x, tempY);
            yield return new WaitForSeconds(0f);
        }
    }
}
