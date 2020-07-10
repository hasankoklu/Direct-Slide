using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnEnable()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            foreach (Transform child in transform.GetChild(i))
            {
                child.GetComponent<Renderer>().material = ObjectPoolManager.instance.ObstacleMaterialList[i];
            }
        }

        foreach (Transform child in transform.GetChild(Random.Range(0, transform.childCount)).gameObject.transform)
        {
            child.gameObject.GetComponent<Renderer>().material = ObjectPoolManager.instance.selectedMaterial;
            child.gameObject.tag = "TrueObstacle";
        }
    }

    void Update()
    {
        if (PlayerManager.instance.gameObject.transform.position.z > transform.position.z + 50f)
        {
            gameObject.SetActive(false);
        }
    }
}
