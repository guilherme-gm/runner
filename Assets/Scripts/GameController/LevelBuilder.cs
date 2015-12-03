using UnityEngine;
using System.Collections;

public class LevelBuilder : MonoBehaviour {
    private ObjectPool startPlatforms;
    private ObjectPool middlePlatforms;
    private ObjectPool endPlatforms;

    private float tileWidth;

    public GameObject startPlatformObject;
    public GameObject middlePlatformObject;
    public GameObject endPlatformObject;

    public float cameraRange = 6f;

    void Start () {
        startPlatforms = new ObjectPool(startPlatformObject, 5);
        middlePlatforms = new ObjectPool(middlePlatformObject, 30);
        endPlatforms = new ObjectPool(endPlatformObject, 5);

        tileWidth = startPlatformObject.GetComponent<SpriteRenderer>().GetComponent<Renderer>().bounds.size.x;
            //.renderer.bounds.size.x;

        // Faz o chão inicial
        float i = 0;
        while (i < cameraRange * 2)
        {
            GameObject p = middlePlatforms.Get();
            p.transform.position = new Vector3(i, 0f, 0f);
            p.transform.rotation = new Quaternion(0, 0, 0, 0);
            p.SetActive(true);

            i += tileWidth;
        }
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
