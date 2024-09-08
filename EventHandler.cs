using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using RemoteKeycard.Extensions;
using Players = Exiled.Events.Handlers.Player;
using Interactables.Interobjects.DoorUtils;

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
            Players.InteractingDoor += OnDoorInteraction;
            Players.InteractingLocker += OnLockerInteraction;
            Players.UnlockingGenerator += OnGeneratorInteraction;
            Players.ActivatingWarheadPanel += OnWarheadInteraction;
        }

        public void Stop()
        {
            Players.InteractingDoor -= OnDoorInteraction;
            Players.InteractingLocker -= OnLockerInteraction;
            Players.UnlockingGenerator -= OnGeneratorInteraction;
            Players.ActivatingWarheadPanel -= OnWarheadInteraction;
        }

        public void OnDoorInteraction(InteractingDoorEventArgs ev)
        {
            if (ev.Door.IsLocked || ((int)ev.Door.RequiredPermissions.RequiredPermissions) == 0)
            {
                return;
            }

            if (ev.Player.HasPermissionFor(ev.Door.RequiredPermissions.RequiredPermissions))
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
            if (ev.Player.HasPermissionFor(ev.Chamber.RequiredPermissions))
            {
                if (p.Config.Debug)
                {
                    Log.Info(string.Format("Opening locker {0}", ev.Chamber.name));
                }
                ev.IsAllowed = true;
            }
            else
            {
                if (p.Config.Debug)
                {
                    Log.Info(string.Format("NOT Opening locker {0}", ev.Chamber.name));
                }
            }
        }

        public void OnGeneratorInteraction(UnlockingGeneratorEventArgs ev)
        {
            if (ev.Player.HasPermissionFor((KeycardPermissions)ev.Generator.KeycardPermissions))
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
            if (ev.Player.HasPermissionFor(KeycardPermissions.AlphaWarhead))
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
