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
        private PopupComponent popupComponent;
        private float speed;

        public void Start()
        {
            model = new ShipModel();
            popupComponent = GetComponent<PopupComponent>();
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
            model.position = view.transform.position;
        }

        public void MessageFromTheRadio(RadioMessage message)
        {
            ShowPopUp("message: " + message.message + "\ntime: " + message.time);
        }

        public void ShipClickHandler()
        {
            Debug.Log("S C H");
            SendMessage();
        }

        private void ShowPopUp(string text)
        {
            popupComponent.AddNewPopup(text);
        }

        private void SendMessage()
        {
            if (ShipEvent != null) ShipEvent(model);
        }
    }
}
