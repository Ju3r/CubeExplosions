using System.Collections.Generic;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    private float _explosionRadius = 10;
    private float _explosionForce = 400;

    public void Explode(Cube cube)
    {
        foreach (var explodableObject in GetExplodableObjects(cube))
            explodableObject.AddExplosionForce(_explosionForce, cube.transform.position, _explosionRadius);
    }

    private List<Rigidbody> GetExplodableObjects(Cube cube)
    {
        Collider[] hits = Physics.OverlapSphere(cube.transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}