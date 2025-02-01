using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private float _chanceToCreate = 100f;
    private float _divisionNumber = 2.0f;

    private int _startRandomNumber = 2;
    private int _endRandomNumber = 6;

    private int _startChanceNumber = 1;
    private int _endChanceNumber = 100;

    private float _explosionRadius = 10;
    private float _explosionForce = 400;

    private void OnMouseUpAsButton()
    {
        if (IsCreateSuccessful())
        {
            GameObject gameObjectToCreate = Modify();
            int count = GetRandomObjectsCount();

            SpawnObjects(gameObjectToCreate, count);
        }

        Explode();
        DestroyObject();
    }

    private void SpawnObjects(GameObject gameObjectToCreate, int countOfObjects)
    {
        for (int i = 0; i < countOfObjects; i++)
        {
            GameObject newCube = Instantiate(gameObjectToCreate, transform.position, Quaternion.identity);
            Cube newCubeScript = newCube.GetComponent<Cube>();
            newCubeScript._chanceToCreate = _chanceToCreate / _divisionNumber;
        }
    }

    private int GetRandomObjectsCount()
    {
        return Random.Range(_startRandomNumber, _endRandomNumber);
    }

    private bool IsCreateSuccessful()
    {
        if (Random.Range(_startChanceNumber, _endChanceNumber + 1) <= _chanceToCreate)
            return true;

        return false;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private GameObject Modify()
    {
        Vector3 scale = transform.localScale;

        float minRandomColorNumber = 0f;
        float maxRandomColorNumber = 1f;

        Color randomColor = new Color(
                                Random.Range(minRandomColorNumber, maxRandomColorNumber), 
                                Random.Range(minRandomColorNumber, maxRandomColorNumber),
                                Random.Range(minRandomColorNumber, maxRandomColorNumber)
                                );

        GameObject gameObjectToCreate = gameObject;
        gameObjectToCreate.transform.localScale = scale / _divisionNumber;
        gameObjectToCreate.GetComponent<MeshRenderer>().material.color = randomColor;

        return gameObjectToCreate;
    }
    
    private void Explode()
    {
        foreach (var explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}