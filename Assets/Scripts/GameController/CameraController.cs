using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Const.Tags.Player);
    }

    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        pos.x = player.transform.position.x;
        gameObject.transform.position = pos;
    }
}
