using UnityEngine;


namespace Ships
{
    public class CargoShip : AbstractShip
    {
        public float cargoShipSpeed = 1.0f;

        protected override void Init()
        {
            base.Init();
            Setup("CargoShip", gameObject, cargoShipSpeed);
        }

    }
}
