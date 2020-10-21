using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask raycastMask;
    [SerializeField] private LayerMask explosionMask;
    private bool isInMovement;
    private Vector2 lastPos = Vector2.right;
    
    [SerializeField] private GameObject bomb;

    private void Start()
    {
        PlayerEvents.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void Update()
    {
        if (isInMovement) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer(Vector2.left);
            lastPos = Vector2.left; 
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer(Vector2.right);
            lastPos = Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            MovePlayer(Vector2.up);
            lastPos = Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            MovePlayer(Vector2.down);
            lastPos = Vector2.down;
        }

        /*if (Input.GetMouseButtonDown(0))
        {
            var obj = RaycastFromCamera();
            if (obj != null && obj.CompareTag("Explosive"))
            {
                Destroy(obj);
                var colliders = Physics2D.OverlapCircleAll(obj.transform.position, 1f, explosionMask);
                foreach (var collide in colliders)
                {
                    Destroy(collide.gameObject);
                }
            }
        }*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Raycast(lastPos)) return;
            Instantiate(bomb, (Vector2)transform.position + lastPos, Quaternion.identity);
        }

    }
    
    private void MovePlayer(Vector2 dir)
    {
        if (Raycast(dir)) return;
        
        isInMovement = true;
        var position = (Vector2) transform.position + dir;
        transform.DOMove(position, 0.2f).OnComplete(() => isInMovement = false);
    }
    
    private bool Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position, dir, 1f, raycastMask);
        return hit.collider != null;
    }
}
