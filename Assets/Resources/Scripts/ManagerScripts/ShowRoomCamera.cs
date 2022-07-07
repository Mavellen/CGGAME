using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGGame.Cameras
{
    public class ShowRoomCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform m_Target;

        [SerializeField]
        private float m_Hight = 30f;

        [SerializeField]
        private float m_Distance = 30f;

        [SerializeField]
        private float m_Angle = 45f;

        [SerializeField]
        private float m_RotatingSpeed = 30f;

        private Vector3 refVelocity;
        private float inputX;
        private float inputZ;
        void Start()
        {
            HandleCamera();
        }

        void Update()
        {
            HandleCamera();
        }

        protected virtual void HandleCamera()
        {
            if(m_Target)
            {

                m_Angle = (m_Angle+(m_RotatingSpeed*Time.deltaTime)) % 381;
                
                Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_Hight);
                Vector3 rotatedVector = Quaternion.AngleAxis(m_Angle ,Vector3.up) * worldPosition;
                Vector3 flat_TargetPosition = m_Target.position;
                flat_TargetPosition.y = 0f;
                Vector3 finalPosition = flat_TargetPosition + rotatedVector;

                m_Target.localEulerAngles = new Vector3(0, m_Angle, 0);
                transform.position = finalPosition;
                transform.LookAt(flat_TargetPosition);
            }   
        }
    }
}
