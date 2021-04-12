using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace TwojaStaraXD.Handlers
{
    class Player
    {
        public void OnMedical(UsedMedicalItemEventArgs e)
        {
            e.Player.Health = 500;
            
            Log.Info($"{e.Player.Nickname} użył przedmiotu medycznego");
        }
    }
}
