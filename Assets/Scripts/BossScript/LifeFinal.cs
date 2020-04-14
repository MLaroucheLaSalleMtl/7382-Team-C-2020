using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFinal : MonoBehaviour
{
    private static LifeFinal lf = null;
    private bool invincible = true;
    private bool attackReady = true;
    private BossManager bm;
    private AudioSource audio;
    [SerializeField] private AudioClip[] clips;
    [SerializeField]private SpriteRenderer shield;
    [SerializeField] private ParticleSystem windParticles;
    [SerializeField] private ParticleSystem windParticles2;
    private static Vector3 rightPos = new Vector3(-23, 0, 0);
    private static Vector3 leftPos = new Vector3(23, 0, 0);

    public static LifeFinal Lf { get => lf; set => lf = value; }
    public bool Invincible { get => invincible; set => invincible = value; }
    public bool AttackReady { get => attackReady; set => attackReady = value; }

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
    private void Start()
    {
        bm = BossManager.Bm;
        audio = GetComponent<AudioSource>();
    }
    private float angleHoming;
    public void IceShardTarget(Vector2 target, GameObject homingShard)
    {
        if (this.AttackReady)
        {
            GameObject shard = Instantiate(homingShard, transform.position, Quaternion.identity);
            angleHoming = LifeBossAI.Angle(-3.681f, -4.236f, target.x - transform.position.x, target.y - transform.position.y);
            if (LifeBossAI.D(-3.681f, -4.236f, target, transform.position) < 0) { angleHoming = -angleHoming; }
            shard.transform.Rotate(0, 0, angleHoming);
            shard.GetComponent<IceShardHoming>().Shoot(target);
            bm.ToParent(shard);
        }
    }
    private float angle;
    private float incrementX;
    private float incrementY;
    float fireballX;
    float fireballY;
    public IEnumerator FireLine(Vector2 target, GameObject iceShardPrefab)
    {
        audio.PlayOneShot(clips[0]);
        incrementX = 0.09f * (target.x - transform.position.x);
        incrementY = 0.09f * (target.y - transform.position.y);
        angle = LifeBossAI.Angle(-3.681f, -4.236f, target.x - transform.position.x, target.y - transform.position.y);
        if (LifeBossAI.D(-3.681f, -4.236f, target, transform.position) < 0) { angle = -angle; }

        for (int i = 0; i < 100; ++i)
        {
            yield return new WaitForSecondsRealtime(0.04f);
            if (this.AttackReady)
            {
                GameObject iceShard = Instantiate(iceShardPrefab, new Vector2(transform.position.x + fireballX, transform.position.y + fireballY), Quaternion.identity);
                iceShard.transform.Rotate(0, 0, angle);
                bm.ToParent(iceShard);
            }
            
            fireballX += incrementX;
            fireballY += incrementY;
        }
    }
    public IEnumerator Wind(Vector2 target, Rigidbody2D rigid, float force, int length, bool xPosition)
    {
        audio.PlayOneShot(clips[1]);
        if (transform.position.x > target.x)
        {
            force = -force;
            xPosition = false;
        }
        Vector2 pushForce = new Vector2(force, 0);
        for(int i = 0; i < length; ++i)
        {
            if (this.AttackReady)
            {
                rigid.AddForce(pushForce);
                if (xPosition) windParticles2.Play();
                else windParticles.Play();
            }
            yield return new WaitForSeconds(0.01f);
        }
        
    }
    private void GetHit(float damage)
    {
        if (!Invincible) bm.ReduceHp(damage);
        else bm.ReduceHp(0);

    }

    public void Switch(bool toggle)
    {
        if (toggle)
        {
            shield.enabled = false;
            Invincible = false;

        }
        else
        {
            shield.enabled = true;
            Invincible = true;
        }
    }
    public void Teleport()
    {
        if (transform.position == leftPos) transform.position = rightPos;
        else transform.position = leftPos;
        transform.Rotate(0, 0, 180);

    }
}
