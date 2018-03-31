using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    public int playerMaxHealth;
    public int playerCurrentHealth;

    public bool flashActive;
    public float flashLength;
    private float flashCounter;

    private SpriteRenderer playerSprite;

    private SFXManager sfxMan;

    // Use this for initialization
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        playerSprite = GetComponent<SpriteRenderer>();
        sfxMan = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        if (flashActive)
        {
            var RGB = playerSprite.color;
            if (flashCounter > flashLength * .66f)
            {
                playerSprite.color = new Color(RGB.r, RGB.g, RGB.b, 0f);
            }
            else if (flashCounter > flashLength * .33f)
            {
                playerSprite.color = new Color(RGB.r, RGB.g, RGB.b, 1f);
            }
            else if (flashCounter > 0)
            {
                playerSprite.color = new Color(RGB.r, RGB.g, RGB.b, 0f);
            }
            else
            {
                flashActive = false;
                playerSprite.color = new Color(RGB.r, RGB.g, RGB.b, 1f);
            }
            flashCounter -= Time.deltaTime;
        }
    }

    public void HurtPlayer(int damage)
    {
        playerCurrentHealth -= damage;
        flashActive = true;
        flashCounter = flashLength;
        sfxMan.playerHurt.Play();
    }

    public void setMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }
}
