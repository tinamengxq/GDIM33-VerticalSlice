using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGetO2 : MonoBehaviour
{
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameController.Instance.IncreaseO2();
        }
    }
}
