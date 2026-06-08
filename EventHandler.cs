using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using RemoteKeycard.Extensions;
using Players = Exiled.Events.Handlers.Player;
using Exiled.API.Enums;

namespace RemoteKeycard
{
    public class GenHandler
    {
        private readonly RemoteKeycard p;

        public GenHandler(RemoteKeycard pluginInstance)
        {
            p = pluginInstance;
        }

        public void Start()
        {
            if (p.Config.Doors)
            {
                Players.InteractingDoor += OnDoorInteraction;
            }
            if (p.Config.Lockers)
            {
                Players.InteractingLocker += OnLockerInteraction;
            }
            if (p.Config.Generators)
            {
                Players.UnlockingGenerator += OnGeneratorInteraction;
            }
            if (p.Config.Warhead)
            {
                Players.ActivatingWarheadPanel += OnWarheadInteraction;
            }
        }

        public void Stop()
        {
            if (p.Config.Doors)
            {
                Players.InteractingDoor -= OnDoorInteraction;
            }
            if (p.Config.Lockers)
            {
                Players.InteractingLocker -= OnLockerInteraction;
            }
            if (p.Config.Generators)
            {
                Players.UnlockingGenerator -= OnGeneratorInteraction;
            }
            if (p.Config.Warhead)
            {
                Players.ActivatingWarheadPanel -= OnWarheadInteraction;
            }
        }

        public void OnDoorInteraction(InteractingDoorEventArgs ev)
        {
            if (ev.Door.IsLocked)
            {
                return;
            }

            if (ev.Player.HasPermissionFor((ushort)ev.Door.RequiredPermissions))
            {
                ev.IsAllowed = true;
            }
            Log.Debug($"Open door {ev.Door.Name} status {ev.IsAllowed}");
        }

        public void OnLockerInteraction(InteractingLockerEventArgs ev)
        {
            if (ev.Player.HasPermissionFor((ushort)ev.InteractingChamber.RequiredPermissions))
            {
                ev.IsAllowed = true;
            }
            Log.Debug($"Open locker in {ev.InteractingChamber.Locker.Room.Name} status {ev.IsAllowed}");
        }

        public void OnGeneratorInteraction(UnlockingGeneratorEventArgs ev)
        {
            if (ev.Player.HasPermissionFor((ushort)ev.Generator.KeycardPermissions))
            {
                ev.IsAllowed = true;
            }
            Log.Debug($"Unlock generator in {ev.Generator.Room.Name} status {ev.IsAllowed}");
        }

        public void OnWarheadInteraction(ActivatingWarheadPanelEventArgs ev)
        {
            if (ev.Player.HasPermissionFor((ushort)KeycardPermissions.AlphaWarhead))
            {
                ev.IsAllowed = true;
            }
            Log.Debug($"Unlock alpha warhead status {ev.IsAllowed}");
        }
    }
}
