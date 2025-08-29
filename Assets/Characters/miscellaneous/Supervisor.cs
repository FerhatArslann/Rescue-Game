using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Supervisor : MonoBehaviour
{
    //this is a script for extra character called 'supervisor'
    [Header("basic setup")]
    private Rigidbody2D rb;
    public GameObject bubble;
    public GameObject player;
    public Animator anim;
    public GameObject dialog;
    public TMP_Text chatInput;
    private float timeRemaining = 1;

    [Header("movement setup")]
    public float rotationSpeed;
    private int times = 0;

    [Header("Talk talk")]
    public string[] chatA = new string[3];
    private int count;
    private bool found = false;

    [Header("Action triggers")]
    //action states 1 doing this 0 not doing this
    public int talk = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        times = 1;
        count = 0;
        found = false;
    }
    void Collisiondetection()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (playerDistance < 2 && talk == 1)
        {
            talktoPlayer();
        }
        if (playerDistance < 2 && times == 1)
        {
            FaceDirection(player);
            bubble.SetActive(true);
            times = 0;
        }
        if (playerDistance < 4 && playerDistance > 3)
        {
            dialog.SetActive(false);
            bubble.SetActive(false);
            times = 1;
        }


    }

    void talktoPlayer()
    {
        // dialog input & checking level progression
        if (found == false)
        {
            chatA[0] = "Supervisor: Hello, I'm the supervisor of the fire department here. I'll give instructions if needed";
            chatA[1] = "Supervisor: We are here to help find this missing child. Who is very likely near this location";
            chatA[2] = "Supervisor: Now let's not faste time here. We also have trained dogs for search and rescue, It's good to use them to locate that kid.";
        }
        //dialog cycle
        if (timeRemaining < 0)
        {
            if (dialog.activeSelf == true)
            {
                chatInput.text = chatA[count];
            }

            count = (count + 1) % chatA.Length; // Increment count and reset to 0 when it reaches the last index
            timeRemaining = 6.0f;
        }
        if (dialog.activeSelf == false)
        {
            dialog.SetActive(true);
            chatInput.text = chatA[count];
        }
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

    }
    void FaceDirection(GameObject kohde)
    {
        float offset = 0f;
        Vector3 direction = kohde.transform.position - transform.position;
        //direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        //Debug.Log("angle " + angle);
    }

    // Update is called once per frame
    void Update()
    {
        if (bubble.activeSelf == true)
        {
            //scale speech bubble up and down
            Vector3 vec = new Vector3(Mathf.Sin(Time.time * 0.8f), Mathf.Sin(Time.time * 0.8f), Mathf.Sin(Time.time * 0.8f));
            bubble.transform.localScale = vec;
        }
        if (talk == 1)
        {
            Collisiondetection();
            //talktoPlayer if player is close;
        }
    }
}
