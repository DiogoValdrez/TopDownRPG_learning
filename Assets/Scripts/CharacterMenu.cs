using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // Text fields
    public Text levelText, hitpointText, pesosText, upgradeCostText, xpText;

    // Logic
    private int currentCaracterSelection = 0;
    public Image characterSelectionSprite, weaponSprite;
    public RectTransform xpBar;

    // Character selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCaracterSelection++;

            //if we went to far away
            if(currentCaracterSelection == GameManager.instance.playerSprites.Count)
            {
                currentCaracterSelection = 0;
            }

            OnSelectionChanged();
        }
        else
        {
            currentCaracterSelection--;

            //if we went to far away
            if (currentCaracterSelection < 0)
            {
                currentCaracterSelection = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChanged();
        }
    }
    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCaracterSelection];
        GameManager.instance.player.SwapSprite(currentCaracterSelection);
    }

    // Weapon upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    //Update Character info
    public void UpdateMenu()
    {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel >= GameManager.instance.weaponPrices.Count)
        {
            upgradeCostText.text = "MAX";
        }
        else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }
        
        // Meta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        // xp Bar
        int currentLevel = GameManager.instance.GetCurrentLevel();
        if (GameManager.instance.GetCurrentLevel() == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total xp points";// Display total xp if max level
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currentLevel-1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currentLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);//we use new when creating a vector(unless we use vector.zero/one/etc)
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }
    }

}
