using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    
    [SerializeField] private float weaponSize = 1f;
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }

    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }

    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float attackRange = 5f;
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public float delay = 0.7f;

    public int numberofProjectilesPerShot = 1;

    public float multipleProjectileAngle = 1f;

    public LayerMask target;

    [Header("Knockback Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value;}

    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime;  set => knockbackTime = value; }

    private static readonly int isMeleeAttack = Animator.StringToHash("isMeleeAttack");
    private static readonly int SwordUp01 = Animator.StringToHash("SwordUp01");
    private static readonly int SwordUp02 = Animator.StringToHash("SwordUp02");

    private static readonly int isRangeAttack = Animator.StringToHash("isRangeAttack");

    public BaseController Controller { get; private set; }
    public PlayerController playerController { get; private set; }

    public Animator animator;
    private SpriteRenderer weaponRenderer;

    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        animator.speed = 1.0f / delay;
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {

    }

    public virtual void Attack()
    {
        AttackAnimation();
    }

    public void AttackAnimation()
    {
        MeleeBasic(false, false, false);
        MeleeUpgrade01(false, true, false);
        MeleeUpgrade02(false, false, true);

        RangeBasic(true, false, false);
        RangeBasic(true, true, false);
        RangeBasic(true, false, true);
    }

    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }

    public void MeleeBasic(bool mode, bool up1, bool up2)
    {
        mode = playerController.AttackModeChange;
        up1 = playerController.WeaponUpgrade01;
        up2 = playerController.WeaponUpgrade02;

        if (!mode && !up1 && !up2) animator.SetTrigger(isMeleeAttack);
    }

    public void RangeBasic(bool mode, bool up1, bool up2)
    {
        mode = playerController.AttackModeChange;
        up1 = playerController.WeaponUpgrade01;
        up2 = playerController.WeaponUpgrade02;

        if (mode) animator.SetTrigger(isRangeAttack);
    }

    public void MeleeUpgrade01(bool mode, bool up1, bool up2)
    {
        mode = playerController.AttackModeChange;
        up1 = playerController.WeaponUpgrade01;
        up2 = playerController.WeaponUpgrade02;

        if (!mode && up1 && !up2) animator.SetTrigger(SwordUp01);
    }

    public void MeleeUpgrade02(bool mode, bool up1, bool up2)
    {
        mode = playerController.AttackModeChange;
        up1 = playerController.WeaponUpgrade01;
        up2 = playerController.WeaponUpgrade02;

        if (!mode && !up1 && up2) animator.SetTrigger(SwordUp02);
    }
}
