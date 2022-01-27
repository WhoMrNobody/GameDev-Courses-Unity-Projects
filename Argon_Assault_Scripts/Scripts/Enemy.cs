using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 20;
    [SerializeField] int hits = 3;

    ScoreBoard scoreBoard;


    void Start()
    {
        AddNonTriggerBoxCollider();

        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

     void AddNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    
    void Update()
    {
        
    }

     void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hits <=0)
        {

            KillEnemy();
        }

    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }

    void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        hits--;
    }
}
