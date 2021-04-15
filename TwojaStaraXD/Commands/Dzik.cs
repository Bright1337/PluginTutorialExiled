using System;
using System.Threading.Tasks;
using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;

namespace TwojaStaraXD.Commands
{
    

    [CommandHandler(typeof(ClientCommandHandler))]
    class Dzik : ICommand
    {
        public string Command { get; } = "Zjeb";
        public string[] Aliases { get; } = new string[] {"xd","serio","zjebany" };
        public string Description { get; } = "Yes it does something.... BUT WOT ?";
        public bool Execute(ArraySegment<string> a, ICommandSender s, out string r)
        {
            
            if (s is PlayerCommandSender player)
            {
                r = "Jesteś dzbanem";
                Player.Get(player.SenderId).Kill(DamageTypes.Flying);
            }
            else
            {
                r = "Co ty kurwa konsole chcesz zajebać ?";
                Environment.SetEnvironmentVariable("JEBAĆ","JEBAĆ");
            }
            
            return true;
        }
    }
}
