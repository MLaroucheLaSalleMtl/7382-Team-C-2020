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
    private Animator anim;
    [SerializeField] private Text timer;
    private float hp;
    [SerializeField] private Image hpRed;
    [SerializeField] private Image[] bossHPSmall;
    [SerializeField] private Image redPlayer;
    [SerializeField] private Image stamina;
    [SerializeField] private Image staminaSmall;
    [SerializeField] private Image blood;
    [SerializeField] private GameObject panelUpgrade;
    private float maxStamina = 0;
    private float maxHp = 0;
    private float hpPlayer;
    private float maxhpPlayer;
    [SerializeField] private float bloodA;
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
        else anim.SetTrigger("TakeDamage");
        hpPlayer = _hp;
        redPlayer.fillAmount = hpPlayer / maxhpPlayer;
        //bloodA = Mathf.Clamp(0.5f - (hpPlayer / maxhpPlayer), 0, 0.5f);
        //bloodA = 1;
        //blood.color = new Vector4(255, 255, 255, bloodA);
        
    }
    public void HpUpdate(float _hp)
    {
        if (maxHp == 0) maxHp = _hp;
        hp = _hp;
        hpRed.fillAmount = hp / maxHp;
        for (int i = 0; i < bossHPSmall.Length; ++i)
        {
            bossHPSmall[i].fillAmount = hp / maxHp;
        }
    }
    //public void StaminaUpgrade()
    //public void StaminaUpgrade()
    //{
    //    ++variables.StaminaUpgrade;
    //    ChangeScene(_sceneToLoad);
    //}
    //public void HpUpgrade()
    //{
    //    ++variables.HealthUpgrade;
    //    ChangeScene(_sceneToLoad);
    //}
    private void ChangeScene(string sceneToLoad)
    {
        //panelUpgrade.SetActive(false);
        if (async == null)
        {
            if (variables != null)
            {
                variables.LastScene = SceneManager.GetActiveScene().name;
                if (variables.LastScene == "FinalBoss") sceneToLoad = "WinScreen";//these two overrides the upgrade scene
                if (variables.Warp) sceneToLoad = "MainMenu";
            }

            PlayerPrefs.SetString("SceneToLoad", sceneToLoad);

            async = SceneManager.LoadSceneAsync("Loading");
            async.allowSceneActivation = true;
        }
    }
    private string _sceneToLoad = "MainMenu";
    public void Die(string sceneToLoad)
    {
        variables.SceneLoad = sceneToLoad;
        ChangeScene("UpgradeScene");
        //panelUpgrade.SetActive(true);
        //_sceneToLoad = sceneToLoad;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        variables = FixedVariables.instance;
        anim = GetComponent<Animator>();
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
        staminaSmall.fillAmount = _stamina / maxStamina;
    }
}
