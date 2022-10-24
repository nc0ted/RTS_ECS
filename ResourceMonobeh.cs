using System;
using Leopotam.Ecs;
using UnityEngine;

public class ResourceMonobeh : MonoBehaviour
{
    public Type type;
    public enum Type
    {
        wood,
        stone,
        iron
    }
    public EcsEntity entity;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,3);
    }
}