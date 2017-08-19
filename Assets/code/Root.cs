using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ships;
using Radio;

public class Root : MonoBehaviour
{
    private RadioStation radioController;
    private AbstractShip[] shipControllers;

    public void Start()
    {
        InitShips();
        InitRadio();
    }

    private void InitShips()
    {
        GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
        shipControllers = new AbstractShip[ships.Length];

        for (int i = 0; i < ships.Length; i++)
        {
            AbstractShip shipController = ships[i].GetComponent<AbstractShip>();
            shipControllers[i] = shipController;
            shipController.ShipEvent += ShipEventHandler;
        }
    }

    private void InitRadio()
    {
        GameObject radioStation = GameObject.FindGameObjectWithTag("RadioStation");
        radioController = radioStation.GetComponent<RadioStation>();
        radioController.RadioEvent += RadioEventHandler;
    }

    public void Update()
    {
		
	}

    private void ShipEventHandler(ShipModel ship)
    {
        radioController.MessageFromTheShip(ship);
    }

    private void RadioEventHandler(RadioMessage message)
    {
        for (int i = 0; i < shipControllers.Length; i++)
        {
            shipControllers[i].MessageFromTheRadio(message);
        }
    }
}
