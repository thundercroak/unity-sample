using Ships;
using UnityEngine;

namespace Radio
{
    public class RadioStation : MonoBehaviour
    {
        private enum States { TURNED_OFF, RECEIVING_MODE, DISPATCHING_MODE }

        public delegate void radioEventDelegate(RadioMessage message);
        public event radioEventDelegate RadioEvent;

        public void Start()
        {

        }

        public void MessageFromTheShip(ShipModel ship)
        {
            Vector3 deltaVector = transform.position - ship.position;
            float distance = deltaVector.magnitude;
            ShowPopUp(ship.name + " distance: " + distance);
        }

        private void SendMessage(string text, float time)
        {
            RadioMessage message = new RadioMessage();
            RadioEvent(message);
        }

        private void ShowPopUp(string text)
        {

        }
    }
}
