using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public int id;
    public GameObject explosionPrefab;

    private Vector3 fromPos = Vector3.zero;
    private Vector3 toPos = Vector3.zero;
    private float lastTime;
    public void Initialize(int _id)
    {
        id = _id;
    }
    private void Update()
    {
        this.transform.position = Vector3.Lerp(fromPos, toPos, (Time.time - lastTime) / (1.0f / 30f));
    }
    public void SetPosition(Vector3 position)
    {
        fromPos = toPos;
        toPos = position;
        lastTime = Time.time;
    }
    public void Explode(Vector3 position)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameManager.prokectiles.Remove(id);
        Destroy(gameObject);
    }
}
