using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Tools;
public class CameraController : MonoBehaviour
{
    Vector3 velocity = new Vector3();
    Vector2 mouseClickPos;
    Vector2 mouseCurrentPos;
    bool panning = false;
    float _time = 0.0f;

    private void Update()
    {
        //Zoom with scrollwheel
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - Input.mouseScrollDelta.y, 5,30) ;


        // When LMB clicked get mouse click position and set panning to true
        if (Input.GetKeyDown(KeyCode.Mouse0) && !panning){
            mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            panning = true;
        }
        
        // If LMB is released, stop moving the camera
        if (Input.GetKeyUp(KeyCode.Mouse0)){
            panning = false;
            velocity = PanDistance();
        }

        // If LMB is already clicked, move the camera following the mouse position update
        if (panning){
            transform.position += PanDistance();
        }


        //Let the map pan for a second after you let go for a smooth feel
        if (!panning && _time <= 1f){
            transform.position += velocity;
            velocity = Vector3.Lerp(velocity, Vector3.zero, _time);
            _time += Time.smoothDeltaTime;
        }
        else{
            _time = 0.0f;
        }
    }

    Vector3 PanDistance()
    {
        mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var distance = mouseCurrentPos - mouseClickPos;
        return new Vector3(-distance.x, -distance.y, 0);
    }

}
