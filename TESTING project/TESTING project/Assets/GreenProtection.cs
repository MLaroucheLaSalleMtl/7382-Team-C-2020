using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenProtection : MonoBehaviour
{
    public static GreenProtection instance = null;
    [SerializeField]private float touched = 0;
    private TileSwap tileSwap;
    [SerializeField]private GameObject[] greenPatches;
    private AudioSource audio;

    private void Awake()
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
    public void Collision()
    {
        ++touched;
        audio.Play();
        if (touched == 6)
        {
            touched = 0;
            tileSwap.TileCool();
            Invoke("ColliderActive", tileSwap.coolDelay + tileSwap.coolWarning);
        }
    }
    private void ColliderActive()
    {
        for (int i = 0; i <greenPatches.Length; ++i)
        {
            Debug.Log("test");
            greenPatches[i].GetComponent<BoxCollider2D>().enabled = true;
            greenPatches[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        tileSwap = GameObject.Find("Tilemap").GetComponent<TileSwap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
