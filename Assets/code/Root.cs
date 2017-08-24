using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ships;
using Radio;
using GUI;
using RootStuff;

public class Root : MonoBehaviour
{
    private RadioStation radioController;
    private AbstractShip[] shipControllers;
    private GUIController guiController;
    private InputController inputController;

    public void Start()
    {
        InitShips();
        InitRadio();
        InitGUI();
        radioController.SendYourState();
        inputController = new InputController();
        inputController.InputEvent += InputEventHandler;
    }

    private void InputEventHandler(RaycastHit[] hits, InputController.EventTypes type)
    {
        int hitsLength = hits.Length;
        for (int i = 0; i < hitsLength; i++)
        {
            RaycastHit hit = hits[i];
            GameObject go = hit.collider.gameObject;
            if (go.tag == "Ship")
            {
                AbstractShip ship = go.GetComponent<AbstractShip>();
                ship.ShipClickHandler();
            }
        }
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
        radioController.RadioMessageEvent += RadioEventHandler;
        radioController.StateSwitchedEvent += StateSwitchedEventHandler;
    }

    private void InitGUI()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        guiController = canvas.GetComponent<GUIController>();
        guiController.MessageSendEvent += MessageSendHandler;
        guiController.SwitchRadioModeEvent += SwitchRadioModeHandler;
    }

    public void Update()
    {
        inputController.Update();
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

    private void StateSwitchedEventHandler(RadioStation.States newState)
    {
        guiController.StateSwitched(newState);
    }

    private void MessageSendHandler(string text)
    {
        radioController.SendRadioMessage(text);
    }

    private void SwitchRadioModeHandler()
    {
        radioController.SwitchState();
    }
}
