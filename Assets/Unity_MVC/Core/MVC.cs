namespace UnityMVC
{
    public class MVC
    {
        public static ControllerLocator Controllers => _controllers;
        private static ControllerLocator _controllers = new ControllerLocator();

        public static ContainerLocator Containers => _containers;
        private static ContainerLocator _containers = new ContainerLocator();

        public static LoaderLocator Loaders => _loaders;
        private static LoaderLocator _loaders = new LoaderLocator();

        public static SolverLocator Solvers => _solvers;
        private static SolverLocator _solvers = new SolverLocator();

        public static ComponentControllersFactory ComponentControllerFactory => _componentControllersFactory;
        private static ComponentControllersFactory _componentControllersFactory = new ComponentControllersFactory();

        public static void ClearControllers()
        {
            _controllers.Clear();
        }

        public static void ClearContainers()
        {
            _containers.Clear();
        }

        public static void ClearLoaders()
        {
            _loaders.Clear();
        }
        
        public static void ClearSolvers()
        {
            _solvers.Clear();
        }
    }
}