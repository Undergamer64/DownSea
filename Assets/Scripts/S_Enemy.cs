using System;
using UnityEngine;

public class S_Enemy : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private Rigidbody2D m_playerRigidbody;
    private Collider2D m_playerCollider;
    private Collider2D m_groundCheckCollider;
    public bool IsBouncable = true;
    private int m_hp = 3;

    [SerializeField]
    private float m_maxCoolDownMove = 1.5f;
    private float m_coolDownMove;

    [SerializeField]
    private float m_speed = 3f;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_playerRigidbody = S_Player.instance.GetComponent<Rigidbody2D>();
        m_playerCollider = S_Player.instance.GetComponent<Collider2D>();
        m_groundCheckCollider = S_Player.instance.m_groundCheck.GetComponent<Collider2D>();
        m_coolDownMove = m_maxCoolDownMove;
    }

    private void Update()
    {
        if (IsBouncable)
        {
            FishMove();
        }
        else
        {
            JellyFishMove();
        }
    }

    private void FishMove()
    {
        float x_value = S_Player.instance.transform.position.x - transform.position.x;
        float y_value = S_Player.instance.transform.position.y - transform.position.y;
        Vector2 value = new Vector2(x_value, y_value).normalized * m_speed;
        m_rigidbody.velocity = value;
    }

    private void JellyFishMove()
    {
        m_coolDownMove -= Time.deltaTime * (m_maxCoolDownMove / 2);
        m_rigidbody.velocity = Vector2.up * (m_coolDownMove / (m_maxCoolDownMove / 3));
        if (m_coolDownMove <= 0)
        {
            m_rigidbody.velocity = Vector2.up;
            m_coolDownMove = m_maxCoolDownMove;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsBouncable && collision == m_groundCheckCollider)
        {
            m_playerRigidbody.velocity = new Vector2(m_playerRigidbody.velocity.x, 10f);
            S_Player.instance.m_NbShoot = S_Player.instance.m_MaxNbShoot;
            Destroy(gameObject);
        }
        else if (collision == m_playerCollider)
        {
            S_Player.instance.m_oxygen -= 20;
            S_Player.instance.transform.position += Vector3.left * (1 * Mathf.Sign(transform.position.x - S_Player.instance.transform.position.x));
        }
        else if (collision.CompareTag("Bullet"))
        {
            m_hp -= 1;
        }
    }
}
