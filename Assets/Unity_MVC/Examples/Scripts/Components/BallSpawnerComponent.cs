using System;
using System.Collections;
using UnityEngine;
using UnityMVC;
using Component = UnityMVC.Component;
using Random = UnityEngine.Random;

public class BallSpawnerComponentEvents
{
    
}

public class BallSpawnerComponentInfo
{
    
}

public class BallSpawnerComponent : Component
{
    public BallSpawnerComponentInfo Info => _info;
    private BallSpawnerComponentInfo _info = new BallSpawnerComponentInfo();
    public BallSpawnerComponentEvents Events => _events;
    private BallSpawnerComponentEvents _events = new BallSpawnerComponentEvents();
    
    // Start your code here
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private float _height = 5;
    [SerializeField] private float _size = 4;
    [SerializeField] private float _cooldDownTime = 1;
    
    protected override void Awake()
    {
        base.Awake();
        transform.position = new Vector3(0, _height, 0);
    }

    protected override void SolveDependencies()
    {
        
    }

    public void StartSpawnRoutine()
    {
        StartCoroutine(SpawnBalls());
    }

    public void SetLimits(float height, float size)
    {
        _height = height;
        _size = size;
        transform.position = new Vector3(0, _height, 0);
    }

    private IEnumerator SpawnBalls()
    {
        while (true)
        {
            yield return new WaitForSeconds(_cooldDownTime);
            SpawnBall(_ballPrefab, _height, _size);
        }
    }
    
    private void SpawnBall(GameObject prefab, float height, float limits)
    {
        Vector3 instancePosition = new Vector3(Random.Range(-limits, limits), height, 0);
        Instantiate(prefab, instancePosition, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position, new Vector3(_size, 0.2f, 0));
    }
}