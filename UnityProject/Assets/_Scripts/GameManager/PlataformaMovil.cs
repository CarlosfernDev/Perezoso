using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [SerializeField] private Transform StopPoint;
    private Transform PlayerStoped;
    private bool Stoped;
    private bool _Reset = true;
    private bool _moved;

    [SerializeField] private LayerMask _WallLayer;
    [SerializeField] private float _WallCheckerDistance;
    [SerializeField] private Vector2 _CheckDirection;
    [SerializeField] private Vector2 _CheckOffset;

    [SerializeField] private bool _FlipChecker;
    [HideInInspector] public bool _IsLeft;
    int symbol = 1;

    [SerializeField] private float _Speed;
    [SerializeField] private Vector2 _MoveDirection;

    private void FixedUpdate()
    {
        if (Stoped)
            Move();

        if(_Reset)
            WallChecker();
    }

    private void WallChecker()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)transform.position, _CheckDirection * symbol, _WallCheckerDistance + transform.localScale.x / 2, _WallLayer);

        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider?.gameObject?.GetComponent<PlataformaMovil>() == this)
                continue;

            if (!_moved)
                return;

            Debug.Log("Entraste");
            _Reset = false;
            Stoped = false;
            PlayerStoped.gameObject.GetComponent<PlayerAi>().SetStopBool(false);
            Debug.Log(hit);
            return;
        }
        if (Stoped)
            Debug.Log("Saliste");
        if(Stoped)
            _moved = true;
    }

    private void ChangeDirection(bool value)
    {
        if (!_FlipChecker)
            return;

        _IsLeft = value;
        symbol = (_IsLeft) ? -1 : 1;
    }

    private void Move()
    {
        transform.Translate(Time.deltaTime * _MoveDirection * symbol * _Speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player")
            return;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player" || Stoped)
            return;

        if((( collision.transform.position.x - StopPoint.position.x > 0 && !_IsLeft )|| (collision.transform.position.x - StopPoint.position.x < 0 && _IsLeft)) && _Reset)
        {
            Debug.Log("Para");
            collision.gameObject.GetComponent<PlayerAi>().SetStopBool(true);
            PlayerStoped = collision.transform;
            collision.transform.parent.transform.parent = gameObject.transform;
            Stoped = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player")
            return;

        Debug.Log("Ya no sos hijo");
        PlayerStoped.transform.parent.transform.parent = null;
        PlayerStoped = null;
        _Reset = true;
        _moved = false;
        ChangeDirection(!_IsLeft);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
   
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + (_CheckDirection * symbol * (_WallCheckerDistance + transform.localScale.x / 2)));
    }
}
