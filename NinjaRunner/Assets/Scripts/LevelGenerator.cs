using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> obsPrefabs;

    private float elapsedTime = 0;
    public int timeBetweenObs;

    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        CreateObs();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > timeBetweenObs && GameControl.instance.gameOver == false) {
            CreateObs();
            elapsedTime = 0;
        }
    }

    void CreateObs() {
        GameObject e = Instantiate(obsPrefabs[Random.Range(0, obsPrefabs.Count)]) as GameObject;
        e.transform.position = new Vector3(screenBounds.x * 2, 0, 0);
    }
}
