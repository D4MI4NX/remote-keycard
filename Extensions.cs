using Exiled.API.Features;
using Exiled.API.Features.Items;
using Interactables.Interobjects.DoorUtils;

namespace RemoteKeycard.Extensions
{
    public static class PlayerExtensions
    {
        public static bool HasPermissionFor(this Player player, KeycardPermissions kp)
        {
            int kpPerm = (int)kp;

            foreach (Item i in player.Items.ToArray())
            {
                if (!i.IsKeycard)
                {
                    continue;
                }

                var keycard = (Keycard)i;

                int keyPerm = ((int)keycard.Permissions);

                if (((int)kp & keyPerm) != 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsHoldingKeycard(this Player player)
        {
            return player.CurrentItem is Keycard;
        }
    }
}