using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallToCamera : MonoBehaviour
{
    public GameObject BallObject;

    Vector3 startPos;

	private void Start()
	{
        startPos = BallObject.transform.position;
	}

	void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.G))
        {
            Vector3 direction = Camera.main.transform.position;
            BallObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(10f, 0f, 0f);
            BallObject.transform.position = Vector3.Lerp(BallObject.transform.position, direction, 5f * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.H))
		{
            BallObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            BallObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            BallObject.GetComponent<Rigidbody>().useGravity = false;
            BallObject.GetComponent<SphereCollider>().enabled = false;
            BallObject.transform.position = startPos;
		}
    }
}
