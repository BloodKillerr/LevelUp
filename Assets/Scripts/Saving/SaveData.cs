using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public PlayerData MyPlayerData { get; set; }

    public InventoryData MyInventoryData { get; set; }

    public List<EquipmentData> MyEquipmentData { get; set; }

    public List<PotionData> MyPotionData { get; set; }

    public LevelingData MyLevelingData { get; set; }

    public List<QuestData> MyQuestData { get; set; }

    public List<RiddleObjectsData> MyRiddleObjectsData { get; set; }

    public TraitsData MyTraitsData { get; set; }

    public List<VendorData> MyVendorsData { get; set; }

    public SkillTreeData MySkillTreeData { get; set; }

    public List<ChestData> MyChestData { get; set; }

    public List<EnemyData> MyEnemyData { get; set; }

    public string MyScene { get; set; }

    public SpawnPointData MySpawnPointData { get; set; }

    public List<ItemPickupData> MyItemPickupData { get; set; }

    public SpellSystemData MySpellSystemData { get; set; }

    public SaveData()
    {
        MyInventoryData = new InventoryData();
        MyTraitsData = new TraitsData();
        MyEquipmentData = new List<EquipmentData>();
        MyPotionData = new List<PotionData>();
        MyQuestData = new List<QuestData>();
        MyRiddleObjectsData = new List<RiddleObjectsData>();
        MyVendorsData = new List<VendorData>();
        MySkillTreeData = new SkillTreeData();
        MyChestData = new List<ChestData>();
        MyEnemyData = new List<EnemyData>();
        MyItemPickupData = new List<ItemPickupData>();
    }
}

[System.Serializable]
public class PlayerData
{
    public int MyMoney { get; set; }

    public string MyTitle { get; set; }

    public int MyCurrentMana { get; set; }

    public int MyMaxMana { get; set; }

    public int MyCurrentHealth { get; set; }

    public int MyMaxHealth { get; set; }

    public float MyX { get; set; }

    public float MyY { get; set; }

    public float MyZ { get; set; }

    public float MyRotX { get; set; }

    public float MyRotY { get; set; }

    public float MyPotion1Cooldown { get; set; }

    public float MyPotion2Cooldown { get; set; }

    public PlayerData(int money, string title, int currentMana, int maxMana, int currentHealth, int maxHealth, Vector3 position, float rotX, float rotY, float potion1Cooldown, float potion2Cooldown)
    {
        MyMoney = money;
        MyTitle = title;
        MyCurrentMana = currentMana;
        MyMaxMana = maxMana;
        MyCurrentHealth = currentHealth;
        MyMaxHealth = maxHealth;
        MyX = position.x;
        MyY = position.y;
        MyZ = position.z;
        MyRotX = rotX;
        MyRotY = rotY;
        MyPotion1Cooldown = potion1Cooldown;
        MyPotion2Cooldown = potion2Cooldown;
    }
}

[System.Serializable]
public class ItemData
{
    public string MyItemName { get; set; }

    public int MyItemAmount { get; set; }

    public ItemData(string name, int amount)
    {
        MyItemName = name;
        MyItemAmount = amount;
    }
}

[System.Serializable]
public class InventoryData
{
    public List<ItemData> MyItems { get; set; }

    public InventoryData()
    {
        MyItems = new List<ItemData>();
    }
}

[System.Serializable]
public class EquipmentData
{
    public string MyName { get; set; }

    public EquipmentData(string name)
    {
        MyName = name;
    }
}

[System.Serializable]
public class PotionData
{
    public string MyName { get; set; }

    public PotionData(string name)
    {
        MyName = name;
    }
}

[System.Serializable]
public class LevelingData
{
    public int MyCurrentXp { get; set; }

    public int MyRequiredXp { get; set; }

    public int MyLevel { get; set; }

    public int MySkillpoints { get; set; }

    public int MyHpUpgrade { get; set; }

    public int MyMpUpgrade { get; set; }

    public LevelingData(int currentXp, int requiredXp, int level, int skillpoints, int hpUpgrade, int mpUpgrade)
    {
        MyCurrentXp = currentXp;
        MyRequiredXp = requiredXp;
        MyLevel = level;
        MySkillpoints = skillpoints;
        MyHpUpgrade = hpUpgrade;
        MyMpUpgrade = mpUpgrade;
    }
}

[System.Serializable]
public class QuestLootData
{
    public string MyItemName { get; set; }

    public int MyAmount { get; set; }

    public QuestLootData(string itemName, int amount)
    {
        MyItemName = itemName;
        MyAmount = amount;
    }
}

[System.Serializable]
public class QuestData
{
    public string MyTitle { get; set; }

    public string MyDesc { get; set; }

    public CollectObjective[] MyCollectObjectives { get; set; }

    public KillObjective[] MyKillObjectives { get; set; }

    public RiddleObjective[] MyRiddleObjectives { get; set; }

    public int MyXp { get; set; }

    public int MyGold { get; set; }

    public string MyTitleReward { get; set; }

    public string MyTraitReward { get; set; }

    public string MyQuestGiverName { get; set; }

    public List<QuestLootData> MyQuestLootData { get; set; }

