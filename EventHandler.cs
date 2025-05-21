using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using RemoteKeycard.Extensions;
using Players = Exiled.Events.Handlers.Player;
using Interactables.Interobjects.DoorUtils;
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
            if (ev.Door.IsLocked || ev.Door.RequiredPermissions == 0)
            {
                return;
            }

            if (ev.Player.HasPermissionFor(ev.Door.RequiredPermissions))
            {
                if (p.Config.Debug)
                    {
                        Log.Info(string.Format("Opening door {0}", ev.Door.Name));
                    }
                    ev.IsAllowed = true;
            }
            else
            {
                if (p.Config.Debug)
                {
                    Log.Info(string.Format("NOT Opening door {0}", ev.Door.Name));
                }
            }
        }

        public void OnLockerInteraction(InteractingLockerEventArgs ev)
        {
            if (ev.Player.HasPermissionFor((DoorPermissionFlags)ev.InteractingChamber.RequiredPermissions))
            {
                if (p.Config.Debug)
                {
                    Log.Info(string.Format("Opening locker {0}", ev.InteractingChamber.Locker.Room.Name));
                }
                ev.IsAllowed = true;
            }
            else
            {
                if (p.Config.Debug)
                {
                    Log.Info(string.Format("NOT Opening locker {0}", ev.InteractingChamber.Locker.Room.Name));
                }
            }
        }

        public void OnGeneratorInteraction(UnlockingGeneratorEventArgs ev)
        {
            if (ev.Player.HasPermissionFor((DoorPermissionFlags)ev.Generator.KeycardPermissions))
            {
                if (p.Config.Debug)
                {
                    Log.Info(string.Format("Unlocking generator in {0}", ev.Generator.Room.Name));
                }
                ev.IsAllowed = true;
            }
            else
            {
                if (p.Config.Debug)
                {
                    Log.Info(string.Format("NOT unlocking generator in {0}", ev.Generator.Room.Name));
                }
            }
        }

        public void OnWarheadInteraction(ActivatingWarheadPanelEventArgs ev)
        {
            if (ev.Player.HasPermissionFor((DoorPermissionFlags)KeycardPermissions.AlphaWarhead))
            {
                if (p.Config.Debug)
                {
                    Log.Info("Unlocking alpha warhead");
                }
                ev.IsAllowed = true;
            }
            else
            {
                if (p.Config.Debug)
                {
                    Log.Info("NOT unlocking alpha warhead");
                }
            }
        }
    }
}
