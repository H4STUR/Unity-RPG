using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target = null;


        [SerializeField]
        private Vector3 offsetPosition = Vector3.zero;

        [SerializeField]
        private Space offsetPositionSpace = Space.Self;

        [SerializeField]
        private bool lookAt = true;        
        
        [SerializeField]
        private bool rotate = true;

        private void LateUpdate()
        {
            if (rotate) Refresh();
            else follow();
        }

        public void Refresh()
        {
            if (target == null)
            {
                Debug.LogWarning("Missing target ref !", this);

                return;
            }

            // compute position
            if (offsetPositionSpace == Space.Self)
            {
                transform.position = target.TransformPoint(offsetPosition);
            }
            else
            {
                follow();
            }

            // compute rotation
            if (lookAt)
            {
                transform.LookAt(target);
            }
            else
            {
                transform.rotation = target.rotation;
            }
        }

        public void follow()
        {
            transform.position = target.position + offsetPosition;
        }
    }

}