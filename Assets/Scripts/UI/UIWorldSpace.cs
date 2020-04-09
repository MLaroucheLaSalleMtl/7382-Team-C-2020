using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWorldSpace : MonoBehaviour
{

    public Image staminaBackground;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 staminaPos = Camera.main.WorldToScreenPoint(this.transform.position);
        staminaBackground.transform.position = staminaPos;
    }
}
