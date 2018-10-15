using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Damageable), typeof(Rigidbody), typeof(NavMeshAgent))]
public class BaseEnemy : MonoBehaviour
{

    [SerializeField]
    Transform moveTarget;

    private Damageable health;
    private Rigidbody rb;
    private LineRenderer lineRenderer;
    public Sprite icon;


    void Start()
    {
        moveTarget = GameObject.FindGameObjectWithTag("Goal").transform;
        this.rb = GetComponent<Rigidbody>();
        this.health = GetComponent<Damageable>();
        this.health.Init();
        this.health.Destroyed += HandleDestroyed;

        GetComponent<NavMeshAgent>().SetDestination(this.moveTarget.position);
        lineRenderer = GetComponent<LineRenderer>();

    }

    private void Update()
    {
        //NavMeshAgent agent = this.gameObject.GetComponent<NavMeshAgent>();
        //lineRenderer.positionCount = agent.path.corners.Length;
        //lineRenderer.SetPositions(agent.path.corners);
        //lineRenderer.enabled = true;

    }

    void OnDestroy()
    {
        if (this.health != null)
            this.health.Destroyed -= HandleDestroyed;
    }


    private void HandleDestroyed(Damageable d)
    {
        Destroy(this.gameObject);
    }

}
