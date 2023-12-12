using System.Collections;
using MazeGame.EnemyAI;
using MazeGame.UI;
using UnityEngine;

namespace MazeGame.Player
{
    public class Player : MonoBehaviour
    {
        public delegate void OnPress();
        public static event OnPress OnEscPressed;
        public delegate void OnHold(bool arg);
        public static event OnHold OnTab;
        [SerializeField] private float gravityForce = -19.62f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        float durability = 4f, fatigueDuration = 4f, speed = 3f, 
            runSpeed = 6f, currentSpeed, radius = 0.4f, startTimer = 0;
        CharacterController controller;
        Vector3 velocity;
        UIprompt uIprompt;
        bool isGrounded, canRun;

        Transform enemy;
    

        private void Awake()
        {
            controller = gameObject.GetComponent<CharacterController>();
            uIprompt = FindObjectOfType<UIprompt>();
            canRun = true;
        }
        private void Start()
        {
            enemy = FindObjectOfType<MonsterListener>().transform;
        }
        private void Update()
        {
            MovePlayer();
            OpenSettings();
            CheckInfo();
        }
        private void CheckInfo()
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                OnTab?.Invoke(true);
            }
            else
            {
                OnTab?.Invoke(false);
            }
        }
        private void OpenSettings()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnEscPressed?.Invoke();
            }
        }
        private void MovePlayer()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            isGrounded = Physics.CheckSphere(groundCheck.position, radius, groundLayer);
        
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            if (Input.GetKey(KeyCode.LeftShift) && canRun)
            {
                if (startTimer < durability)
                {
                    startTimer += Time.deltaTime;
                    currentSpeed = runSpeed;
                    float value = startTimer/durability;
                    value = Mathf.Lerp(1, 0, value);
                    uIprompt.SetSlider(value);
                }
                else
                {
                    startTimer = 0;
                    StartCoroutine(Fatigue());
                }
            }
            else if (startTimer > 0)
            {
                startTimer -= Time.deltaTime;
                float value = startTimer/durability;
                value = Mathf.Lerp(1, 0, value);
                uIprompt.SetSlider(value);
                currentSpeed = speed;
            }
            else
            {
                currentSpeed = speed; 
            }


            Vector3 move = (transform.right * x + transform.forward * z) * currentSpeed * Time.deltaTime;
            move = Vector3.ClampMagnitude(move, 1.0f);

            velocity.y += gravityForce * Time.deltaTime;
            move += velocity * Time.deltaTime;

            controller.Move(move);

        }



        private IEnumerator Fatigue()
        {
            canRun = false;
            float elapsedTime = 0f;
            while (elapsedTime < fatigueDuration)
            {
                elapsedTime+= Time.deltaTime;
                float value = elapsedTime/fatigueDuration;
                value = Mathf.Lerp(0, 1, value);
                uIprompt.SetSlider(value);
                yield return null;
            }
            canRun = true;
        }

        

        private void OnControllerColliderHit(ControllerColliderHit hit) 
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null && !rb.isKinematic)
            {
                rb.velocity = hit.moveDirection * currentSpeed;
            }

        }
    }
}