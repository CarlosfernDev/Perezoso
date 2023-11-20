using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform _ExitTeleport;

    [Header("When Teleport")]
    [SerializeField] private bool _TeleportAlways;
    [SerializeField] private bool _TeleportOnNight;

    [Header("Change Time")]
    [SerializeField] private bool _ChangeTime;
    [SerializeField] private GridChanger _GridChanger;

    [Header("Change Direction")]
    [SerializeField] private bool _ChangeDirection;
    [SerializeField] private bool _ChangeToLeft;

    [Header("Hold Momentum")]
    [SerializeField] private bool _HoldMomentum;

    private void TeleportPlayer()
    {
        GameObject Player = PlayerAi.Player.gameObject;
        Rigidbody2D playerRigidBody = Player.GetComponent<Rigidbody2D>();
        playerRigidBody.isKinematic = true;
        Player.transform.position = _ExitTeleport.position;
        playerRigidBody.isKinematic = false;


        if (_ChangeDirection)
        {
            PlayerAi.Player.ChangeDirection(_ChangeToLeft);
            playerRigidBody.velocity = new Vector2(-playerRigidBody.velocity.x, playerRigidBody.velocity.y);
        }


        if (!_HoldMomentum)
            playerRigidBody.velocity = Vector2.zero;

        if (_ChangeTime)
            _GridChanger.ChangeGrid();

        return;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        if (!_TeleportAlways && GridChanger._IsNight != _TeleportOnNight)
            return;

        TeleportPlayer();
    }
}
