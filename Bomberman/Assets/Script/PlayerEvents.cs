using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public static event Action OnPlayerDeath;

    public static void FirePlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    private void Start()
    {
        OnPlayerDeath = null;
    }
}
