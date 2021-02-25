using UnityEngine;
using System.Collections.Generic;

public class SimpleCharacterControl : MonoBehaviour {

    public Joystick Joystick;
    public float Yground;
    public GameObject  cam;
    public bool active;
   
    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;
    [SerializeField] public Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

  

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;

    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();

    private void Start()
    {
        cam = Camera.main.gameObject;
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag=="terrain" || collision.gameObject.transform.tag == "destroyable")
            m_isGrounded = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.tag=="terrain" || collision.gameObject.transform.tag == "destroyable")
           {  Yground = transform.position.y;
                cam.GetComponent<cameraFollow>().resetYCamera();
                m_isGrounded = true;
            }
       
            m_isGrounded=true;
      
    }
 private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.transform.tag=="terrain"  || collision.gameObject.transform.tag == "destroyable")
           {
            Yground = transform.position.y;
                cam.GetComponent<cameraFollow>().resetYCamera();
                m_isGrounded = true;
            }       
            m_isGrounded=true;
     
    }

    private void FixedUpdate () {
        m_animator.SetBool("Grounded", m_isGrounded);
        float v = 0, h = 0;
        if(active)
        {
            v = Joystick.Vertical + Input.GetAxis("Vertical");
            h = Joystick.Horizontal + Input.GetAxis("Horizontal");
        }
         
        Transform camera = cam.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero && active == true)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }
        m_wasGrounded = m_isGrounded;
    }
    

   

    public void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;
        Debug.Log("Jump");
        if (jumpCooldownOver && m_isGrounded)
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }

   
}
