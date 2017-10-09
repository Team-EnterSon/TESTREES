using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace TESTREES.GamePlay
{
	public class GameContext : NetworkBehaviour
	{
		[SyncVar]
		public int NumberOfPlayers;

	}
}
