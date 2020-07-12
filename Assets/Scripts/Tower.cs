using UnityEngine;

public class Tower : MonoBehaviour
{
    // Parameters of each tower
    [SerializeField] Transform objectToPan;
    [SerializeField] ParticleSystem turret;
    [SerializeField] float attackRange = 10f;

    // State of each tower
    Transform targetEnemy;
    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            ProcessFiring();
        }
        else
        {
            SetGunsActive(false);
        }

    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyMovement>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyMovement testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distanceToA = Vector3.Distance(transformA.position, gameObject.transform.position);
        var distanceToB = Vector3.Distance(transformB.position, gameObject.transform.position);

        if (distanceToA < distanceToB)
        {
            return transformA;
        }
        return transformB;

    }
    private void ProcessFiring()
    {
        var distanceToEnemy = Vector3.Distance(targetEnemy.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange)
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }
    private void SetGunsActive(bool isActive)
    {
        var emissionModule = turret.emission;
        emissionModule.enabled = isActive;
        
    }
}
