using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if (UNITY_EDITOR)
public class CheatConsole : MonoBehaviour
{
    bool showConsole = false;
    bool showHelp;

    string input;

    public static DebugCommand KILL_ALL;
    public static DebugCommand HEAL;
    public static DebugCommand REPLENISH_MANA;
    public static DebugCommand<int> GIVE_GOLD;
    public static DebugCommand<int> GIVE_XP;
    public static DebugCommand<int> HURT_PLAYER;
    public static DebugCommand<int> LOOSE_MANA;
    public static DebugCommand<int> SPEED;
    public static DebugCommand<int> SCENE;
    public static DebugCommand<int> ITEM;
    public static DebugCommand<string> GIVE_Title;
    public static DebugCommand HELP;

    public List<object> commandList;

    public void ToogleConsole()
    {
        showConsole = !showConsole;
    }

    public void Execute()
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
        }    
    }

    private void Awake()
    {
        KILL_ALL = new DebugCommand("kill_all", "Removes all enemies from the scene", "kill_all", () =>
        {
            KillAllEnemies();
        });

        HEAL = new DebugCommand("heal", "Heals player", "heal", () =>
        {
            HealPlayer();
        });

        GIVE_GOLD = new DebugCommand<int>("give_gold", "Gives given amount of gold to player", "give_gold <gold_amount>", (x) =>
        {
            PlayerStats ps = PlayerManager.instance.player.GetComponent<PlayerStats>();
            ps.money += x;
        });

        HELP = new DebugCommand("help", "Shows list of commands", "help", () =>
        {
            showHelp = true;
        });

        GIVE_XP = new DebugCommand<int>("give_xp", "Gives player given amount of xp", "give_xp <xp_amount>", (x) =>
        {
            LevelingSystem ls = LevelingSystem.instance;
            ls.GainExp(x);
        });

        HURT_PLAYER = new DebugCommand<int>("hurt_player", "Deals damage to player", "hurt_player <damage_amount>", (x) =>
        {
            PlayerStats ps = PlayerManager.instance.player.GetComponent<PlayerStats>();
            ps.TakeDamage(x);

            if(ps.MyCurrentHealth<0)
            {
                ps.MyCurrentHealth = 0;
            }
        });

        LOOSE_MANA = new DebugCommand<int>("loose_mana", "Takes mana away from player", "loose_mana <mana_amount>", (x) =>
        {
            PlayerStats ps = PlayerManager.instance.player.GetComponent<PlayerStats>();

            if (ps.currentMana > 0)
            {
                ps.currentMana -= x;
            }

            if (ps.currentMana < 0)
            {
                ps.currentMana = 0;
            }
        });

        REPLENISH_MANA = new DebugCommand("replenish_mana", "Gives mana back to player", "replenish_mana", () =>
        {
            PlayerStats ps = PlayerManager.instance.player.GetComponent<PlayerStats>();

            ps.currentMana = ps.maxMana;
        });

        SPEED = new DebugCommand<int>("speed", "Changes player's speed", "speed <speed_amount>", (x) =>
        {
            PlayerManager.instance.player.GetComponent<PlayerMove>().movementSpeed = x;
        });

        SCENE = new DebugCommand<int>("scene", "Changes scene", "scene <scene_index>", (x) =>
        {
            SceneManager.LoadScene(x);
        });

        ITEM = new DebugCommand<int>("item", "give player an item of specific ID", "item <item_id>", (x) =>
        {
            bool added = Inventory.instance.Add(SaveManager.instance.items[x] as Item);
            if (!added)
            {
                Inventory.instance.DropOnTheGround(SaveManager.instance.items[x] as Item);
            }
        });

        GIVE_Title = new DebugCommand<string>("give_title", "Give player a new title", "give_title <title>", (x) =>
        {
            PlayerStats ps = PlayerManager.instance.player.GetComponent<PlayerStats>();

            ps.MyTitle = x;
        });

        commandList = new List<object>
        {
            KILL_ALL,
            HEAL,
            GIVE_GOLD,
            HELP,
            GIVE_XP,
            HURT_PLAYER,
            LOOSE_MANA,
            REPLENISH_MANA,
            SPEED,
            SCENE,
            ITEM,
            GIVE_Title
        };
    }

    Vector2 scroll;

    public void OnGUI()
    {
        if(!showConsole)
        {
            return;
        }

        float y = 0f;

        if(showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for(int i=0; i<commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y += 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }

    private void HandleInput()
    {
        string[] properties = input.Split(' ');
        for(int i=0; i<commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if(input.Contains(commandBase.commandID))
            {
                if(commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if(commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
                else if(commandList[i] as DebugCommand<string> != null)
                {
                    (commandList[i] as DebugCommand<string>).Invoke(properties[1]);
                }
            }
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            ToogleConsole();
        }

        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Execute();
        }
    }

    public void KillAllEnemies()
    {
        EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();

        foreach(EnemyStats enemy in enemies)
        {
            enemy.Die();
        }
    }

    public void HealPlayer()
    {
        PlayerStats ps = PlayerManager.instance.player.GetComponent<PlayerStats>();

        ps.MyCurrentHealth = ps.maxHealth;
    }
}
#endif
