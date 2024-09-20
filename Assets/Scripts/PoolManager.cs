using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public int poolSize = 20;
    public GameObject prefab;

    private List<GameObject> pool;

    // Start is called before the first frame update
    void Start()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(prefab);
            go.SetActive(false);
            pool.Add(go);
        }
    }

    public GameObject GetObject(Vector3 pos)
    {
        if (pool.Count > 0)
        {
            GameObject go = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            go.transform.position = pos;
            go.SetActive(true);
            go.GetComponent<IPoolableGameObject>().Activate();
            return go;
        }

        Debug.Log("Nos hemos quedado sin objetos!!!");

        return null;
    }

    public void ReturnObject(GameObject go)
    {
        go.SetActive(false);
        pool.Add(go);
    }
}
