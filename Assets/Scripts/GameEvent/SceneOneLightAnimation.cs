using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SceneOneLightAnimation : MonoBehaviour
{
    [SerializeField] private VoidGameEventSO lightEvent;
 
    [SerializeField] private GameObject lightBG;
    [SerializeField] private GameObject blackBG;
 
    private Light2D globalLight;

    private Animator ani;

    private void Start() 
    {
        ani = GetComponent<Animator>();
        globalLight = GetComponent<Light2D>();
    }

    private void OnEnable()
    {
        lightEvent.eventRaised += PlayAnimation;
    }

    private void OnDisable()
    {
        lightEvent.eventRaised -= PlayAnimation;
    }

    private void PlayAnimation()
    {
        ani.SetTrigger("LightToDark");
    }

    private void SwitchToDarkBackGround()
    {
        lightBG.SetActive(false);
        blackBG.SetActive(true);
        globalLight.intensity = 0.65f;
        Destroy(ani);
    }
}
