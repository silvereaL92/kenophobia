using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test_script : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator ani;
    public GameObject aim_point;

    public bool execute_walking;
    public bool execute_picking_up;
    public bool execute_running;

    public float walk_speed;
    public float run_speed;

    public bool walk;
    public bool run;
    public bool pick_up;

    public bool destermine_new_aim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        way_points.Clear();
        pick_up_points.Clear();

        GameObject[] waypointsFind = GameObject.FindGameObjectsWithTag("waypoint");
        GameObject[] pick_up_pointsFind = GameObject.FindGameObjectsWithTag("pickuppoint");

        foreach(GameObject g in waypointsFind)
        {
            way_points.Add(g);
        }
        foreach (GameObject g in pick_up_pointsFind)
        {
            pick_up_points.Add(g);
        }
    }

    bool in_pickup;

    Coroutine pickup_start;

    public GameObject crowbar;

    IEnumerator pickup_execute()
    {
        yield return new WaitForSeconds(0);

        ani.SetInteger("legs", 32);
        ani.SetInteger("arms", 32);

        yield return new WaitForSeconds(2);

        in_pickup = false;
        destermine_new_aim = false;

        StopCoroutine(pickup_start);
    }

    public bool ready;

    void Update()
    {
        if(!ready)
        {
            return;
        }
     
        if(!destermine_new_aim)
        {
            int what_to_choose = UnityEngine.Random.Range(0, 3);

            walk = false;
            run = false;
            pick_up = false;

            if(what_to_choose == 0)
            {
                walk = true;

                int Which_point = UnityEngine.Random.Range(0, way_points.Count);
                aim_point = way_points[Which_point].gameObject;
                destermine_new_aim = true;
            }

            if (what_to_choose == 1)
            {
                run = true;

                int Which_point = UnityEngine.Random.Range(0, way_points.Count );
                aim_point = way_points[Which_point].gameObject;
                destermine_new_aim = true;
            }

            if (what_to_choose == 2)
            {
                pick_up = true;

                int Which_point = UnityEngine.Random.Range(0, pick_up_points.Count);
                aim_point = pick_up_points[Which_point].gameObject;
                destermine_new_aim = true;
            }
        }

        if (destermine_new_aim)
        {
            if (walk)
            {
                if (Vector3.Distance(transform.position,aim_point.transform.position) > 0.25f)
                {
                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;

                    ani.SetInteger("arms", 5);
                    ani.SetInteger("legs", 5);

                    destermine_new_aim = false;
                }
            }

            if(run)
            {
                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {
                    agent.speed = run_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 2);
                    ani.SetInteger("legs", 2);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;

                    ani.SetInteger("arms", 5);
                    ani.SetInteger("legs", 5);

                    destermine_new_aim = false;
                }
            }

            if(pick_up && !in_pickup)
            {
                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {
                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;

                    if (!in_pickup)
                    {
                        in_pickup = true;
                        pickup_start = StartCoroutine(pickup_execute());
                    }
                }
            }
        }
    }

    public List<GameObject> way_points = new List<GameObject>();
    public List<GameObject> pick_up_points = new List<GameObject>();
}
