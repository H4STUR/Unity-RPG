using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;
        bool isDead = false;

        public bool IsDead() { return this.isDead; }

        public void TakeDammage(float damage)
        {      
            this.health = Mathf.Max(this.health -damage, 0);
            if (this.health == 0) Die();

        }

        private void Die()
        {
            if (isDead) return;

            this.isDead = true;
            this.GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Animator>().SetTrigger("die");
            //Destroy(this.gameObject);
        }
    }

}

