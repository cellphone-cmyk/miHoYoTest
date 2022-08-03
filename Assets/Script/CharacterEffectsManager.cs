using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        //CharacterStatsManager characterStatsManager;

        [Header("Damage FX")]
        public GameObject hitFX;

        protected virtual void Awake()
        {
            //characterStatsManager = GetComponent<CharacterStatsManager>();
        }

        public virtual void PlayBloodSplatterFX(Vector3 FxLocation)
        {
            GameObject blood = Instantiate(hitFX, FxLocation, Quaternion.identity);
        }

    }
}
