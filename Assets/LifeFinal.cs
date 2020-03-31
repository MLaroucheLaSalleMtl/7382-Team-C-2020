using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFinal : MonoBehaviour
{
    private static LifeFinal lf = null;
    private bool invincible = true;

    public static LifeFinal Lf { get => lf; set => lf = value; }
    public bool Invincible { get => invincible; set => invincible = value; }

    private void Awake()
    {
        if (Lf == null)
        {
            Lf = this;
        }
        else if (Lf != this)
        {
            Destroy(gameObject);
        }
    }
    private float angleHoming;
    public void IceShardTarget(Vector2 target, GameObject homingShard)
    {
        GameObject shard = Instantiate(homingShard, transform.position, Quaternion.identity);
        angleHoming = LifeBossAI.Angle(-3.681f, -4.236f, target.x - transform.position.x, target.y - transform.position.y);
        if (LifeBossAI.D(-3.681f, -4.236f, target, transform.position) < 0) { angleHoming = -angleHoming; }
        shard.transform.Rotate(0, 0, angleHoming);
        shard.GetComponent<IceShardHoming>().Shoot(target);
    }
    private float angle;
    private float incrementX;
    private float incrementY;
    float fireballX;
    float fireballY;
    public IEnumerator FireLine(Vector2 target, GameObject iceShardPrefab)
    {
        incrementX = 0.09f * (target.x - transform.position.x);
        incrementY = 0.09f * (target.y - transform.position.y);
        angle = LifeBossAI.Angle(-3.681f, -4.236f, target.x - transform.position.x, target.y - transform.position.y);
        if (LifeBossAI.D(-3.681f, -4.236f, target, transform.position) < 0) { angle = -angle; }

        for (int i = 0; i < 100; ++i)
        {
            yield return new WaitForSecondsRealtime(0.04f);

            GameObject iceShard = Instantiate(iceShardPrefab, new Vector2(transform.position.x + fireballX, transform.position.y + fireballY), Quaternion.identity);
            iceShard.transform.Rotate(0, 0, angle);
            fireballX += incrementX;
            fireballY += incrementY;
        }
    }
}
