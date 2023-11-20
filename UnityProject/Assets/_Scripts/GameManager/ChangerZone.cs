using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerZone : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GridChanger _GridChanger;

    [Header("Changer")]
    [SerializeField] bool _CantChange;
    [SerializeField] bool _ForceToChange;
    [SerializeField] bool _ChangeToDay;

    [SerializeField] bool _AlternativeChange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player")
            return;

        if (_ForceToChange)
        {
            if (!_AlternativeChange)
                GridChanger._IsNight = _ChangeToDay;

            _GridChanger.ChangeGrid();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player")
            return;

        if (_CantChange)
            _GridChanger._NoChange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player")
            return;

        if (_CantChange)
            _GridChanger._NoChange = false;
    }
}
