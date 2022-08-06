using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTarget : MonoBehaviour
{
    public GameObject character;
    public int currentWeaponDamage = 25;
    string currentDamageAnimation;
    TraumaInducer traumaInducer;

    private void Awake()
    {
        traumaInducer = FindObjectOfType<TraumaInducer>();
    }

    private void OnTriggerEnter(Collider collision)
    {

        CharacterEffectsManager enemyEffects = collision.GetComponent<CharacterEffectsManager>();

        if (collision.tag == "Enemy")
        {
            //挂特效
            Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            //攻击方向
            float directionHitFrom = (Vector3.SignedAngle(character.transform.forward, collision.transform.forward, Vector3.up));
            ChooseWhichDirectionDamageCameFrom(directionHitFrom);
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyEffects.PlayBloodSplatterFX(contactPoint);
            
            traumaInducer.StartCoroutine("Shake");
            traumaInducer.HitPause();

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(currentWeaponDamage,currentDamageAnimation);
            }
        }
    }

    protected virtual void ChooseWhichDirectionDamageCameFrom(float direction)
    {
        //Debug.Log(direction);

        if (direction >= 145 && direction <= 180)
        {
            currentDamageAnimation = "Damage_Forward_01";
        }
        else if (direction <= -145 && direction >= -180)
        {
            currentDamageAnimation = "Damage_Forward_01";
        }
        else if (direction >= -45 && direction <= 45)
        {
            currentDamageAnimation = "Damage_Back_01";
        }
        else if (direction >= -144 && direction <= -45)
        {
            currentDamageAnimation = "Damage_Left_01";
        }
        else if (direction >= 45 && direction <= 144)
        {
            currentDamageAnimation = "Damage_Right_01";
        }
    }
}
