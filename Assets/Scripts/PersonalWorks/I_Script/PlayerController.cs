using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private Camera m_Camera;

    private float maxExp;

    protected override void Start()
    {
        base.Start();
        m_Camera = Camera.main;        
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

    void OnFire(InputValue inputValue)
    {
        isAttacking = inputValue.isPressed;
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
}
