using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundManager : MonoBehaviour
{
    Tilemap tiles;
    Grid grid;

    void Start()
    {
        tiles = transform.GetComponentInChildren<Tilemap>();
        grid = transform.GetComponentInChildren<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            Vector3Int cellPosition = grid.WorldToCell(pz);
            var tile = tiles.GetTile(cellPosition);
            Debug.Log("Tile name = " + tile.name);
            //tiles.SetTile(cellPosition, null);
        }
    }

    public static GroundManager GetInstance()
    {
       return FindObjectOfType<GroundManager>();
    }


}
