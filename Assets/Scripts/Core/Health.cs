using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
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

            if(this.GetComponent<CapsuleCollider>())
                this.GetComponent<CapsuleCollider>().enabled = false;

            GetComponent<Animator>().SetTrigger("die");

            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }

}

