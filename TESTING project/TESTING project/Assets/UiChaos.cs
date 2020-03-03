using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiChaos : MonoBehaviour
{
    public static UiChaos instance = null;
    private AsyncOperation async;
    private FixedVariables variables;
    [SerializeField] private Text timer;
    private float hp;
    [SerializeField]private Image hpRed;
    [SerializeField] private Image redPlayer;
    [SerializeField] private Image stamina;
    [SerializeField] private Image blood;
    private float maxStamina = 0;
    private float maxHp = 0;
    private float hpPlayer;
    private float maxhpPlayer;

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
    public void PlayerHp(float _hp)
    {
        if (maxhpPlayer == 0) maxhpPlayer = _hp;
        hpPlayer = _hp;
        redPlayer.fillAmount = hpPlayer / maxhpPlayer;
        blood.color = new Vector4(255, 255, 255, 1 - (hpPlayer / maxhpPlayer));
        
    }
    public void HpUpdate(float _hp)
    {
        if (maxHp == 0) maxHp = _hp;
        hp = _hp;
        hpRed.fillAmount = hp / maxHp;
    }
    public void Die(string sceneToLoad)
    {
        if (async == null)
        {
            if (variables != null) variables.LastScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("SceneToLoad", sceneToLoad);

            async = SceneManager.LoadSceneAsync("Loading");
            async.allowSceneActivation = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        variables = FixedVariables.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (variables != null) timer.text = LoadingText.GetTime(variables.Timer);
        
    }
    public void Stamina(float _stamina)
    {
        if (maxStamina == 0) maxStamina = _stamina;
        stamina.fillAmount = _stamina / maxStamina;
    }
}
