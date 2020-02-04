using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIScript : MonoBehaviour
{
    private float hp;
    private Animator anim;

    public float Hp { get => hp; set => hp = value; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("Test", 5f);
    }
    private void Test()
    {
        anim.SetBool("Follow", true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
