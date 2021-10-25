using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyExplosion;
    [SerializeField] GameObject enemyHit;    
    [SerializeField] int scorePerHit;
    [SerializeField] int hitPoints;

    GameObject parentObject;
    ScoreBoard scoreBoard;
    int maxScore;

    void Start()
    {
        maxScore = hitPoints;
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbody();
    }      

    private void OnParticleCollision(GameObject other)
    {
        ProcessHitAndScore();
        
        if (hitPoints <= 0)
        {
            DestroyEnemy();
        }
    }

    private void AddRigidbody()
    {
        Rigidbody enemyRb = gameObject.AddComponent<Rigidbody>();
        enemyRb.useGravity = false;
    }

    private void ProcessHitAndScore()
    {
        hitPoints -= scorePerHit;
        GameObject vfx = Instantiate(enemyHit, transform.position, Quaternion.identity);
        vfx.transform.parent = parentObject.transform;

        scoreBoard.IncreaseScore(scorePerHit);
    }

    private void DestroyEnemy()
    {
        GameObject vfx = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
        vfx.transform.parent = parentObject.transform;

        scoreBoard.IncreaseScore(maxScore);
        Destroy(gameObject);
    }
}
