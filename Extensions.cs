using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Items.Keycards;

namespace RemoteKeycard.Extensions
{
    public static class PlayerExtensions
    {
        public static bool HasPermissionFor(this Player player, ushort doorPerm, bool allowSingleUseKeycards)
        {
            if (doorPerm == 0) {
                return true;
            }

            foreach (Item i in player.Items.ToArray())
            {
                if (!i.IsKeycard)
                {
                    continue;
                }

                Keycard keycard = (Keycard)i;

                bool isSingleUseKeycard = keycard.Type == ItemType.SurfaceAccessPass;

                if (!allowSingleUseKeycards && isSingleUseKeycard) {
                    continue;
                }

                ushort keycardPerm = (ushort)keycard.Permissions;

                // Fixes
                keycardPerm |= 0b010000000000; // Checkpoints
                keycardPerm |= 0b100000000000; // Gates

                ushort result = (ushort)(doorPerm & keycardPerm);

                Log.Debug(string.Format("Required: {0} ({1}), got: {2} ({3}), result: {4} ({5})",
                    doorPerm,
                    Convert.ToString(doorPerm, 2),
                    keycardPerm,
                    Convert.ToString(keycardPerm, 2),
                    result,
                    Convert.ToString(result, 2)
                ));

                if (result == doorPerm)
                {
                    if (isSingleUseKeycard) {
                        SingleUseKeycard card = (SingleUseKeycard)keycard;
                        // TODO: implement AllowClosingDoors logic
                        card.Uses--;
                        Log.Debug($"Used single use keycard, now {card.Uses} uses");
                    }
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