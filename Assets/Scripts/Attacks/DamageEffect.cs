using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageEffect : MonoBehaviour
{

    [SerializeField] private GameObject image;
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        image.SetActive(false);
    }

    public void Damage()
    {
        image.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
