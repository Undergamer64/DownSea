using UnityEngine;

public class S_EnemyHitBox : MonoBehaviour
{
    private Rigidbody2D m_playerRigidbody;
    private Collider2D m_playerCollider;
    private int m_hp = 3;

    private void Start()
    {
        m_playerRigidbody = S_Player.instance.GetComponent<Rigidbody2D>();
        m_playerCollider = S_Player.instance.m_groundCheck.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == m_playerCollider)
        {
            m_playerRigidbody.velocity = new Vector2(m_playerRigidbody.velocity.x, 10f);
            S_Player.instance.m_NbShoot = S_Player.instance.m_MaxNbShoot;
            Destroy(transform.parent.gameObject);
        }
        else if (collision.CompareTag("Bullet"))
        {
            m_hp -= 1;
        }
    }
}
