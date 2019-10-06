using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{

    public int margin = 30;

    private Vector3 middleClickPosition;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(2))
        {
            middleClickPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(2))
        {
            var pixelMotion = -Input.mousePosition + middleClickPosition;
            transform.Translate(pixelMotion.x * .01f, pixelMotion.y * .01f, 0);
            middleClickPosition = Input.mousePosition;
        }
        else
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            //Do not move camera if mouse leaves screen
            if (Input.mousePosition.x < 0 || Input.mousePosition.y < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height)
            {
                return;
            }

            if (Input.mousePosition.x < margin)
            {
                transform.Translate(-Time.deltaTime * 10, 0, 0);
            }
            if (Input.mousePosition.y < margin)
            {
                transform.Translate(0, -Time.deltaTime * 10, 0);
            }
            if (Input.mousePosition.x > (Screen.width - margin))
            {
                transform.Translate(Time.deltaTime * 10, 0, 0);
            }
            if (Input.mousePosition.y > (Screen.height - margin))
            {
                transform.Translate(0, Time.deltaTime * 10, 0);
            }
        }
    }
}
