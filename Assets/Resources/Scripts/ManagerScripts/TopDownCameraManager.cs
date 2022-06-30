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
        private float m_MovmentSpeed = 40f;

        [SerializeField]
        private float m_RotatingSpeed = 30f;

        [SerializeField]
        private float minX = -150f;

        [SerializeField]
        private float minZ = -150f;

        [SerializeField]
        private float maxX = 1500f;

        [SerializeField]
        private float maxZ = 150f;

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
                inputX = Input.GetAxis("Horizontal");
                inputZ = Input.GetAxis("Vertical");

                m_Distance = Mathf.Clamp(Mathf.Abs(m_Distance + (Input.mouseScrollDelta.y*2)) ,5 ,20);
                m_Hight = Mathf.Pow(1.2f*m_Distance,1.1f);

                m_Target.Translate(new Vector3(inputX,7,inputZ)*Time.deltaTime*m_MovmentSpeed);

                m_Target.SetPositionAndRotation(new Vector3(Mathf.Clamp(m_Target.position.x,minX,maxX),7,Mathf.Clamp(m_Target.position.z,minZ,maxZ)),Quaternion.identity);
                
                if (Input.GetKey(KeyCode.R))
                {
                    m_Angle = (m_Angle+(m_RotatingSpeed*Time.deltaTime)) % 381;
                }
                if (Input.GetKey(KeyCode.F))
                {
                    m_Angle = (m_Angle-(m_RotatingSpeed*Time.deltaTime)) % 381;
                }
                
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
