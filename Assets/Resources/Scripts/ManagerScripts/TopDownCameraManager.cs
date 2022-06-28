using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGGame.Cameras
{
    public class TopDownCameraManager : MonoBehaviour
    {
        [SerializeField]
        private Transform m_Target;

        [SerializeField]
        private float m_Hight = 10f;

        [SerializeField]
        private float m_Distance = 20f;

        [SerializeField]
        private float m_Angle = 45f;

        [SerializeField]
        private float m_SmoothSpeed = 0.8f;

        [SerializeField]
        private float m_MovmentSpeed = 20f;

        private Vector3 refVelocity;
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
                m_Distance = Mathf.Clamp(Mathf.Abs(m_Distance + (Input.mouseScrollDelta.y*2)) ,5 ,20);
                m_Hight = 1.2f*m_Distance;
                
                if (Input.GetKey(KeyCode.W))
                {
                    m_Target.Translate(Vector3.forward*Time.deltaTime* m_MovmentSpeed);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    m_Target.Translate(Vector3.back*Time.deltaTime* m_MovmentSpeed); 
                }
                if (Input.GetKey(KeyCode.A))
                {
                    m_Target.Translate(Vector3.left*Time.deltaTime* m_MovmentSpeed); 
                }
                if (Input.GetKey(KeyCode.D))
                {
                    m_Target.Translate(Vector3.right*Time.deltaTime* m_MovmentSpeed); 
                }
                if (Input.GetKey(KeyCode.R))
                {
                    m_Angle = (m_Angle+0.1f) % 381;
                }
                if (Input.GetKey(KeyCode.F))
                {
                    m_Angle = (m_Angle-0.1f) % 381;
                }
                
                Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_Hight);
                Vector3 rotatedVector = Quaternion.AngleAxis(m_Angle ,Vector3.up) * worldPosition;
                Vector3 flat_TargetPosition = m_Target.position;
                flat_TargetPosition.y = 0f;
                Vector3 finalPosition = flat_TargetPosition + rotatedVector;

                m_Target.localEulerAngles = new Vector3(0, m_Angle, 0);
                transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, m_SmoothSpeed);
                transform.LookAt(flat_TargetPosition);
            }   
        }
    }
}
