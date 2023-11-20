using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AreaEffector2D))]
public class JumpPad : MonoBehaviour
{
    [SerializeField] AreaEffector2D _Effector;
    [SerializeField] private float _UseColdown;
    private float _TimeReferenceColdown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_TimeReferenceColdown >= Time.time)
            return;

        if (collision.transform.tag != "Player")
            return;

        _Effector.enabled = true;

        _TimeReferenceColdown = _UseColdown + Time.time;

        collision.GetComponent<PlayerAi>().canMoveinAir = true;
        collision.GetComponent<PlayerAi>().timeReferenceMoveinAir = Time.time;

        GameObject Player = PlayerAi.Player.gameObject;
        Rigidbody2D playerRigidBody = Player.GetComponent<Rigidbody2D>();
        playerRigidBody.isKinematic = true;
        Player.transform.position = transform.position;
        playerRigidBody.isKinematic = false;

        playerRigidBody.velocity = Vector2.zero;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _Effector.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + new Vector2(Mathf.Cos(Mathf.Deg2Rad * _Effector.forceAngle), Mathf.Sin(Mathf.Deg2Rad * _Effector.forceAngle)));
    }
}
