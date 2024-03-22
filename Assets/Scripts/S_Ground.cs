using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Ground : MonoBehaviour
{
    public bool m_isDestructable = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_isDestructable && collision.CompareTag("Bullet")) //doesn't work XD
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
