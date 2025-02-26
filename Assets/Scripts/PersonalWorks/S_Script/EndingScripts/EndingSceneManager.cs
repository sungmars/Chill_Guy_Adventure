using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingSceneManager : MonoBehaviour
{
    [SerializeField] private AudioClip endingBgm;
    [SerializeField] Image endingBgImage;
    [SerializeField] GameObject player;
    [SerializeField] Sprite chill;
    [SerializeField] SpriteRenderer boss;

    private void Start()
    {
        AudioManager.Instance.PlayBGM(endingBgm);
        StartCoroutine(PlayerJump());
    }

    IEnumerator PlayerJump()
    {
        float firstY = player.transform.position.y;
        float tartgetY = firstY + 3f;
        float speed = 0.2f;
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
        yield return new WaitForSeconds(0.5f);
        speed = 0.005f;
        while (boss.color.a > 0f)
        {
            boss.color = new Color(255f, 255f, 255f, boss.color.a - speed);
            yield return new WaitForSeconds(0f);
        }
        yield return new WaitForSeconds(1.5f);
        SpriteRenderer playerSr = player.GetComponent<SpriteRenderer>();
        playerSr.sprite = chill;
        yield return new WaitForSeconds(1f);
        StartCoroutine(EndingBackGroundOn());
        yield return new WaitForSeconds(1f);
    }

    IEnumerator EndingBackGroundOn()
    {
        float speed = 0.001f;
        while (endingBgImage.color.a < 1f)
        {
            endingBgImage.color = new Color(255f, 255f, 255f, endingBgImage.color.a + speed);
            yield return new WaitForSeconds(0f);
        }
    }
}
