using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Walls : MonoBehaviour
{
    private Transform m_playerTransform;

    private void Start()
    {
        m_playerTransform = S_Player.instance.transform;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(0, m_playerTransform.position.y, m_playerTransform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
