using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyBoardAttackController : MonoBehaviour
{
    [SerializeField] Stack<string> keyCodes = new Stack<string>();
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0:
                    keyCodes.Push("w");
                    break;
                case 1:
                    keyCodes.Push("a");
                    break;
                case 2:
                    keyCodes.Push("s");
                    break;
                case 3:
                    keyCodes.Push("d");
                    break;
            }
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
                keyCodes.Pop();
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
