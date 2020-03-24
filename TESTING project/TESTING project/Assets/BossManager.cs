using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private ChaosFinal cf;

    [SerializeField] private Transform target;
    [SerializeField] private GameObject chaosPrefab1;
    // Start is called before the first frame update
    void Start()
    {
        cf = ChaosFinal.Cf;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) cf.AoeUnder(target.position, chaosPrefab1);
    }
}
