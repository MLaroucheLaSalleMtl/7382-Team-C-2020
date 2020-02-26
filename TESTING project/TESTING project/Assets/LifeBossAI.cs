using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBossAI : MonoBehaviour
{
    [Header("General",order = 0)]
	[SerializeField] private Transform target;
    public Transform Target { get => target; set => target = value; }
    [SerializeField] private Rigidbody2D targetR;
    [SerializeField] private bool attackReady = true;
    [SerializeField] private float attackChance;
    [SerializeField] private int previousAttack = 0;
    [SerializeField] private int attackToDo = 0;
    private bool spawnDelay = false;
    private static float spawnLeft = -23;
    private static float spawnRight = 23;
    private static float rotationLeft = 90;
    private static float rotationRight = 270;
    private float initialPos;
    private float previousPos;
    private float initialRot;
    private bool firstTime = true;
    public ParticleSystem windParticles;

    #region//Line
    [Header("FireLine Attack",order =1)]
    [SerializeField] private GameObject iceShardPrefab;
    [SerializeField]private float fireLineCooldown;

    private float reducedX;
    private float reducedY;
    private float incrementX;
    private float incrementY;

    
    private float angle;
    #endregion

    #region//Wind
    [Header("Wind Attack",order = 2)]
    [Tooltip("Should not be changed")][SerializeField] private float yieldValue;
    [SerializeField] private float forceX;
    [SerializeField] private float forceLength;
    [SerializeField] private float windCooldown;
    private Vector2 force;
    #endregion

    #region//Homing
    [Header("Homing Attack", order = 3)]
    [SerializeField] private GameObject homingShard;
    [SerializeField] private int amount;
    [SerializeField] private float homingCooldown;
    [SerializeField] private float delayBetweenShards;
    private GameObject shard;
    private float angleHoming;
    
    #endregion



    // Start is called before the first frame update
    void Start()
	{
        //make battle cry
        Invoke("Spawn", 5f);
        Invoke("AttackCooldown", 7f);
        
    }

	// Update is called once per frame
	void Update()
	{
		if(attackReady && spawnDelay && Random.Range(0,100) > attackChance)
        {
            do
            {
                attackToDo = Random.Range(0, 3);
            } while (attackToDo == previousAttack);
            previousAttack = attackToDo;

            switch (attackToDo)
            {
                case 0:
                    StartCoroutine(Wind(windCooldown));
                    break;
                case 1:
                    StartCoroutine(FireLine(Target, fireLineCooldown));
                    break;
                case 2:
                    StartCoroutine(IceShardTarget(homingCooldown, Target));
                    break;
            }
            attackReady = false;
        }
	}

    public void Spawn()
    {
        //make battle cry
        if (firstTime)
        {
            firstTime = false;
            if (Random.Range(0, 2) == 0)
            {
                initialPos = spawnLeft;
                initialRot = rotationLeft;

            }
            else
            {
                initialPos = spawnRight;
                initialRot = rotationRight;
            }
            previousPos = initialPos;
        }
        else
        {
            if(previousPos == spawnLeft)
            {
                initialPos = spawnRight;
                initialRot = rotationRight;
            }
            else
            {
                initialPos = spawnLeft;
                initialRot = rotationLeft;
            }
        }
        
        spawnDelay = true;
        Teleport(initialPos, initialRot);
    }
    private void Teleport(float x, float z)
    {
        transform.position = new Vector3(x, -0.5f);
        transform.eulerAngles = new Vector3(0, 0, z);
    }
	
	private IEnumerator IceShardTarget(float cooldown, Transform lockedTarget)
    {
        Invoke("AttackCooldown", cooldown);
        for (int i = 0; i < amount; ++i)
        {
            shard = Instantiate(homingShard, transform.position, Quaternion.identity);
            angleHoming = Angle(-3.681f, -4.236f, lockedTarget.position.x - transform.position.x, lockedTarget.position.y - transform.position.y);
            if (D(-3.681f, -4.236f, Target, transform) < 0) { angleHoming = -angleHoming; }
            shard.transform.Rotate(0, 0, angleHoming);
            shard.GetComponent<IceShardHoming>().Shoot(Target);
            yield return new WaitForSecondsRealtime(delayBetweenShards);
        }
    }
    public float Angle(float uX, float uY, float vX, float vY)
    {
        float scalarProduct = uX * vX + uY * vY;
        
        float normProduct = Mathf.Sqrt(uX * uX + uY * uY) * Mathf.Sqrt(vX * vX + vY * vY);
        return Mathf.Acos(scalarProduct / normProduct) * Mathf.Rad2Deg;
    }
    public float D(float uX, float uY, Transform lockedTarget, Transform baseTransform)
    {
        float vectorX = baseTransform.position.x - uX;
        float vectorY = baseTransform.position.y - uY;
        return ((lockedTarget.position.x - baseTransform.position.x) * (vectorY - baseTransform.position.y)) - ((lockedTarget.position.y - baseTransform.position.y) * (vectorX - baseTransform.position.x));
    }
	private IEnumerator FireLine(Transform lockedTarget, float cooldown)
	{
        Invoke("AttackCooldown", cooldown);
        //bunch of math equations
		reducedX = 0.9f * (lockedTarget.position.x - transform.position.x);
		reducedY = 0.9f * (lockedTarget.position.y - transform.position.y);
		incrementX = reducedX * 0.1f;
		incrementY = reducedY * 0.1f;
		float fireballX = 0;
		float fireballY = 0;
		float n = 0;
		
        angle = Angle(-3.681f, -4.236f, lockedTarget.position.x - transform.position.x, lockedTarget.position.y - transform.position.y);        
		if(D(-3.681f, -4.236f, lockedTarget, transform) < 0) { angle = -angle; }
		
		for (int i = 0; i < 100; ++i)
		{
			yield return new WaitForSecondsRealtime(0.04f);

			GameObject iceShard = Instantiate(iceShardPrefab, new Vector2(transform.position.x + fireballX, transform.position.y + fireballY), Quaternion.identity);
			iceShard.transform.Rotate(0, 0, angle);
			fireballX += incrementX;
			fireballY += incrementY;
			n += 1f;
		}
	}
    
    private IEnumerator Wind(float cooldown)
    {
        windParticles.Play();
        Invoke("AttackCooldown", cooldown);
        float n = 0;
        if(transform.position.x < Target.position.x) { force = new Vector2(forceX, 0); }
        else { force = new Vector2(-forceX, 0); }
        do
        {
            
            targetR.AddForce(force);
            yield return new WaitForSecondsRealtime(yieldValue);
            ++n;
        } while (n < 200);
        
        
    }
    private void AttackCooldown()
    {
        attackReady = true;
    }
}
