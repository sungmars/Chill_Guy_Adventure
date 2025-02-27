using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private Camera m_Camera;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] public WeaponHandler WeaponPrefab;
    [SerializeField] private Animator weaponanimator;

    private float timeSinceLastAttack = float.MaxValue;
    private float maxExp;

    public List<GameObject> spawnedEnemies;

    private List<Vector2> playerToEnemyVectors;
    public List<Vector2> PlayerToEnemyVectors { get { return playerToEnemyVectors; } }

    private List<bool> isInClosedRange;
    public List<bool> IsInClosedRange { get { return isInClosedRange; } }

    private List<bool> isInLongRange;
    public List<bool> IsInLongRange { get { return isInLongRange; } }

    private List<bool> isAttackingEnemyIndex;

    private float meleeAttackRange = 1.5f;
    private float longAttackRange = 5f;

    public bool AttackModeChange = false; // false가 근접공격, true가 원거리공격
    public bool WeaponUpgrade01 = false;
    public bool WeaponUpgrade02 = false;

    // 맞았을 때
    private float timeSinceLastChange = float.MaxValue;
    private float healthChangeDelay = .5f; // 맞았을 때 0.5초 동안 빨간색

    // 플레이어 이미지 추가
    public Sprite playerImg;

    protected override void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animationHandler = GetComponent<AnimationHandler>();

        if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();

        weaponanimator = weaponHandler.GetComponentInChildren<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        m_Camera = Camera.main;
    }

    protected void Update()
    {
        _animationHandler.UpdateState(movementDirection);
        Rotate(lookDirection);
        HandleAttackDelay();

        if (Input.GetKeyDown(KeyCode.F)) ToMeleeWeapon(true, false, false);
        if (Input.GetKeyDown(KeyCode.G)) ToRangeWeapon(false, false, false);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!AttackModeChange) UpgradeMeleeWeaponToVer01(false);
            else UpgradeRangeWeaponToVer01(true);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!AttackModeChange && WeaponUpgrade01) UpgradeMeleeWeaponToVer02(false, true);
            else if (AttackModeChange && WeaponUpgrade01) UpgradeRangeWeaponToVer02(true, true);
        }

        if (timeSinceLastChange < healthChangeDelay)
        {
            Debug.Log("호복중?");
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                _animationHandler.InvincibilityEnd();
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        timeSinceLastChange = 0f;
        _animationHandler.Damage();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        spawnedEnemies = new List<GameObject>();
        foreach (GameObject spawnedEnemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            spawnedEnemies.Add(spawnedEnemy);
        }

        playerToEnemyVectors = new List<Vector2>();
        isInLongRange = new List<bool>();
        isInClosedRange = new List<bool>();
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            playerToEnemyVectors.Insert(i, spawnedEnemies[i].transform.position - _rigidbody2D.transform.position);

            if (Mathf.Abs(playerToEnemyVectors[i].magnitude) <= meleeAttackRange)
                isInClosedRange.Insert(i, true);
            else if (Mathf.Abs(playerToEnemyVectors[i].magnitude) > meleeAttackRange)
                isInClosedRange.Insert(i, false);

            if (Mathf.Abs(playerToEnemyVectors[i].magnitude) <= longAttackRange)
                isInLongRange.Insert(i, true);
            else if (Mathf.Abs(playerToEnemyVectors[i].magnitude) > longAttackRange)
                isInLongRange.Insert(i, false);
        }

        if (AttackModeChange == false)
            OnMeleeAttackMode();
        else
            OnRangeAttackMode();

        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (lookDirection.magnitude < 0.9f) lookDirection = Vector2.zero;
            else if (isInClosedRange[i] == true) lookDirection = playerToEnemyVectors[i].normalized;
        }
    }

    private void HandleAttackDelay()
    {
        if (weaponHandler == null) return;
        if (timeSinceLastAttack <= weaponHandler.delay)
            timeSinceLastAttack += Time.deltaTime;

        if (isAttacking && timeSinceLastAttack > weaponHandler.delay)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        _spriteRenderer.flipX = isLeft;

        if (weaponPivot != null)
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);

        weaponHandler?.Rotate(isLeft);
    }

    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();

        movementDirection = movementDirection.normalized;
    }

    void OnLook(InputValue inputValue)
    {
        Vector2 mousePosition = inputValue.Get<Vector2>();
        Vector2 worldPos = m_Camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < 0.9f) lookDirection = Vector2.zero;
        else lookDirection = lookDirection.normalized;
    }

    void OnMeleeAttackMode()
    {
        isAttackingEnemyIndex = new List<bool>();
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (isInClosedRange[i] == true) isAttackingEnemyIndex.Insert(i, true);
            else if (isInClosedRange[i] == false) isAttackingEnemyIndex.Insert(i, false);
        }

        for (int j = 0; j < spawnedEnemies.Count; j++)
        {
            if (isAttackingEnemyIndex[j] == true) isAttacking = true;
        }

        if (isAttackingEnemyIndex.All(temp => temp.Equals(false))) isAttacking = false;
    }

    void OnRangeAttackMode()
    {
        isAttackingEnemyIndex = new List<bool>();
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (isInLongRange[i] == true) isAttackingEnemyIndex.Insert(i, true);
            else if (isInLongRange[i] == false) isAttackingEnemyIndex.Insert(i, false);
        }

        for (int j = 0; j < spawnedEnemies.Count; j++)
        {
            if (isAttackingEnemyIndex[j] == true) isAttacking = true;
        }

        if (isAttackingEnemyIndex.All(temp => temp.Equals(false))) isAttacking = false;
    }

    protected void ToRangeWeapon(bool mode, bool up1, bool up2)
    {
        mode = AttackModeChange;
        up1 = WeaponUpgrade01;
        up2 = WeaponUpgrade02;

        if (!mode && !up1 && !up2)
        {
            AttackModeChange = true;
            weaponanimator.SetBool("WeaponChange", true);
        }
    }

    protected void ToMeleeWeapon(bool mode, bool up1, bool up2)
    {
        mode = AttackModeChange;
        up1 = WeaponUpgrade01;
        up2 = WeaponUpgrade02;

        if (mode && !up1 && !up2)
        {
            AttackModeChange = false;
            weaponanimator.SetBool("WeaponChange", false);
        }
    }

    protected void UpgradeMeleeWeaponToVer01(bool mode)
    {
        mode = AttackModeChange;
        if (!mode)
        {
            WeaponUpgrade01 = true;
            meleeAttackRange = 2f;
        }
    }

    protected void UpgradeMeleeWeaponToVer02(bool mode, bool up1)
    {
        mode = AttackModeChange;
        up1 = WeaponUpgrade01;

        if (!mode && up1)
        {
            WeaponUpgrade01 = false;
            WeaponUpgrade02 = true;
            meleeAttackRange = 2.5f;
        }
    }

    protected void UpgradeRangeWeaponToVer01(bool mode)
    {
        mode = AttackModeChange;
        if (mode)
        {
            WeaponUpgrade01 = true;
            longAttackRange = 4f;
            weaponHandler.delay = 0.4f;
            weaponHandler.numberofProjectilesPerShot = 2;
            weaponHandler.multipleProjectileAngle = 8f;
        }
    }

    protected void UpgradeRangeWeaponToVer02(bool mode, bool up1)
    {
        mode = AttackModeChange;
        up1 = WeaponUpgrade01;

        if (mode && up1)
        {
            WeaponUpgrade01 = false;
            WeaponUpgrade02 = true;
            longAttackRange = 4.8f;
            weaponHandler.numberofProjectilesPerShot = 3;
            weaponHandler.multipleProjectileAngle = 10f;
        }
    }

    private void PlusExp(float exp)
    {
        this.exp += exp;

        if (this.exp >= maxExp) SetLevel();
    }

    private void SetLevel()
    {
        level++;
        SetMaxExp();
    }

    private void SetMaxExp()
    {
        maxExp *= 1.2f;
    }

    private void SetGold(int gold)
    {
        this.gold += gold;
    }

    private void SetHp(int damage)
    {
        this.hp += damage - this.defense;
    }
    //외부에서 호출할 메서드(리시브 경험치,골드)
    public void ReceiveExp(float expAmount)
    {
        PlusExp(expAmount);
    }

    public void ReceiveGold(int goldAmount)
    {
        SetGold(goldAmount);
    }
}
