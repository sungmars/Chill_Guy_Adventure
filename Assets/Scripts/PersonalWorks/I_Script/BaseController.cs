using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{
    // 컴포넌트 참조들
    protected Rigidbody2D _rigidbody2D;
    protected AnimationHandler _animationHandler;
    protected WeaponHandler weaponHandler;

    // 이동 및 바라보는 방향
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    // 넉백
    [HideInInspector] public Vector2 knockback = Vector2.zero;
    [HideInInspector] public float knockbackDuration = 0.1f;
    public float knockbackPower = 5f;
    public float knockbackTime = 1f;

    // 공격
    protected bool isAttacking;


    // 스탯
    public string characterName;
    public int level = 1;
    public float attack;
    public float defense;
    public string equipItem;
    public float speed;
    public float attackSpeed;
    public float exp;
    public int gold;
    public float hp;
    public float maxHp;

    //사망시 불값
    public static bool isgameOver;


    public Image hpBarimage; // 맞았을 때 0.5초 동안 빨간색
    //오디오클립선언
    public AudioClip attackAudio;

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        isgameOver = false;
    }



    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    private void Movement(Vector2 direction)
    {
        // 이동 속도 조정 (원래 5 배속)
        direction = direction * 5;
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody2D.velocity = direction;
    }



    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
        Debug.Log("넉백");
    }



    protected virtual void Attack()
    {
        if (lookDirection != Vector2.zero)
            weaponHandler?.Attack();
        AudioManager.Instance.PlayPlayerSound(attackAudio);
    }


    public virtual void TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(damage - (int)defense, 0);
        hp -= actualDamage;
        if (hp <= 0)
            Die();

        if (hpBarimage != null)
        {
            hpBarimage.fillAmount = hp / maxHp;
        }
    }


    public virtual void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            isgameOver = true;
            Debug.Log("플레이어 사망");
        }
        else if (gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
        else
            Destroy(gameObject);
    }
}
