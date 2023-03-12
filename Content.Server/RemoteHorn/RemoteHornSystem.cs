using Content.Server.DeviceNetwork;
using Content.Server.DeviceNetwork.Components;
using Content.Server.DeviceNetwork.Systems;
using Content.Shared.Interaction.Events;
using Robust.Shared.Audio;
using Robust.Shared.Player;

namespace Content.Server.RemoteHorn
{

    public class RemoteHornSystem : EntitySystem
    {
        [Dependency] private readonly DeviceNetworkSystem _deviceNetworkSystem = default!;
        [Dependency] private readonly SharedAudioSystem _audio = default!;
        public override void Initialize()
        {
            base.Initialize();
            SubscribeLocalEvent<RemoteHornComponent, DeviceNetworkPacketEvent>(OnRecive);
            SubscribeLocalEvent<DeviceNetworkComponent, UseInHandEvent>(OnUseInHand);
        }

        private void OnRecive(EntityUid uid, RemoteHornComponent component, DeviceNetworkPacketEvent args)
        {
            if (args.Data.TryGetValue(DeviceNetworkConstants.Command, out string? command))
            {
                if (command == "Honk")
                    _audio.PlayPvs(component.Sound,uid);
            }
        }

        private void OnUseInHand(EntityUid uid, DeviceNetworkComponent component, UseInHandEvent args)
        {
            var payload = new NetworkPayload
            {
                [DeviceNetworkConstants.Command] = "Honk"
            };
            _deviceNetworkSystem.QueuePacket(uid,"0",payload,component.TransmitFrequency);
        }
    }
}
