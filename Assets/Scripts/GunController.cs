// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GunController : MonoBehaviour
// {
//     SpriteRenderer sprite;
//     AudioSource shootFx;

//     public GameObject bullet;
//     public Transform spawnBullet;
//     float timer = 0f;

//     // Start is called before the first frame update
//     void Start()
//     {
//         sprite = GetComponent<SpriteRenderer>();
//         shootFx = GetComponent<AudioSource>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         Aim();
//         Shoot();
//         Special();
//     }

//     void Shoot()
//     {
//         Quaternion newRotation = transform.rotation * Quaternion.Euler(0f, 0f, 180f);
//         if (Input.GetButtonDown("Fire1"))
//         {
//             Instantiate(bullet, spawnBullet.position, newRotation);
//             shootFx.Play();
//         }
//     }

//     void Special()
//     {
//         if (Input.GetButtonDown("Fire2"))
//         {
//             timer += Time.deltaTime;
//             if (timer >= 5f)
//             {
//                 gameObject.SetActive(true);
//                 Time.timeScale = 0;
//             }
//             else {
//                 gameObject.SetActive(true);
//                 Time.timeScale = 1;
//             }
//         }
//     }

//     void Aim()
//     {
//         Vector3 mousePos = Input.mousePosition;
//         Vector3 screemPoint = Camera.main.WorldToScreenPoint(transform.position);

//         Vector2 offset = new Vector2(mousePos.x - screemPoint.x, mousePos.y - screemPoint.y);

//         float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

//         transform.rotation = Quaternion.Euler(0, 0, angle + 180);

//         if (mousePos.x > screemPoint.x) {
//             sprite.flipY = true;
//         } else {
//             sprite.flipY = false;
//         }
//     }
// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GunController : MonoBehaviour
// {
//     SpriteRenderer sprite;
//     AudioSource shootFx;

//     public GameObject bullet;
//     public Transform spawnBullet;
//     bool isSpecialActive = false;
//     float specialDuration = 5f;

//     // Start is called before the first frame update
//     void Start()
//     {
//         sprite = GetComponent<SpriteRenderer>();
//         shootFx = GetComponent<AudioSource>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         Aim();
//         Shoot();
//         Special();
//     }

//     void Shoot()
//     {
//         Quaternion newRotation = transform.rotation * Quaternion.Euler(0f, 0f, 180f);
//         if (Input.GetButtonDown("Fire1"))
//         {
//             Instantiate(bullet, spawnBullet.position, newRotation);
//             shootFx.Play();
//         }
//     }

//     void Special()
//     {
//         if (Input.GetMouseButtonDown(1) && !isSpecialActive)
//         {
//             isSpecialActive = true;
//             Time.timeScale = 0;
//             StartCoroutine(RestartGameAfterDelay());
//         }
//     }

//     IEnumerator RestartGameAfterDelay()
//     {
//         yield return new WaitForSecondsRealtime(specialDuration);
//         Time.timeScale = 1;
//         isSpecialActive = false;
//     }

//     void Aim()
//     {
//         Vector3 mousePos = Input.mousePosition;
//         Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

//         Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);

//         float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

//         transform.rotation = Quaternion.Euler(0, 0, angle + 180);

//         sprite.flipY = mousePos.x > screenPoint.x;
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    SpriteRenderer sprite;
    AudioSource shootFx;

    public GameObject bullet;
    public Transform spawnBullet;
    bool isSpecialActive = false;
    bool canShoot = true;
    float specialDuration = 5f;
    float shootCooldown = 3f;
    float shootTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        shootFx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Shoot();
        Special();
        UpdateTimers();
    }

    void Shoot()
    {
        Quaternion newRotation = transform.rotation * Quaternion.Euler(0f, 0f, 180f);
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Instantiate(bullet, spawnBullet.position, newRotation);
            shootFx.Play();
            shootTimer = shootCooldown;
        }
    }

    void Special()
    {
        if (Input.GetMouseButtonDown(1) && !isSpecialActive)
        {
            isSpecialActive = true;
            Time.timeScale = 0;
            shootTimer = shootCooldown;
            StartCoroutine(RestartGameAfterDelay());
        }
    }

    IEnumerator RestartGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(specialDuration);
        Time.timeScale = 1;
        isSpecialActive = false;
        canShoot = false;
    }

    void UpdateTimers()
    {
        if (!canShoot)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                canShoot = true;
            }
        }
    }

    void Aim()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle + 180);

        sprite.flipY = mousePos.x > screenPoint.x;
    }
}
