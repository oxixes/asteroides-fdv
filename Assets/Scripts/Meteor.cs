using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Meteor : MonoBehaviour, IPoolableGameObject
{
    public float maxLifeTime = 4f;

    private float timeToDestroy;
    private Rigidbody _rigid;
    private PoolManager meteorPoolManager;
    private bool isSmall = false;
    private Vector3 initialScale = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // Get the second pool manager
        meteorPoolManager = GameObject.FindGameObjectWithTag("Pool").GetComponents<PoolManager>()[1];
    }

    public void Activate()
    {
        if (_rigid == null)
            _rigid = GetComponent<Rigidbody>();
        if (initialScale == Vector3.zero)
            initialScale = transform.localScale;

        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = Vector3.zero;
        _rigid.useGravity = true;

        transform.localScale = initialScale;
        timeToDestroy = Time.time + maxLifeTime;
        isSmall = false;
    }

    public void Hit(Vector2 bulletVector)
    {
        if (isSmall)
        {
            meteorPoolManager.ReturnObject(gameObject);
        } else
        {
            Vector2 meteor1Vector = Quaternion.Euler(0, 0, 45) * new Vector2(-bulletVector.y, bulletVector.x);
            Vector2 meteor2Vector = Quaternion.Euler(0, 0, -45) * new Vector2(-bulletVector.y, bulletVector.x);

            // Divide the meteor in two
            var meteor1 = meteorPoolManager.GetObject((Vector2) transform.position + meteor1Vector * 1.5f);
            meteor1.GetComponent<Meteor>().SetSmall();
            meteor1.GetComponent<Rigidbody>().AddForce(meteor1Vector * 400, ForceMode.Force);

            var meteor2 = meteorPoolManager.GetObject((Vector2) transform.position - meteor2Vector * 1.5f);
            meteor2.GetComponent<Meteor>().SetSmall();
            meteor2.GetComponent<Rigidbody>().AddForce(-meteor2Vector * 400, ForceMode.Force);

            meteorPoolManager.ReturnObject(gameObject);
        }
    }

    public void SetSmall()
    { 
        isSmall = true;
        transform.localScale = initialScale * 0.5f;
        _rigid.useGravity = false;
    }    

    // Update is called once per frame
    void Update()
    {
        if (PauseMenuManager.isPaused)
            return;

        if (Time.time > timeToDestroy)
        {
            meteorPoolManager.ReturnObject(gameObject);
        }
    }
}
