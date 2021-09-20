using UnityMVC.Locator;

namespace UnityMVC
{
    public partial class MVCApplication
    {
        public static ControllerLocator Controllers => _controllers;
        private static ControllerLocator _controllers = new ControllerLocator();

        public static ContainerLocator Containers => _containers;
        private static ContainerLocator _containers = new ContainerLocator();
        
        public static void ClearControllers()
        {
            _controllers.Clear();
        }

        public static void ClearContainers()
        {
            _containers.Clear();
        }
    }
}