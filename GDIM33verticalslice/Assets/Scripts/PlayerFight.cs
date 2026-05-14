using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    public int playerATK = 1;
    public GameController gameController;
    private bool fight = false;
    public Fish fish;
    [SerializeField] private GameObject hurtUI;
    [SerializeField] private float attackDistance;

    void Start()
    {
        gameController.FindFish += CanFight;  
        hurtUI.SetActive(false);
    }
    [SerializeField] private float interval = 1f;
    private float newTime;
    void Update()
    {
        if(fight == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RayCasting();
            }
        }

        if (hurtUI)
        {
            newTime -= Time.deltaTime;
            if (newTime <= 0f)
            {
                hurtUI.SetActive(false);
            }
        }
    }

    private void CanFight()
    {
        fight = true;
        Debug.Log("can fight");
    }

    private void RayCasting()
    {
        RaycastHit raycastHit;
        Vector3 rayDirection = transform.forward;
        Vector3 rayOrigin = transform.position;

        if (Physics.Raycast(rayOrigin, rayDirection, out raycastHit, attackDistance))
        {
            Fish fish = raycastHit.collider.GetComponent<Fish>();
            if (fish.die != true && fish != null)
            {
                fish.kill();
                Debug.Log("Fish life -=1");
                hurtUI.SetActive(true);
                newTime = interval;
            }
            else
            {
                Debug.Log("wrong direction");
            }
        }
        
    }
}
