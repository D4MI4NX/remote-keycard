using Exiled.API.Features;

namespace RemoteKeycard
{
    public class RemoteKeycard : Plugin<Config.Config>
    {
        /// <inheritdoc>
        public override string Name => "Remote Keycard";
    
        /// <inheritdoc>
        public override string Prefix => "remote_keycard";
        
        /// <inheritdoc>
        public override string Author => "D4MI4NX";
        
        /// <inheritdoc>
        public override Version Version => new(0, 1, 0);
        
        /// <inheritdoc>
        public override Version RequiredExiledVersion => new(8, 9, 11);


        public GenHandler Handler { get; private set; }

        public override void OnEnabled()
        {
            Handler = new GenHandler(this);
            Handler.Start();

            base.OnEnabled();
        }
        
        /// <inheritdoc>
        public override void OnDisabled()
        {
            Handler?.Stop();

            base.OnDisabled();
        }
    }
}
