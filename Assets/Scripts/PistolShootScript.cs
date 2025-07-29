using UnityEngine;

public class PistolShootScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float bulletSpeed = 100f;
    private bool isShooting = false;
    public bool isReloading = false;
    public int bulletMax = 12;
    public int bulletCount = 12;
    public BulletCountUpdateScript bulletUI;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) 
            && bulletCount > 0 
            && !isReloading
            && !isShooting)
        {
            Fire();
        }
        else
        {
            isShooting = false;
        }

        if (Input.GetKeyDown(KeyCode.R) 
            && !isReloading 
            && !isShooting 
            && bulletCount < bulletMax 
            || bulletCount == 0 
            && !isReloading 
            && !isShooting)
        {
            Reload();
        }
    }

    void Fire()
    {
        isShooting = true;

        // Ray from camera center
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(
            Screen.width / 2,
            Screen.height / 2));

        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);  // Fire into distance
        }

        // Direction from spawn point to where the player is aiming
        Vector3 direction = (targetPoint - spawnPoint.position).normalized;
        Vector3 spawnOffset = spawnPoint.forward * 1.2f;

        // Instantiate bullet and rotate to face direction
        GameObject spawnedBullet = Instantiate(
            bullet,
            spawnPoint.position + spawnOffset, 
            Quaternion.LookRotation(direction)
            );

        bulletCount--;

        // Pass speed to bullet
        BulletMoveScript moveScript = spawnedBullet.GetComponent<BulletMoveScript>();
        if (moveScript != null)
        {
            moveScript.bulletSpeed = bulletSpeed;
        }

        bulletUI.UpdateBulletCounter(bulletCount, bulletMax);
    }

    public void Reload()
    {
        if (!isShooting 
            && Input.GetKey(KeyCode.R) 
            || bulletCount == 0)
        {
            isReloading = true;
            bulletUI.UpdateBulletCounter(null, null);
            bulletCount = bulletMax;
            isReloading = false;
        }
    }
}