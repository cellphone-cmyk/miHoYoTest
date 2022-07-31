using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("One Handed Attack Animations")]
        public string oh_light_attack_01;
        public string oh_light_attack_02;
        public string oh_heavy_Attack_01;
        public string th_light_attack_01;
        public string th_light_attack_02;
        public string th_light_attack_03;

        [Header("idle Animation")]
        public string left_arm_idle;
        public string right_arm_idle;
        public string th_idle;
    }
}