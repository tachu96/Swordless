using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame_Script : MonoBehaviour {

	void Update() {
		if (Input.GetKey("escape"))
			Application.Quit();

	}
}
