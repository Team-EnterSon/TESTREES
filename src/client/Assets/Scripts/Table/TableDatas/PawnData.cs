using System.Collections.Generic;

namespace TESTREES.Tables
{
	// Dictionary<ID, Data>
	[HasTable("PawnTable", typeof(Dictionary<int, PawnData>))]
	public struct PawnData
	{
		public int ID;
		public string NameKey;
		public string DescriptionKey;
		public int HP;
		public int? Shield;
		// m/s
		public float MoveSpeed;
		public bool IsAirUnit;
		public int WeaponID;
		public int MobilityID;
		public List<int> SkillIDs;
	}
}