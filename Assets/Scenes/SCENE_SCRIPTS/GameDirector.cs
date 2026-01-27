using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance; // Easy way for other scripts to find this

    [Header("Scene Flow")]
    [SerializeField] private List<string> sceneOrder = new List<string>();
    private int currentSceneIndex = 0;

    [Header("Stored Data")]
    public List<int> savedValuesFromSession = new List<int>();

    void Awake()
    {
        // Singleton pattern: ensures only one "Brain" exists
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // Add this inside the class, under your Awake() method
    void Start()
    {
        // This triggers the very first scene in your list (Index 0) 
        // as soon as the MasterScene starts up.
        if (sceneOrder.Count > 0)
        {
            LoadNextScene();
        }
        else
        {
            Debug.LogError("The Scene Order list is empty on the Game Director!");
        }
    }

    public void LoadNextScene()
    {
        if (currentSceneIndex < sceneOrder.Count)
        {
            string sceneToLoad = sceneOrder[currentSceneIndex];
            SceneManager.LoadScene(sceneToLoad);
            currentSceneIndex++;

            // CHECK: If we just loaded the very last scene in the list
            if (currentSceneIndex == sceneOrder.Count)
            {
                Debug.Log("--- FINAL SESSION RESULTS ---");
                Debug.Log("Full List: " + string.Join("s, ", savedValuesFromSession) + "s");
                Debug.Log("-----------------------------");
            }
    }
        else
        {
            Debug.Log("No more scenes in the list!");
        }
    }

    // Add this inside the GameDirector class
    public bool IsLastScene()
    {
        // If the next index is equal to or greater than the count, we are at the end
        return currentSceneIndex >= sceneOrder.Count;
    }
}