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

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;
            Debug.Log("Enemy Health:"+currentHealth);

            animator.Play("Damage");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Dead");
                //HANDLE PLAYER DEATH
            }
        }
    }
}