using Exiled.API.Interfaces;

namespace RemoteKeycard.Config
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        public bool Doors { get; set; } = true;
        public bool Lockers { get; set; } = true;
        public bool Generators { get; set; } = true;
        public bool Warhead { get; set; } = true;
    }
}