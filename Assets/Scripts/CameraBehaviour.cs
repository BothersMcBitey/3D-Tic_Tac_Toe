using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Update () {
		if (Input.GetMouseButton (1)) {
			yaw += speedH * Input.GetAxis ("Mouse X");
			pitch -= speedV * Input.GetAxis ("Mouse Y");

			transform.RotateAround (new Vector3 (0, 0, 0), transform.right, yaw);
			transform.RotateAround (new Vector3 (0, 0, 0), transform.up, pitch);
			//transform.eulerAngles = new Vector3 (pitch, yaw, 0.0f);
		}
    }
}