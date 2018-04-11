using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carousel : MonoBehaviour {

	// 
	public int visibleModel = 1;
	public GameObject[] volcaModels;
	public GameObject lastModel;

	void Start () {
		if (volcaModels.Length == 0)
			volcaModels = GameObject.FindGameObjectsWithTag("volcaModel");
		
		for ( int i=0;i < volcaModels.Length;i++)
		{
			volcaModels[i].GetComponent<Renderer>().enabled = false;
			//Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);
		}
		volcaModels [visibleModel].GetComponent<Renderer> ().enabled = true;	
		lastModel = volcaModels [visibleModel]; // currently visible model
		StartCoroutine(fadeWait()); // to delay hiding previous model to allow time for fade out

	}
		
	void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			var  time = 1.0f; // fade speed
			var  alphaVal = 0.0f; // target alpha value for fade
			lastModel = volcaModels [visibleModel]; // currently visible model

			LeanTween.alpha(lastModel, alphaVal, time); //fade out currently visible model 
			// LeanTween.alphaVertex(lastModel, alphaVal, time);
			// volcaModels [visibleModel].GetComponent<Renderer>().enabled = false; // now put into delayed fadewait routine

			if (visibleModel < volcaModels.Length-1) {				
				visibleModel += 1;
			} else {
				visibleModel = 0;
			}

			volcaModels [visibleModel].GetComponent<Renderer>().enabled = true; //show new model

			alphaVal = 1.0f; // target alpha value for fade	 	
			LeanTween.alpha(volcaModels [visibleModel], alphaVal, time); //fade in currently visible model

			//	Debug.Log("visibleModel is set to: " + visibleModel);
			//	Debug.Log (volcaModels[visibleModel].name);

			StartCoroutine(fadeWait()); // to delay hiding previous model to allow time for fade out
		}

	}


	IEnumerator fadeWait()
	{
		yield return new WaitForSecondsRealtime(1);
		lastModel.GetComponent<Renderer>().enabled = false; // hide previous model
			
	}

}
