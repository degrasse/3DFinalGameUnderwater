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
					newheights [i, j] += newheight;
				}
			}
			terrain.terrainData.SetHeights (0, 0, newheights);
			yield return new WaitForSeconds (0.01f);
		}
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
	}
}
