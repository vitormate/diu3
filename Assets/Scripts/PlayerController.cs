using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Vector2 moveInput;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
    }

    void Move() {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        if (moveInput.x < 0)
        {
            anim.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (moveInput.x > 0)
        {
            anim.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        transform.Translate(moveInput * Time.deltaTime * moveSpeed);


        anim.SetBool("isMoving", Mathf.Abs(moveInput.x) > 0 || Mathf.Abs(moveInput.y) > 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // aqui é onde leva pra um menu
            // Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }
}
