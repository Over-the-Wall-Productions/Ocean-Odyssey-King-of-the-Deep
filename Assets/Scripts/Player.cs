using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    // to access camera in unity
    private Camera _camera;

    public Transform bulletSpawn;

    public GameObject bulletPrefab;


    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Move()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontalMovement, verticalMovement);

        transform.Translate(move * speed * Time.deltaTime);

        OffBounds();
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

    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawn.up * 500f);
    }

}
