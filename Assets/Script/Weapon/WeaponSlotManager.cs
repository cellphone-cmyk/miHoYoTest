﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;

        Animator animator;

        InputHandler inputHandler;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            inputHandler = GetComponentInParent<InputHandler>();

            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();

                if (weaponItem!= null)
                {
                    animator.CrossFade(weaponItem.left_arm_idle,0.2f);
                }
                else
                {
                    animator.CrossFade("LeftArmEmpty", 0.2f);
                }
            }
            else
            {
                if (inputHandler.twoHandFlag)
                {
                    animator.CrossFade(weaponItem.th_idle, 0.2f);
                }
                else
                {
                    #region Handle Right Weapon Idle Animations
                    animator.CrossFade("Both Arms Empty", 0.2f);
                    if (weaponItem != null)
                    {
                        animator.CrossFade(weaponItem.right_arm_idle, 0.2f);
                    }
                    else
                    {
                        animator.CrossFade("RightArmEmpty", 0.2f);
                    }
                    #endregion
                }
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
            }
        }

        #region Handle Weapon's Damage Collider

        private void LoadLeftWeaponDamageCollider()
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        private void LoadRightWeaponDamageCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void OpenRightDamageCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
        }

        public void OpenLeftDamageCollider()
        {
            leftHandDamageCollider.EnableDamageCollider();
        }

        public void CloseRightHandDamageCollider()
        {
            rightHandDamageCollider.DisaleDamageCollider();
        }

        public void CloseLeftHandDamageCollider()
        {
            leftHandDamageCollider.DisaleDamageCollider();
        }

        #endregion
    }
}