using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public GameObject Platform;
    public GameObject particlePrefab;

    public float speed;
    float currentSpeed;
    public float slodownFactor;

    bool isCounterUsable;


    Vector2 firstMousePosition;
    public float rotateFactor;
    float rotateDistance;

    float tempTime;
    float remainingTime;
    private void Update()
    {
        if (!GameManager.instance.isGameRunning)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            firstMousePosition = Input.mousePosition;
            if (isCounterUsable)
            {
                tempTime = Time.time;
                GameSceneManager.instance.counterText.gameObject.SetActive(true);
                currentSpeed = speed / 6;
                //isCounterUsable = false;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (remainingTime - (Time.time - tempTime) >= 0)
            {
                remainingTime = (remainingTime - (Time.time - tempTime));

                GameSceneManager.instance.counterText.text = remainingTime.ToString("00.00");
                currentSpeed = Mathf.Lerp(currentSpeed, speed, 0.001f);
            }
            else
            {
                GameSceneManager.instance.counterText.gameObject.SetActive(false);
                currentSpeed = Mathf.Lerp(currentSpeed, speed, 0.001f);
                //currentSpeed = speed;
            }
            rotateDistance = Input.mousePosition.x - firstMousePosition.x;
            Platform.transform.Rotate(0f, 0f, rotateDistance * rotateFactor);
        }
        else
        {
            currentSpeed = speed;
            if (rotateDistance > 0.3f)
            {
                rotateDistance = Mathf.Lerp(rotateDistance, 0f, 0.025f);
                Platform.transform.Rotate(0f, 0f, rotateDistance * rotateFactor);
            }
            else if (rotateDistance < -0.3f)
            {
                rotateDistance = Mathf.Lerp(rotateDistance, 0f, 0.025f);
                Platform.transform.Rotate(0f, 0f, rotateDistance * rotateFactor);
            }
            GameSceneManager.instance.counterText.gameObject.SetActive(false);
        }
    }

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

    private void Start()
    {
        temp1Color = UnityEngine.Random.Range(0, ObjectPoolManager.instance.ObstacleMaterialList.Count);
        transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material = ObjectPoolManager.instance.ObstacleMaterialList[temp1Color];
        temp2Color = UnityEngine.Random.Range(0, ObjectPoolManager.instance.ObstacleMaterialList.Count);
        transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material = ObjectPoolManager.instance.ObstacleMaterialList[temp2Color];

        isCounterUsable = true;
        remainingTime = GameManager.instance.counterTime;
        GameManager.instance.currentColor = UnityEngine.Random.Range(0, ObjectPoolManager.instance.ObstacleMaterialList.Count);
        GetComponent<Renderer>().material = ObjectPoolManager.instance.ObstacleMaterialList[GameManager.instance.currentColor];
    }

    public IEnumerator Movement()
    {
        currentSpeed = speed;
        while (GameManager.instance.isGameRunning)
        {
            transform.Translate(Vector3.forward * currentSpeed);

            yield return new WaitForFixedUpdate();
        }
    }

    int trueAnswer;
    int falseAnswer;
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "TrueObstacle")
        {
            foreach (Transform item in other.transform.parent.transform)
            {
                item.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        else if (other.tag == "Obstacle")
        {
            GameManager.instance.isGameRunning = false;
            GameSceneManager.instance.restartButton.SetActive(true);
            //currentSpeed = 0;
        }

        //if (other.tag == "Obstacle" && other.GetComponent<ObstaclePiece>().ColorIndex == GameManager.instance.currentColor)
        //{
        //    Debug.Log(other.GetComponent<ObstaclePiece>().ColorIndex + " : " + GameManager.instance.currentColor);
        //    trueAnswer++;
        //    GameSceneManager.instance.scoreText.text = trueAnswer + " / " + falseAnswer;
        //    GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;

        //    GameObject go = Instantiate(particlePrefab);
        //    go.transform.position = transform.position + (Vector3.forward * 50f);

        //    SetNewColor();
        //    remainingTime += GameManager.instance.counterTime;
        //    isCounterUsable = true;
        //}
        //else if (other.tag == "Obstacle")
        //{
        //    SceneManager.LoadScene("SampleScene");
        //    falseAnswer++;
        //    GameSceneManager.instance.scoreText.text = trueAnswer + " / " + falseAnswer;
        //    remainingTime += GameManager.instance.counterTime;
        //    isCounterUsable = true;
        //}
    }


    int temp1Color;
    int temp2Color;
    public void SetNewColor()
    {
        GameManager.instance.currentColor = temp1Color;
        GetComponent<Renderer>().material = ObjectPoolManager.instance.ObstacleMaterialList[GameManager.instance.currentColor];

        temp1Color = temp2Color;
        transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material = ObjectPoolManager.instance.ObstacleMaterialList[temp1Color];

        temp2Color = UnityEngine.Random.Range(0, ObjectPoolManager.instance.ObstacleMaterialList.Count);
        transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material = ObjectPoolManager.instance.ObstacleMaterialList[temp2Color];
    }
}
