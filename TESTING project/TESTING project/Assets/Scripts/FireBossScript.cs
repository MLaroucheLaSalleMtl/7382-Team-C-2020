using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class FireBossScript : MonoBehaviour
{
    private IndividualAISettings aiSettings;
    private float attackToDo;
    private bool readyAttack = true;
    public static FireBossScript instance = null;
    
    [SerializeField] private GameObject aoeMelee;
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
        attackToDo = 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack()
    {
        if (readyAttack)
        {
            readyAttack = false;
            switch (AttackToDo)
            {
                default:
                    {
                        AoeMelee(2);
                    }
                    break;
            }
        }
    }
    private void AoeMelee(float cooldown)
    {
        Invoke("AttackCooldown", cooldown);
        GameObject aoeSprite = Instantiate(aoeMelee,transform.position, transform.rotation) as GameObject;
    }
    private void AttackCooldown()
    {
        readyAttack = true;
    }
    public void YouDeadMyNigga()
    {

    }
}
