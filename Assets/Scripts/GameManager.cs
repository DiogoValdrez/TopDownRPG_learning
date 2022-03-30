using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;//static makes it a global variable(to access: GameManager.instance) 
    private void Awake()
    {
        //this prevents creating new game manager
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);

            return;
        }

        //This cleans up all saves
        //PlayerPrefs.DeleteAll();

        // Make sure this is the only "instance"
        instance = this;

        //this event get triggred after a scene is loaded and calls the functions in the plus
        SceneManager.sceneLoaded += LoadState;

        //this make sure that the game manager maintains alive even if we change scene
        DontDestroyOnLoad(gameObject);
        //to prevent duplicates of the game manager when returning to main we could just put the game manager on the loading screen before the game starts
    }

    //Ressources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References 
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitPointBar;//there is 2 ways to dont destry on load, just say dontdestroyonload in awayke, or this way

    //Logic
    public int pesos;
    public int experience;

    //With this we dont need to have a reference to Floating TextMAnager, we can just call this in other files
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon max lvl?
        if(weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }
        if(pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //HITPOINT BAr
    public void OnHitPointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitPointBar.localScale = new Vector3(1, ratio, 1);
    }

    // Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while(experience >= add)
        {
            add += xpTable[r];
            r++;

            if(r == xpTable.Count)// MAx level
            {
                return r;
            }
        }

        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }
    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }
    public void OnLevelUp()
    {
        Debug.Log("LVL UP");
        player.OnLevelUp();
    }

    //Save state
    /*
     * INT preferedSkin
     * INT pesos
     * INT experience
     * INT weaponLevel 
     * 
     * */
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        //prevents from calling multiple times
        //SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        
        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
        {
            player.SetLevel(GetCurrentLevel());
        }
        
        weapon.SetWeaponLevel(int.Parse(data[3]));

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;

        Debug.Log("LoadState");
    }
}
