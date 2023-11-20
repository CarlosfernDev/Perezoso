using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [Header("Dependecies")]
    [SerializeField] private PlayerSpawn _PlayerSpawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        _PlayerSpawn.SpawnPlayer();
    }
}
