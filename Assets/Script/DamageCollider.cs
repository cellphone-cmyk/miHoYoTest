using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageCollider : MonoBehaviour
{
    public GameObject weaponModel;
    Collider damageCollider;
        

    private void Awake()
    {

        damageCollider = weaponModel.GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public void DisaleDamageCollider()
    {
        damageCollider.enabled = false;
    }

    public void EnableTrailRenderer()
    {
        weaponModel.GetComponentInChildren<TrailRenderer>().enabled = true;
    }

    public void DisableTrailRenderer()
    {
        weaponModel.GetComponentInChildren<TrailRenderer>().enabled = false;
    }
}
