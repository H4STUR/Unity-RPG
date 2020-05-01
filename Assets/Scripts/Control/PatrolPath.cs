using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Gizmos.DrawSphere(GetWaypoint(i), .2f);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(GetNextIndex(i)));
            }

        }

        public int GetNextIndex(int i)
        {
            if (i + 1 >= this.transform.childCount)
                return 0;
            return i + 1;
        }
        public Vector3 GetWaypoint(int i)
        {
            return this.transform.GetChild(i).position;
        }
    }
}