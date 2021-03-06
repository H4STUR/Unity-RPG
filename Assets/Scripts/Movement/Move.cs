﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Move : MonoBehaviour, IAction
    {
        NavMeshAgent navMeshAgent;
        Health health;

        private void Start()
        {
            this.health = GetComponent<Health>();
            this.navMeshAgent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 dest)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<IAction>().Cancel();
            MoveTo(dest);
        }
        private void MoveTo(Vector3 dest)
        {
            this.navMeshAgent.destination = dest;
            this.navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            this.navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = this.navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;

            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
