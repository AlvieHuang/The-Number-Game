using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		//Creates the ray for the coursor
		RaycastHit hitInfo;

		if (Physics.Raycast (ray, out hitInfo)) {
			//Get the main object that the ray is in contact with
			Debug.Log ("Mouse is Over: " + hitInfo.collider.name);
			GameObject hitObject = hitInfo.collider.gameObject;

			selectObject (hitObject);
		} 
		else {
			ClearSelection ();
		}

	}
	void selectObject(GameObject obj){
		GameObject selectObject = obj;
		if (selectObject != null) {
			if (obj == selectObject) 
				return;

			ClearSelection ();
		}

		selectObject = obj;

		Renderer[] render = selectObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in render) {
			Material mat = r.material;
			mat.color = Color.green;
			r.material = mat;
		}
	}

	void ClearSelection(){
		GameObject selectObject = null;
		if (selectObject == null) {
			return;
		}

		Renderer[] render = selectObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in render) {
			Material mat = r.material;
			mat.color = Color.blue;
			r.material = mat;


			selectObject = null;
		}
	}
}
