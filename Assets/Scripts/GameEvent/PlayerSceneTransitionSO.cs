using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName="PlayerSceneTransitionEvent", menuName="Game Event/SceneTransition")]
public class PlayerSceneTransitionSO : ScriptableObject
{
    public UnityAction<Vector2> request;

    public void RaiseEvent(Vector2 position)
    {
        request?.Invoke(position);
    }
}
