using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChillAttackController : MonoBehaviour
{
    [SerializeField] private GameObject chillAttackObjPrefab;

    public void PublicChillAttack(Transform player)
    {
        CreateChillAttack(player);
    }

    private void CreateChillAttack(Transform player)
    {
        GameObject chillAttackParent;

        for (int i = 0; i < 10; i++)
        {
            chillAttackParent = Instantiate(chillAttackObjPrefab);
            chillAttackParent.name = $"ChillAttackParent {i}";
            chillAttackParent.transform.GetChild(0).name = $"ChillAttack {i}";
            chillAttackParent.transform.GetChild(1).name = $"ChillAttackBottom {i}";
            if (i == 0)
            {
                chillAttackParent.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0f);               
            }
            else
            {
                float x = Random.Range(-8f, 9f);
                float y = Random.Range(-4f, 5f);
                chillAttackParent.transform.position = new Vector3(x, y, 0f);
            }
        }
    }
}
