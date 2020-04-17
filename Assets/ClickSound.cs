using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickSound : MonoBehaviour
{

    [SerializeField] public AudioClip sound;
    [SerializeField] public Button BtnHP;
    [SerializeField] public Button BtnStamina;
    [SerializeField] public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
        BtnHP.onClick.AddListener(() => PlaySound());
        BtnStamina.onClick.AddListener(() => PlaySound());
    }

    public void PlaySound()
    {
        source.PlayOneShot(sound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
