using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    private float spriteWidth, starPosition;

    [Range (0f,1f)]
    [SerializeField] float ScrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        starPosition = transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * ScrollSpeed;
        float moveAmount = cameraTransform.position.x * (1 - ScrollSpeed);
        transform.Translate(new Vector3(deltaX, 0, 0));
        previousCameraPosition = cameraTransform.position;
        if (moveAmount > starPosition + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
            starPosition += spriteWidth;
        }
        else if (moveAmount < starPosition - spriteWidth)
        {
            transform.Translate(new Vector3(-spriteWidth, 0, 0));
            starPosition -= spriteWidth;
        }
    }
}
