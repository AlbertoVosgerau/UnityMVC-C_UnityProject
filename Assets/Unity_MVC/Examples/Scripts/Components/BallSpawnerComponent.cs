using System;
using System.Collections;
using UnityEngine;
using UnityMVC;
using Component = UnityMVC.Component;

public class BallSpawnerComponent : Component
{
    private BallSpawnerComponentController _controller;
    
    // Start your code here
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private float _height = 5;
    [SerializeField] private float _size = 4;
    [SerializeField] private float _cooldDownTime = 1;
    protected override void Awake()
    {
        _controller = new BallSpawnerComponentController();
        _controller.SetComponent(this);
        base.Awake();
        transform.position = new Vector3(0, _height, 0);
    }
    protected virtual void Start()
    {
        _controller.OnComponentStart();
    }

    protected virtual void Update()
    {
        _controller.OnComponentUpdate();
    }

    protected void OnDestroy()
    {
        _controller.OnComponentDestroy();
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
            _controller.SpawnBall(_ballPrefab, _height, _size);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position, new Vector3(_size, 0.2f, 0));
    }
}