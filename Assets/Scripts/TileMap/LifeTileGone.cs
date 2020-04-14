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
    private AudioSource audio;
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
        audio = GetComponent<AudioSource>();
    }
    public void Swap()
    {
        tilemap.SwapTile(tileA, tileBlack);
        if (swapCounter > 0) tilemap.SwapTile(tileB, tileBlack);
        parents[swapCounter].BroadcastMessage("ActivateCollider", swapCounter);
        audio.Play();
        ++swapCounter;
        
    }
}
