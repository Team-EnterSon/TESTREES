using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterSon.GameStage
{
	public abstract class GameStage : MonoBehaviour
	{
		public Type NextStageType { get; private set; } = null;

		public virtual void InitializeStage()
		{

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