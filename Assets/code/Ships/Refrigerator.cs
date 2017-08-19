using UnityEngine;
using System.Collections;


namespace Ships
{
    public class Refrigerator : AbstractShip
    {
        public float RefrigeratorSpeed = 1.0f;

        protected override void Init()
        {
            base.Init();
            Setup("Refrigerator", gameObject, RefrigeratorSpeed);
        }

    }
}
