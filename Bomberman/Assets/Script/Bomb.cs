using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int radius = 3;
    [SerializeField] private float timer = 3f;
    [SerializeField] private LayerMask destroyable;
    [SerializeField] private GameObject effect;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timer);
        Blow();
    }

    private void Blow()
    {
        InstantiateEffect();
        var collidersVert = Physics2D.OverlapBoxAll(transform.position, Vector2.up * radius, 0, destroyable);
        foreach (var collide in collidersVert)
        {
            if (collide.gameObject.CompareTag("Player"))
            {
                PlayerEvents.FirePlayerDeath();
            }
            Destroy(collide.gameObject);
        }
        var collidersHor = Physics2D.OverlapBoxAll(transform.position, Vector2.right * radius, 0, destroyable);
        foreach (var collide in collidersHor)
        {
            if (collide.gameObject.CompareTag("Player"))
            {
                PlayerEvents.FirePlayerDeath();
            }
            Destroy(collide.gameObject);
        }
        Destroy(gameObject);
    }

    private void InstantiateEffect()
    {
        GameObject[] effects = new GameObject[(radius - 1) * 4];
        int j = 0;
        for (int i = 1; i < radius; i++)
        {
            effects[j] = Instantiate(effect, (Vector2) transform.position + Vector2.up * i, Quaternion.identity);
            j++;
            effects[j] = Instantiate(effect, (Vector2) transform.position + Vector2.down * i, Quaternion.identity);
            j++;
            effects[j] = Instantiate(effect, (Vector2) transform.position + Vector2.left * i, Quaternion.identity);
            j++;
            effects[j] = Instantiate(effect, (Vector2) transform.position + Vector2.right * i, Quaternion.identity);
            j++;
        }
    }
}
