// #define VERBOSE

using UnityEngine;
using System.Runtime.InteropServices;
using System;

public static class Receiver
{

	private static bool isActivated = false;

	[DllImport("__Internal")]
	private static extern void set_callbacks(delegate_Vtt vtt, delegate_Vft vft);

	delegate void delegate_Vtt(Vector3 position, Quaternion rotation, Vector3 scale, float cameraFov);
	delegate void delegate_Vft(Vector3 position, Quaternion rotation, Vector3 scale, float cameraFov, string faceMeshData);

	public static Action<
		Vector3,
		Quaternion,
		Vector3,
		float > onTargetTransformReceived;

	public static Action<
		Vector3,
		Quaternion,
		Vector3,
		float,
		string> onFacemeshTransformReceived;

	public static void Activate()
	{
		if (!isActivated)
		{
			set_callbacks(
				TargetVtt,
				TargetVft);
		}
	}

	[MonoPInvokeCallback(typeof(delegate_Vtt))]
	private static void TargetVtt(Vector3 position, Quaternion rotation, Vector3 scale, float cameraFov)
	{
		onTargetTransformReceived?.Invoke(position, rotation, scale, cameraFov);
	}

	[MonoPInvokeCallback(typeof(delegate_Vft))]
	private static void TargetVft(Vector3 position, Quaternion rotation, Vector3 scale, float cameraFov, string faceMeshData)
	{
		onFacemeshTransformReceived?.Invoke(position, rotation, scale, cameraFov, faceMeshData);
	}
}