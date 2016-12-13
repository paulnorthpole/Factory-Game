using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour {
    public int level;
    public int minimumLoadTime = 2;
    AsyncOperation async;

    public void Start()
    {
        StartCoroutine("load");
    }

    IEnumerator load()
    {
        
        async = SceneManager.LoadSceneAsync(level);
        async.allowSceneActivation = false;
        yield return async;
    }
    public void Update()
    {
        if ((async.progress >= 0.89) && (Time.time > minimumLoadTime))
        {
            async.allowSceneActivation = true;
        }
    }
}
