using Ships;
using System;
using UnityEngine;

namespace Radio
{
    public class RadioStation : MonoBehaviour
    {
        public enum States { TURNED_OFF, RECEIVING_MODE, DISPATCHING_MODE }
        private States state = States.TURNED_OFF;
        private int statesNumber;
        private PopupComponent popupComponent;

        public delegate void radioEventDelegate(RadioMessage message);
        public event radioEventDelegate RadioMessageEvent;

        public delegate void StateSwitched(States newState);
        public event StateSwitched StateSwitchedEvent;

        public void Start()
        {
            statesNumber = Enum.GetNames(typeof(States)).Length;
            popupComponent = GetComponent<PopupComponent>();
        }

        public void MessageFromTheShip(ShipModel ship)
        {
            if (state == States.RECEIVING_MODE)
            {
                Vector3 deltaVector = transform.position - ship.position;
                float distance = deltaVector.magnitude;
                ShowPopUp("Message received. \nFrom: " + ship.name + "\ndistance: " + distance);
            }
        }

        public void SendRadioMessage(string message)
        {
            if (state == States.DISPATCHING_MODE)
            {
                SendMessage(message, Time.time);
                ShowPopUp("New message sent.");
            }
        }

        public void SwitchState()
        {
            state++;
            if ((int)state >= statesNumber) state = 0;
            Debug.Log("state: " + state);
            if (StateSwitchedEvent != null) StateSwitchedEvent(state);
        }

        public void SendYourState()
        {
            if (StateSwitchedEvent != null) StateSwitchedEvent(state);
        }

        private void SendMessage(string text, float time)
        {
            RadioMessage message = new RadioMessage();
            message.message = text;
            message.time = time;
            if (RadioMessageEvent != null) RadioMessageEvent(message);
        }

        private void ShowPopUp(string text)
        {
            popupComponent.AddNewPopup(text);
        }
    }
}
