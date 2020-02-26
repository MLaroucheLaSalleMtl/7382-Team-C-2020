using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosTile : MonoBehaviour
{
    private GameManager code;
    private bool isTouching = false;
    [SerializeField] private float damageTick = 0.9f;
    [SerializeField] private float damageValue = 3f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTouching = true;
        code.GetHit(damageValue);
        StartCoroutine(ChaosTileDamage());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;
    }

    private IEnumerator ChaosTileDamage()
    {
        while (isTouching)
        {
            yield return new WaitForSecondsRealtime(damageTick);
            code.GetHit(damageValue);
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
        
    }
}
