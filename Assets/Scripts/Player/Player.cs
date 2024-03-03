using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    // to access camera in unity
    public Camera _camera;

    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 mousePosn; // reference to mouse position

    
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        mousePosn = _camera.ScreenToWorldPoint(Input.mousePosition); // convert to world point because game doesn't exist in pixel units

        OffBounds();
    }



    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

        // this allows the sprite to look in the direction of the mouse position (vector - vector = vector that points from one to other)
        Vector2 lookDirection = mousePosn - rb.position;

        // Atan2 allows us to rotate player (our z rotation), we then convert from radians to degrees
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f; 

        rb.rotation = angle;
    }



    private void OffBounds()
    {
        Vector3 viewPos = _camera.WorldToViewportPoint(transform.position);
        

        float minX = 0.015f; 
        float maxX = 0.985f; 
        float minY = 0.015f;
        float maxY = 0.9728f; 

        // Check if the player is out of bounds and adjust their position if necessary
        viewPos.x = Mathf.Clamp(viewPos.x, minX, maxX);
        viewPos.y = Mathf.Clamp(viewPos.y, minY, maxY);

        // convert the adjusted viewport coordinates back to world coordinates
        transform.position = _camera.ViewportToWorldPoint(viewPos);
    }


}
