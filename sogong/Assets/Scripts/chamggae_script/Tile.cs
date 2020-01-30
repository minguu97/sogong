using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile : MonoBehaviour
{
    public Tilemap tilemap;

    private void OnMouseDrag()
    {
        try
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);


            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);


            if (this.tilemap = hit.transform.GetComponent<Tilemap>())
            {
                int x, y;
                x = this.tilemap.WorldToCell(ray.origin).x;
                y = this.tilemap.WorldToCell(ray.origin).y;

                Vector3Int v3Int = new Vector3Int(x, y, 0);

                this.tilemap.SetTileFlags(v3Int, TileFlags.None);

                this.tilemap.SetColor(v3Int, (Color.clear));

            }
        }
        catch (NullReferenceException)
        {

        }
    }

    public void ResetTile() 
    {
        this.tilemap.RefreshAllTiles();
    }
}
