using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridChanger : MonoBehaviour
{
    [Header("Dependecies")]
    [SerializeField] private PlayerSpawn _PlayerSpawn;

    [Header("Tilemap Day")]
    [SerializeField] private TilemapCollider2D[] _DayTileCollider;
    [SerializeField] private TilemapRenderer[] _DayTileRenderer;
    [SerializeField] private Tilemap[] _DayTileMap;
    [SerializeField] private Color _DayColorDisable;
    [SerializeField] private Color _DayColorEnable;

    [Header("Tilemap Night")]
    [SerializeField] private TilemapCollider2D[] _NightTileCollider;
    [SerializeField] private TilemapRenderer[] _NightTileRenderer;
    [SerializeField] private Tilemap[] _NightTileMap;
    [SerializeField] private Color _NightColorDisable;
    [SerializeField] private Color _NightColorEnable;

    [Header("Tilemap Night")]
    [SerializeField] private GameObject _Player;
    [SerializeField] private LayerMask _WallLayer;
    [SerializeField] private float _SizeOffset;

    [HideInInspector] static public bool _IsNight;
    [HideInInspector] public bool _NoChange;

    private void Awake()
    {
        SetDay();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (_NoChange)
                return;

            ChangeGrid();
        }
    }

    public void ChangeGrid()
    {
        if (_IsNight)
        {
            SetDay();
        }
        else
        {
            SetNight();
        }
    }

    private void SetNight()
    {
        foreach(TilemapCollider2D i in _NightTileCollider)
        {
            i.enabled = true;
        }
        foreach (TilemapCollider2D i in _DayTileCollider)
        {
            i.enabled = false;
        }

        foreach (TilemapRenderer i in _NightTileRenderer)
        {
            i.sortingOrder = 3;
        }
        foreach (TilemapRenderer i in _DayTileRenderer)
        {
            i.sortingOrder = 2;
        }

        foreach (Tilemap i in _NightTileMap)
        {
            i.color = _NightColorEnable;
        }
        foreach (Tilemap i in _DayTileMap)
        {
            i.color = _DayColorDisable;
        }

        _IsNight = true;
        CheckPlayerPosition();
    }

    private void SetDay()
    {
        foreach (TilemapCollider2D i in _NightTileCollider)
        {
            i.enabled = false;
        }
        foreach (TilemapCollider2D i in _DayTileCollider)
        {
            i.enabled = true;
        }

        foreach (TilemapRenderer i in _NightTileRenderer)
        {
            i.sortingOrder = 2;
        }
        foreach (TilemapRenderer i in _DayTileRenderer)
        {
            i.sortingOrder = 3;
        }

        foreach (Tilemap i in _NightTileMap)
        {
            i.color = _NightColorDisable;
        }
        foreach (Tilemap i in _DayTileMap)
        {
            i.color = _DayColorEnable;
        }

        _IsNight = false;
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        Collider2D PlayerChecker = Physics2D.OverlapBox(_Player.transform.position, new Vector2(_Player.transform.localScale.x - _SizeOffset, _Player.transform.localScale.y - _SizeOffset), 0, _WallLayer);

        if (PlayerChecker == null)
            return;

        _PlayerSpawn.SpawnPlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(_Player.transform.position, new Vector2(_Player.transform.localScale.x - _SizeOffset, _Player.transform.localScale.y - _SizeOffset));
    }
}
