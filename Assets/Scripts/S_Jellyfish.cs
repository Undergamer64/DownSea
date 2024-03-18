using UnityEngine;

public class S_Enemy : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    [SerializeField]
    private float m_maxCoolDownMove = 1.5f;
    private float m_coolDownMove;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_coolDownMove = m_maxCoolDownMove;
    }

    private void Update()
    {
        m_coolDownMove -= Time.deltaTime * (m_maxCoolDownMove/2);
        m_rigidbody.velocity = Vector2.up * (m_coolDownMove/(m_maxCoolDownMove /3));
        if (m_coolDownMove <= 0)
        {
            m_rigidbody.velocity = Vector2.up;
            m_coolDownMove = m_maxCoolDownMove;
        }
    }

}
