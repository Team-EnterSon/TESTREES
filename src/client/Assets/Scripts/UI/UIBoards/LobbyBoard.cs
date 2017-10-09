using EnterSon.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace TESTREES.UI
{
	public class LobbyBoard : UIBoard
	{
		public event Action OnGameStartButtonClicked = delegate{};

		public void ClickGameStartButton()
		{
			OnGameStartButtonClicked?.Invoke();
		}

		[SerializeField]
		private Text _nicknameLabel = null;

		[SerializeField]
		private Text _serverAddressInput = null;

		public string NicknameBanner
		{
			set
			{
				if (_nicknameLabel != null)
					_nicknameLabel.text = value;
				else
					Debug.LogWarningFormat("[LobbyBoard] SerializeField <color=orange>{0}</color> is null!", nameof(_nicknameLabel));
			}
		}

		public string ServerAddress
		{
			get
			{
				return _serverAddressInput?.text;
			}
		}
	}
}
