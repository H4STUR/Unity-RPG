using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDammage = 5f;

        Health target;

        float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (this.target.IsDead()) return;

            if (!GetIsInWeaponRange(this.target))
            {
                GetComponent<Move>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            GetComponent<Move>().transform.LookAt(target.transform);

            if (this.timeBetweenAttacks < this.timeSinceLastAttack)
            {
                //tHIS WILL TRIGGER THE hIT() EVENT
                GetComponent<Animator>().ResetTrigger("stopAttack");
                GetComponent<Animator>().SetTrigger("attack");
                this.timeSinceLastAttack = 0;
            }
        }

        public bool GetIsInWeaponRange(Health target)
        {
            return (Vector3.Distance(this.transform.position, target.transform.position) < weaponRange);
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return (targetToTest != null && !targetToTest.IsDead());
        }
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
            this.target = null;
        }

        //Animation event
        void Hit()
        {
            if (target == null) return;
            this.target.TakeDammage(this.weaponDammage);
        }
    }
}