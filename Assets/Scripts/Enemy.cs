using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyExplosion;
    [SerializeField] GameObject enemyHit;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit;
    [SerializeField] int hitPoints;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHitAndScore();
        
        if (hitPoints <= 0)
        {
            DestroyEnemy();
        }
    }

    private void ProcessHitAndScore()
    {
        hitPoints -= scorePerHit;
        GameObject vfx = Instantiate(enemyHit, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;

        scoreBoard.IncreaseScore(scorePerHit);
    }

    private void DestroyEnemy()
    {
        GameObject vfx = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;

        Destroy(gameObject);
    }
}
