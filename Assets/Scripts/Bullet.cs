using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour, IPoolableGameObject
{
    public float speed = 10f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;

    private float timeToDestroy;
    private PoolManager bulletPoolManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get the first pool manager
        bulletPoolManager = GameObject.FindGameObjectWithTag("Pool").GetComponents<PoolManager>()[0];
    }

    public void Activate()
    {
        timeToDestroy = Time.time + maxLifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);

        if (Time.time > timeToDestroy)
        {
            bulletPoolManager.ReturnObject(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider collision) 
    {
        if(collision.gameObject.CompareTag("Enemy")) 
        {
            collision.gameObject.GetComponent<Meteor>().Hit(targetVector);
            bulletPoolManager.ReturnObject(gameObject);
            IncreaseScore();
            UpdateScoreText();
        }
    }
    
    private void IncreaseScore()
    {
        Player.SCORE += 1;
    }
    
    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Puntos: " + Player.SCORE;
    }
}
