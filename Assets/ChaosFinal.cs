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
    public void AoeUnder(Vector2 target,GameObject prefab)
    {
        GameObject explosion = Instantiate(prefab, target, Quaternion.identity);
    }
    public IEnumerator FireLine(Vector2 target, GameObject prefab, GameObject parent, int wallNumber, float startPos, float[] array, float interval)
    {
        GameObject p = Instantiate(parent, transform.position, transform.rotation);
        int safe;
        if (target.y < 5) interval = -interval;//goes downward
        if (target.y < 5 && target.y > -5) startPos = 15; //middle
        for (int i = 0; i < wallNumber; ++i)
        {
            safe = Random.Range(9, 20);
            for(int i2 = 0; i2 < array.Length; ++i2)
            {
                if(i2 != safe && i2 != safe + 1)
                {
                    array[i2] = startPos + (interval * i2);
                    GameObject fireBall = Instantiate(prefab, new Vector2(23, array[i2]), Quaternion.identity);
                    fireBall.transform.parent = p.transform;
                }
                
            }
            yield return new WaitForSeconds(2f);
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
            } while (Vector2.Distance(target, position) > 5);
            GameObject meteor = Instantiate(prefab, position, Quaternion.identity);
        }
    }
}
