using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 1.0f;
    Vector3 lastPosition;

    [SerializeField] float limitLeft, limitRight, limitUp, limitDown;

    [SerializeField] Game_Manager gameManager;

    void Update()
    {
        PanCamera();
    }

    void PanCamera()
    {
        //For Saving the last position of the mouse
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            lastPosition = Input.mousePosition;
        }

        // For Getting the Drag Input
        if (Input.GetKey(KeyCode.Mouse0))
        {
            // Checking if Turret is Getting Dragged
            if (gameManager.turretManager.dragOnViewSpace)
            {
                Vector3 delta = Input.mousePosition - lastPosition;
                Vector3 CameralastLocation = gameObject.transform.position;

                // Panning the camera using translate function
                transform.Translate(delta.x * mouseSensitivity, delta.y * mouseSensitivity, 0);

                //Setting the Value of Y Axis as Constant
                transform.position = new Vector3(transform.position.x, CameralastLocation.y, transform.position.z);

                // Limiting the panning area
                if (transform.position.x > limitRight)
                    transform.position = new Vector3(CameralastLocation.x, transform.position.y, transform.position.z);
                if (transform.position.x < limitLeft)
                    transform.position = new Vector3(CameralastLocation.x, transform.position.y, transform.position.z);
                if (transform.position.z < limitUp)
                    transform.position = new Vector3(transform.position.x, transform.position.y, CameralastLocation.z);
                if (transform.position.z > limitDown)
                    transform.position = new Vector3(transform.position.x, transform.position.y, CameralastLocation.z);

                // Updating the last position of the mouse
                lastPosition = Input.mousePosition;
            }
        }
    }
}