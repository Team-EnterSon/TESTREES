using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnterSon.GameStage
{
	public abstract class GameStage : MonoBehaviour
	{
		public Type NextStageType { get; private set; } = null;

		public virtual void InitializeStage()
		{
			Debug.LogFormat("[GameStage] Initializing <color=blue>{0}</color>...", this.GetType().ToString().Split('.').Last());
		}

		public virtual void EnterStage()
		{
			
		}

		public virtual void ExitStage()
		{

		}

		public virtual void UpdateStage(float dt)
		{

		}

		protected void setNextStage<TTargetStage>() where TTargetStage : GameStage
		{
			NextStageType = typeof(TTargetStage);
		}
	}
}