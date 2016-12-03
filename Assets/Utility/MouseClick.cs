using UnityEngine;
using System.Collections;


namespace UnityStandardAssets.Characters.FirstPerson{

	public class MouseClick : MonoBehaviour {
		FirstPersonController player;
		// Use this for initialization
		void Start () {
				player = this.GetComponent<FirstPersonController> ();
		}
		
		// Update is called once per frame
		void Update () {
			if (Input.GetKeyDown(KeyCode.Mouse0)){
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				//Creates the ray for the coursor
				RaycastHit hitInfo;

				if (Physics.Raycast (ray, out hitInfo)) {
					//Get the main object that the ray is in contact with
					Debug.Log ("Mouse is Over: " + hitInfo.collider.name);
					GameObject hitObject = hitInfo.collider.gameObject;

					selectObject (hitObject);
					Card clickedCard = hitObject.GetComponent<Card> ();
					if (clickedCard != null) {
						string temp = clickedCard.CardValue;
						player.Inventory.Add (temp);
						Debug.Log (temp);
					}
				 
				} 
				else {
					ClearSelection ();
				}

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
}
