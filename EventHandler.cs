using System.Linq;
using Exiled.API;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using Exiled.Permissions.Extensions;
using UnityEngine;
using Players = Exiled.Events.Handlers.Player;

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
        }

        public void Stop()
        {
            
        }

        public void OnDoorInteraction(InteractingDoorEventArgs ev)
        {
            foreach (Item i in ev.Player.Items.ToArray())
            {
                if (!i.IsKeycard)
                {
                    continue;
                }

                var keycard = (Keycard)i;

                if (p.Config.Debug)
                {
                    Log.Info(string.Format("Door: {0}", ev.Door.RequiredPermissions.RequiredPermissions));
                    Log.Info(string.Format("Perm: {0}", ((int)ev.Door.KeycardPermissions)));
                    Log.Info(string.Format("Card: {0}", keycard.Permissions));
                    Log.Info(string.Format("Perm: {0}", ((int)keycard.Permissions)));
                }
                

                int doorPerm = ((int)ev.Door.KeycardPermissions);
                int cardPerm = ((int)keycard.Permissions);

                if ((doorPerm & cardPerm) != 0)
                {
                    ev.Door.IsOpen = !ev.Door.IsOpen;
                    return;
                }
            }
        }
    }
}
