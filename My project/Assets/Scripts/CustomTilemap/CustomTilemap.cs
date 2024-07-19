using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;

public class CustomTilemap : MonoBehaviour {
    public Tilemap tilemap;
    private GameObject destructibleTilePrefab;
    public string tilePath;
    private List<GameObject> tileSet = new List<GameObject>();

    void Awake() {
        tilemap = GetComponent<Tilemap>();
        // tilePath = "DestructibleTiles/Ice";
        destructibleTilePrefab = Resources.Load<GameObject>(tilePath);
        Initialize();
    }

    // Credit to ChatGPT
    void Initialize()
    {
        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);
            if (tile != null)
            {
                Vector3 worldPosition = tilemap.CellToWorld(position) + Vector3.Scale(tilemap.tileAnchor, transform.localScale);
                GameObject newTile = PhotonNetwork.Instantiate(tilePath, worldPosition, Quaternion.identity);
                newTile.transform.parent = transform;
                tileSet.Add(newTile);
            }
        }

        // Optionally, clear the original tilemap to avoid rendering overlap
        tilemap.ClearAllTiles();
    }

    public void Reset() {
        foreach (GameObject tile in tileSet) {
            tile.GetComponent<PhotonCustomControl>().EnableRPC();
        }
    }
}
