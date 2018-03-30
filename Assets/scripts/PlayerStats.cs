using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int currentLevel;

    public int currentExp;

    public int[] toLevelUp;
    public int[] hpLevels;
    public int[] attackLevels;
    public int[] defenseLevels;

    public int currentHP;
    public int currentAttack;
    public int currentDefense;

    private PlayerHealthManager playerHealth;

	// Use this for initialization
	void Start () {
        currentHP = hpLevels[1];
        currentAttack = attackLevels[1];
        currentDefense = defenseLevels[1];

        playerHealth = FindObjectOfType<PlayerHealthManager>();

    }
	
	// Update is called once per frame
	void Update () {
		if(currentExp >= toLevelUp[currentLevel])
        {
            LevelUp();
        }
	}

    public void addExperience(int xp)
    {
        currentExp += xp;
    }

    public void LevelUp()
    {
        currentLevel++;
        currentHP = hpLevels[currentLevel];
        currentAttack = attackLevels[currentLevel];
        currentDefense = defenseLevels[currentLevel];

        playerHealth.playerMaxHealth = currentHP;
        int HPUp = currentHP - hpLevels[currentLevel - 1];
        playerHealth.playerCurrentHealth += HPUp;
    }
}
