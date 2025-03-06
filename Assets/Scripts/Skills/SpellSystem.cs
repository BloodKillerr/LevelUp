using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSystem : MonoBehaviour
{
    #region Singleton
    public static SpellSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        spell1Position = GameObject.FindGameObjectWithTag("fireballpos");
        spell2Position = GameObject.FindGameObjectWithTag("frostpos");
        spell3Position = GameObject.FindGameObjectWithTag("healpos");
        spell4Position = GameObject.FindGameObjectWithTag("berserkpos");
    }
    #endregion

    public Slider spell1Slider;

    public Slider spell2Slider;

    public Slider spell3Slider;

    public Slider spell4Slider;

    public Sprite spell1Icon;

    public Sprite spell2Icon;

    public Sprite spell3Icon;

    public Sprite spell4Icon;

    [SerializeField]
    private bool spell1Unlocked = false;

    [SerializeField]
    private float spell1Cooldown = 0f;

    [SerializeField]
    private float spell1Time = 5f;

    [SerializeField]
    private int spell1Cost = 30;

    [SerializeField]
    private bool spell2Unlocked = false;

    [SerializeField]
    private float spell2Cooldown = 0f;

    [SerializeField]
    private float spell2Time = 20f;

    [SerializeField]
    private int spell2Cost = 60;

    [SerializeField]
    private bool spell3Unlocked = false;

    [SerializeField]
    private float spell3Cooldown = 0f;

    [SerializeField]
    private float spell3Time = 25f;

    [SerializeField]
    private int spell3Cost = 250;

    [SerializeField]
    private bool spell4Unlocked = false;

    [SerializeField]
    private float spell4Cooldown = 0f;

    [SerializeField]
    private float spell4Time = 60f;

    [SerializeField]
    private int spell4Cost = 500;

    public GameObject spell1Position;

    public GameObject spell2Position;

    public GameObject spell3Position;

    public GameObject spell4Position;

    public GameObject fireballProjectile;

    public GameObject frostblastProjectile;

    public GameObject blessingProjectile;

    public GameObject berserkProjectile;

    [SerializeField]
    private int fireballDamage = 15;

    [SerializeField]
    private float freezeTime = 4f;

    [SerializeField]
    private float freezeRadius = 6f;

    [SerializeField]
    private int healAmount = 1000;

    [SerializeField]
    private float damageBuffTime = 4f;

    public bool MySpell1Unlocked { get => spell1Unlocked; set => spell1Unlocked = value; }
    public bool MySpell2Unlocked { get => spell2Unlocked; set => spell2Unlocked = value; }
    public bool MySpell3Unlocked { get => spell3Unlocked; set => spell3Unlocked = value; }
    public bool MySpell4Unlocked { get => spell4Unlocked; set => spell4Unlocked = value; }

    public float MySpell1Cooldown { get => spell1Cooldown; set => spell1Cooldown = value; }
    public float MySpell2Cooldown { get => spell2Cooldown; set => spell2Cooldown = value; }
    public float MySpell3Cooldown { get => spell3Cooldown; set => spell3Cooldown = value; }
    public float MySpell4Cooldown { get => spell4Cooldown; set => spell4Cooldown = value; }

    public float MySpell1Time { get => spell1Time; set => spell1Time = value; }
    public float MySpell2Time { get => spell2Time; set => spell2Time = value; }
    public float MySpell3Time { get => spell3Time; set => spell3Time = value; }
    public float MySpell4Time { get => spell4Time; set => spell4Time = value; }

    public int MyFireballDamage { get => fireballDamage; set => fireballDamage = value; }
    public float MyFreezeTime { get => freezeTime; set => freezeTime = value; }
    public float MyFreezeRadius { get => freezeRadius; set => freezeRadius = value; }
    public int MyHealAmount { get => healAmount; set => healAmount = value; }
    public float MyDamageBuffTime { get => damageBuffTime; set => damageBuffTime = value; }

    public int MySpell1Cost { get => spell1Cost; set => spell1Cost = value; }
    public int MySpell2Cost { get => spell2Cost; set => spell2Cost = value; }
    public int MySpell3Cost { get => spell3Cost; set => spell3Cost = value; }
    public int MySpell4Cost { get => spell4Cost; set => spell4Cost = value; }

    private void Update()
    {
        MySpell1Cooldown -= Time.deltaTime;
        MySpell2Cooldown -= Time.deltaTime;
        MySpell3Cooldown -= Time.deltaTime;
        MySpell4Cooldown -= Time.deltaTime;

        spell1Slider.maxValue = MySpell1Time;
        spell1Slider.value = MySpell1Cooldown;
        spell2Slider.maxValue = MySpell2Time;
        spell2Slider.value = MySpell2Cooldown;
        spell3Slider.maxValue = MySpell3Time;
        spell3Slider.value = MySpell3Cooldown;
        spell4Slider.maxValue = MySpell4Time;
        spell4Slider.value = MySpell4Cooldown;

        if (MySpell1Cooldown <= 0f)
        {
            spell1Slider.gameObject.SetActive(false);
        }
        else
        {
            spell1Slider.gameObject.SetActive(true);
        }

        if (MySpell2Cooldown <= 0f)
        {
            spell2Slider.gameObject.SetActive(false);
        }
        else
        {
            spell2Slider.gameObject.SetActive(true);
        }

        if (MySpell3Cooldown <= 0f)
        {
            spell3Slider.gameObject.SetActive(false);
        }
        else
        {
            spell3Slider.gameObject.SetActive(true);
        }

        if (MySpell4Cooldown <= 0f)
        {
            spell4Slider.gameObject.SetActive(false);
        }
        else
        {
            spell4Slider.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseSpellOne();
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseSpellTwo();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseSpellThree();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseSpellFour();
        }
    }

    public void UseSpellOne()
    {
        if(MySpell1Unlocked)
        {
            if(MySpell1Cooldown <= 0f && PlayerManager.instance.player.GetComponent<PlayerStats>().currentMana >= MySpell1Cost)
            {
                //Use spell 1
                Instantiate(fireballProjectile, spell1Position.transform.position, spell1Position.transform.rotation);
                MessageFeedManager.MyInstance.WriteMessage("Użyto umiejętności: Kula ognia");
                PlayerManager.instance.player.GetComponent<PlayerStats>().currentMana -= MySpell1Cost;
                MySpell1Cooldown = MySpell1Time;
            }
            else
            {
                MessageFeedManager.MyInstance.WriteMessage("Nie można użyć umiejętności: Kula ognia");
            }
        }
    }

    public void UseSpellTwo()
    {
        if(MySpell2Unlocked)
        {
            if (MySpell2Cooldown <= 0f && PlayerManager.instance.player.GetComponent<PlayerStats>().currentMana >= MySpell2Cost)
            {
                //Use spell 2
                Instantiate(frostblastProjectile, spell2Position.transform.position, spell2Position.transform.rotation);
                MessageFeedManager.MyInstance.WriteMessage("Użyto umiejętności: Podmuch mrozu");
                PlayerManager.instance.player.GetComponent<PlayerStats>().currentMana -= MySpell2Cost;
                MySpell2Cooldown = MySpell2Time;
            }
            else
            {
                MessageFeedManager.MyInstance.WriteMessage("Nie można użyć umiejętności: Podmuch mrozu");
            }
        }
    }

    public void UseSpellThree()
    {
        if(MySpell3Unlocked)
        {
            if (MySpell3Cooldown <= 0f && PlayerManager.instance.player.GetComponent<PlayerStats>().currentMana >= MySpell3Cost)
            {
                //Use spell 3
                Instantiate(blessingProjectile, spell3Position.transform.position, spell3Position.transform.rotation, spell3Position.transform);
                MessageFeedManager.MyInstance.WriteMessage("Użyto umiejętności: Błogosławieństwo");
                PlayerManager.instance.player.GetComponent<PlayerStats>().currentMana -= MySpell3Cost;
                MySpell3Cooldown = MySpell3Time;
            }
            else
            {
                MessageFeedManager.MyInstance.WriteMessage("Nie można użyć umiejętności: Błogosławieństwo");
            }
        }
    }

    public void UseSpellFour()
    {
        if(MySpell4Unlocked)
        {
            if (MySpell4Cooldown <= 0f && PlayerManager.instance.player.GetComponent<PlayerStats>().currentMana >= MySpell4Cost)
            {
                //Use spell 4
                Instantiate(berserkProjectile, spell4Position.transform.position, spell4Position.transform.rotation, spell4Position.transform);
                MessageFeedManager.MyInstance.WriteMessage("Użyto umiejętności: Berserk");
                PlayerManager.instance.player.GetComponent<PlayerStats>().currentMana -= MySpell4Cost;                
                MySpell4Cooldown = MySpell4Time;
            }
            else
            {
                MessageFeedManager.MyInstance.WriteMessage("Nie można użyć umiejętności: Berserk");
            }
        }
    }
}
