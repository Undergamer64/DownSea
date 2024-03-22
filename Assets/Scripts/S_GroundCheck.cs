using System;
using System.Collections.Generic;
using UnityEngine;

public class S_GroundCheck : MonoBehaviour
{
    [SerializeField] 
    private S_Player m_player;
    private Animator m_playerAnimator;

    private void Awake()
    {
        m_playerAnimator = m_player.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            m_playerAnimator.SetBool("IsJumping", false);
            m_player.m_IsGrounded = true;
            m_player.m_NbShoot = m_player.m_MaxNbShoot;
            S_Player.instance.AmmoUpdate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            m_player.m_IsGrounded = false;
        }
    }
}
