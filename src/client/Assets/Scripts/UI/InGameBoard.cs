using System;
using System.Collections;
using TESTREES.GamePlay;

namespace EnterSon.UI
{
	public class InGameBoard : UIBoard
	{
		public override void Dispose()
		{
			base.Dispose();


		}

		public IEnumerator WaitForPickSpawningPosition(out GameCoord @posToSpawn, out Type @pawnTypeToSpawn)
		{
			// TODO(sorae): impl..
			throw new NotImplementedException();
		}

	}
}