using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        Animator animator;
        Collider colliderMine;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            colliderMine = GetComponent<Collider>();
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            animator.applyRootMotion = isInteracting;
            animator.SetBool("isInteracting", isInteracting);
            animator.CrossFade(targetAnim, 0.2f);
        }
        public void TakeDamage(int damage,string damageAnimation)
        {
            //currentHealth = currentHealth - damage;
            //Debug.Log("Enemy Health:"+currentHealth);
            PlayTargetAnimation(damageAnimation, true);
            animator.Play(damageAnimation);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Dead");
                //HANDLE PLAYER DEATH
            }
        }
    }
}