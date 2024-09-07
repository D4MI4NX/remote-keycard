using Exiled.API.Features.Items;
using Exiled.API.Features.Doors;

namespace RemoteKeycard.Utils
{
    public class RemoteKeycard
    {
        public static bool KeycardHasPermissionForDoor(Keycard card, Door door)
        {
            int doorPerm = (int)door.KeycardPermissions;
            int cardPerm = (int)card.Permissions;

            return (doorPerm & cardPerm) != 0;
        }
    }
    
}