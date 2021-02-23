using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneNeededEditor : MonoBehaviour
{
    [SerializeField] private GameSceneSO init_scene;


    private void Awake() 
    {
        for(int i=0; i<SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if(scene.name == init_scene.SceneName.ToString())
                return;
        }

        SceneManager.LoadScene(init_scene.SceneName.ToString(), LoadSceneMode.Additive);
    }
}
