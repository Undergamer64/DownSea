using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MapManager : MonoBehaviour
{
    private GameObject m_player;
    private float m_wallSpacingFromCenter;
    private float m_levelSize;
    private float m_seed;
    private Vector3 m_lastPlayerPos;

    [SerializeField]
    private GameObject m_wall;

    [SerializeField]
    private List<GameObject> m_enemyPrefab;

    private void Start()
    {
        m_seed = Random.Range(-1000, 1000);
        m_player = S_Player.instance.gameObject;
        m_wallSpacingFromCenter = Camera.main.orthographicSize / 2 + 4;
        m_levelSize = (Camera.main.transform.position.y + m_wallSpacingFromCenter) + 5;
        m_lastPlayerPos = m_player.transform.position;
        StartCoroutine(GeneratePlateform()); 
    }

    private void Update()
    {
        CheckPos();
    }

    private void CheckPos()
    {
        Vector2 currentPlayerPos = m_player.transform.position;
        if (Mathf.Abs(currentPlayerPos.y - m_lastPlayerPos.y) >= 5)
        {

            StartCoroutine(GeneratePlateform());
            m_lastPlayerPos = currentPlayerPos;
        }
    }

    private IEnumerator GeneratePlateform()
    {
        float playerBottomY = m_player.transform.position.y - Camera.main.orthographicSize;
        for (int y = 0; y < m_levelSize; y++)
        {
            for (int x = 0; x < Camera.main.orthographicSize+8; x++)
            {
                Vector3Int spawnPos = new Vector3Int((int)-m_wallSpacingFromCenter + x,(int) playerBottomY, 0);
                Vector3 blockPos = new Vector3(spawnPos.x, spawnPos.y, 0);
                Collider2D blockExist = Physics2D.OverlapCircle(blockPos, 0.1f);

                if (blockExist == null)
                {
                    float spawnProbSub = 0;
                    float screenPercent = 0.3f;
                    float lessBlockMidPercent = 0.15f;

                    if (Mathf.Abs(spawnPos.x) < Camera.main.orthographicSize * screenPercent)
                    {
                        spawnProbSub += lessBlockMidPercent;
                    }
                    if (Mathf.PerlinNoise(spawnPos.x / 10f + m_seed, spawnPos.y / 10f + m_seed) >= 0.7f + spawnProbSub)
                    {
                        GenerateWall(spawnPos);
                    }
                    else
                    {
                        GenerateEnemy(spawnPos);
                    }
                }
            }
            playerBottomY -= 1;
        }
        yield return new WaitForSeconds(0.0001f);
    }

    private void GenerateWall(Vector3Int spawnPos)
    {

        if (80f < Mathf.PerlinNoise(spawnPos.x / 10f + m_seed, spawnPos.y / 10f + m_seed))                                                // 50% Chance to spawn with "Breackable" Tag
        {
            GameObject block = Instantiate(m_wall, spawnPos, Quaternion.identity, transform);
        }
        else
        {
            GameObject block = Instantiate(m_wall, spawnPos, Quaternion.identity, transform);
        }
    }

    private void GenerateEnemy(Vector3Int spawnPos)
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue < 0.2f)
        {
            GameObject enemy = Instantiate(m_enemyPrefab[Random.Range(0, 2)], spawnPos, Quaternion.identity);
        }
    }
}
