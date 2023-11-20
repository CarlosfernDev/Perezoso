using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] GameObject Player;

    private void Start()
    {
        Player = PlayerAi.Player.gameObject;
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Rigidbody2D playerRigidBody = Player.GetComponent<Rigidbody2D>();
        playerRigidBody.isKinematic = true;
        Player.transform.position = transform.position;
        playerRigidBody.isKinematic = false;
        playerRigidBody.velocity = Vector2.zero;
        Player.GetComponent<PlayerAi>().ResetPlayer();
    }
}
