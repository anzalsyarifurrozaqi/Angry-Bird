using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public SlingShooter SlingShooter;
    public TrailController TrailController;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    public BoxCollider2D TapCollider;

    private Bird _shotBird;

    private bool _isGameEnded = false;

    private void Start()
    {
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }

    private void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    private void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (Enemies.Count == 0)
        {
            _isGameEnded = true;
        }
    }

    private void ChangeBird()
    {
        TapCollider.enabled = false;

        if (_isGameEnded)
        {
            return;
        }

        Birds.RemoveAt(0);

        if (Birds.Count > 0) {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }
    }

    private void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
}
