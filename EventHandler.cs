using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
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
            if (ev.Player.CurrentItem is Keycard)
            {
                if (p.Config.Debug)
                {
                    Log.Info("Player is holding a card");
                }
                return;
            }

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
                
                if (Utils.RemoteKeycard.KeycardHasPermissionForDoor(keycard, ev.Door))
                {
                    if (p.Config.Debug)
                    {
                        Log.Info(string.Format("Opening door {0}", ev.Door.Name));
                    }
                    ev.Door.IsOpen = !ev.Door.IsOpen;
                    return;
                }
                else
                {
                    if (p.Config.Debug)
                    {
                        Log.Info(string.Format("NOT Opening door {0}", ev.Door.Name));
                    }
                }
            }
        }
    }
}
