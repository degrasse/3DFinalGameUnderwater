using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour {

	public float waveAmplitude;
	public float waveTimeConstant;
	public float waveFrequency;
	public float waveSpeed;

	public float granularity;

	private Terrain terrain;
	private float[,] origHeights;

	private const float terrainMaxHeight = 600f;
	private const float terrainFlatHeight = 200f / terrainMaxHeight;
	//private float startHeight;

	// Use this for initialization
	void Start () {
		terrain = gameObject.GetComponent<Terrain> ();
		origHeights = terrain.terrainData.GetHeights (0, 0, 
			(int)(terrain.terrainData.heightmapResolution/* * terrain.terrainData.heightmapResolution*/),
			(int)(terrain.terrainData.heightmapResolution/* * terrain.terrainData.heightmapResolution*/));
		//startHeight = terrain.terrainData.heightmapHeight;
		float[,] flatHeights = origHeights.Clone() as float[,];
		for (int i = 0; i < flatHeights.GetLength (0); i++) {
			for (int j = 0; j < flatHeights.GetLength (1); j++) {
				flatHeights [i, j] = terrainFlatHeight;
			}
		}
		terrain.terrainData.SetHeights (0, 0, flatHeights);
	}

	void raiseSquare(Vector3 point, int radius, float h){
		int centerX = (int)((point.x / terrain.terrainData.size.x) * terrain.terrainData.heightmapResolution);
		int centerZ = (int)((point.z / terrain.terrainData.size.z) * terrain.terrainData.heightmapResolution);
		float[,] heights = terrain.terrainData.GetHeights(centerX - radius, centerZ - radius, radius * 2, radius * 2);
		for (int i = 0; i < heights.GetLength(0); i++) {
			for (int j = 0; j < heights.GetLength (1); j++) {
				heights [i,j] += h;
			}
		}
		terrain.terrainData.SetHeights (centerX - radius, centerZ - radius, heights);

	}

	void resetHeightMap(){
		terrain.terrainData.SetHeights (0, 0, origHeights);
	}

	IEnumerator WaveForSeconds(float seconds){
		Coroutine cr = StartCoroutine (Wave ());
		yield return new WaitForSeconds (seconds);
		StopCoroutine (cr);
	}

	IEnumerator Wave(){
		float waitTime = 1.0f / waveSpeed;
		int grainSize = Mathf.CeilToInt(1.0f / granularity);
		float startTime = Time.time;
		while (true) {
			float dTime = Time.time - startTime;
			float[,] newheights = origHeights.Clone() as float[,];
			for (int i = 0; i < newheights.GetLength (0) - (grainSize - 1); i+= grainSize) {
				float newheight = (waveAmplitude * Mathf.Sin (waveFrequency * ((i * 1.0f / terrain.terrainData.size.x) + waveTimeConstant * dTime)));
				for (int j = 0; j < newheights.GetLength (1); j++) {
					for(int i2 = i; i2 < i + grainSize; i2++){
						newheights [i2, j] += newheight;// + (waveAmplitude * Mathf.Sin (waveFrequency * ((j * 1.0f / terrain.terrainData.size.x) + waveTimeConstant * dTime)));
					}
				}
			}
			terrain.terrainData.SetHeights (0, 0, newheights);
			yield return new WaitForSeconds (waitTime);
		}
	}

	IEnumerator RaiseFromFlat(){
		int duration = 70;
		int atanGrade = 6;
		float atanHeight = 2.55f;
		float rumbleMag = 1.0f / 4800.0f;

		float[,] newHeights = origHeights.Clone () as float[,];
		for (int c = 0; c < duration; c++) {
			float c2 = ((c + 1) / (float)(duration));
			//float t = (1 - c2) * ((1 - c2) * c2 + c2) + c2 * ((c2 + 1) * c2 - c2 + 1);
			//float t = (Mathf.Atan(4*c2 - 2) / 2.25f) + 0.5f;
			float t = (Mathf.Atan(atanGrade*c2 - atanGrade * 0.5f) / atanHeight) + 0.5f;
			float trumble = -0.5f * Mathf.Cos (6.28f * c2) + 0.5f;
			//float rumble = (Random.value - 0.5f) * trumble / 4800.0f;
			for (int i = 0; i < origHeights.GetLength (0); i++) {
				for (int j = 0; j < origHeights.GetLength (1); j++) {
					float rumble = (Random.value - 0.5f) * trumble * rumbleMag;
					newHeights[i,j] = terrainFlatHeight + (origHeights[i,j] - terrainFlatHeight) * t + rumble;
				}
			}
			terrain.terrainData.SetHeights (0, 0, newHeights);
			yield return new WaitForSeconds (0.01f);
		}
		terrain.terrainData.SetHeights (0, 0, origHeights);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			raiseSquare (new Vector3 (100, 0, 100), 4, 0.1f);
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			resetHeightMap ();
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			StartCoroutine (WaveForSeconds (20));
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			StartCoroutine (RaiseFromFlat ());
		}
	}
}
