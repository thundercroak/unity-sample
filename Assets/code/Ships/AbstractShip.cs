using System;
using Radio;
using UnityEngine;

namespace Ships
{
    public abstract class AbstractShip : MonoBehaviour
    {
        public delegate void shipRadioEventDelegate(ShipModel ship);
        public event shipRadioEventDelegate ShipEvent;

        public ShipModel model;
        private GameObject view;
        private float speed;

        public void Start()
        {
            model = new ShipModel();
            Init();
        }

        protected virtual void Init()
        {

        }

        protected void Setup(string name, GameObject view, float speed)
        {
            model.name = name;
            this.view = view;
            this.speed = speed;
        }

        public void Update()
        {

        }

        public void MessageFromTheRadio(RadioMessage message)
        {
            ShowPopUp("message: " + message.message + " time: " + message.time);
        }

        private void ShowPopUp(string text)
        {

        }

        private void SendMessage()
        {
            ShipEvent(model);
        }
    }
}
