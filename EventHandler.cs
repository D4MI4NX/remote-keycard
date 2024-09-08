using Exiled.API.Features;
using Exiled.API.Features.Items;
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
            Players.InteractingDoor += OnDoorInteraction;
            Players.InteractingLocker += OnLockerInteraction;
        }

        public void Stop()
        {
            Players.InteractingDoor -= OnDoorInteraction;
            Players.InteractingLocker -= OnLockerInteraction;
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
                    Log.Info(string.Format("Opening door {0}", ev.Chamber.name));
                }
                ev.IsAllowed = true;
            }
            else
            {
                if (p.Config.Debug)
                {
                    Log.Info(string.Format("NOT Opening door {0}", ev.Chamber.name));
                }
            }
        }
    }
}
