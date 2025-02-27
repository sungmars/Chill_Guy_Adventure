using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    GameObject player;
    SpriteRenderer playerSR;
    SpriteRenderer playerWeapon;
    [SerializeField] RectTransform uIPanel;
    [SerializeField] Image uiFaceImage;
    [SerializeField] Sprite kimFace;
    [SerializeField] Sprite soFace;
    [SerializeField] TextMeshProUGUI chillText;

    [SerializeField] RectTransform endPosition;
    string[] chillTexts = { "������ ����� �޽��� ��. �׳� chill�ϰ� ���� �־�.", "���� ����? �ƴ�, �׳� ��� ���� ����. Chill.", "�̰� ���̶��? ��¥ chill�� ����� ������ �ż��� ���ƿ´�.", "����ִ� �� �ǰ�����? ���� �� chill�� �ð��̴�." };

    private void Awake()
    {
        player = GameManager.Instance.GetPlayer().gameObject;//GameObject.FindGameObjectWithTag("Player");
        playerSR = player.transform.GetComponentInChildren<SpriteRenderer>();
        player.GetComponent<PlayerInput>().enabled = false;
        player.GetComponentInChildren<Animator>().speed = 0.0f;
        playerWeapon = player.transform.GetChild(1).GetComponentInChildren<SpriteRenderer>();
        int rand = Random.Range(0, 4);
        chillText.text = chillTexts[rand];
        if (GameManager.Instance.playerOrder == 0)
            uiFaceImage.sprite = kimFace;
        else
            uiFaceImage.sprite = soFace;
    }

    void Start()
    {
        StartCoroutine(PlayerDie());
        StartCoroutine(PlayerWeaponFadeOut());
    }
    IEnumerator PlayerDie()
    {
        float speed = 0.001f;
        while (playerSR.material.color.r > 0.4f)
        {
            playerSR.material.color -= new Color(speed, speed, speed, 0f);
            yield return null;
        }
        playerSR.color = new Color(0.4f, 0.4f, 0.4f, playerSR.color.a);
    }

    IEnumerator PlayerWeaponFadeOut()
    {
        float speed = 0.003f;
        while (playerWeapon.material.color.a > 0.1f)
        {
            playerWeapon.material.color = new Color(playerWeapon.material.color.r, playerWeapon.material.color.g, playerWeapon.material.color.b, playerWeapon.material.color.a - speed);
            yield return null;
        }
        playerWeapon.material.color = new Color(playerWeapon.material.color.r, playerWeapon.material.color.g, playerWeapon.material.color.b, 0f);
        Debug.Log("���� ���̵� �ƿ� ��");
        yield return new WaitForSeconds(1f);
        StartCoroutine(UIIn());
        yield return new WaitForSeconds(1f);
    }

    IEnumerator UIIn()
    {
        float speed = 1f;
        Debug.Log(uIPanel.position.y);
        while (uIPanel.position.y > endPosition.position.y)
        {
            uIPanel.position -= new Vector3(0, speed);
            yield return null;
        }
        uIPanel.position = endPosition.position;// new Vector3(uIPanel.position.x, 225);
    }

    public void GoFirstScene()
    {
        SceneManager.LoadScene("Start_Scene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
