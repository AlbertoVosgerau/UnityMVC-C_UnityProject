namespace UnityMVC
{
    public class MVC
    {
        public static ControllerLocator Controllers => _controllers;
        private static ControllerLocator _controllers = new ControllerLocator();

        public static RepositoryLocator Repositories => _repositories;
        private static RepositoryLocator _repositories = new RepositoryLocator();

        public static ServiceLocator Services => _services;
        private static ServiceLocator _services = new ServiceLocator();

        public static void ClearControllers()
        {
            _controllers.Clear();
        }

        public static void ClearRepositories()
        {
            _repositories.Clear();
        }

        public static void ClearServices()
        {
            _services.Clear();
        }
    }
}