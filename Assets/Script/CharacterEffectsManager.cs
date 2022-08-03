using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        //CharacterStatsManager characterStatsManager;

        [Header("Damage FX")]
        public GameObject bloodSplatterFX;

        protected virtual void Awake()
        {
            //characterStatsManager = GetComponent<CharacterStatsManager>();
        }

        public virtual void PlayBloodSplatterFX(Vector3 bloodSplatterLocation)
        {
            GameObject blood = Instantiate(bloodSplatterFX, bloodSplatterLocation, Quaternion.identity);
        }

    }
}
