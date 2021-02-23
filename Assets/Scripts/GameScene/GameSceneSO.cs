using UnityEngine;

public enum Type 
{
    Initialization, MainMenu, SceneOne, GamePlay
}

[CreateAssetMenu(fileName="GameScene", menuName="Game/GameScene")]
public class GameSceneSO : ScriptableObject
{
    public Type SceneName;
}
