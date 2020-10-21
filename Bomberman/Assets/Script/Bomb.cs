using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float radius = 3f;
    [SerializeField] private float timer = 3f;
    [SerializeField] private LayerMask destroyable;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timer);
        Blow();
    }

    private void Blow()
    {
        var collidersVert = Physics2D.OverlapBoxAll(transform.position, Vector2.up * radius, 0, destroyable);
        foreach (var collide in collidersVert)
        {
            Destroy(collide.gameObject);
        }
        var collidersHor = Physics2D.OverlapBoxAll(transform.position, Vector2.right * radius, 0, destroyable);
        foreach (var collide in collidersHor)
        {
            Destroy(collide.gameObject);
        }
        Destroy(gameObject);
    }
}
