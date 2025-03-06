using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveManager : MonoBehaviour
{
    #region Singleton
    public static SaveManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        foreach (SavedGame saved in MySaveSlots)
        {
            try
            {
                ShowSavedFiles(saved);
            }
            catch (System.Exception)
            {
                Debug.Log("Nie działa!");
            }         
        }
    }
    #endregion

    [SerializeField]
    public Item[] items = null;

    [SerializeField]
    private SavedGame[] saveSlots = null;

    [SerializeField]
    private GameObject dialogue = null;

    [SerializeField]
    private TMP_Text dialogueText = null;

    private SavedGame currentSavedGame;

    private string action;

    public SavedGame[] MySaveSlots { get => saveSlots; set => saveSlots = value; }

    public GameObject ItemOrb;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Load"))
        {
            Load(saveSlots[PlayerPrefs.GetInt("Load")]);
            InventoryUI.instance.UpdateUI();
        }
        else
        {
            PlayerManager.instance.SetDefaultValues();
        }

        SceneManager.sceneLoaded += OnSceneWasChanged;
    }

    public void OnSceneWasChanged(Scene scene, LoadSceneMode mode)
    {
        SavedGame savedGame = saveSlots[PlayerPrefs.GetInt("Load")];

        if (File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            if(SceneManager.GetActiveScene().name == data.MyScene)
            {
                PlayerPrefs.SetInt("Load", savedGame.MyIndex);
                PlayerPrefs.Save();

                for (int i = 0; i < Inventory.instance.items.Count; i++)
                {
                    Inventory.instance.items[i].CompletelyRemoveFromInventory();
                }

                Load(savedGame);
                FindObjectOfType<PauseMenu>().Resume();
            }          
        }
    }

    public void ShowDialogue(GameObject clickButton)
    {
        CloseDialogue();
        currentSavedGame = clickButton.GetComponentInParent<SavedGame>();

        action = clickButton.name;

        if (!File.Exists(Application.persistentDataPath + "/" + currentSavedGame.gameObject.name + ".dat"))
        {
            switch(action)
            {
                case "Save":
                    dialogueText.text = "Zapisać grę?";
                    dialogue.SetActive(true);
                    break;
            }
        }
        else
        {
            switch (action)
            {
                case "Load":
                    dialogueText.text = "Wczytać grę?";
                    break;
                case "Save":
                    dialogueText.text = "Zapisać grę?";
                    break;
                case "Delete":
                    dialogueText.text = "Usunąć zapis gry?";
                    break;
            }
            dialogue.SetActive(true);
        }
    }

    public void ExecuteAction()
    {
        switch (action)
        {
            case "Load":
                LoadScene(currentSavedGame);
                break;
            case "Save":
                Save(currentSavedGame);
                break;
            case "Delete":
                Delete(currentSavedGame);
                break;
        }
        CloseDialogue();
    }

    private void LoadScene(SavedGame savedGame)
    {
        if (File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            PlayerPrefs.SetInt("Load", savedGame.MyIndex);
            PlayerPrefs.Save();
            SceneManager.LoadScene(data.MyScene);
            FindObjectOfType<PauseMenu>().Resume();
        }
    }

    public void CloseDialogue()
    {
        dialogue.SetActive(false);
    }

    public void CloseSave()
    {
        SaveWindow.instance.Close();
        CloseDialogue();
    }

    public void Delete(SavedGame savedGame)
    {
        File.Delete(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat");
        PlayerPrefs.DeleteKey("Load");
        PlayerPrefs.Save();
        savedGame.HideVisuals();
    }

    private void ShowSavedFiles(SavedGame savedGame)
    {
        if(File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            savedGame.ShowInfo(data);
        }
    }

    public void Save(SavedGame savedGame)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat", FileMode.Create);

            SaveData data = new SaveData();

            data.MyScene = SceneManager.GetActiveScene().name;

            PlayerPrefs.SetInt("Load", savedGame.MyIndex);

            SaveEquipment(data);

            SavePotions(data);

            SaveInventory(data);

            SaveLevelingSystem(data);

            SaveQuests(data);

            SaveRiddleObjects(data);

            SaveTraits(data);

            SaveVendors(data);

            SaveSkillTree(data);

            SaveChests(data);

            SaveEnemies(data);

            SaveSpawnPoint(data);

            SaveItemPickups(data);

            SaveSpellSystem(data);

            SavePlayer(data);

            bf.Serialize(file, data);

            file.Close();

            ShowSavedFiles(savedGame);

            MessageFeedManager.MyInstance.WriteMessage("Zapisano stan gry");
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    private void SavePlayer(SaveData data)
    {
        PlayerStats ps = PlayerManager.instance.player.GetComponent<PlayerStats>();
        ps.gameObject.GetComponent<CharacterController>().enabled = false;
        data.MyPlayerData = new PlayerData(ps.money,
            ps.MyTitle,
            ps.currentMana,
            ps.maxMana,
            ps.MyCurrentHealth,
            ps.maxHealth,
            ps.gameObject.transform.position,
            GameObject.FindGameObjectWithTag("PlayerCamera").transform.rotation.eulerAngles.x,
            PlayerManager.instance.player.transform.rotation.eulerAngles.y,
            ps.MyPotion1Cooldown,
            ps.MyPotion2Cooldown);
        ps.gameObject.GetComponent<CharacterController>().enabled = true;
    }

    private void SaveEquipment(SaveData data)
    {
        foreach (Equipment eqItem in EquipmentManager.instance.currentEquipment)
        {
            if(eqItem)
            {
                data.MyEquipmentData.Add(new EquipmentData(eqItem.name));
                Debug.Log("Name: " + eqItem.name + " DM: " + eqItem.damageModifier + " | AM: " + eqItem.armorModifier);
            }
        }
    }

    private void SavePotions(SaveData data)
    {
        foreach(Potions potion in PotionsUsage.instance.currentPotions)
        {
            if(potion)
            {
                data.MyPotionData.Add(new PotionData(potion.name));
            }
        }
    }

    private void SaveInventory(SaveData data)
    {
        foreach(Item item in Inventory.instance.items)
        {
            data.MyInventoryData.MyItems.Add(new ItemData(item.name, item.amount));
        }
    }

    private void SaveLevelingSystem(SaveData data)
    {
        LevelingSystem ls = LevelingSystem.instance;
        data.MyLevelingData = new LevelingData(ls.currentExp,
            ls.requiredExp,
            ls.level,
            ls.skillPoints,
            ls.hpUpgrade,
            ls.mpUpgrade);
    }

    private void SaveQuests(SaveData data)
    {
        for (int i=0; i < QuestLog.MyInstance.MyQuests.Count; i++)
        {
            Quest quest = QuestLog.MyInstance.MyQuests[i];
            data.MyQuestData.Add(new QuestData(quest.MyTitle, quest.MyDescription, quest.MyCollectObjectives, quest.MyKillObjectives, quest.MyRiddleObjectives, quest.xp, quest.gold, quest.MyTitleReward, quest.MyTraitReward, quest.MyQuestGiverName));

            foreach (QuestLoot questLoot in quest.MyItems)
            {
                data.MyQuestData[i].MyQuestLootData.Add(new QuestLootData(questLoot.MyItem.name, questLoot.amount));
            }
        }
    }

    private void SaveRiddleObjects(SaveData data)
    {
        RiddleInteraction[] riddleObjects = FindObjectsOfType<RiddleInteraction>();

        foreach(RiddleInteraction interaction in riddleObjects)
        {
            data.MyRiddleObjectsData.Add(new RiddleObjectsData(interaction.id));
        }
    }

    private void SaveTraits(SaveData data)
    {
        foreach(string trait in TraitsSystem.instance.MyTraitsList)
        {
            data.MyTraitsData.MyTraits.Add(new TraitData(trait));
        }
    }

    private void SaveVendors(SaveData data)
    {
        Vendor[] vendors = FindObjectsOfType<Vendor>();
        for (int i=0; i<vendors.Length; i++)
        {
            data.MyVendorsData.Add(new VendorData(vendors[i].MyVendorName));

            foreach(VendorItem vendorItem in vendors[i].MyItems)
            {
                data.MyVendorsData[i].MyVendorItems.Add(new VendorItemData(vendorItem.MyUnlimited, vendorItem.MyQuantity, vendorItem.MyItem.name));
            }

            foreach(Item item in vendors[i].MyItemsToBuyBack)
            {
                data.MyVendorsData[i].MyItemsToBuyBack.Add(new ItemData(item.name, item.amount));
            }
        }
    }

    private void SaveSkillTree(SaveData data)
    {
        foreach(Skill skill in SkillTree.instance.MySkills)
        {
            data.MySkillTreeData.MySkills.Add(new SkillData(skill.MySkillID, skill.MyCurrentCount, skill.MyUnlocked));
        }
    }

    private void SaveChests(SaveData data)
    {
        Chest[] chests = FindObjectsOfType<Chest>();

        for(int i=0; i<chests.Length; i++)
        {
            data.MyChestData.Add(new ChestData(chests[i].MyChestID, chests[i].goldReward));
            
            foreach(Item item in chests[i].MyChestLoot)
            {
                data.MyChestData[i].Items.Add(new ItemData(item.name, item.amount));
            }
        }
    }

    private void SaveEnemies(SaveData data)
    {
        EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();

        foreach (EnemyStats enemy in enemies)
        {
            data.MyEnemyData.Add(new EnemyData(enemy.MyEnemyID, enemy.MyCurrentHealth, enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z));
        }
    }

    private void SaveSpawnPoint(SaveData data)
    {
        SpawnPoint sp = FindObjectOfType<SpawnPoint>();

        data.MySpawnPointData = new SpawnPointData(sp.MyID, sp.MyWasUsed);
    }

    private void SaveItemPickups(SaveData data)
    {
        ItemPickup[] ItemPickups = FindObjectsOfType<ItemPickup>();

        foreach(ItemPickup pickup in ItemPickups)
        {
            data.MyItemPickupData.Add(new ItemPickupData(pickup.item.name, pickup.howMany, pickup.gameObject.transform.position));
        }
    }

    private void SaveSpellSystem(SaveData data)
    {
        SpellSystem sS = SpellSystem.instance;

        data.MySpellSystemData = new SpellSystemData(sS.MyFireballDamage, sS.MySpell1Cooldown, sS.MySpell2Cooldown, sS.MySpell3Cooldown, sS.MySpell4Cooldown, sS.MySpell1Unlocked, sS.MySpell2Unlocked, sS.MySpell3Unlocked, sS.MySpell4Unlocked);
    }

    public void Load(SavedGame savedGame)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();

            LoadEquipment(data);

            LoadPotions(data);

            LoadQuests(data);

            LoadRiddleObjects(data);

            LoadInventory(data);

            LoadLevelingSystem(data);

            LoadTraits(data);

            LoadVendors(data);

            LoadSkillTree(data);

            LoadChests(data);

            LoadEnemies(data);

            LoadSpawnPoint(data);

            LoadItemPickups(data);

            LoadSpellSystem(data);

            LoadPlayer(data);

            PauseMenu pauseMenu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseMenu>();

            pauseMenu.canBeOpened = true;

            Debug.Log("Załadowano dane");

            MessageFeedManager.MyInstance.ClearFeed();
        }
        catch (System.Exception)
        {
            Delete(savedGame);
            PlayerPrefs.DeleteKey("Load");
            throw;
        }
    }

    public void LoadPlayer(SaveData data)
    {
        PlayerStats ps = PlayerManager.instance.player.GetComponent<PlayerStats>();
        Vector3 pos;
        ps.money = data.MyPlayerData.MyMoney;
        ps.MyTitle = data.MyPlayerData.MyTitle;
        ps.currentMana = data.MyPlayerData.MyCurrentMana;
        ps.maxMana = data.MyPlayerData.MyMaxMana;
        ps.MyCurrentHealth = data.MyPlayerData.MyCurrentHealth;
        ps.maxHealth = data.MyPlayerData.MyMaxHealth;
        pos.x = data.MyPlayerData.MyX;
        pos.y = data.MyPlayerData.MyY;
        pos.z = data.MyPlayerData.MyZ;
        ps.gameObject.GetComponent<CharacterController>().enabled = false;
        ps.gameObject.transform.position = pos;
        Quaternion q = Quaternion.Euler(new Vector3(data.MyPlayerData.MyRotX, 180, 0));
        GameObject.FindGameObjectWithTag("PlayerCamera").transform.localRotation = q;     
        PlayerManager.instance.player.transform.eulerAngles = new Vector3(0, data.MyPlayerData.MyRotY, 0);
        ps.gameObject.GetComponent<CharacterController>().enabled = true;
        ps.MyPotion1Cooldown = data.MyPlayerData.MyPotion1Cooldown;
        ps.MyPotion2Cooldown = data.MyPlayerData.MyPotion2Cooldown;
    }

    private void LoadEquipment(SaveData data)
    {
        WeaponEquip we = FindObjectOfType<WeaponEquip>();

        PlayerStats ps = PlayerManager.instance.player.GetComponent<PlayerStats>();

        for (int i = 0; i < EquipmentManager.instance.currentEquipment.Length; i++)
        {
            ps.OnEquipmentChanged(null, EquipmentManager.instance.currentEquipment[i]);  
            EquipmentManager.instance.Clear(i);
        }

        if(we.hand)
        {
            foreach (Transform child in we.hand.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        foreach (EquipmentData eqData in data.MyEquipmentData.ToArray())
        {
            Equipment eqItem = Instantiate(System.Array.Find(items, x => x.name == eqData.MyName)) as Equipment;

            EquipmentManager.instance.EquipWithoutSound(eqItem);
            ps.OnEquipmentChanged(null, EquipmentManager.instance.currentEquipment[(int)eqItem.equipSlot]);
            ps.OnEquipmentChanged(eqItem, null);

            Debug.Log("Name: " + eqItem.name + " DM: " + eqItem.damageModifier + " | AM: " + eqItem.armorModifier);

            if ((int)eqItem.equipSlot == 5)
            {
                we.EquipWeapon(eqItem, null);
            }
        }
    }

    private void LoadPotions(SaveData data)
    {
        System.Array.Clear(PotionsUsage.instance.currentPotions, 0, PotionsUsage.instance.currentPotions.Length);

        for (int i = 0; i < 2; i++)
        {
            PotionsUsage.instance.ClearPotions(i);
        }

        foreach(PotionData potionData in data.MyPotionData.ToArray())
        {
            Potions potion = Instantiate(System.Array.Find(items, x => x.name == potionData.MyName)) as Potions;

            PotionsUsage.instance.EquipPotionWithoutSound(potion);
        }
    }

    private void LoadInventory(SaveData data)
    {
        foreach(Item item in Inventory.instance.items.ToArray())
        {
            item.CompletelyRemoveFromInventory();
        }

        foreach(ItemData itemData in data.MyInventoryData.MyItems.ToArray())
        {
            Item item = Instantiate(System.Array.Find(items, x => x.name == itemData.MyItemName));
            item.amount = itemData.MyItemAmount;

            bool added = Inventory.instance.Add(item);

            if (!added)
            {
                Inventory.instance.DropOnTheGround(item);
            }
        }

        InventoryUI.instance.UpdateUI();
    }

    private void LoadLevelingSystem(SaveData data)
    {
        LevelingSystem ls = LevelingSystem.instance;
        ls.currentExp = data.MyLevelingData.MyCurrentXp;
        ls.requiredExp = data.MyLevelingData.MyRequiredXp;
        ls.level = data.MyLevelingData.MyLevel;
        ls.skillPoints = data.MyLevelingData.MySkillpoints;
        ls.hpUpgrade = data.MyLevelingData.MyHpUpgrade;
        ls.mpUpgrade = data.MyLevelingData.MyMpUpgrade;
    }

    private void LoadQuests(SaveData data)
    {
        foreach(Quest quest in QuestLog.MyInstance.MyQuests.ToArray())
        {
            QuestLog.MyInstance.AbandonQuest(quest);
        }

        QuestGiver[] questGivers = FindObjectsOfType<QuestGiver>();

        foreach (QuestData questsData in data.MyQuestData.ToArray())
        {
            QuestGiver qg = System.Array.Find(questGivers, x => x.MyQuestGiverName == questsData.MyQuestGiverName);
            if(qg)
            {
                Destroy(qg);
            }

            Quest q = new Quest
            {
                MyTitle = questsData.MyTitle,
                MyDescription = questsData.MyDesc,
                MyCollectObjectives = questsData.MyCollectObjectives,
                MyKillObjectives = questsData.MyKillObjectives,
                MyRiddleObjectives = questsData.MyRiddleObjectives,
                xp = questsData.MyXp,
                gold = questsData.MyGold,
                MyTitleReward = questsData.MyTitleReward,
                MyTraitReward = questsData.MyTraitReward,
                MyQuestGiverName = questsData.MyQuestGiverName
            };

            foreach(QuestLootData qld in questsData.MyQuestLootData.ToArray())
            {
                Item item = Instantiate(System.Array.Find(items, x => x.name == qld.MyItemName));

                QuestLoot questLoot = new QuestLoot
                {
                    MyItem = item,
                    amount = qld.MyAmount
                };
                q.MyItems.Add(questLoot);
            }

            QuestLog.MyInstance.AcceptQuest(q);
        }
        QuestLog.MyInstance.ShowDescription(null);
    }

    private void LoadRiddleObjects(SaveData data)
    {
        RiddleInteraction[] riddleObjects = FindObjectsOfType<RiddleInteraction>();
        RiddleObjectsData[] riddleObjectsData = data.MyRiddleObjectsData.ToArray();

        foreach (RiddleInteraction riddleObject in riddleObjects)
        {
            RiddleObjectsData riddleObjectData = System.Array.Find(riddleObjectsData, x => x.MyID == riddleObject.id);

            if (riddleObjectData == null)
            {
                Destroy(riddleObject);
            }
        }
    }

    private void LoadTraits(SaveData data)
    {
        foreach(string trait in TraitsSystem.instance.MyTraitsList.ToArray())
        {
            TraitsSystem.instance.RemoveTrait(trait);
        }

        foreach(TraitData td in data.MyTraitsData.MyTraits.ToArray())
        {
            TraitsSystem.instance.AddTrait(td.MyTrait);
        }
    }

    private void LoadVendors(SaveData data)
    {
        Vendor[] vendors = FindObjectsOfType<Vendor>();

        foreach(VendorData vendorData in data.MyVendorsData.ToArray())
        {
            Vendor vendor = System.Array.Find(vendors, x => x.MyVendorName == vendorData.MyVendorName);

            if(vendor != null)
            {
                int i = 0;
                foreach (VendorItemData vid in vendorData.MyVendorItems.ToArray())
                {
                    Item item = Instantiate(System.Array.Find(items, x => x.name == vid.MyItemName));
                    VendorItem vendorItem = new VendorItem
                    {
                        MyItem = item,
                        MyQuantity = vid.MyAmount,
                        MyUnlimited = vid.MyUnlimitedAmount
                    };
                    vendor.MyItems[i] = vendorItem;
                    i++;
                }

                i = 0;
                foreach (ItemData id in vendorData.MyItemsToBuyBack.ToArray())
                {
                    Item item = Instantiate(System.Array.Find(items, x => x.name == id.MyItemName));
                    item.amount = id.MyItemAmount;
                    vendor.MyItemsToBuyBack.Add(item);
                    i++;
                }
            }
        }
    }

    private void LoadSkillTree(SaveData data)
    {
        foreach (SkillData skillData in data.MySkillTreeData.MySkills.ToArray())
        {
            Skill skill = System.Array.Find(SkillTree.instance.MySkills, x => x.MySkillID == skillData.MySkillID);

            skill.Lock();

            if (skillData.MyUnlocked)
            {
                skill.Unlock();
            }

            skill.ResetSkill();

            for (int i=0; i<skillData.MyCurrentAmount; i++)
            {
                skill.Click();
            }

            skill.RefreshCount();
        }
    }

    private void LoadChests(SaveData data)
    {
        Chest[] chests = FindObjectsOfType<Chest>();
        ChestData[] chestsData = data.MyChestData.ToArray();

        foreach (Chest chest in chests)
        {
            ChestData chestdata = System.Array.Find(chestsData, x => x.MyID == chest.MyChestID);

            if (chestdata == null)
            {
                Destroy(chest.gameObject);
            }
            else
            {
                chest.MyChestLoot.Clear();
                chest.goldReward = chestdata.MyGold;

                foreach(ItemData itemData in chestdata.Items)
                {
                    Item item = Instantiate(System.Array.Find(items, x => x.name == itemData.MyItemName));
                    item.amount = itemData.MyItemAmount;
                    chest.MyChestLoot.Add(item);
                }
            }
        }
    }

    private void LoadEnemies(SaveData data)
    {
        EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();
        EnemyData[] enemiesData = data.MyEnemyData.ToArray();

        foreach(EnemyStats enemy in enemies)
        {
            EnemyData enemyData = System.Array.Find(enemiesData, x => x.MyID == enemy.MyEnemyID);

            if(enemyData == null)
            {
                Destroy(enemy.gameObject);
            }
            else
            {
                enemy.MyCurrentHealth = enemyData.MyCurrentHealth;
                enemy.transform.position = new Vector3(enemyData.MyX, enemyData.MyY, enemyData.MyZ);
            }
        }
    }

    public void LoadSpawnPoint(SaveData data)
    {
        SpawnPoint sp = FindObjectOfType<SpawnPoint>();

        if(sp.MyID == data.MySpawnPointData.MyID)
        {
            sp.MyWasUsed = data.MySpawnPointData.MyWasUsed;
        }
    }

    private void LoadItemPickups(SaveData data)
    {
        ItemPickup[] ItemPickups = FindObjectsOfType<ItemPickup>();

        foreach(ItemPickup pickup in ItemPickups)
        {
            Destroy(pickup.gameObject);
        }

        
        foreach (ItemPickupData ipd in data.MyItemPickupData.ToArray())
        {
            Item item = Instantiate(System.Array.Find(items, x => x.name == ipd.MyItemName));
            ItemOrb.GetComponent<ItemPickup>().item = item;
            ItemOrb.GetComponent<ItemPickup>().howMany = ipd.MyAmount;
            Instantiate(ItemOrb, new Vector3(ipd.MyX, ipd.MyY, ipd.MyZ), Quaternion.identity);
        }
        ItemOrb.GetComponent<ItemPickup>().item = null;
        ItemOrb.GetComponent<ItemPickup>().howMany = 1;
    }

    private void LoadSpellSystem(SaveData data)
    {
        SpellSystem sS = SpellSystem.instance;

        sS.MyFireballDamage = data.MySpellSystemData.MyFireballDamage;

        sS.MySpell1Cooldown = data.MySpellSystemData.MySpell1Cooldown;
        sS.MySpell2Cooldown = data.MySpellSystemData.MySpell2Cooldown;
        sS.MySpell3Cooldown = data.MySpellSystemData.MySpell3Cooldown;
        sS.MySpell4Cooldown = data.MySpellSystemData.MySpell4Cooldown;

        sS.MySpell1Unlocked = data.MySpellSystemData.MySpell1Unlocked;
        sS.MySpell2Unlocked = data.MySpellSystemData.MySpell2Unlocked;
        sS.MySpell3Unlocked = data.MySpellSystemData.MySpell3Unlocked;
        sS.MySpell4Unlocked = data.MySpellSystemData.MySpell4Unlocked;

        SpellsDisplay.instance.UpdateSpellInfo();
    }
}