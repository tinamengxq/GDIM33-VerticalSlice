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
                fish.kill();
                Debug.Log("Fish life -=1");
                hurtUI.SetActive(true);
                newTime = interval;
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
}
