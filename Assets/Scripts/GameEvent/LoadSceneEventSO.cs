using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName="LoadGameEvent", menuName="Game Event/Load Game")]
public class LoadSceneEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO[], bool> LoadRequest;

    public void RaiseEvent(GameSceneSO[] locationToLoad, bool showLoadingProgress)
    {
        LoadRequest?.Invoke(locationToLoad, showLoadingProgress);
    }
}
