using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [Header("스탯설정")]
    public string characterName;
    public int level = 1;
    public float attack;
    public float defense;
    public string equipItem;
    public float speed;
    public float attackSpeed;
    public float exp;
    public int gold;
    public int hp;
    public static bool isgameOver;

    protected Vector2 movementDirection;
    protected Vector2 lookDirection;

    protected virtual void Start()
    {
        isgameOver = false;
    }
    public virtual void TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(damage - (int)defense, 0);
        hp -= actualDamage;
        if (hp <= 0)
            Die();
    }
    public virtual void Die()
    {
       if(gameObject.CompareTag("Player"))
        {
            isgameOver = true;
            Debug.Log("플레이어 사망");
        }
        else
            Destroy(gameObject);
    }
}
