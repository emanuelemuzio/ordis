using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrdisV2 : MonoBehaviour
{
    public NavMeshAgent ordis;
    private List<GameObject> fringeList = new List<GameObject>();
    private bool searchState = false;

    // Dato il tag goal (discriminante per capire quali oggetti nel labirinto rappresentano un goal)
    // pianifichiamo il moto per visitare tutti i goal ordinandoli per la distanza

    void Update()
    {
        if (searchState)
        {
            if (ordis.pathStatus == NavMeshPathStatus.PathComplete && ordis.remainingDistance < 0.1)
            {
                searchState = false;
                fringeList.RemoveAt(0);
                if (fringeList.Count != 0)
                {
                    ordis.SetDestination(fringeList[0].transform.position);
                    searchState = true;
                }
            }
        }
        else
        {
            if (fringeList.Count != 0)
            {
                searchState = true;
                ordis.SetDestination(fringeList[0].transform.position);
            }
        }
    }

    public void Search(string goal)
    {
        fringeList = PlanPath(goal);
    }

    List<GameObject> PlanPath(string goal)
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag(goal);
        List<GameObject> sortedSpawns = new List<GameObject>();
        List<float> pathsCost = new List<float>();

        foreach (GameObject spawn in spawns)
        {
            var path = new NavMeshPath();
            ordis.CalculatePath(spawn.transform.position, path);
            float pathCost = 0.0f;

            Vector3 lastPos = transform.position;
            foreach (Vector3 corner in path.corners)
            {
                pathCost += Vector3.Distance(lastPos, corner);
            }

            pathsCost.Add(pathCost);
        }

        List<int> indexSortedArray = new List<int>();
        List<float> pathsCostTmp = new List<float>(pathsCost);
        pathsCost.Sort();

        foreach (float pathCost in pathsCost)
        {
            int sortedIndex = pathsCostTmp.IndexOf(pathCost);
            indexSortedArray.Add(sortedIndex);
            sortedSpawns.Add(spawns[sortedIndex]);
        }

        return sortedSpawns;
    }
}
