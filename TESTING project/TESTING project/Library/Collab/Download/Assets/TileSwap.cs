using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSwap : MonoBehaviour
{
    private TileBase tileA;
    private TileBase tileB;
    [SerializeField] private TileBase tileHot;
    [SerializeField] private TileBase tileCool;
    private bool ready = false;
    [SerializeField] public float coolDelay;
    [SerializeField] public float coolWarning;
    [SerializeField] private float tickRate;
    private Tilemap tilemap;
    private ChaosBossAi boss;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        boss = GameObject.Find("Boss-ChaosStage-1").GetComponent<ChaosBossAi>();
        Initialize();
    }
    private void Initialize()
    {
        tileA = tileHot;
        tileB = tileCool;
    }
    public void TileCool()
    {
        tilemap.SwapTile(tileHot, tileCool);
        StartCoroutine(Warning());
        boss.invincible = false;
    }
    private IEnumerator Warning()
    {
        yield return new WaitForSecondsRealtime(coolDelay);
        Invoke("TileHeat", coolWarning);
        do
        {
            Swap();
            yield return new WaitForSecondsRealtime(tickRate);
            
        } while (!ready);
        ready = false;
        Initialize();
    }
    private void Swap()
    {
        tilemap.SwapTile(tileA, tileB);
        TileBase temp = tileA;
        tileA = tileB;
        tileB = temp;
    }
    private void TileHeat()
    {
        tilemap.SwapTile(tileCool, tileHot);
        ready = true;
        boss.invincible = true;
    }
    
}
