using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string levelToLoad = LoadScene.nextLevel;
    }

    IEnumerator cargar(string level)
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation op = SceneManager.LoadSceneAsync(level);
        while (!op.isDone)
        {
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
