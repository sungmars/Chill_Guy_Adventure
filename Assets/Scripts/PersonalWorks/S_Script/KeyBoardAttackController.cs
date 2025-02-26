using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyBoardAttackController : MonoBehaviour
{
    [SerializeField] Queue<string> keyCodes = new Queue<string>();
    Queue<GameObject> keyCodesObj = new Queue<GameObject>();
    [SerializeField] GameObject[] keyPrefabs;
    [SerializeField] GameObject player;
    GameObject keyObj;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        float x = player.transform.position.x - 2f;
        float y = player.transform.position.y + 1.1f;
        for (int i = 0; i < 5; i++)
        {
            int rand = Random.Range(1, 4);
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
            keyObj = Instantiate(keyPrefabs[rand - 1], transform);
            keyObj.transform.position = new Vector2(x, y);
            keyCodesObj.Enqueue(keyObj);
            x += 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (keyCodes.Count > 0)
        {
            if (Input.inputString == keyCodes.Peek())
            {
                Debug.Log(Input.inputString);
                keyCodes.Dequeue();
                Destroy(keyCodesObj.Dequeue());
            }
        }
        else
        {
            BossController bossController = GetComponentInParent<BossController>();
            bossController.StopKeyBoardInputAttack();
            Destroy(gameObject);
        }
    }
}
