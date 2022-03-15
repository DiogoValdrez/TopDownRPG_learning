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
    // public weapon, ...
    public FloatingTextManager floatingTextManager;

    //Logic
    public int pesos;
    public int experience;

    //With this we dont need to have a reference to Floating TextMAnager, we can just call this in other files
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
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
        s += "0";

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
        //Change player skin
        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        //Change weapon Level

        Debug.Log("LoadState");
    }
}
