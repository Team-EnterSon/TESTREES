using EnterSon.GameStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTREES.Tables;
using UnityEngine;
using UnityEngine.Networking;

namespace TESTREES.GameStages
{
	public class GameStageTestLauncher : GameStage
	{
		public override void EnterStage()
		{
			base.EnterStage();

			DataTables.LoadTables();
		}
	}
}
