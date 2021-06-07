using System;
using UnityEngine;
using Component = UnityMVC.Component;

/// <summary>
/// Components will handle visual stuff, local events of the object and internal logic.
/// In this case, It handles the player object.
/// It will move based on an implementation of an IPlayerInput, that listens to inputs.
/// If it collides with a ball, it destroys it and raises an event.
/// Events will inform external listeners of important events that might be used by another entity or controller.
/// </summary>
/// 
public class PlayerComponentEvents
{
    public Action onCreated;
    public Action onDestroyed;
    public Action onGotTheBall;
}

public class PlayerComponentInfo
{
    
}

public class PlayerComponent : Component
{
    public PlayerComponentInfo Info => _info;
    private PlayerComponentInfo _info = new PlayerComponentInfo();
    public PlayerComponentEvents Events => _events;
    private PlayerComponentEvents _events = new PlayerComponentEvents();
    
    [SerializeField] private float _speed = 2;
    private PlayerInputImpl _input;
    
    protected override void Awake()
    {
        base.Awake();
        _events.onCreated?.Invoke();
    }

    private void OnDestroy()
    {
        _events.onDestroyed?.Invoke();
    }

    protected override void SolveDependencies()
    {
        _input = new PlayerInputImpl();
    }

    private void Update()
    {
        float horizontalSpeed = _input.Horizontal * _speed * Time.deltaTime;
        transform.Translate(new Vector3(horizontalSpeed, 0,0));
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        _events.onGotTheBall?.Invoke();
    }
}