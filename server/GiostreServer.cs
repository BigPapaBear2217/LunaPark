using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
 
namespace NuovaGM.Server.Veicoli
{
	static class ParkServer
	{
		public static void Init()
		{
			Server.Instance.AddEventHandler("FerrisWheel:syncState", new Action<Player, string, int>(SyncRuotaPan));
			Server.Instance.AddEventHandler("FerrisWheel:StopWheel", new Action<bool>(FermaRuota));
			Server.Instance.AddEventHandler("FerrisWheel:UpdateCabins", new Action<int, int>(UpdateCabins));
			Server.Instance.AddEventHandler("FerrisWheel:playerGetOff", new Action<Player, int, int>(RuotaGetOff));
			Server.Instance.AddEventHandler("FerrisWheel:playerGetOn", new Action<Player, int, int>(RuotaGetOn));
			Server.Instance.AddEventHandler("FerrisWheel:updateGradient", new Action<Player, int>(UpdateGradient));
			Server.Instance.AddEventHandler("RollerCoaster:playerGetOff", new Action<Player, int>(MontagneGetOff));
			Server.Instance.AddEventHandler("RollerCoaster:playerGetOn", new Action<Player, int, int, int>(MontagneGetOn));
			Server.Instance.AddEventHandler("RollerCoaster:syncState", new Action<Player, string>(SyncMontagne));
			Server.Instance.AddEventHandler("RollerCoaster:SyncCars", new Action<int, int>(SyncCars));
		}

		private static void UpdateGradient([FromSource] Player player, int gradient)
		{
			if (Server.Instance.GetPlayers.ToList().OrderBy(x => x.Handle).FirstOrDefault() == player)
			{
				BaseScript.TriggerClientEvent("FerrisWheel:UpdateGradient", gradient);
			}
		}

		public static void SyncRuotaPan([FromSource] Player p, string state, int Player)
		{
			if (Server.Instance.GetPlayers.ToList().OrderBy(x => x.Handle).FirstOrDefault() == p)
			{
				BaseScript.TriggerClientEvent("FerrisWheel:forceState", state);
			}
		}

		public static void SyncMontagne([FromSource] Player p, string state)
		{
			if(Server.Instance.GetPlayers.ToList().OrderBy(x => x.Handle).FirstOrDefault() == p)
			{
				BaseScript.TriggerClientEvent("RollerCoaster:forceState", state);
			}
		}

		public static void UpdateCabins(int cabin, int player) => BaseScript.TriggerClientEvent("FerrisWheel:UpdateCabins", cabin, player);

		public static void FermaRuota(bool stato) => BaseScript.TriggerClientEvent("FerrisWheel:FermaRuota", stato);

		public static void RuotaGetOn([FromSource] Player p, int player, int cabin) => BaseScript.TriggerClientEvent("FerrisWheel:playerGetOn", player, cabin);

		public static void RuotaGetOff([FromSource] Player p, int player, int cabin) => BaseScript.TriggerClientEvent("FerrisWheel:playerGetOff", player, cabin);

		public static void MontagneGetOn([FromSource] Player p, int player, int index, int carrello) => BaseScript.TriggerClientEvent("RollerCoaster:playerGetOn", player, index, carrello);

		public static void MontagneGetOff([FromSource] Player p, int player) => BaseScript.TriggerClientEvent("RollerCoaster:playerGetOff", player);

		public static void SyncCars(int Carrello, int Occupato) => BaseScript.TriggerClientEvent("RollerCoaster:SyncCars", Carrello, Occupato);
	}
}
