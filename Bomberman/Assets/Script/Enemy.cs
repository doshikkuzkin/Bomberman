using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private LayerMask raycastMask;
    [SerializeField] private float speed = 5f;

    private void Update()
    {
        CheckObstacles();
        transform.position += (Vector3) direction * speed * Time.deltaTime;
    }

    private bool Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position, dir, 1f, raycastMask);
        return hit.collider != null;
    }

    private void CheckObstacles()
    {
        if (Raycast(direction))
        {
            direction *= -1;
        }
    }
}
