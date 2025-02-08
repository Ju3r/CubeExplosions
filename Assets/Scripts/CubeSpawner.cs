using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private List<Cube> _cubes = new List<Cube>();
    [SerializeField] private CubeExploder _exploder;

    private float _divisionNumber = 2.0f;

    private int _startChanceNumber = 1;
    private int _endChanceNumber = 100;

    private int _startRandomNumber = 2;
    private int _endRandomNumber = 6;

    private void OnEnable()
    {
        foreach (Cube cube in _cubes)
        {
            Subscription(cube);
        }
    }

    private void OnDisable()
    {
        foreach (Cube cube in _cubes)
        {
            Unsubscription(cube);
        }
    }

    public void Spawn(Cube cube)
    {
        _cubes.Remove(cube);

        if (IsCreateSuccessful(cube))
        {
            List<Cube> createdCubes = new List<Cube>();

            Cube modifiedCube = Modify(cube);

            for (int i = 0; i < GetRandomObjectsCount(); i++)
            {
                Cube newCube = Instantiate(modifiedCube, cube.transform.position, Quaternion.identity);
                ChangeColor(newCube);
                newCube.SetChanceToCreate(cube.ChanceToCreate / _divisionNumber);

                createdCubes.Add(newCube);
                _cubes.Add(newCube);

                Subscription(newCube);
            }

            _exploder.Explode(createdCubes);
        }
    }

    private void Subscription(Cube cube)
    {
        cube.OnCubeClicked += OnCubeClicked;
    }

    private void Unsubscription(Cube cube)
    {
        cube.OnCubeClicked -= OnCubeClicked;
    }

    private void OnCubeClicked(Cube cube)
    {
        Spawn(cube);
    }

    private bool IsCreateSuccessful(Cube cube)
    {
        if (Random.Range(_startChanceNumber, _endChanceNumber + 1) <= cube.ChanceToCreate)
            return true;

        return false;
    }

    private int GetRandomObjectsCount()
    {
        return Random.Range(_startRandomNumber, _endRandomNumber + 1);
    }

    private Cube Modify(Cube cube)
    {
        Vector3 scale = cube.transform.localScale / _divisionNumber;

        cube.transform.localScale = scale;

        return cube;
    }

    private static void ChangeColor(Cube cube)
    {
        float minRandomColorNumber = 0f;
        float maxRandomColorNumber = 1f;

        Color randomColor = new Color(
                                Random.Range(minRandomColorNumber, maxRandomColorNumber),
                                Random.Range(minRandomColorNumber, maxRandomColorNumber),
                                Random.Range(minRandomColorNumber, maxRandomColorNumber)
                                );

        cube.SetColor(randomColor);
    }
}