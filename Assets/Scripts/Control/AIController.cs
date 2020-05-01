using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Combat;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspisionTime = 3f;
        [SerializeField] PatrolPath patrolPath = null;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointWaitTime = 2f;

        GameObject player;
        Fighter fighter;
        Health health;
        Move move;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeEnemyWaited = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Start()
        {
            this.player = GameObject.FindWithTag("Player");
            this.fighter = GetComponent<Fighter>();
            this.health = GetComponent<Health>();
            this.move = GetComponent<Move>();
            this.guardPosition = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InPlayerAttackRange() && fighter.CanAttack(player))
            {
                this.timeSinceLastSawPlayer = 0;

                AttackBehaviour();
            }
            else if (this.timeSinceLastSawPlayer < this.suspisionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            this.timeSinceLastSawPlayer += Time.deltaTime;
            this.timeEnemyWaited += Time.deltaTime;
        }



        #region Behaviour

        private void AttackBehaviour()
        {
            this.fighter.Attack(player);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void GuardBehaviour()
        {
            Vector3 nextPosition = this.guardPosition;

            if(patrolPath!= null)
            {
                if(AtWaypoint())
                {
                    CycleWaypoint();
                    this.timeEnemyWaited = 0;
                }
                

               nextPosition = patrolPath.GetWaypoint(currentWaypointIndex);
                    
                
            }

            if (this.timeEnemyWaited > this.waypointWaitTime)
            {
                this.move.StartMoveAction(nextPosition);
            }
        }



        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, patrolPath.GetWaypoint(currentWaypointIndex));
            return distanceToWaypoint < this.waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }


        private bool InPlayerAttackRange()
        { 
            return Vector3.Distance(this.transform.position, player.transform.position) <= this.chaseDistance;
        }

        #endregion

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, this.chaseDistance);
        }
    }
}