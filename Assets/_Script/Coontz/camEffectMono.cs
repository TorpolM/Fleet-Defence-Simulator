using UnityEngine;
using System.Collections;

public class camEffectMono : MonoBehaviour {

	public Material monoTone;

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit (src, dest, monoTone);
	}
}