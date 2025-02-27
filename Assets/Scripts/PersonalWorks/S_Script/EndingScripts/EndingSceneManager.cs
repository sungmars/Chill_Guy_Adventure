using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EndingSceneManager : MonoBehaviour
{
    [SerializeField] private AudioClip endingBgm;
    [SerializeField] SpriteRenderer endingBgImage;
    [SerializeField] GameObject player;
    [SerializeField] Sprite chill;
    [SerializeField] SpriteRenderer boss;
    [SerializeField] Transform LeftPosition;
    [SerializeField] Transform RightPosition;

    private void Start()
    {
        AudioManager.Instance.PlayBGM(endingBgm);
        StartCoroutine(MainCorotuine());
    }

    IEnumerator MainCorotuine()
    {
        StartCoroutine(PlayerJump());
        StartCoroutine(BossFadeOut());
        yield return new WaitForSeconds(3f);
        StartCoroutine(PlayerFilp());
        yield return new WaitForSeconds(5f);
        StartCoroutine(PlayerScaleSetting());
        StartCoroutine(PlayerPositionSetting());
        StartCoroutine(EndingBackGroundOn());
        yield return new WaitForSeconds(1f);
    }

    IEnumerator PlayerJump()
    {
        float firstY = player.transform.position.y;
        float tartgetY = firstY + 3f;
        float speed = 0.05f;
        while (player.transform.position.y < tartgetY)
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + speed);
            yield return new WaitForSeconds(0f);
        }
        while (player.transform.position.y > firstY)
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - speed);
            yield return new WaitForSeconds(0f);
        }
        yield return new WaitForSeconds(0.5f);
        while (player.transform.position.y < tartgetY)
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + speed);
            yield return new WaitForSeconds(0f);
        }
        while (player.transform.position.y > firstY)
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - speed);
            yield return new WaitForSeconds(0f);
        }
        yield return new WaitForSeconds(0.5f);
        while (player.transform.position.y < tartgetY)
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + speed);
            yield return new WaitForSeconds(0f);
        }
        while (player.transform.position.y > firstY)
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - speed);
            yield return new WaitForSeconds(0f);
        }
    }

    IEnumerator PlayerFilp()
    {
        SpriteRenderer playerSr = player.GetComponent<SpriteRenderer>();
        bool flip = false;
        for (int i = 0; i < 5; i++)
        {
            playerSr.flipX = flip;
            flip = !flip;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
        playerSr.flipX = flip;
        playerSr.sprite = chill;
        playerSr.sortingOrder = 20;
    }

    IEnumerator PlayerScaleSetting()
    {
        Vector2 firstScale = player.transform.localScale;
        Vector2 targetScale = player.transform.localScale * 2;
        while (player.transform.localScale.x < targetScale.x)
        {
            player.transform.localScale = new Vector2(player.transform.localScale.x + 0.001f, player.transform.localScale.y + 0.001f);
            yield return new WaitForSeconds(0f);
        }
    }

    IEnumerator PlayerPositionSetting()
    {
        Vector2 playerPosition = player.transform.position;
        if (playerPosition.x < 0)
        {
            while (Vector2.Distance(player.transform.position, LeftPosition.position) > 0.3f)
            {
                Vector2 tempPos = Vector2.Lerp(player.transform.position, LeftPosition.position, 0.001f);
                player.transform.position = tempPos;
                yield return new WaitForSeconds(0f);
            }
        }
        else
        {
            while (Vector2.Distance(player.transform.position, RightPosition.position) > 0.3f)
            {
                Vector2 tempPos = Vector2.Lerp(player.transform.position, RightPosition.position, 0.001f);
                player.transform.position = tempPos;
                yield return new WaitForSeconds(0f);
            }
        }

    }


    IEnumerator BossFadeOut()
    {
        float speed = 0.0005f;
        while (boss.color.a > 0f)
        {
            boss.color = new Color(255f, 255f, 255f, boss.color.a - speed);
            yield return new WaitForSeconds(0f);
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator EndingBackGroundOn()
    {
        float speed = 0.0005f;
        while (endingBgImage.color.a < 1f)
        {
            endingBgImage.color = new Color(255f, 255f, 255f, endingBgImage.color.a + speed);
            yield return new WaitForSeconds(0f);
        }
    }
}
