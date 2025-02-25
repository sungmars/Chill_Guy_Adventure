using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private Camera m_Camera;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] public WeaponHandler WeaponPrefab;

    private float timeSinceLastAttack = float.MaxValue;
    private float maxExp;

    [SerializeField] public List<GameObject> spawnedEnemies;

    [SerializeField] private List<Vector2> playerToEnemyVectors;
    public List<Vector2> PlayerToEnemyVectors { get { return playerToEnemyVectors; } }

    [SerializeField] private List<bool> isInLongRange;
    public List<bool> IsInLongRange { get { return isInLongRange; } }

    [SerializeField] private List<bool> isInClosedRange;
    public List<bool> IsInClosedRange { get { return isInLongRange; } }

    [SerializeField] private List<bool> isAttackingEnemyIndex;
    public List<bool> IsAttackingEnemyIndex { get { return isAttackingEnemyIndex; } }

    [SerializeField] private float longAttackRange = 5f;
    public float LongAttackRange { get { return longAttackRange; } }

    [SerializeField] private float meleeAttackRange = 1.5f;
    public float MeleeAttackRange { get { return meleeAttackRange; } }

    private GameObject RangeWeapon;
    private GameObject MeleeWeapon;

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animationHandler = GetComponent<AnimationHandler>();

        if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();

        //RangeWeapon = GameObject.FindGameObjectWithTag("RangeWeapon");
        MeleeWeapon = GameObject.FindGameObjectWithTag("MeleeWeapon");
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

            if (Mathf.Abs(playerToEnemyVectors[i].magnitude) <= longAttackRange) isInLongRange.Insert(i, true);
            else if (Mathf.Abs(playerToEnemyVectors[i].magnitude) > longAttackRange) isInLongRange.Insert(i, false);

            if (Mathf.Abs(playerToEnemyVectors[i].magnitude) <= meleeAttackRange) isInClosedRange.Insert(i, true);
            else if (Mathf.Abs(playerToEnemyVectors[i].magnitude) > meleeAttackRange) isInClosedRange.Insert(i, false);
        }
                
        //if (RangeWeapon.activeSelf == true) OnFire();        
        if (MeleeWeapon.activeSelf == true) OnMeleeAttack();

        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (lookDirection.magnitude < 0.9f) lookDirection = Vector2.zero;
            else if (isInClosedRange[i] == true) lookDirection = playerToEnemyVectors[i].normalized;                        
        }
    }

    private void HandleAttackDelay()
    {
        if (weaponHandler == null) return;
        if (timeSinceLastAttack <= weaponHandler.Delay)
            timeSinceLastAttack += Time.deltaTime;

        if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
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

    void OnFire()
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

    void OnMeleeAttack()
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
