using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FollowCamera : MonoBehaviour
{
    private GameObject m_player;
    public float m_speed = 1.0f;

    private void Start()
    {
        m_player = S_Player.instance.gameObject;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, m_player.transform.position.y, Time.deltaTime * m_speed), transform.position.z);
    }
}
