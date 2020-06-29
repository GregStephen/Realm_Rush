using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] ParticleSystem turret;
    [SerializeField] float attackRange = 10f;
    // Update is called once per frame
    void Update()
    {
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
