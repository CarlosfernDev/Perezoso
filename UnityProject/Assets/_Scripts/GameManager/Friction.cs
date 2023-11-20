using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friction : MonoBehaviour
{
    [SerializeField] private float _SpeedChange;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player")
            return;

        collision.gameObject.GetComponent<PlayerAi>().ChangeSpeed(_SpeedChange);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player")
            return;

        collision.gameObject.GetComponent<PlayerAi>().ResetSpeed();
    }
}
