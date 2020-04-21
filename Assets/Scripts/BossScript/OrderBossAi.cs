using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderBossAi : MonoBehaviour
{
	#region //General
	[Header("General", order = 0)]
	[SerializeField] private Transform target;
	[SerializeField] private AudioClip[] clips;
    [SerializeField] private SpriteRenderer tookDamage;
	private AudioSource audio;
	private float hp;
	private static float maxHp = 200;
	private UiChaos ui;
	#endregion

	#region //Burst
	[Header("Burst", order = 1)]
	[SerializeField] private GameObject bulletPrefab;
	private float burstAngle;
	private float burstTempAngle;
	private float burstStartAngle = -20;
	private float burstFocusAngle;
	private int burstAmount = 3;
	private float burstDelay = 2;
	private static float burstSpeed = 10f; 
	private int burstN;
	#endregion

	#region //Constant
	[Header("Constant", order = 2)]
	[SerializeField] private GameObject purplePrefab;
	private static float constantSpeed = 5f;
	private static float purpleStartAngle = -75f;
	private static float redStartAngle = -45f;
	private float constantAngle;
	private static float constantDelay = 0.5f;
	private int constantN = 0;

    #endregion

    #region //Gargoyle
    [SerializeField] private GameObject gargoyleFirePrefab;
	[SerializeField] private GameObject[] gargoyleA;
	[SerializeField] private GameObject[] gargoyleB;
	private float gargoyleDelay = 10f;
	private float gargoyleClose;
	private float gargoyleDistance;
	private bool gargoyleBoolA;
    public static Vector4 red = new Vector4(1, 0.337f, 0.337f, 1);
	#endregion

	// Start is called before the first frame update
	void Start()
	{
		hp = maxHp;
		audio = GetComponent<AudioSource>();
		ui = UiChaos.instance;
		ui.HpUpdate(hp);
		StartCoroutine(Missile());
		StartCoroutine(ConstantShot());
        StartCoroutine(GargoyleShoot());
    }

	
	private IEnumerator GargoyleShoot()
	{
		while (true)
		{
			yield return new WaitForSeconds(gargoyleDelay);
            gargoyleClose = 100f;
            for (int i = 0; i < gargoyleA.Length; ++i)
			{
                gargoyleDistance = Mathf.Abs(gargoyleA[i].transform.position.y - target.position.y);

				if (gargoyleDistance < gargoyleClose)
				{
					gargoyleClose = gargoyleDistance;
					gargoyleBoolA = true;
				}
                gargoyleDistance = Mathf.Abs(gargoyleB[i].transform.position.y - target.position.y);
				if (gargoyleDistance < gargoyleClose)
				{

					gargoyleClose = gargoyleDistance;
                    gargoyleBoolA = false;
				} 
			}
            audio.PlayOneShot(clips[1]);
            if (gargoyleBoolA)
            {
                foreach(GameObject gar in gargoyleA)
                {
                    gar.GetComponent<SpriteRenderer>().color = red;
                    GameObject fire = Instantiate(gargoyleFirePrefab, gar.transform.position, Quaternion.Euler(0, 0, 180));
                }
                yield return new WaitForSeconds(3);
                foreach(GameObject gar in gargoyleA)
                {
                    gar.GetComponent<SpriteRenderer>().color = Color.white;
                }
                
            }
            else
            {
                foreach(GameObject gar in gargoyleB)
                {

                    gar.GetComponent<SpriteRenderer>().color = red;
                    GameObject fire = Instantiate(gargoyleFirePrefab, gar.transform.position, Quaternion.Euler(0, 0, 0));
                }
                yield return new WaitForSeconds(3);
                foreach (GameObject gar in gargoyleB)
                {
                    gar.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
		}
		

	}
	private IEnumerator Missile()
	{
		while (true)
		{
			burstN = 0;
			burstAngle = LifeBossAI.Angle(0, 1, target.position.x - transform.position.x, target.position.y - transform.position.y);
			if (LifeBossAI.D(0, 1, target.position, transform.position) > 0) burstAngle = -burstAngle;
			burstAngle += burstStartAngle;
			burstTempAngle = burstAngle;
            audio.PlayOneShot(clips[0]);
            do
			{
				burstAngle = burstTempAngle;
				for (int i = 0; i < burstAmount; ++i)
				{
					GameObject bul = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, -burstAngle));
					
					bul.GetComponent<Bullet>().SetDirection(Vector3Return(burstAngle, transform.position), burstSpeed);
					burstAngle += 20;
				}
				yield return new WaitForSeconds(0.1f);
				burstN++;
			} while (burstN < 3);
            
            yield return new WaitForSeconds(burstDelay);
		}
		
	}
	private IEnumerator ConstantShot()
	{
        for (int i2 = 1 ; i2 > -1; ++i2) //goes on forever
        {
            if (constantN % 2 == 0) constantAngle = purpleStartAngle;
            else constantAngle = redStartAngle;

            for (int i = 0; i < 3; ++i)
            {
                GameObject bul = Instantiate(purplePrefab, transform.position, transform.rotation);
                if (constantN % 2 != 0) bul.GetComponent<SpriteRenderer>().color = Color.yellow;
                bul.GetComponent<Bullet>().SetDirection(Vector3Return(constantAngle, transform.position), constantSpeed);
                constantAngle += 60;
            }
            ++constantN;
            yield return new WaitForSeconds(constantDelay);
            if (i2 % 10 == 0) yield return new WaitForSeconds(3);//gives a break
        }
			
		
	}
    public static Vector2 Vector3Return(float angle, Vector3 pos)
	{
		float x = pos.x + Mathf.Sin(angle * Mathf.PI / 180f);
		float y = pos.y + Mathf.Cos(angle * Mathf.PI / 180f);
		return (new Vector3(x, y) - pos).normalized;
	}
    private int tookDamageCounter = 0;
	public void GetHit(float damage)
	{
		hp = hp - damage;
		ui.HpUpdate(hp);
		HpCheck();
		audio.PlayOneShot(clips[2]);
        tookDamage.enabled = true;
        tookDamageCounter++;
        Invoke("RemoveDamage", 0.5f);
	}
    private void RemoveDamage()
    {
        tookDamageCounter = BridgeBossAi.RemoveDamage(tookDamageCounter, tookDamage);
    }
	private void HpCheck()
	{
		if(hp <= maxHp * 0.5f)
		{
			burstDelay = 1.5f;
			burstStartAngle = -40;
			burstAmount = 5;
            gargoyleDelay = 7f;
		}
		if (hp <= 0) ui.Die("FinalBoss");
	}
}
