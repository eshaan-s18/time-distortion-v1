using UnityEngine;
using System.Collections;

public class SceneTimer : MonoBehaviour
{
    [SerializeField] private float waitTime = 15f;

    void Start()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(waitTime);

        // Tell the Brain to move to the next scene
        if (GameDirector.Instance != null)
        {
            GameDirector.Instance.LoadNextScene();
        }
    }
}