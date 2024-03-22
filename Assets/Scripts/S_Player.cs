using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class S_Player : MonoBehaviour
{
    public static S_Player instance;

    public GameObject m_groundCheck;

    public Rigidbody2D m_rigidBody;
    private Animator m_animator;

    [SerializeField]
    private GameObject m_oxygenBar;

    [SerializeField]
    private TextMeshProUGUI m_ammoText;
    public TextMeshProUGUI m_scoreText;

    [SerializeField]
    private GameObject m_bulletPrefab;

    [SerializeField]
    private Transform m_bulletSpawner;

    [SerializeField]
    private Transform m_bulletHolderTransform;

    [SerializeField]
    private float m_jumpPower;
    public bool m_IsGrounded = true;

    [SerializeField]
    private float m_speed = 1f;
    private bool m_isMoving;
    private float m_moveDir = 0;

    [SerializeField]
    private float m_maxShootCooldown = 0.15f;
    private float m_shootCooldown;
    private bool m_isShooting;
    public int m_MaxNbShoot = 10;
    public int m_NbShoot;

    public float m_oxygen = 100f;
    public int m_score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_NbShoot = m_MaxNbShoot;
        m_shootCooldown = m_maxShootCooldown;
    }
    private void FixedUpdate()
    {
        if (m_rigidBody.velocity.y <= -15f)
        {
            m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, -20f);
        }
    }

    private void Update()
    {
        Move();
        Shoot();
        Drowning();
    }

    private void Move()
    {
        if (m_isMoving)
        {
            m_rigidBody.velocity = new Vector2(m_moveDir * m_speed, m_rigidBody.velocity.y);
        }
        else
        {
            m_rigidBody.velocity = new Vector2(0, m_rigidBody.velocity.y);
        }
    }


    private void Shoot()
    {
        if (m_shootCooldown <= 0)
        {
            if (m_isShooting && m_NbShoot > 0)
            {
                if (m_rigidBody.velocity.y < -0.5f)
                {
                    m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, -0.5f);
                }
                m_rigidBody.velocity += Vector2.up * 0.25f;
                m_NbShoot--;
                S_Player.instance.AmmoUpdate();
                GameObject bullet = Instantiate(m_bulletPrefab, m_bulletSpawner.position, m_bulletSpawner.rotation, m_bulletHolderTransform);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * 10;
                m_shootCooldown = 0.25f;
            }
        }
        else if (m_IsGrounded)
        {
            m_shootCooldown = m_maxShootCooldown;
        }
        else
        {
            m_shootCooldown -= Time.deltaTime;
        }
        
    }

    private void Drowning()
    {
        float value = 5 * Time.deltaTime;
        m_oxygen -= value;
        if (m_oxygen <= 0)
        {
            Destroy(gameObject);
        }
        RectTransform rt = m_oxygenBar.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.rect.width, m_oxygen*5);
        m_oxygenBar.transform.localPosition = new Vector2( 760, (m_oxygen - 100) * 2.5f);
    }

    public void AmmoUpdate()
    {
        m_ammoText.text = "Ammo : " + S_Player.instance.m_NbShoot.ToString();
    }

    public void JumpOrShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (m_IsGrounded)
            {
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, m_jumpPower);
                m_animator.SetBool("IsJumping", true);
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
            if (m_rigidBody.velocity.y > 0)
            {
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, m_rigidBody.velocity.y / 1.5f);
            }
            m_isShooting = false;
            m_shootCooldown = 0;
        }
    }

    public void MoveAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            float value = context.ReadValue<float>();
            if (value < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (value > 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            m_moveDir = value;
            m_animator.SetBool("IsMoving", true);
            m_isMoving = true;
        }
        if (context.canceled)
        {
            m_moveDir = 0;
            m_animator.SetBool("IsMoving", false);
            m_isMoving = false;
        }
    }
}