using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float thrustForce = 800f;
    public float rotationSpeed = 100f;
    public GameObject gun, bulletPrefab;
    private Rigidbody _rigid;
    private PoolManager bulletPoolManager;

    private float xBorderLimit;
    private float yBorderLimit;
    
    public static int SCORE = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        // Get the first pool manager
        bulletPoolManager = GameObject.FindGameObjectWithTag("Pool").GetComponents<PoolManager>()[0];

        yBorderLimit = Camera.main.orthographicSize + 1;
        xBorderLimit = Camera.main.orthographicSize * Screen.width / Screen.height;
    }
    
    void FixedUpdate() 
    {
        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenuManager.isPaused)
            return;

        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit + 1;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit - 1;
        else if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit + 1;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit - 1;
        transform.position = newPos;
        
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            // GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            GameObject bullet = bulletPoolManager.GetObject(gun.transform.position);
            if (bullet == null)
                return;

            Bullet balaScript = bullet.GetComponent<Bullet>();

            balaScript.targetVector = transform.right;
        }
    }
    
    private void OnTriggerEnter(Collider collision) 
    {
        if(collision.gameObject.CompareTag("Enemy")) 
        {
            Player.SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
