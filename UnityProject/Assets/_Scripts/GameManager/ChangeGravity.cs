using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    [Header("Momentum Gravity")]
    [SerializeField] private Vector2 _Gravity;
    [SerializeField] private bool _HoldMomentum;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        GameObject Player = PlayerAi.Player.gameObject;
        Rigidbody2D playerRigidBody = Player.GetComponent<Rigidbody2D>();

        Physics2D.gravity = _Gravity * 9.8f;

        if (!_HoldMomentum)
            playerRigidBody.velocity = Vector2.zero;

    }
}
