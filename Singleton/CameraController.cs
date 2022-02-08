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

    private void Update()
    {
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
        else if(velocity != Vector3.zero){
            velocity = Vector3.Lerp(velocity, Vector3.zero, 3f);
            transform.position += velocity;
        }


    }


    Vector3 PanDistance()
    {
        mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var distance = mouseCurrentPos - mouseClickPos;
        return new Vector3(-distance.x, -distance.y, 0);
    }

}
