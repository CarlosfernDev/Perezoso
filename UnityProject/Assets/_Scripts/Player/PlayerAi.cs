using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAi : MonoBehaviour
{
    [HideInInspector] static public PlayerAi Player;

    [Header("Dependencias")]
    [SerializeField] private Rigidbody2D _RigidBody;

    [Header("MovementValue")]
    [SerializeField] private float _Speed;
    [SerializeField] private float _MaxFallingSpeed;
    private float _ActualSpeed;
    [HideInInspector] public bool stopMove;

    [Header("WallChcker")]
    [SerializeField] private float _WallCheckerDistance;
    [SerializeField] private LayerMask _WallLayer;
    [HideInInspector] public bool _IsLeft;
    int symbol = 1;

    [Header("IsFallingChcker")]
    [SerializeField] private Vector2 _FallingBox;
    [SerializeField] private float _ColdownChangeFall;
    [HideInInspector] public bool canMoveinAir = false;
    [HideInInspector] public float timeReferenceMoveinAir;


    private void Awake()
    {
        Player = this;
        ResetSpeed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stopMove)
            return;

        if (!isFalling())
        {
            CheckWall();
            move();
        }
        else
        {
            if (GravityDirection().x != 0)
                _RigidBody.velocity = new Vector2(((canMoveinAir) ? _RigidBody.velocity.x : 0), Mathf.Clamp(_RigidBody.velocity.y, -_MaxFallingSpeed, _MaxFallingSpeed));
            else
                _RigidBody.velocity = new Vector2(Mathf.Clamp(_RigidBody.velocity.x, -_MaxFallingSpeed, _MaxFallingSpeed), ((canMoveinAir) ? _RigidBody.velocity.y : 0));
        }
    }

    private bool isFalling()
    {
        Collider2D OverlapBox = Physics2D.OverlapBox(new Vector2(transform.position.x + ((Mathf.Clamp(Physics2D.gravity.x, -1, 1)) * (((transform.localScale.x / 2)) + (_FallingBox.y / 2))), transform.position.y + ((Mathf.Clamp(Physics2D.gravity.y, -1, 1)) * (((transform.localScale.y / 2)) + (_FallingBox.y / 2)))), (new Vector2((Mathf.Clamp((Mathf.Abs(Physics2D.gravity.x)), 0, 1) * _FallingBox.y) + (Mathf.Clamp(Mathf.Abs(Physics2D.gravity.y), 0, 1) * _FallingBox.x), (Mathf.Clamp((Mathf.Abs(Physics2D.gravity.y)), 0, 1) * _FallingBox.y) + (Mathf.Clamp(Mathf.Abs(Physics2D.gravity.x), 0, 1) * _FallingBox.x))), 0, _WallLayer);
        if (OverlapBox == null)
            return true;

        return false;
    }

    private void CheckWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, GravityDirection() * symbol, _WallCheckerDistance + transform.localScale.x / 2, _WallLayer);

        if (hit)
        {
            _IsLeft = !_IsLeft;
            symbol = (_IsLeft) ? -1 : 1;
            Debug.Log(hit);
        }
        else
        {
            if (Time.time - timeReferenceMoveinAir > _ColdownChangeFall)
                canMoveinAir = false;
        }
    }

    private void move()
    {
        _RigidBody.velocity = symbol * GravityDirection() * _ActualSpeed * Time.deltaTime;
    }

    public void SetStopBool(bool value)
    {
        stopMove = value;

        if (value)
            _RigidBody.velocity = new Vector2(0, _RigidBody.velocity.y);
    }

    public void ResetSpeed()
    {
        _ActualSpeed = _Speed;
    }

    public void ChangeSpeed(float value)
    {
        _ActualSpeed = value;
    }

    public void ResetPlayer()
    {
        _IsLeft = false;
        symbol = 1;
    }

    private Vector2 GravityDirection(){
       return new Vector2(Mathf.Clamp(Mathf.Abs(Physics2D.gravity.y), 0, 1), Mathf.Clamp(Mathf.Abs(Physics2D.gravity.x), -1, 1));
    }

    public void ChangeDirection(bool value)
    {
        _IsLeft = value;
        symbol = (_IsLeft) ? -1 : 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector2(transform.position.x + +((Mathf.Clamp(Physics2D.gravity.x, -1, 1)) * ((transform.localScale.x / 2) + (_FallingBox.y / 2))), transform.position.y + ((Mathf.Clamp(Physics2D.gravity.y, -1, 1)) * ((transform.localScale.y / 2) + (_FallingBox.y / 2)))), (new Vector2((Mathf.Clamp((Mathf.Abs(Physics2D.gravity.x)), 0, 1) * _FallingBox.y) + (Mathf.Clamp(Mathf.Abs(Physics2D.gravity.y), 0, 1) * _FallingBox.x), (Mathf.Clamp((Mathf.Abs(Physics2D.gravity.y)), 0, 1) * _FallingBox.y) + (Mathf.Clamp(Mathf.Abs(Physics2D.gravity.x), 0, 1) * _FallingBox.x))));
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + (GravityDirection() * symbol * (_WallCheckerDistance + transform.localScale.x / 2)));
    }
}
