using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    [SerializeField] private int phaseAmount;
    private float[] maxHpPhase;//make sure that the array is the same size as the phase amount
    private int currentPhase;
    private float hp;
    private GameManager code;
    private FireBossScript fireboss;
    private IndividualAISettings aISettings;
    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
        fireboss = FireBossScript.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

































    private void PlayerLosesLife()
    {
        hp = maxHpPhase[currentPhase];
    }
    private void HpDepleted()
    {
        --currentPhase;
        if (currentPhase <= 0) Die();
    }
    private void Die()
    {
        switch (aISettings.BossID)
        {
            case 0:
                {
                    fireboss.YouDeadMyNigga();
                }break;
        }
    }
}
