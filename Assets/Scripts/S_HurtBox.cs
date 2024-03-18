using UnityEngine;

public class S_HurtBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == S_Player.instance.GetComponent<Collider2D>())
        {
            S_Player.instance.m_oxygen -= 20;
            S_Player.instance.transform.position += Vector3.left * (1 * Mathf.Sign(transform.position.x - S_Player.instance.transform.position.x));
        }
    }
}
