using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterSon.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TESTREES.UI
{

	public class LoginBoard : UIBoard
	{
		public event Action OnLoginButtonClicked = delegate { };
		
		public void ClickLoginButton()
		{
			OnLoginButtonClicked?.Invoke();
		}

		[SerializeField]
		private Text _nicknameComponent = null;

		public string Nickname
		{
			get
			{
				return _nicknameComponent.text;
			}
		}
	}
}
