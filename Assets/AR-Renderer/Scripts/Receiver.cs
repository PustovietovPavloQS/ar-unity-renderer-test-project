// #define VERBOSE

using UnityEngine;
using System.Runtime.InteropServices;
using System;

public static class Receiver
{

	private static bool isActivated = false;

	[DllImport("__Internal")]
	private static extern void set_callbacks(delegate_Vtt vtt, delegate_Vft vft);

	delegate void delegate_Vtt(Vector3 position, Quaternion rotation, Vector3 scale, float cameraFov, int targetIndex);
	delegate void delegate_Vft(Vector3 position, Quaternion rotation, Vector3 scale, float cameraFov, string faceMeshData);

	//Set callbacks
	public static void Activate()
	{
		if (!isActivated)
		{
			set_callbacks(
				TargetVtt,
				TargetVft);
		}
	}

	//Target transform (Vtt) callback
	[MonoPInvokeCallback(typeof(delegate_Vtt))]
	private static void TargetVtt(Vector3 position, Quaternion rotation, Vector3 scale, float cameraFov, int targetIndex)
	{
        EventBus.onTargetTracking?.Invoke(position, rotation, scale, cameraFov, targetIndex);
	}

	//Face transform (Vft) callback
	[MonoPInvokeCallback(typeof(delegate_Vft))]
	private static void TargetVft(Vector3 position, Quaternion rotation, Vector3 scale, float cameraFov, string faceMeshData)
	{
        EventBus.onFaceTracking?.Invoke(position, rotation, scale, cameraFov, faceMeshData);
	}
}