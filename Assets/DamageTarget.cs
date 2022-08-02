using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public class DamageTarget : MonoBehaviour
{
    public int currentWeaponDamage = 25;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                playerStats.TakeDamage(currentWeaponDamage);
            }
        }

        if (collision.tag == "Enemy")
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(currentWeaponDamage);
            }
        }
    }
}
