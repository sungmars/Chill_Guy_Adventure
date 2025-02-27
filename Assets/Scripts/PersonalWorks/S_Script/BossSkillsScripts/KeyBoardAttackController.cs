using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyBoardAttackController : MonoBehaviour
{
    Queue<string> keyCodes = new Queue<string>();
    Queue<GameObject> keyCodesObj = new Queue<GameObject>();
    [SerializeField] GameObject[] keyPrefabs;
    GameObject player;
    GameObject keyObj;

    public float damage = 10f;
    public float lifetime = 5f;
    public float knockbackPower = 5f;
    public float knockbackDuration = 0.2f;

    private bool onSkill = false;

    [SerializeField] private AudioClip keyBoardAttackPressed;
    [SerializeField] private AudioClip keyBoardAttackSuccess;
    [SerializeField] private AudioClip keyBoardAttackFail;
    

    Coroutine coroutine;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (keyCodes.Count > 0)
        {
            if (Input.inputString == keyCodes.Peek())
            {
                AudioManager.Instance.PlayBossSound(keyBoardAttackPressed);
                keyCodes.Dequeue();
                Destroy(keyCodesObj.Dequeue());
                if (keyCodes.Count == 0) 
                {
                    AudioManager.Instance.PlayBossSound(keyBoardAttackSuccess);
                }
            }
        }
        else
        {
            StopKeyBoardInputAttack();
        }
    }

    public void PublicKeyBoardAttack()
    {
        CreateKeyBoardAttack();
        coroutine = StartCoroutine(KeyBoardInputAttack());
    }

    private void CreateKeyBoardAttack()
    {
        float x = player.transform.position.x - 2f;
        float y = player.transform.position.y + 1.1f;
        for (int i = 0; i < 5; i++)
        {
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0:
                    keyCodes.Enqueue("w");
                    break;
                case 1:
                    keyCodes.Enqueue("a");
                    break;
                case 2:
                    keyCodes.Enqueue("s");
                    break;
                case 3:
                    keyCodes.Enqueue("d");
                    break;
            }
            keyObj = Instantiate(keyPrefabs[rand], transform);
            keyObj.transform.position = new Vector2(x, y);
            keyCodesObj.Enqueue(keyObj);
            x += 1f;
        }
    }

    private IEnumerator KeyBoardInputAttack()
    {
        onSkill = true;
        player.GetComponent<PlayerInput>().enabled = false;
        float time = 0;
        PlayerController playerController = player.GetComponent<PlayerController>();
        while (time < 6f)
        {
            time += Time.deltaTime;
            yield return new WaitForSeconds(0);
        }
        AudioManager.Instance.PlayBossSound(keyBoardAttackFail);
        playerController.TakeDamage((int)damage);
        playerController.ApplyKnockback(transform, knockbackPower, knockbackDuration);
        player.GetComponent<PlayerInput>().enabled = true;
        ClearData();
        onSkill = false;
        yield return new WaitForSeconds(2);
    }

    private void StopKeyBoardInputAttack()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        player.GetComponent<PlayerInput>().enabled = true;
    }

    private void ClearData()
    {
        keyCodes.Clear();
        foreach (var keyObj in keyCodesObj)
        {
            Destroy(keyObj);
        }
        keyCodesObj.Clear();
    }
}
