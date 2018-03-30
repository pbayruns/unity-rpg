using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour {

    public int damageToGive;
    public GameObject damageBurst;
    public Transform hitpoint;
    public GameObject damageNumber;

    private PlayerStats playerStats;

    // Use this for initialization
    void Start () {
        playerStats = FindObjectOfType<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            int damage = damageToGive + playerStats.currentAttack;
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
            Instantiate(damageBurst, hitpoint.position, hitpoint.rotation);
            var clone = (GameObject) Instantiate(damageNumber, hitpoint.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = damage;
        }
    }
}
