using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGamePlaneController : BaseGamePlane
{
    [SerializeField]
    private ResizableController ground;

    public override void SpawnGameplayObjects()
    {
        var shooterCount = LevelSettingsManager.Instance.CurrentLevel.ObjectsCount;

        ground.SetSize(new Vector3(Mathf.Pow(shooterCount, 0.8f), 0 , Mathf.Pow(shooterCount, 0.8f)));
        var listOfShooters = new List<ResizableController>();
        for (int i = 0; i < shooterCount; i++)
        {
            var shooter = ObjectPoolingManager.Instance.GetFromPool("Shooter").GetComponent<ResizableController>();
            
            var x = Random.Range(ground.Bounds.min.x + shooter.Bounds.size.x / 2,
                ground.Bounds.max.x - shooter.Bounds.size.x / 2);
            var z = Random.Range(ground.Bounds.min.z + shooter.Bounds.size.z / 2,
                ground.Bounds.max.z - shooter.Bounds.size.z / 2);
            shooter.transform.position = new Vector3(x, shooter.Bounds.size.y / 2, z);
            shooter.gameObject.SetActive(true);
            listOfShooters.Add(shooter);
        }
    }
}
