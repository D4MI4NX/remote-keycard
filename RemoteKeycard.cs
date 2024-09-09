using Exiled.API.Features;

namespace RemoteKeycard
{
    public class RemoteKeycard : Plugin<Config.Config>
    {
        public override string Name => "Remote Keycard";
    
        public override string Prefix => "remote_keycard";
        
        public override string Author => "D4MI4NX";
        
        public override Version Version => new(1, 0, 1);
        
        public override Version RequiredExiledVersion => new(8, 9, 11);


        public GenHandler Handler { get; private set; }

        public override void OnEnabled()
        {
            Handler = new GenHandler(this);
            Handler.Start();

            base.OnEnabled();
        }
        
        public override void OnDisabled()
        {
            Handler?.Stop();

            base.OnDisabled();
        }
    }
}
