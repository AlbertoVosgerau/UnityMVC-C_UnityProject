using UnityEngine;
using UnityMVC;

public class BallSpawnerComponentController : ComponentController
{
    private BallSpawnerComponent _component;
    
    // Start your code here
    public void SetComponent(BallSpawnerComponent component)
    {
        _component = component;
    }

    public virtual void SpawnBall(GameObject prefab, float height, float limits)
    {
        Vector3 instancePosition = new Vector3(Random.Range(-limits, limits), height, 0);
        Object.Instantiate(prefab, instancePosition, Quaternion.identity);
    }
}