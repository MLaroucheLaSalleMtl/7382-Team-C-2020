using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFinal : MonoBehaviour
{
	private static OrderFinal of = null;
	private bool invincible = true;
    private bool attackReady = true;
    private BossManager bm;
    private AudioSource audio;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private SpriteRenderer shield;
    [SerializeField] private GameObject[] gargoyles;

	public static OrderFinal Of { get => of; set => of = value; }
	public bool Invincible { get => invincible; set => invincible = value; }
    public bool AttackReady { get => attackReady; set => attackReady = value; }

    private void Awake()
	{
		if (Of == null)
		{
			Of = this;
		}
		else if (Of != this)
		{
			Destroy(gameObject);
		}
	}
    private void Start()
    {
        bm = BossManager.Bm;
        audio = GetComponent<AudioSource>();
    }

    private static float burstSpeed = 10;
	public IEnumerator Missile(Vector2 target, GameObject bulletPrefab, float burstStartAngle, int burstAmount)
	{
        int burstN;
        float burstTempAngle;
        float burstAngle;

        burstN = 0;
        burstAngle = LifeBossAI.Angle(0, 1, target.x - transform.position.x, target.y - transform.position.y);
        if (LifeBossAI.D(0, 1, target, transform.position) > 0) burstAngle = -burstAngle;
        burstAngle += burstStartAngle;
        audio.PlayOneShot(clips[0]);
        burstTempAngle = burstAngle;
        do
        {
            burstAngle = burstTempAngle;
            for (int i = 0; i < burstAmount; ++i)
            {
                if (this.AttackReady)
                {
                    GameObject bul = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, -burstAngle));
                    bul.GetComponent<Bullet>().SetDirection(OrderBossAi.Vector3Return(burstAngle, transform.position), burstSpeed);
                    bm.ToParent(bul);
                }
                
                burstAngle += 20;
            }
            yield return new WaitForSeconds(0.1f);
            burstN++;
        } while (burstN < 3);

    }

    private static float purpleStartAngle = -75f;
    private static float redStartAngle = -45f;
    private static float constantSpeed = 5f;
    private static float constantDelay = 0.8f;
    public IEnumerator ConstantFiring(GameObject purplePrefab)
    {
        if (this.AttackReady)
        {
            int constantN = 0;
            float constantAngle;
            while (true)
            {
                if (constantN % 2 == 0) constantAngle = purpleStartAngle;
                else constantAngle = redStartAngle;

                for (int i = 0; i < 3; ++i)
                {
                    if (this.AttackReady)
                    {
                        GameObject bul = Instantiate(purplePrefab, transform.position, transform.rotation);
                        if (constantN % 2 != 0) bul.GetComponent<SpriteRenderer>().color = Color.yellow;
                        bul.GetComponent<Bullet>().SetDirection(OrderBossAi.Vector3Return(constantAngle, transform.position), constantSpeed);
                        bm.ToParent(bul);
                    }
                    constantAngle += 60;
                }
                ++constantN;
                yield return new WaitForSeconds(constantDelay);
            }
        }
    }
    public IEnumerator GargoyleFire(GameObject firePrefab)
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            if (this.AttackReady)
            {
                audio.PlayOneShot(clips[1]);
                for (int i = 0; i < gargoyles.Length; ++i)
                {
                    if (this.AttackReady)
                    {
                        gargoyles[i].GetComponent<SpriteRenderer>().color = OrderBossAi.red;
                        GameObject fire = Instantiate(firePrefab, gargoyles[i].transform.position, Quaternion.Euler(0, 0, 90));
                        fire.GetComponent<GarFire>().Vertical = true;
                    }
                }
                yield return new WaitForSeconds(3);
                gargoyles[0].GetComponent<SpriteRenderer>().color = Color.white;
                gargoyles[1].GetComponent<SpriteRenderer>().color = Color.white;
            }
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
}
