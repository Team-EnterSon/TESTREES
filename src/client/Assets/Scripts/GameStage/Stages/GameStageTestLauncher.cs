using EnterSon.GameStage;
using EnterSon.I18N;
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
			initializeUI();
			I18N.Instance.Initialize(I18N.Language.kEnglish);

			Debug.Log(I18N.Instance["BUTTON:NOK"]);
		}

		private void initializeUI()
		{
			var networkManagerHUD = new GameObject("NetworkHUD");
			networkManagerHUD.AddComponent<NetworkManagerHUD>();
		}
	}
}
