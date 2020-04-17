using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFinalClose : MonoBehaviour
{
    private LifeFinal lf;
    private bool isInside = false;
    private float timeSpentClose;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            isInside = true;
            StartCoroutine(Tick());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInside = false;
        timeSpentClose = timeSpentClose * 0.5f;
    }
    private IEnumerator Tick()
    {
        while (isInside)
        {
            timeSpentClose++;
            if (lf.AttackReady && timeSpentClose > 10) lf.Teleport(); ;
            yield return new WaitForSeconds(1f);
        }
        

    }
    // Start is called before the first frame update
    void Start()
    {
        lf = transform.parent.GetComponent<LifeFinal>();
    }
}
