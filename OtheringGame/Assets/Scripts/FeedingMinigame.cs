using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingMinigame : MonoBehaviour
{
    public float movePerClick = 25f;

    public float score = 0;

    public float moveOverTime;

    public Rigidbody2D rb;

    public Transform transform;

    public ProgressBar progressBar;


    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = new Vector2(-5f, 0);
    }

    private void FixedUpdate()
    {

        if (transform.position.x > -0.15f && transform.position.x < 0.15f)
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
}
