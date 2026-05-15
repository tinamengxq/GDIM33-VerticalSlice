using UnityEngine;
using Unity.VisualScripting;

public class WallGetO2 : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //GameController.Instance.IncreaseO2();
            EventBus.Trigger(EventNames.IncreaseOxygen, GameController.Instance);
        }
    }
}
