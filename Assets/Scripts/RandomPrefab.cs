using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPrefab : MonoBehaviour
{
    
    public Dictionary<string, GameObject> customPrefabs = new Dictionary<string, GameObject>();
    public GameObject spawnPoint;
    public GameObject bottlePrefab;
    public GameObject bookPrefab;
    public GameObject rockPrefab;

    void Start()
    {
        customPrefabs.Add("book", bookPrefab);
        customPrefabs.Add("bottle", bottlePrefab);
        customPrefabs.Add("rock", rockPrefab);

        List<string> tagList = new List<string>(customPrefabs.Keys);

        int tagIndex = UnityEngine.Random.Range(0, tagList.Count);
        string prefabTag = tagList[tagIndex];
        GameObject pts = Instantiate(customPrefabs[prefabTag]);
        pts.transform.position = spawnPoint.transform.position;
        pts.tag = prefabTag;

        // pts.AddComponent<BoxCollider>();
        // BoxCollider boxCollider = pts.GetComponent<BoxCollider>();
        // boxCollider.size = new Vector3(1, 1, 1);
        // ordis.instantiatedPrefabs.Add(pts);

        switch (prefabTag)
        {
            case "bottle":
            case "book":
                pts.transform.localScale += new Vector3(1, 1, 1);
                break;
            default:
                break;
        }
    }
}
