namespace UnityMVC
{
    public class Controller
    {
        private View _view;

        public void SetView(View view)
        {
            _view = view;
        }

        public virtual void OnViewStart(){ }
        public virtual void OnViewDestroy(){ }
        
    }
}