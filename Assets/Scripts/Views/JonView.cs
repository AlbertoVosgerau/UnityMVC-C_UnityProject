using System;
using UnityMVC.Events;

namespace UnityMVC.Events
{
    public class JonViewEvents
    {
        public Action<View.View> onViewDestroyed;
    }
}

public partial class JonView
{
    private JonController _controller;
    
    public JonViewEvents Events => _events;
    private JonViewEvents _events = new JonViewEvents();
    
    protected override void SolveDependencies()
    {
    }
    
    protected override void RegisterControllerEvents()
    {
    }

    protected override void UnregisterControllerEvents()
    {
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}