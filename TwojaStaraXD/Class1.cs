using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;

namespace TwojaStaraXD
{
    public class PluginBoNieInaczejAleJednakCzemuByNieInaczejKolego : Plugin<Config>
    {
        public static readonly Lazy<PluginBoNieInaczejAleJednakCzemuByNieInaczejKolego> LI = new Lazy<PluginBoNieInaczejAleJednakCzemuByNieInaczejKolego>(valueFactory: () => new PluginBoNieInaczejAleJednakCzemuByNieInaczejKolego());
        public PluginBoNieInaczejAleJednakCzemuByNieInaczejKolego Instance => LI.Value;

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        private Handlers.Player player;
        private Handlers.Server server;

        public override void OnEnabled()
        {
            RegisterEvents();
        }
        public override void OnDisabled()
        {
            UnRegisterEvents();
        }

        public void RegisterEvents() 
        {
            Environment.SetEnvironmentVariable("JEBAĆ", "wat");
            player = new Handlers.Player();
            server = new Handlers.Server();

            player.XD = Config.TwojaStara;


            Player.MedicalItemUsed += player.OnMedical;
          
            Player.UsingMedicalItem += player.MultiuseMedkit;
            Server.WaitingForPlayers += server.Start;
            Player.Died += player.OnDie;
            Server.RoundEnded += player.OnEnd;
            Player.ItemDropped += player.OnItemDropped;
        }
        public void UnRegisterEvents()
        {
            Player.MedicalItemUsed -= player.OnMedical;
            Player.UsingMedicalItem -= player.MultiuseMedkit;
            Server.WaitingForPlayers -= server.Start;
            Player.Died -= player.OnDie;
            Server.RoundEnded -= player.OnEnd;
            Player.ItemDropped -= player.OnItemDropped;
            player = null;
            server = null;
        }
    }
}
