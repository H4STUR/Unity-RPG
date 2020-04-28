using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Combat;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        GameObject player;
        Fighter fighter;
        Health health;
        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (health.IsDead()) return;
            if (InPlayerAttackRange())
            {
                if(fighter.GetIsInWeaponRange(this.player.GetComponent<Health>()) && fighter.CanAttack(player))
                {
                    fighter.Attack(player);
                }
                //else 
                    //GetComponent<NavMeshAgent>().destination = player.transform.position;
                
                
            }
        }

        private bool InPlayerAttackRange()
        { 
            return Vector3.Distance(this.transform.position, player.transform.position) <= this.chaseDistance;
        }
    }
}