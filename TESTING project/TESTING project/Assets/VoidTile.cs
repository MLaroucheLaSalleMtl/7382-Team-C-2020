using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidTile : MonoBehaviour
{
    private GameManager code;
    [SerializeField] private int val;
    [SerializeField] private GameObject[] boxes;
    private EdgeCollider2D edgeCollider2D;
    private IsometricPlayerMovementController iso;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player" && !iso.isDashing)
        {
            code.GetHit(10);
            StartCoroutine(Alive(collision.transform.parent.gameObject));
        }
    }
    private IEnumerator Alive(GameObject player)
    {
        if (Random.Range(0, 2) == 1) player.transform.position = new Vector2(-12.25f, 3.31f);
        else player.transform.position = new Vector2(12.25f, -3.31f);
        for (int i = 0; i < boxes.Length; ++i) boxes[i].SetActive(true); 
        yield return new WaitForSecondsRealtime(3f);
        for (int i = 0; i < boxes.Length; ++i) boxes[i].SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        code = GameManager.instance;
        iso = GameObject.Find("Player and Input").GetComponent<IsometricPlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateCollider(int counter)
    {
        if (val == counter) edgeCollider2D.enabled = true;
        
    }
}
