using System;
using UnityMVC;

public class BallComponentEvents
{
    
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
        Destroy(gameObject);
    }
}