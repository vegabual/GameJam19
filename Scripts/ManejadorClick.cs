using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorClick : MonoBehaviour {

	bool mobileVersion = true;
	public float velSpeed=2;
	float YaxisRotation;
	float XaxisRotation;
	bool dragging = false;
	bool dropped=false;

	private void Awake() {
		if (Application.platform == RuntimePlatform.WindowsPlayer) {
			mobileVersion = false;
		}
	}


	// Update is called once per frame
	void Update () {
		if( Input.GetMouseButton(0))
		{

			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (hit.collider != null && hit.collider.GetComponent<ManejadorClick>()) 
			{
				if (!mobileVersion || Application.isEditor)
				{
					YaxisRotation = Input.GetAxis("Mouse Y") * velSpeed;
					XaxisRotation = Input.GetAxis("Mouse X") * velSpeed;
					dragging = true;
				
				}

				/*if (Input.touchCount == 1)
				{
					Touch touch = Input.GetTouch(0);

					// Move the cube if the screen has the finger moving.
					if (touch.phase == TouchPhase.Moved)
					{
						float YaxisRotation = Input.GetAxis("Mouse Y") * velSpeed;
						float XaxisRotation = Input.GetAxis("Mouse X") * velSpeed;
						if (parent)
						{
							transform.Rotate(YaxisRotation, -XaxisRotation, 0, Space.World);
						}
						else
						{
							transform.Rotate(-YaxisRotation, 0, 0, Space.Self);
						}
					}

				}*/
			}
		}
		if(dragging)
		{
			FollowMouse ();
		}
		if(Input.GetMouseButtonUp(0))
		{
			dragging = false;
			dropped = true;
		}
		if(dropped)
		{
			DropObject ();
		}

	}
	private void DropObject()
	{
		Vector3 dir = new Vector3 (XaxisRotation, YaxisRotation, 0);
		transform.Translate (dir*Time.deltaTime,Space.World);
	}
	private void FollowMouse()
	{
		transform.position= new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0) ;
	}
}
