using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
    public float projectileLifetime = 5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = (hit.point - transform.position).normalized;
            projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
        }
        else
        {
            projectile.GetComponent<Rigidbody>().velocity = ray.direction * projectileSpeed;
        }

        Destroy(projectile, projectileLifetime);
    }
}