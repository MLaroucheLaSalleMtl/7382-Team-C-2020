using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidTile : MonoBehaviour
{
    private GameManager code;
    [SerializeField] private BoxCollider2D[] boxes;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            code.GetHit(25);
            StartCoroutine(Alive(collision.transform.parent.gameObject));
        }
    }
    private IEnumerator Alive(GameObject player)
    {
        player.transform.position = new Vector2(0, 0);
        for (int i = 0; i < boxes.Length; ++i) boxes[i].enabled = true; 
        yield return new WaitForSecondsRealtime(3f);
        for (int i = 0; i < boxes.Length; ++i) boxes[i].enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
