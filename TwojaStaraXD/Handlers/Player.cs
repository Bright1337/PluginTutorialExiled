using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using UnityEngine;

namespace TwojaStaraXD.Handlers
{
    class Player
    {
        public string XD;
        private Dictionary<Exiled.API.Features.Player, int> kille = new Dictionary<Exiled.API.Features.Player, int>();
        public void OnMedical(UsedMedicalItemEventArgs e)
        {
            e.Player.Health = 500;
            Map.Broadcast(3,$"{e.Player.Nickname} Uzył {e.Item} jak mała pizda"); // wyświetla wiadomość dla całego serwera
            e.Player.Broadcast(10,"Jesteś pizdą"); // wyswietla wiadomość dla danego gracza
            e.Player.ShowHint(XD, 17f); // wyświetla wiadomość dla gracza na środku ekranu hyba
            
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




        public void OnItemDropped(ItemDroppedEventArgs e)
        {
            e.Player.ShowHint("Wypadło ci coś dzbanie");
            ItemType typ = e.Pickup.ItemId;
            if (typ == ItemType.MicroHID)
            {
                e.Pickup.durability = 0f;
                e.Player.ShowHint("Rozładował ci się HID dzbanie");
            }
            else if (typ == ItemType.Coin)
            {
                e.Player.IsBypassModeEnabled = true;
                e.Player.ShowHint("Magiczna moneta otwiera każde drzwi na twej drodze");
                e.Pickup.Delete();

            }
            else if (typ == ItemType.Adrenaline)
            {
                e.Pickup.SetIDFull(ItemType.SCP500);
                e.Player.ShowHint("Ale heca SCP-500");
                //
                //Tu zaczynamy Game Object
                //
                e.Pickup.gameObject.transform.localScale = new Vector3(3,3,3);
                e.Pickup.gameObject.name = "Twoja Stara";
                e.Pickup.gameObject.transform.localPosition += new Vector3(1.000134f,6.124f,1.234f);
                e.Pickup.gameObject.transform.localRotation = new Quaternion(90,180,45,0);
                
            }


        }
    }
}
