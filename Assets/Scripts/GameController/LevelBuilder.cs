using UnityEngine;
using System.Collections.Generic;

public class LevelBuilder : MonoBehaviour {
    private ObjectPool startPlatforms;
    private ObjectPool middlePlatforms;
    private ObjectPool endPlatforms;

    private List<GameObject> spawnedPlatforms;

    private float tileWidth;
    private float lastTile;
    private float emptyTile;
    private int lastHeight = 0;

    private Transform player;

    public GameObject startPlatformObject;
    public GameObject middlePlatformObject;
    public GameObject endPlatformObject;

    public float cameraRange = 6f;

    void Start () {
        startPlatforms = new ObjectPool(startPlatformObject, 5);
        middlePlatforms = new ObjectPool(middlePlatformObject, 30);
        endPlatforms = new ObjectPool(endPlatformObject, 5);

        spawnedPlatforms = new List<GameObject>();

        player = GameObject.FindGameObjectWithTag(Const.Tags.Player).transform;

        tileWidth = startPlatformObject.GetComponent<SpriteRenderer>().GetComponent<Renderer>().bounds.size.x;
        emptyTile = tileWidth * 1.5f;

        // Faz o chão inicial
        while (lastTile < cameraRange * 2)
        {
            AddPlatform(middlePlatforms.Get(), 0);
        }
        lastTile += emptyTile;
	}

    // Update is called once per frame
	void Update () {
        if (player.transform.position.x + cameraRange > lastTile)
        {
            Generate();
        }

        while(spawnedPlatforms.Count > 0)
        {
            if ((spawnedPlatforms[0].transform.position.x + tileWidth) < player.transform.position.x - cameraRange)
            {
                spawnedPlatforms[0].SetActive(false);
                spawnedPlatforms.RemoveAt(0);
            }
            else
            {
                break;
            }
        }
    }

    private void Generate()
    {
        int height = 1;// Random.Range(0, lastHeight + 1);
        int width = Random.Range(0, 5);
        
        AddPlatform(startPlatforms.Get(), height);
        for(int i = 0; i < width; i++)
            AddPlatform(middlePlatforms.Get(), height);
        AddPlatform(endPlatforms.Get(), height);

        lastHeight = height;
        lastTile += emptyTile;
    }

    private void AddPlatform(GameObject p, int height)
    {
        Debug.Log(height);
        p.transform.position = new Vector3(lastTile, height, 0f);
        p.transform.rotation = new Quaternion(0, 0, 0, 0);
        p.SetActive(true);
        spawnedPlatforms.Add(p);

        lastTile += tileWidth;
    }
}
