using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFinal : MonoBehaviour
{
    private GameManager code;
    private float damage = 1;
    private bool isTouching = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTouching = true;
        StartCoroutine(TakeDamage());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        damage = 1;
        isTouching = false;
        
    }
    private IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(2);
        while (isTouching)
        {
            code.GetHit(Mathf.Floor(damage));
            
            yield return new WaitForSeconds(0.9f);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (isTouching)
        {
            damage += damage * 0.002f;
        }
    }
}
