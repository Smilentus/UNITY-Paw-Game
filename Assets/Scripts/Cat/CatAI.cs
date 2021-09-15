using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class CatAI : MonoBehaviour
{
    public float currentSpeed = 40f;
    public float scareSpeed = 80f;
    public float normalSpeed = 40f;
    public float obastacleRange = 10f;
    public GameObject ext;
    Vector3 extVec;
    private NavMeshAgent agent;
    public bool catScare = false;

    void Start()
    {
        ext = GameObject.FindGameObjectWithTag("CatExit");
        extVec = ext.transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!catScare)
        {
            transform.Translate(0, 0, currentSpeed * Time.deltaTime);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                if (hit.distance < obastacleRange)
                {
                    float angle = UnityEngine.Random.Range(-100, 100);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void ScareCat()
	{
        catScare = true;
        currentSpeed = scareSpeed;
        agent.speed = scareSpeed;
        agent.acceleration = scareSpeed * 0.7f;
        agent.SetDestination(extVec);
    }

    public void CalmCat()
	{
        catScare = false;
        currentSpeed = normalSpeed;
        agent.speed = normalSpeed;
        agent.acceleration = normalSpeed;
    }
}
