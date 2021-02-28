using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameSceneSO init_scene = default;
    [SerializeField] private GameSceneSO game_play_scene = default;

    [SerializeField] private GameSceneSO[] main_menu_scene = default;
 

    [Header("Load Event")]
    [SerializeField] private LoadSceneEventSO load_event = default;

    private List<Scene> sceneToUnLoad = new List<Scene>();

    private GameSceneSO currentScene;

    private void OnEnable() 
    {
        load_event.LoadRequest += LoadScene;
    }

    private void OnDisable() 
    {
        load_event.LoadRequest -= LoadScene;
    }

    public void Start() 
    {
        if(SceneManager.GetActiveScene().name == init_scene.SceneName.ToString())
        {
            LoadMainMenu();
        }
    }


    private void LoadMainMenu()
    {
        LoadScene(main_menu_scene, false);
    }




    private void LoadScene(GameSceneSO[] sceneToLoad, bool showLoadingProgress)
    {
        AddSceneToUnLoad();

        currentScene = sceneToLoad[0];

        for(int i=0; i < sceneToLoad.Length; i++)
        {
            if(!CheckLoadState(sceneToLoad[i].SceneName.ToString()))
            {
                SceneManager.LoadScene(sceneToLoad[i].SceneName.ToString(), LoadSceneMode.Additive);
            }
        }

        UnLoadScene();
    }

    private void AddSceneToUnLoad()
    {
        for(int i=0; i<SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if(scene.name != init_scene.SceneName.ToString() && scene.name != game_play_scene.name.ToString())
            {
                sceneToUnLoad.Add(scene);
            }
        }
    }

    private void UnLoadScene()
    {
        if(sceneToUnLoad != null)
        {
            for(int i=0; i<sceneToUnLoad.Count; i++)
            {
                SceneManager.UnloadSceneAsync(sceneToUnLoad[i]);
            }
        }

        sceneToUnLoad.Clear();
    }

    // This function checks if a scene is already loaded
    private bool CheckLoadState(string sceneName)
    {
        for(int i=0; i < SceneManager.sceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).name == sceneName)
            {
                return true;
            }
        }

        return false;
    }
    
    
    public void OnExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game !");
    }
}
