using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.gameObject.transform.position.z > transform.position.z + 500f)
        {
            gameObject.SetActive(false);
        }
    }
}
