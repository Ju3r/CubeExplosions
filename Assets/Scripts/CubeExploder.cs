using System.Collections.Generic;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    private float _explosionRadius = 10;
    private float _explosionForce = 400;

    public void Explode(List<Cube> cubes)
    {
        foreach (var cube in cubes)
        {
            Rigidbody cubeRigidbody = cube.GetRigidbody();
            cubeRigidbody.AddExplosionForce(_explosionForce, cube.transform.position, _explosionRadius);
        }
    }
}