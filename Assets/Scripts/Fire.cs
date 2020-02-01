using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    GameObject bullet;
    Transform shootingPoint;
    public float bulletForce;
    float Cooldown = 0.0f;
    int direction = 1;
    Queue<GameObject> FiredBullets = new Queue<GameObject>();
    void Start()
    {
        bullet = (GameObject)Resources.Load("Bullet");
        shootingPoint = transform.Find("FiringPoint");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = -1;
        }
        if (Cooldown > 0.0f)
        {
            Cooldown -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.F) && Cooldown <= 0.0f)
        {
            Cooldown = 3.0f;
            GameObject obj = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
            var body = obj.AddComponent<Rigidbody2D>();
            body.gravityScale = 0;
            body.AddForce(shootingPoint.transform.right * bulletForce * 10 * direction);
            FiredBullets.Enqueue(obj);
            Invoke(nameof(DestroyBullet), 3.0f);
        }
    }
    void DestroyBullet()
    {
        Destroy(FiredBullets.Dequeue());
    }
}
