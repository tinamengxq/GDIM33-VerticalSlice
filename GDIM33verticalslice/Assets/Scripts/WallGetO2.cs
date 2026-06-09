using UnityEngine;
using Unity.VisualScripting;

public class WallGetO2 : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //GameController.Instance.IncreaseO2();
            EventBus.Trigger(EventNames.IncreaseOxygen, GameController.Instance);
            //cameraController.ChangeCamera(0);
            //Debug.Log("Deactivate water effect");

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //cameraController.ChangeCamera(1);
            //Debug.Log("Active water effect");
        }
    }
}
