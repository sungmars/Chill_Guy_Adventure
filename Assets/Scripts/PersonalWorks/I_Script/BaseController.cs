using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody2D;

    [SerializeField] private SpriteRenderer _spriteRenderer;
        
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    protected AnimationHandler _animationHandler;

    protected string _name;
    protected int hp;
    protected int maxHp;
    protected int damage;
    protected int level;
    protected int attack;
    protected int defense;
    protected float speed;
    protected float exp;
    protected int gold;

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void Start()
    {

    }

    protected void Update()
    {    
        _animationHandler.UpdateState(movementDirection);
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
    }

    private void Movement(Vector2 direction)
    {
        direction = direction * 5;
        _rigidbody2D.velocity = direction;
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        _spriteRenderer.flipX = isLeft;
    }

}
