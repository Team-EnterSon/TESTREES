﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTREES.Tables;
using UnityEngine;
using UnityEngine.Networking;

namespace TESTREES.GamePlay
{
	public class Pawn : NetworkBehaviour
	{
		public uint OwnerUID { get; private set; }

		public static class Factory
		{
			public static Pawn Create()
			{
				return null;
			}
		}
	}
}