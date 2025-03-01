using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    protected Animator _animator;

    private static readonly int isDamage = Animator.StringToHash("isDamage");

    protected void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {

    }

    public void UpdateState(Vector2 moveVector)
    {
        if (Mathf.Approximately(moveVector.x, 0) && Mathf.Approximately(moveVector.y, 0)) _animator.SetBool("isMove", false);
        else _animator.SetBool("isMove", true);

        _animator.SetFloat("xDir", moveVector.x);
        _animator.SetFloat("yDir", moveVector.y);

        if (moveVector.x < 0)
        {
            _animator.SetFloat("Direction", 0);
        }
        else if (moveVector.x > 0)
        {
            _animator.SetFloat("Direction", 1);
        }
        else if (moveVector.y > 0)
        {
            _animator.SetFloat("Direction", 2);
        }
        else if (moveVector.y < 0)
        {
            _animator.SetFloat("Direction", 3);
        }
    }

    public void Damage()
    {
        _animator.SetBool(isDamage, true);
    }

    public void InvincibilityEnd()
    {
        _animator.SetBool(isDamage, false);
    }
}
