using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace TwojaStaraXD.Handlers
{
    class Player
    {
        private Dictionary<Exiled.API.Features.Player, int> kille = new Dictionary<Exiled.API.Features.Player, int>();
        public void OnMedical(UsedMedicalItemEventArgs e)
        {
            e.Player.Health = 500;
            Map.Broadcast(3,$"{e.Player.Nickname} Uzył {e.Item} jak mała pizda"); // wyświetla wiadomość dla całego serwera
            e.Player.Broadcast(10,"Jesteś pizdą"); // wyswietla wiadomość dla danego gracza
            e.Player.ShowHint("Twoja stara", 17f); // wyświetla wiadomość dla gracza na środku ekranu hyba
            
            Log.Info($"{e.Player.Nickname} użył przedmiotu medycznego o nazwie {e.Item}");
        }
        public void OnDie(DiedEventArgs e)
        {
            if (kille.ContainsKey(e.Killer))
            {
                kille[e.Killer]++;
            }
            else
            {
                kille.Add(e.Killer,1);
            }
        }
        public void OnEnd(RoundEndedEventArgs e)
        {
            int k = 0;
            string nick = "Brak Gracza";
            foreach (var item in kille)
            {
                if (item.Value > k)
                {
                    k = item.Value;
                    nick = item.Key.Nickname;
                }
            }
           


            Map.Broadcast(16, $"Gracz {nick} zdobył najwięcej zabujstw: {k}");
        }
    }
}
