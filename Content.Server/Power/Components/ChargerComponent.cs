using Content.Shared.Power;

namespace Content.Server.Power.Components
{
    [RegisterComponent]
    public sealed class ChargerComponent : Component
    {
        [ViewVariables]
        public CellChargerStatus Status;

        [DataField("chargeRate")]
        public float ChargeRate = 0;

        [DataField("slotId", required: true)]
        public string SlotId = string.Empty;
    }
}
