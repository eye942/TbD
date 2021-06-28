using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    public float initialX=-1.7f;
    public float minX= -21.25f;
    public bool cameraPan=false;
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePos=Input.mousePosition;
            if(mousePos.y<=30.0f)
                cameraPan=true;
            else
                cameraPan=false;
            touchStart = Camera.main.ScreenToWorldPoint(mousePos);
        }
        if(Input.touchCount == 2){
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }else if(cameraPan&&Input.GetMouseButton(0)){
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction.y=0;
            direction.z=0;
            Camera.main.transform.position += direction;
           if(Camera.main.transform.position.x>initialX)
           {
               Vector3 pos=Camera.main.transform.position;
               pos.x=initialX;
               Camera.main.transform.position=pos;
           }
           else if(Camera.main.transform.position.x<minX)
           {
               Vector3 pos=Camera.main.transform.position;
               pos.x=minX;
               Camera.main.transform.position=pos;
           }
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
	}

    void zoom(float increment){
    //    Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
