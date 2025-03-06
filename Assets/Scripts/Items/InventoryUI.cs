using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    #region Singleton
    public static InventoryUI instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        eqDisplay = GameObject.FindGameObjectWithTag("Equipment").GetComponent<EQDisplay>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        pauseMenu = gameManager.GetComponent<PauseMenu>();
        inventory = Inventory.instance;

        if(inventory)
        {
            inventory.onItemChangedCallback += UpdateUI;
        }      
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
    }
    #endregion

    public Transform itemsParent;

    [HideInInspector]
    public EQDisplay eqDisplay;

    public bool isEQDisplayed = false;

    public bool canBeOpened = true;

    Inventory inventory;
    private GameObject gameManager;

    public GameObject playerCamera;

    private PauseMenu pauseMenu;

    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(isEQDisplayed)
            {
                eqDisplay.Close();
                QuestLog.MyInstance.CloseDialogue();
                gameManager.GetComponent<PauseMenu>().enabled = true;
                playerCamera.GetComponent<PlayerLook>().enabled = true;
                pauseMenu.canBeOpened = true;

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;

                isEQDisplayed = false;

                RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;

                Animator[] animators = FindObjectsOfType<Animator>();

                foreach (Animator anim in animators)
                {
                    anim.enabled = true;
                }
            }
            else if(!isEQDisplayed && canBeOpened)
            {
                eqDisplay.Open();
                playerCamera.GetComponent<PlayerLook>().enabled = false;
                pauseMenu.canBeOpened = false;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;

                isEQDisplayed = true;

                SpellsDisplay.instance.UpdateSpellInfo();

                Animator[] animators = FindObjectsOfType<Animator>();

                foreach (Animator anim in animators)
                {
                    anim.enabled = false;
                }
            }
        }
    }

    public void UpdateUI()
    {
        for (int i=0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
