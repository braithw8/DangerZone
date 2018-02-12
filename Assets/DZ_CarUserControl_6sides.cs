using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class DZ_CarUserControl_6sides : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        public float gravityStrength = -2.0f;
 
        public Vector3 gravityVector;
        bool Rotate = false;
        Quaternion WantedRotation;
        private Quaternion cameraWantedRotation;
        AudioSource bgMusic;
        //Transform cameraPivotTransform;
        public GameObject BGmusic; 
        private float t = 0.0f;
        private float desiredPitch = 1;
        private int rotateState = 1;
        public Text countText;
        public Text winText;
        private int count;
        private bool backwardState = false;
        public GameObject cameraPivot;
        //private Quaternion cameraRotation;
        private Vector3 cameraWantedPosition;
        //private Vector3 WantedRotationV;
        //private float timeFactor;



        private void Awake()
        {

            bgMusic = BGmusic.GetComponent<AudioSource>();
            //cameraRotation = cameraPivot.transform.rotation;
            //cameraPosition = cameraPivot.transform.position;



            // get the car controller
            m_Car = GetComponent<CarController>();
            gravityVector = new Vector3(0.0f, gravityStrength, 0.0f);
            rotateState = 1;
            Time.timeScale = 2.0f;
        }


        private void FixedUpdate()

        {
            if (Input.GetButtonDown("Fire1"))
            {
                t = 0;
                Rotatator(0.0f, gravityStrength, 0.0f, 0.0f, 113, 0.0f, 1);
                Physics.gravity = gravityVector;
                Time.timeScale = 2.0f;
                backwardState = false;
                

            }

            if (Input.GetButtonDown("Fire2"))
            {
                t = 0;
                Rotatator(0.0f, -gravityStrength, 0.0f, 0, 34.44f, 180, 0.5f);
                Physics.gravity = gravityVector;
                Time.timeScale = 1.0f;
                backwardState = false;


            }

            if (Input.GetButtonDown("Jump"))
            {
                t = 0;
                Rotatator(0.0f, 0.0f, gravityStrength, 21, 90, 90, -1.0f);
                Physics.gravity = gravityVector;
                Time.timeScale = 2.0f;
                backwardState = true;



            }

            if (Input.GetButtonDown("Fire3"))
            {
                t = 0;
                Rotatator(0.0f, 0.0f, -gravityStrength, -50, 90, -90, -0.25f);
                Physics.gravity = gravityVector;
                Time.timeScale = 0.5f;
                backwardState = true;



            }

            if (Input.GetButtonDown("Extra1"))
            {
                t = 0;
                Rotatator(gravityStrength, 0.0f, 0.0f, 33, 180, 90, -0.5f);
                Physics.gravity = gravityVector;
                Time.timeScale = 1.0f;
                backwardState = true;


            }

            if (Input.GetButtonDown("Extra2"))
            {
                t = 0;
                Rotatator(-gravityStrength, 0.0f, 0.0f, 57, 0, 90, 0.25f);
                Physics.gravity = gravityVector;
                Time.timeScale = 0.50f;
                backwardState = false;


            }

            if (backwardState == false)
            {
                cameraWantedRotation.eulerAngles = new Vector3(0, 0, 0);
            }

            if (backwardState == true)
            {
                cameraWantedRotation.eulerAngles = new Vector3(0, 180, 0);
            }


            //cameraPivot.transform.Rotate(cameraWantedRotation.eulerAngles * Time.deltaTime, Space.Self);
            cameraPivot.transform.localRotation = cameraWantedRotation;

            bgMusic.pitch = Mathf.Lerp(bgMusic.pitch, desiredPitch, t);

            //Time.timeScale = Mathf.Lerp(Time.timeScale, timeFactor, t);
            t += 0.02f * Time.deltaTime;

            if (t < 0.15f)
            {
                //transform.Rotate(WantedRotationV * Time.deltaTime, Space.World);
                transform.localRotation = Quaternion.Slerp(transform.rotation, WantedRotation, Time.deltaTime * 2.0f);


                Debug.Log("rotating");
            }
            else
            {
                Debug.Log("not rotating");
            }

            /*
                        if (t < 0.15f)
                        {
                            Debug.Log("rotating");
                            transform.rotation = Quaternion.Lerp(transform.rotation, WantedRotation, Time.deltaTime);
                        }
                        else
                        {
                            Debug.Log("not rotating");
                        }*/







                
                   









            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            if (backwardState)
            {
                m_Car.Move(-h, -v, -v, handbrake);

            }
            else
            {
                m_Car.Move(h, v, v, handbrake);

            }
#else
            m_Car.Move(h, v, v, 0f);
            
#endif
        }

        private void Rotatator(float gravityX, float gravityY, float gravityZ, float rotationX, float rotationY, float rotationZ, float desiredpitch)
        {

            gravityVector = new Vector3(gravityX, gravityY, gravityZ);
            //WantedRotation = transform.rotation;
            WantedRotation.eulerAngles = new Vector3(rotationX, rotationY, rotationZ);
            //WantedRotationV = new Vector3(rotationX, rotationY, rotationZ);
            desiredPitch = desiredpitch;



        }



        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Pick Up"))
            {
                other.gameObject.SetActive(false);
                count = count + 1;
                SetCountText();
            }
        }

        void SetCountText()
        {
            countText.text = count.ToString();
            if (count >= 36)
            {
                winText.text = "HOLY FUCK YOU WON!!!!";
            }
        }
    }

 
}
