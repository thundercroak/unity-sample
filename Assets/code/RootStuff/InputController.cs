using UnityEngine;
using System.Collections;

namespace RootStuff
{
    public class InputController
    {
        public enum EventTypes { CLICK, RELEASE }
        public delegate void CLickResult(RaycastHit[] hits, EventTypes type);
        public event CLickResult InputEvent;

        private Camera camera;

        public InputController()
        {
            camera = Camera.main;
        }

        public void Update()
        {
            HandlePCInput();
        }

        private void HandlePCInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit[] hits = DoRaycast(Input.mousePosition);
                InputEvent(hits, EventTypes.CLICK);
            }
        }

        private RaycastHit[] DoRaycast(Vector3 position)
        {
            Ray ray = camera.ScreenPointToRay(position);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            return hits;
        }

    }
}

