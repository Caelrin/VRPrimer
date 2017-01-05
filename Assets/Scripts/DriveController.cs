using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveController : MonoBehaviour {
  public GameObject cameraRig;
  public GameObject Lever;

  private bool connectedToLever = false;
  private bool pullingLever = false;
  private static bool controllerLock = false;
  private static float speed = 0;

  private SteamVR_TrackedObject controller;

  // Use this for initialization
  void Start () {
    controller = GetComponent<SteamVR_TrackedObject>();
  }

  void OnTriggerEnter(Collider collider) {
    if (collider.gameObject.layer == LayerMask.NameToLayer("Lever")) {
      Debug.Log("Collided with lever");
      connectedToLever = true;
    }
  }

  void OnTriggerExit(Collider collider) {
    if (collider.gameObject.layer == LayerMask.NameToLayer("Lever")) {
      Debug.Log("Left connection from lever");
      connectedToLever = false;
    }
  }

  // Update is called once per frame
  void Update() {
    if (pullingLever) {
      Lever.transform.LookAt(new Vector3(transform.position.x, transform.position.y, Lever.transform.position.z));
      speed = transform.position.x - Lever.transform.position.x;
      speed *= .5f;
    }
    if (speed < .03 && speed > -.03) {
      speed = 0;
    }
    cameraRig.transform.position += new Vector3(speed, 0, 0);

    if (!controllerLock && connectedToLever &&
        SteamVR_Controller.Input(checked((int) controller.index)).GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
      Debug.Log("Pulling Lever");
      pullingLever = true;
    }
    if (SteamVR_Controller.Input(checked((int)controller.index)).GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
    {
      if (pullingLever) {
        controllerLock = false;
      }
      Debug.Log("Releasing Lever");
      pullingLever = false;
    }
  }
}
