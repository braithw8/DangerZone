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
        AudioSource bgMusic;
        public GameObject BGmusic; 
        private float t = 0.0f;
        private float desiredPitch = 1;
        private int rotateState = 1;
        public Text countText;
        public Text winText;
        private int count;



        private void Awake()
        {

            bgMusic = BGmusic.GetComponent<AudioSource>();

            // get the car controller
            m_Car = GetComponent<CarController>();
            gravityVector = new Vector3(0.0f, gravityStrength, 0.0f);
            rotateState = 1;

        }


        private void FixedUpdate()

        {
            if (Input.GetButtonDown("Fire1"))
            {
                t = 0;
                Rotatator(0.0f, gravityStrength, 0.0f, 0.0f, 0.0f, 0.0f, 1);
                Physics.gravity = gravityVector;
                

            }

            if (Input.GetButtonDown("Fire2"))
            {
                t = 0;
                Rotatator(0.0f, -gravityStrength, 0.0f, 0, 0.0f, 90, 0.5f);
                Physics.gravity = gravityVector;

            }

            if (Input.GetButtonDown("Jump"))
            {
                t = 0;
                Rotatator(0.0f, 0.0f, gravityStrength, -180, 180, 180, -1.0f);
                Physics.gravity = gravityVector;

            }

            if (Input.GetButtonDown("Fire3"))
            {
                t = 0;
                Rotatator(0.0f, 0.0f, -gravityStrength, -90, 90, -90, -0.25f);
                Physics.gravity = gravityVector;

            }

            if (Input.GetButtonDown("Extra1"))
            {
                t = 0;
                Rotatator(gravityStrength, 0.0f, 0.0f, -90, 0, -90, -0.5f);
                Physics.gravity = gravityVector;

            }

            if (Input.GetButtonDown("Extra2"))
            {
                t = 0;
                Rotatator(-gravityStrength, 0.0f, 0.0f, -90, 0, 90, 0.25f);
                Physics.gravity = gravityVector;

            }





            bgMusic.pitch = Mathf.Lerp(bgMusic.pitch, desiredPitch, t);
            t += 0.02f * Time.deltaTime;

            
            if (t < 0.15f)
            {
                Debug.Log("rotating");
                transform.rotation = Quaternion.Lerp(transform.rotation, WantedRotation, Time.deltaTime);
            }
            else
            {
                Debug.Log("not rotating");
            }
            










            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
            
#endif
        }

        private void Rotatator(float gravityX, float gravityY, float gravityZ, float rotationX, float rotationY, float rotationZ, float desiredpitch)
        {

            gravityVector = new Vector3(gravityX, gravityY, gravityZ);
            WantedRotation = transform.rotation;
            WantedRotation.eulerAngles = new Vector3(rotationX, rotationY, rotationZ);
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
