using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineColliderModifier : MonoBehaviour {

    public static LandmineColliderModifier Instance;

    private void Awake() 
    { 
        Instance = this; 
    }

    public CircleCollider2D circleCollider;

    public void SetColliderSize(float colliderSize)
    {
        circleCollider.radius = colliderSize;
    }

}
