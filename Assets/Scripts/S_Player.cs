using UnityEngine.InputSystem;
using UnityEngine;

public class S_Player : MonoBehaviour
{
    private Rigidbody2D m_rigidBody;

    public int m_MaxNbShoot = 1000;
    public int m_NbShoot;
    public bool m_IsGrounded = true;

    [SerializeField]
    private float m_jumpPower;

    private bool m_isShooting;

    [SerializeField]
    private float m_shootCooldown = 0.25f;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_NbShoot = m_MaxNbShoot;
    }

    private void Update()
    {
        if (m_shootCooldown <= 0)
        {
            Shoot();
            m_shootCooldown = 0.25f;
        }
        else
        {
            m_shootCooldown -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        if (m_isShooting && m_NbShoot > 0)
        {
            if (m_rigidBody.velocity.y <= -0.5f)
            {
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, -0.5f);
            }
            m_rigidBody.velocity += Vector2.up * 0.25f;
            m_NbShoot--;
        }
    }

    public void JumpOrShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (m_IsGrounded)
            {
                m_rigidBody.velocity = Vector2.up * m_jumpPower;
                m_IsGrounded = false;
                m_isShooting = false;
            }
            else
            {
                m_isShooting = true;
            }
        }
        else if (context.canceled) 
        {
            m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, m_rigidBody.velocity.y / 1.5f);
            m_isShooting = false;
            m_shootCooldown = 0;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        if (context.canceled)
        {

        }
    }
}
