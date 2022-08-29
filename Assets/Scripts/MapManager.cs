using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{

    //Singleton
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }

    public OverlayTileBehaviour overlayTilePrefab;
    public GameObject overlayContainer;

    ///We check the instance in the awake fuction, 
    ///if this is the first instance created then it assigns it, else we destroy it
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Tilemap tileMap = GetComponentInChildren<Tilemap>();
        if (tileMap == null) { Debug.LogError("The Map Manager needs to be on a grid"); }

        BoundsInt bounds = tileMap.cellBounds;

        //Looping through all of our tiles
        for (int z = bounds.max.z; z > bounds.min.z; z--)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    Vector3Int tileLocation = new Vector3Int(x, y, z);
                    if(tileMap.HasTile(tileLocation))
                    {
                        OverlayTileBehaviour overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                        Vector3 cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);

                        overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y + 0.4f, cellWorldPosition.z + 1);
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder +1;
                    }
                }
            }
        }
    }


}
