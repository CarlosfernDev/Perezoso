using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [SerializeField] private Transform StopPoint;
    private Transform PlayerStoped;
    private bool Stoped;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player")
            return;


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player" || Stoped)
            return;

        if(collision.transform.position.x - StopPoint.position.x > 0)
        {
            Debug.Log("Para");
            collision.gameObject.GetComponent<PlayerAi>().SetStopBool(true);
            PlayerStoped = collision.transform;
            collision.transform.parent = gameObject.transform;
            Stoped = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player" || !Stoped)
            return;

        PlayerStoped = null;
        Stoped = false;
    }
}
