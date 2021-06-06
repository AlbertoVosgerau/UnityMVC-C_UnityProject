using System;
using UnityMVC;
/// <summary>
/// Components will handle visual stuff, local events of the object and internal logic.
/// In this case, the ball has an internal logic that says it dies if some condition is satisfied.
/// Events will inform external listeners of important events that might be used by another entity or controller.
/// </summary>
public class BallComponentEvents
{
    public Action onCreated;
    public Action onDestroyed;
}

public class BallComponentInfo
{
    
}
public class BallComponent : Component
{
    public BallComponentInfo Info => _info;
    private BallComponentInfo _info = new BallComponentInfo();
    public BallComponentEvents Events => _events;
    private BallComponentEvents _events = new BallComponentEvents();
    // Start your code here
    
    protected override void Awake()
    {
        base.Awake();
        _events.onCreated?.Invoke();
    }
    protected override void SolveDependencies()
    {
        
    }

    private void Update()
    {
        if (IsAlive())
        {
            return;
        }
        SelfDestroy();
    }

    public bool IsAlive()
    {
        return transform.position.y > -1;
    }

    public void SelfDestroy()
    {
        _events.onDestroyed?.Invoke();
        Destroy(gameObject);
    }
}