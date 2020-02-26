using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosBossAi : MonoBehaviour
{
    #region//General variables

    [Header("General",order = 0)]
    [SerializeField] private Transform target;
    [SerializeField]private bool attackReady = true;
    [SerializeField] private float attackChance;
    [SerializeField]private int previousAttack = 0;
    [SerializeField]private int attackToDo = 0;
    #endregion
    #region//Aoe variables
    [Header("Aoe Variables", order = 1)]
    [SerializeField] private float aoeCooldown;
    [SerializeField] private GameObject circle;
    [SerializeField]private float distanceBig = 2;
    #endregion

    #region //firewall variables
    [Header("Firewall Variables", order = 2)]
    [SerializeField] private float fireWallCooldown;
    [SerializeField] private int arraySize;
    [SerializeField] private float interval;
    [SerializeField] private GameObject fire;
    [SerializeField] private float distanceWall;
    private float[] array;
    private float firewallAB;
    private int noSpawn;
    private int wallAmount = 3;
    #endregion

    #region//meteor variables
    [Header("Meteor Variables", order = 3)]
    [SerializeField] private float meteorCooldown;
    [SerializeField] private GameObject meteor;
    [SerializeField] private float radius = 4;
    [SerializeField]private int meteorNumber = 10;
    private Vector2 position;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        firewallAB = Mathf.Ceil(arraySize * 0.5f);
        array = new float[arraySize];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackReady && Random.Range(0, 100) > attackChance)
        {
            do
            {
                attackToDo = Random.Range(0, 3);
            } while (attackToDo == previousAttack);


            previousAttack = attackToDo;
                switch (attackToDo)
                {
                    case 0: AoeUnder(aoeCooldown, target);
                        break;
                    case 1: StartCoroutine(FireWall(fireWallCooldown, target.position.x, wallAmount));
                        break;
                    case 2: Meteor(meteorCooldown, target.position);
                        break;
                }
                    
                attackReady = false;
        }
    }

    private void AoeUnder(float cooldown, Transform lockedTarget)
    {
        Invoke("AttackCooldown", cooldown);
        GameObject aoeUnder;
        if (Vector2.Distance(transform.position, lockedTarget.position) < distanceBig)
        {
            aoeUnder = Instantiate(circle, transform.position, transform.rotation);
            aoeUnder.transform.localScale *= 3;
        }
        else
        {
            aoeUnder = Instantiate(circle, target.position, transform.rotation);
        }
    }
    private IEnumerator FireWall(float cooldown, float lockedTarget, int wallNumber)
    {
        Invoke("AttackCooldown", cooldown);
        for (int i = 0; i < wallNumber; ++i)
        {
            noSpawn = Random.Range(0 + (int)Mathf.Ceil(firewallAB * 0.5f), arraySize - (int)Mathf.Floor(firewallAB * 0.5f));
            for (int i2 = 0; i2 < array.Length; ++i2)
            {
                array[i2] = 0 + firewallAB - (interval * i2);
                if (i2 != noSpawn)
                {
                    GameObject test = Instantiate(fire, new Vector2(lockedTarget + 20, array[i2]), Quaternion.identity);
                }
            }
            yield return new WaitForSecondsRealtime(distanceWall);
        }
    }
    private void Meteor(float cooldown, Vector2 lockedTarget)
    {
        Invoke("AttackCooldown", cooldown);
        for(int i = 0; i < meteorNumber; ++i)
        {
            do
            {
                position = new Vector2(lockedTarget.x + Random.Range(-radius, radius), lockedTarget.y + Random.Range(-radius, radius));
            } while (Vector2.Distance(position, lockedTarget) < radius);
            

            GameObject meteorInstance = Instantiate(meteor, position, Quaternion.identity);
        }

    }
    private void AttackCooldown()
    {
        attackReady = true;
    }
}
