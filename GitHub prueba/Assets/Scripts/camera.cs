using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] Transform follow;
    [SerializeField] Transform activeRoom;

    public static camera instance;

    [Range(-5,5)]
    public float minModX, maxModX, minModY, maxModY;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var minPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.min.y + minModY;
        var maxPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.max.y + maxModY;
        var minPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.min.x + minModX;
        var maxPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.max.x + maxModX;

        Vector3 clampedPos = new Vector3(
            Mathf.Clamp(follow.position.x, minPosX, maxPosX),
            Mathf.Clamp(follow.position.y, minPosY, maxPosY),
            Mathf.Clamp(follow.position.z, -20f, -20f)
            );

        transform.position = new Vector3(clampedPos.x, clampedPos.y, clampedPos.z);
    }
}
