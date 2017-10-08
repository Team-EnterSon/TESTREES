using System.Collections.Generic;
using UnityEngine;

namespace TESTREES.Tables
{
	// Dictionary<ID, Data>
	[HasTable("PawnTable", typeof(Dictionary<int, PawnData>))]
	public struct PawnData
	{
		public int ID;
		public string NameKey;
		public string DescriptionKey;
		public string VisualPrefabPath;
		public int HP;
		public int? Shield;
		// m/s
		public float MoveSpeed;
		public int WeaponID;
		public int MobilityID;
		public List<int> SkillIDs;
		[Range(0, 360)]
		public int SightAngle;
		public int SightRadius;
	}
}