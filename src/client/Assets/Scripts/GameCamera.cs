using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TESTREES.GamePlay
{
	[RequireComponent(typeof(Camera))]
	public class GameCamera : MonoBehaviour, IDisposable
	{
		private Camera _cachedCamera = null;
		public Camera Cam { get { return _cachedCamera; } }

		public void Awake()
		{
			_cachedCamera = GetComponent<Camera>();
			Cam.orthographic = true;
			Cam.orthographicSize = 8;
		}

		public void Dispose()
		{
			Destroy(gameObject);
		}

		public void LateUpdate()
		{
			updateCameraPosition();
		}

		private void updateCameraPosition()
		{
			var mousePos = Cam.ScreenToViewportPoint(Input.mousePosition);
			if (mousePos.x > 0.999f)
				transform.position += Vector3.right * 0.2f;
			else if (mousePos.x < 0.001f)
				transform.position += Vector3.left * 0.2f;
		}

		public static class Factory
		{
			public static GameCamera Create()
			{
				var instance = new GameObject("GameCamera").AddComponent<GameCamera>();

				return instance;
			}
		}
	}
}
