using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    public List<Material> ObstacleMaterialList = new List<Material>();
    public Material selectedMaterial;

    public int ObstacleCount;
    public int PlatformCount;
    public float MeanObstacleDisctance;
    public float MeanPlatformDisctance;
    public GameObject ObstaclePrefab;
    public GameObject PlatformPrefab;
    List<GameObject> ObstacleList = new List<GameObject>();
    List<GameObject> PlatformList = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        for (int i = 0; i < ObstacleCount; i++)
        {
            GameObject go = Instantiate(ObstaclePrefab);
            go.transform.SetParent(GameObject.FindGameObjectWithTag("Rotater").transform);
            go.transform.position = Vector3.forward * (i + 1) * MeanObstacleDisctance;

            ObstacleList.Add(go);
        }
        for (int i = 0; i < PlatformCount; i++)
        {
            GameObject go = Instantiate(PlatformPrefab);
            go.transform.SetParent(GameObject.FindGameObjectWithTag("Rotater").transform);
            go.transform.position = Vector3.forward * (i + 1) * MeanPlatformDisctance;
            PlatformList.Add(go);
        }
    }

    public IEnumerator ObjectSpawner()
    {
        int i = ObstacleCount;
        while (GameManager.instance.isGameRunning)
        {
            if (ObstacleList.Where(x => !x.activeInHierarchy).Count() > 0)
            {
                ObstacleList.Where(x => !x.activeInHierarchy).FirstOrDefault().transform.position =
                    ObstacleList.Where(x => x.activeInHierarchy).OrderByDescending(x => x.transform.position.z).FirstOrDefault().transform.position
                    + (Vector3.forward * MeanObstacleDisctance);

                foreach (Transform child in ObstacleList.Where(x => !x.activeInHierarchy).FirstOrDefault().transform)
                {
                    child.gameObject.SetActive(true);
                }

                ObstacleList.Where(x => !x.activeInHierarchy).FirstOrDefault().SetActive(true);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator PlatformSpawner()
    {
        int i = PlatformCount;
        while (GameManager.instance.isGameRunning)
        {
            if (PlatformList.Where(x => !x.activeInHierarchy).Count() > 0)
            {
                PlatformList.Where(x => !x.activeInHierarchy).FirstOrDefault().transform.position =
                    PlatformList.Where(x => x.activeInHierarchy).OrderByDescending(x => x.transform.position.z).FirstOrDefault().transform.position
                    + (Vector3.forward * MeanPlatformDisctance);

                PlatformList.Where(x => !x.activeInHierarchy).FirstOrDefault().SetActive(true);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
