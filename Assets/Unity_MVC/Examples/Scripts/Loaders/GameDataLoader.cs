using UnityEngine;
using UnityMVC;
public class GameDataLoader : Loader
{
    protected GameDataSolver Solver
    {
        get
        {
            if (_solver != null)
            {
                return _solver;
            }
            _solver = MVC.Solvers.Get<GameDataSolver>();
            return _solver;
        }
    }
    protected GameDataSolver _solver;

    private string _storedDataKey = "last_player_score";
    public int GetStoredScore()
    {
        return PlayerPrefs.HasKey(_storedDataKey) ? PlayerPrefs.GetInt(_storedDataKey) : 0;
    }

    public void SavaScore(int score)
    {
        PlayerPrefs.SetInt(_storedDataKey, score);
    }
}