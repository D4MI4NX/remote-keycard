using Exiled.API.Features;
using Exiled.API.Features.Items;
using Interactables.Interobjects.DoorUtils;

namespace RemoteKeycard.Extensions
{
    public static class PlayerExtensions
    {
        public static bool HasPermissionFor(this Player player, DoorPermissionFlags dp)
        {
            int doorPermInt = (int)dp;

            foreach (Item i in player.Items.ToArray())
            {
                if (!i.IsKeycard)
                {
                    continue;
                }

                var keycard = (Keycard)i;

                int keyPerm = (int)keycard.Permissions;

                if ((doorPermInt & keyPerm) == doorPermInt)
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