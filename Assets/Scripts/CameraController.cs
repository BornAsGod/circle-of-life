using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float camSpeed = 30f;
    public float camBorderThickness = 100f;
    public Vector2 camLimit;
    public float scrollSpeed = 20f;
    public float minY = 20;
    public float maxY = 120f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - camBorderThickness)
        {
            pos.z += camSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= camBorderThickness + 40f)
        {
            pos.z -= camSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - camBorderThickness)
        {
            pos.x += camSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= camBorderThickness)
        {
            pos.x -= camSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -camLimit.x, camLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -camLimit.y, camLimit.y);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        transform.position = pos;


    }
}
