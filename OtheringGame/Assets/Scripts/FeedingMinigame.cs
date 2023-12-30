using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FeedingMinigame : MonoBehaviour
{
    public float movePerClick = 25f;

    public float score = 0;

    public float moveOverTime;

    public Rigidbody2D rb;

    public Transform transform;
    
    public CapsuleCollider2D capsuleCollider;

    public ProgressBar progressBar;

    public bool Green;


    // Start is called before the first frame update
    void Start()
    {
        Green = false;
        Physics2D.gravity = new Vector2(-5f, 0);
    }

    private void FixedUpdate()
    {

        if (Green)
        {
            score += 0.002f;
            progressBar.SetProgress(score, 10f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            rb.AddForce(Vector2.right * movePerClick, ForceMode2D.Impulse);
        }
    }
    void OnTriggerStay2D(Collider2D Other)
    {
        if (Other.gameObject.tag == "Player")
        {
            Green = true;
        }
    }
    void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.gameObject.tag == "Player")
        {
            Green = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
