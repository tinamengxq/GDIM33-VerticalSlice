using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGetO2 : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.IncreaseO2();
        }
    }
}
