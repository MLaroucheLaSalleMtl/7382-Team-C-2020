using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class FireBossScript : MonoBehaviour
{
    private IndividualAISettings aiSettings;
    private float attackToDo;
    [SerializeField]private bool readyAttack = true;
    public static FireBossScript instance = null;
    [SerializeField]private Transform target;
    
    [SerializeField] private GameObject aoeMelee;
    [SerializeField] private GameObject fireBall;
    public float AttackToDo { get => attackToDo; set => attackToDo = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        attackToDo = 0;
        
    }
    [SerializeField]private float distance;
    private float distanceX;
    private float distanceY;
    // Update is called once per frame
    void Update()
    {
        
        distance = Vector2.Distance(transform.position, target.position);
        if (distance < 3f) { attackToDo = 1; }//getoffme attack
        else if (distance > 10f) { attackToDo = 2; }//range attack
    }
    public void Attack()
    {
        if (readyAttack)
        {
            readyAttack = false;
            switch (this.AttackToDo)
            {
                case 1f:
                    {
                        AoeMelee(2);
                    }
                    break;
                case 2f:
                    {
                        RangeFire(2);
                    }break;
                default:
                    {
                        readyAttack = true;
                        Debug.Log("Do nothing");
                    }break;
            }
        }
    }
    private void AoeMelee(float cooldown)
    {
        Invoke("AttackCooldown", cooldown);
        GameObject aoeSprite = Instantiate(aoeMelee, target.position, target.rotation) as GameObject;
    }
    private void RangeFire(float cooldown)
    {
        
        Invoke("AttackCooldown", cooldown);
        StartCoroutine(FireLine(target.position.x, target.position.y, transform.position.x, transform.position.y));
        //for(int i = 0; i < 60; ++i)
        //{
        //    GameObject fireLine = Instantiate(fireBall, new Vector2(transform.position.x + distanceX/2, transform.position.y + distanceY/2),transform.rotation)     as GameObject;
        //}
    }
    IEnumerator FireLine(float posTargetX, float posTargetY, float transformX, float transformY)
    {
        
        float testX = posTargetX - transform.position.x;
        float testY = posTargetY - transform.position.y;
        float n = 1;
        testX *= 100;
        testY *= 100;
        float ffff = 2000;
        while (n < 20)
        {
            yield return new WaitForSecondsRealtime(0.03f);
            
            GameObject fuckkkkkkkkkkkkkkkk = Instantiate(fireBall, new Vector2(transformX + (testX /ffff), transformY + (testY/ffff)), transform.rotation);
            ffff -= 2;
            n += 0.00000000001f;
        }
    }
    private void AttackCooldown()
    {
        readyAttack = true;
    }
    public void YouDeadMyNigga()
    {

    }
}
