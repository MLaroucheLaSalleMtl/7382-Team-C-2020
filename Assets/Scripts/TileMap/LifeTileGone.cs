using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LifeTileGone : MonoBehaviour
{
    public static LifeTileGone instance = null;
    [SerializeField] private TileBase tileA;
    [SerializeField] private TileBase tileB;
    [SerializeField] private TileBase tileBlack;
    private Tilemap tilemap;
    private int swapCounter = 0;
    [SerializeField] private GameObject[] parents;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    private void Update()
    {
    }
    public void Swap()
    {
        tilemap.SwapTile(tileA, tileBlack);
        if (swapCounter > 0) tilemap.SwapTile(tileB, tileBlack);
        parents[swapCounter].BroadcastMessage("ActivateCollider", swapCounter);
        ++swapCounter;
        
    }
}
