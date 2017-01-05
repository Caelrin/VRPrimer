using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullYourself : MonoBehaviour {

  public GameObject cameraRig;

  private SteamVR_TrackedObject controller;
  private bool pulling = false;
  private Vector3 basePosition;
  private Vector3 baseRigPosition;

  // Use this for initialization
  void Start () {
	  controller = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
	  if (pulling) {
	    Vector3 difference = basePosition - transform.localPosition;
	    difference.x = difference.x * 5.0f;
      difference.z = difference.z * 5.0f;
      difference.y = 0;

      cameraRig.transform.position = baseRigPosition + difference;
	  }

    if (SteamVR_Controller.Input(checked((int)controller.index)).GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
      Debug.Log("Starting to grab pull");
	    basePosition = transform.localPosition;
	    baseRigPosition = cameraRig.transform.position;
      pulling = true;
	  }
    if (SteamVR_Controller.Input(checked((int)controller.index)).GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
    {
      Debug.Log("Releasing grab pull");
      pulling = false;
    }
  }
}
