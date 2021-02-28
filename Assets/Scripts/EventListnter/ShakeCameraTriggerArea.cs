using UnityEngine;


public class ShakeCameraTriggerArea : MonoBehaviour
{
    [SerializeField] private VoidGameEventSO lightEvent; // play light from light to dark animation
    [SerializeField] private CameraShakeEventAreaSO shakeEvent;

    [SerializeField] private InputReader inputReader;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            inputReader.DisablePlayer();
            lightEvent.Raise();
            shakeEvent.Raise(50f, 5f);
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        inputReader.EnablePlayer();
        Destroy(gameObject, 1.0f);
    }
}
