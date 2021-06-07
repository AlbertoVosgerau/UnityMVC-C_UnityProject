using UnityEngine;
using UnityMVC;
public class GameDataContainer : Container
{
    protected GameDataLoader Loader
    {
        get
        {
            if (_loader != null)
            {
                return _loader;
            }
            _loader = MVC.Loaders.Get<GameDataLoader>();
            return _loader;
        }
    }
    protected GameDataLoader _loader;

    public int BestScore
    {
        get
        {
            if (_bestScore > 0)
            {
                return _bestScore;
            }

            _bestScore =  Loader.GetStoredScore();
            return _bestScore;
        }
    }
    private int _bestScore;

    public void SaveScore(int score)
    {
        Loader.SavaScore(score);
    }
}