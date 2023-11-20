using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    [SerializeField] private int _Scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player")
            return;

        MySceneManager.Instance.NextScene(_Scene);
    }
}
