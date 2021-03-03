using UnityEngine;

public class StartGame : MonoBehaviour
{
    public LoadSceneEventSO loadSceneEvent;
    public GameSceneSO[] sceneToLoad;
    public bool ShowLoadingProgress;

    public void OnStartButtonPress()
    {
        loadSceneEvent.RaiseEvent(sceneToLoad, ShowLoadingProgress);
    }
}
