using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    public int playerATK = 1;
    public GameController gameController;
    private bool fight = false;
    public Fish fish;

    void Start()
    {
        gameController.FindFish += CanFight;  
    }
    void Update()
    {
        if(fight == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                fish.kill();
            }
        }
    }

    private void CanFight()
    {
        fight = true;
    }
}
