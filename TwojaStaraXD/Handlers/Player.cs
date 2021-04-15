using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using UnityEngine;

namespace TwojaStaraXD.Handlers
{
	public class Player
	{
		
		public string XD;
		private Dictionary<Exiled.API.Features.Player, int> players = new Dictionary<Exiled.API.Features.Player, int>();
		private Dictionary<Exiled.API.Features.Player, int> kille = new Dictionary<Exiled.API.Features.Player, int>();
		public void OnMedical(UsedMedicalItemEventArgs e)
		{
			e.Player.Health = 500;
			Map.Broadcast(3,$"{e.Player.Nickname} Uzył {e.Item} jak mała pizda"); // wyświetla wiadomość dla całego serwera
			e.Player.Broadcast(10,"Jesteś pizdą"); // wyswietla wiadomość dla danego gracza
			e.Player.ShowHint(XD, 17f); // wyświetla wiadomość dla gracza na środku ekranu hyba
			
			Log.Info($"{e.Player.Nickname} użył przedmiotu medycznego o nazwie {e.Item}");
		}
		public void MultiuseMedkit(UsingMedicalItemEventArgs e)
		{
			if (e.Item == ItemType.Medkit)
			{
				if (players.ContainsKey(e.Player))
				{
					if (players[e.Player] > 2)
					{
						e.IsAllowed = false;
						e.Player.ShowHint("Debilu wystarczy ci leczenia");
					}
					else
					{
						players[e.Player]++;
					}
				}
				else
				{
					players.Add(e.Player,1);
				}
			}
		}
		public void PickedUpItem(PickingUpItemEventArgs e)
		{
			if (Environment.GetEnvironmentVariable("JEBAĆ") == "JEBAĆ")
			{
				e.IsAllowed = false;
				return;
			}
			if (e.Pickup.itemId == ItemType.Medkit)
			{
				int i = 0;
				foreach (var item in e.Player.Inventory.items)
				{
					if (item.id == ItemType.Medkit)
					{
						i++;
					}

				}
				if (i > 0)
				{
					e.IsAllowed = false;
					e.Player.ShowHint("Złodzieju co tyle apteczek kradniesz?");
					return;
				}
				if (players.ContainsKey(e.Player))
				{
					if (players[e.Player] > 2)
					{
						e.IsAllowed = false;
						e.Player.ShowHint("Nie ma leczenia się cwelu");
					}
				}
			}
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
			 
			}



		}
	}
}
