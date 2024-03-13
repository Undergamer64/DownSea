using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GroundCheck : MonoBehaviour
{
    [SerializeField] 
    private S_Player m_player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_player.m_IsGrounded = true;
        m_player.m_NbShoot = m_player.m_MaxNbShoot;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_player.m_IsGrounded = false;
    }
}
