using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour {

	public float waveAmplitude;
	public float waveTimeConstant;
	public float waveFrequency;

	private Terrain terrain;
	private float[,] origHeights;
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
				flatHeights [i, j] = 200.0f/600.0f;
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
		float startTime = Time.time;
		while (true) {
			float dTime = Time.time - startTime;
			float[,] newheights = origHeights.Clone() as float[,];
			for (int i = 0; i < newheights.GetLength (0); i++) {
				float newheight = (waveAmplitude * Mathf.Sin (waveFrequency * ((i * 1.0f / terrain.terrainData.size.x) + waveTimeConstant * dTime)));
				for (int j = 0; j < newheights.GetLength (1); j++) {
					newheights [i, j] += newheight + (waveAmplitude * Mathf.Sin (waveFrequency * ((j * 1.0f / terrain.terrainData.size.x) + waveTimeConstant * dTime)));
				}
			}
			terrain.terrainData.SetHeights (0, 0, newheights);
			yield return new WaitForSeconds (0.01f);
		}
	}

	IEnumerator RaiseFromFlat(){
		float[,] newHeights = origHeights.Clone () as float[,];
		for (int c = 0; c < 70; c++) {
			float c2 = ((c + 1) / 70.0f);
			//float t = (1 - c2) * ((1 - c2) * c2 + c2) + c2 * ((c2 + 1) * c2 - c2 + 1);
			//float t = (Mathf.Atan(4*c2 - 2) / 2.25f) + 0.5f;
			float t = (Mathf.Atan(6*c2 - 3) / 2.55f) + 0.5f;
			float trumble = -0.5f * Mathf.Cos (6.28f * c2) + 0.5f;
			//float rumble = (Random.value - 0.5f) * trumble / 4800.0f;
			for (int i = 0; i < origHeights.GetLength (0); i++) {
				for (int j = 0; j < origHeights.GetLength (1); j++) {
					float rumble = (Random.value - 0.5f) * trumble / 4800.0f;
					newHeights[i,j] = 200.0f/600.0f + (origHeights[i,j] - 200.0f/600.0f) * t + rumble;
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