    public QuestData(string title, string desc, CollectObjective[] collectObjectives, KillObjective[] killObjectives, RiddleObjective[] riddleObjectives, int xp, int gold, string titleReward, string traitReward, string questGiverName)
    {
        MyTitle = title;
        MyDesc = desc;
        MyCollectObjectives = collectObjectives;
        MyKillObjectives = killObjectives;
        MyRiddleObjectives = riddleObjectives;
        MyXp = xp;
        MyGold = gold;
        MyTitleReward = titleReward;
        MyTraitReward = traitReward;
        MyQuestGiverName = questGiverName;

        MyQuestLootData = new List<QuestLootData>();
    }
}

[System.Serializable]
public class RiddleObjectsData
{
    public int MyID { get; set; }

    public RiddleObjectsData(int id)
    {
        MyID = id;
    }
}

[System.Serializable]
public class TraitData
{
    public string MyTrait { get; set; }

    public TraitData(string trait)
    {
        MyTrait = trait;
    }
}

[System.Serializable]
public class TraitsData
{
    public List<TraitData> MyTraits { get; set; }

    public TraitsData()
    {
        MyTraits = new List<TraitData>();
    }
}

[System.Serializable]
public class VendorItemData
{
    public bool MyUnlimitedAmount { get; set; }

    public int MyAmount { get; set; }

    public string MyItemName { get; set; }
    
    public VendorItemData(bool unlimited, int amount, string itemName)
    {
        MyUnlimitedAmount = unlimited;
        MyAmount = amount;
        MyItemName = itemName;
    }
}

[System.Serializable]
public class VendorData
{
    public string MyVendorName { get; set; }

    public List<VendorItemData> MyVendorItems { get; set; }

    public List<ItemData> MyItemsToBuyBack { get; set; }

    public VendorData(string vendorName)
    {
        MyVendorName = vendorName;
        MyVendorItems = new List<VendorItemData>();
        MyItemsToBuyBack = new List<ItemData>();
    }
}

[System.Serializable]
public class SkillData
{
    public int MySkillID { get; set; }

    public int MyCurrentAmount { get; set; }

    public bool MyUnlocked { get; set; }

    public SkillData(int skillID, int currentAmount, bool unlocked)
    {
        MySkillID = skillID;
        MyCurrentAmount = currentAmount;
        MyUnlocked = unlocked;
    }
}

[System.Serializable]
public class SkillTreeData
{
    public List<SkillData> MySkills { get; set; }

    public SkillTreeData()
    {
        MySkills = new List<SkillData>();
    }
}

[System.Serializable]
public class ChestData
{
    public int MyID { get; set; }

    public int MyGold { get; set; }

    public List<ItemData> Items { get; set; }

    public ChestData(int id, int gold)
    {
        MyID = id;
        MyGold = gold;
        Items = new List<ItemData>();
    }
}

[System.Serializable]
public class EnemyData
{
    public int MyID { get; set; }

    public int MyCurrentHealth { get; set; }

    public float MyX { get; set; }

    public float MyY { get; set; }

    public float MyZ { get; set; }

    public EnemyData(int id, int currentHealth, float x, float y, float z)
    {
        MyID = id;
        MyCurrentHealth = currentHealth;
        MyX = x;
        MyY = y;
        MyZ = z;
    }
}

[System.Serializable]
public class SpawnPointData
{
    public int MyID { get; set; }

    public bool MyWasUsed { get; set; }

    public SpawnPointData(int id, bool wasUsed)
    {
        MyID = id;
        MyWasUsed = wasUsed;
    }
}

[System.Serializable]
public class ItemPickupData
{
    public string MyItemName { get; set; }

    public int MyAmount { get; set; }

    public float MyX { get; set; }

    public float MyY { get; set; }

    public float MyZ { get; set; }

    public ItemPickupData(string itemName, int amount, Vector3 pos)
    {
        MyItemName = itemName;
        MyAmount = amount;
        MyX = pos.x;
        MyY = pos.y;
        MyZ = pos.z;
    }
}

[System.Serializable]
public class SpellSystemData
{
    public int MyFireballDamage { get; set; }

    public bool MySpell1Unlocked { get; set; }

    public bool MySpell2Unlocked { get; set; }

    public bool MySpell3Unlocked { get; set; }

    public bool MySpell4Unlocked { get; set; }

    public float MySpell1Cooldown { get; set; }

    public float MySpell2Cooldown { get; set; }

    public float MySpell3Cooldown { get; set; }

    public float MySpell4Cooldown { get; set; }

    public SpellSystemData(int fireballDamage, float spell1Cooldown, float spell2Cooldown, float spell3Cooldown, float spell4Cooldown, bool spell1Unlocked, bool spell2Unlocked, bool spell3Unlocked, bool spell4Unlocked)
    {
        MyFireballDamage = fireballDamage;

        MySpell1Cooldown = spell1Cooldown;
        MySpell2Cooldown = spell2Cooldown;
        MySpell3Cooldown = spell3Cooldown;
        MySpell4Cooldown = spell4Cooldown;

        MySpell1Unlocked = spell1Unlocked;
        MySpell2Unlocked = spell2Unlocked;
        MySpell3Unlocked = spell3Unlocked;
        MySpell4Unlocked = spell4Unlocked;
    }
}