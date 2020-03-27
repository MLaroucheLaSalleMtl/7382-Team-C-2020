using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosFinal : MonoBehaviour
{
    private static ChaosFinal cf = null;
    private bool invincible = true;
    private bool attackReady;

    public static ChaosFinal Cf { get => cf; set => cf = value; }
    public bool Invincible { get => invincible; set => invincible = value; }
    public bool AttackReady { get => attackReady; set => attackReady = value; }

    private void Awake()
    {
        if (Cf == null)
        {
            Cf = this;
        }
        else if (Cf != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void AoeUnder(Vector2 target,GameObject prefab)
    {
        GameObject explosion = Instantiate(prefab, target, Quaternion.identity);
    }
    public IEnumerator FireLine(Vector2 target, GameObject prefab, int length, int wallNumber, float[] array, float interval)
    {
        if (target.y < 0) interval = -interval; 
        for (int i = 0; i < wallNumber; ++i)
        {
            for(int i2 = 0; i2 < length; ++i2)
            {
                array[i2] = 0 + (interval * i2);
                GameObject fireBall = Instantiate(prefab, new Vector2(23, array[i2]), Quaternion.identity);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
    public void Meteor(Vector2 target, GameObject prefab, int meteorAmount, float radius)
    {
        for(int i = 0; i < meteorAmount; ++i)
        {
            Vector2 position;
            do
            {
                position = new Vector2(target.x + Random.Range(-radius, radius), target.y + Random.Range(-radius, radius));
            } while (Vector2.Distance(target, position) > 1);
            GameObject meteor = Instantiate(prefab, position, Quaternion.identity);
        }
    }
}
