using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{

    public int damageToGive;
    public GameObject damageNumber;

    private PlayerStats playerStats;

    // Use this for initialization
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "player")
        {
            int damage = damageToGive - playerStats.currentDefense;
            damage = (damage < 0) ? 0 : damage;
            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damage);
            var clone = (GameObject)Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = damage;
        }
    }
}
