using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private float checkpointX;
    private float checkpointY;

     void Start()
    {
        if(PlayerPrefs.GetFloat("checkpointX") != 0)
        {
            transform.position = (new Vector2(PlayerPrefs.GetFloat("checkpointX"),PlayerPrefs.GetFloat("checkpointY")));
        }
        
    }

    public void checkpointAlcanzado(float x, float y)
    {
        PlayerPrefs.SetFloat("checkpointX", x);
        PlayerPrefs.SetFloat("checkpointY", y);
    }
}
