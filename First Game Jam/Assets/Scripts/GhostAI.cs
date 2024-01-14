using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class script : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] Vector2 respawn;
    private BoolHolder boolHolder;
    public float despawnDistance;
    private Rigidbody2D rb;
    public float speed = 5;
    public int respawnDelay = 500;
    private int respawnTimer = 0;
    private char direction = 'n'; //u = up, d = down, l = left, r = right, n = none
    private char prevDirection = 'n';
    private bool targetingPlayer = false;
    private float baseSpeed;
    private int currentLives;
    private int iFrames;
    private bool defeated;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boolHolder = GetComponentInParent<BoolHolder>();
        baseSpeed = boolHolder.playerSpeed;
        boolHolder.playerFrozen = false;
        iFrames = 0;
        currentLives = boolHolder.ghostLifeCount;
        defeated = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (defeated)
        {
            return;
        }
        //checks if ghost got hit
        if (Mathf.Abs(Vector3.Distance(player.position, transform.position)) < boolHolder.maxOuterLight && iFrames == 0 & boolHolder.fwoosh)
        {
            currentLives--;
            iFrames = 250;

            //Debug.Log("hit ghost");
            if(currentLives == 0)
            {
                transform.position = new Vector3(100, 100, 100);
                defeated = true;
                boolHolder.defeatedCount++;
            }
        }

        else if (iFrames > 0)
        {
            iFrames--;
            rb.velocity = new Vector2(rb.position.x - player.position.x, rb.position.y - player.position.y).normalized * speed;
            if(iFrames == 0)
            {
                respawnTimer = respawnDelay;
            }
            return;
        }

        RaycastHit2D upRay;
        RaycastHit2D downRay;
        RaycastHit2D leftRay;
        RaycastHit2D rightRay;
        if (respawnTimer > 0)
        {
            respawnTimer++;
            
            //player's distance from designated respawn zone
            float distance = Mathf.Sqrt(Mathf.Pow(respawn.x - player.position.x, 2) + Mathf.Pow(respawn.y - player.position.y, 2));

            if(respawnTimer >= respawnDelay && distance > 3f)
            {
                //checking if the player is right next to the respawn.
                //dont respawn the ghost if they are
                upRay = Physics2D.Raycast(respawn, Vector2.up);
                downRay = Physics2D.Raycast(respawn, Vector2.down);
                leftRay = Physics2D.Raycast(respawn, Vector2.left);
                rightRay = Physics2D.Raycast(respawn, Vector2.right);
                //check if one of the rays hit the player
                if (upRay.rigidbody == player || downRay.rigidbody == player
                    || leftRay.rigidbody == player || rightRay.rigidbody == player)
                {
                    return;
                }
                transform.position = new Vector3(respawn.x, respawn.y, transform.position.z);
                respawnTimer = 0;
            }
            return;
        }
        float dirValue = Random.value;
        int cycleCount = 0;

        //cast a ray in each direction
        upRay = Physics2D.Raycast(transform.position, Vector2.up);
        downRay = Physics2D.Raycast(transform.position, Vector2.down);
        leftRay = Physics2D.Raycast(transform.position, Vector2.left);
        rightRay = Physics2D.Raycast(transform.position, Vector2.right);

        //check if one of the rays hit the player
        if (upRay.rigidbody == player && upRay.distance < (despawnDistance * 2 / 3)
            || downRay.rigidbody == player && downRay.distance < (despawnDistance * 2 / 3)
            || leftRay.rigidbody == player && leftRay.distance < (despawnDistance * 2 / 3)
            || rightRay.rigidbody == player && rightRay.distance < (despawnDistance * 2 / 3))
        {
            targetingPlayer = true;
        }

        while (direction == 'n' && !targetingPlayer)
        {

            float distance;
            if (dirValue < .25f)
            {
                direction = 'u';
                distance = upRay.distance;
            }
            else if (dirValue < .5f)
            {
                direction = 'd';
                distance = downRay.distance;
            }
            else if (dirValue < .75f)
            {
                direction = 'l';
                distance = leftRay.distance;
            }
            else
            {
                direction = 'r';
                distance = rightRay.distance;
            }

             if (distance < 1f || direction == prevDirection)
            {
                direction = 'n';
                dirValue += .25f;
                if (dirValue > 1f) dirValue = 0f;
                if (++cycleCount == 4) direction = prevDirection;
            }
        }

        //targeting player. moving straight towards them
        if (targetingPlayer || boolHolder.playerFrozen)
        {
            float xDistance = player.position.x - transform.position.x,
                  yDistance = player.position.y - transform.position.y,
                  totalDistance = Mathf.Sqrt(xDistance*xDistance + yDistance*yDistance);

            if (boolHolder.playerFrozen)
            {
                rb.velocity = new Vector2(xDistance, yDistance).normalized * speed;
                return;
            }

            //player escapes ghost
            if (totalDistance >= despawnDistance)
            {
                respawnTimer = 1;
                targetingPlayer = false;
                transform.position = new Vector3(100f, 100f, transform.position.z);
            }

            //ghost catches player
            else if (totalDistance < 1f)
            {
                respawnTimer = 1;
                targetingPlayer = false;
                transform.position = new Vector3(100f, 100f, transform.position.z);
                //update fear bar
                boolHolder.fearSlider += .25f;
                //reduce player's speed
                boolHolder.playerSpeed =  baseSpeed * (1 - boolHolder.fearSlider);
                
                //stuff that happens when player is hit
                //we're talking SFX. ANIMATIONS.
            }
            else
            {
                rb.velocity = new Vector2(xDistance, yDistance).normalized * speed;
            }
            return;
        }

        //check if ghost hit the wall yet
        //move towards wall
        if (direction == 'u')
        {
            if (Physics2D.Raycast(transform.position, Vector2.up).distance < 1f)
            {
                prevDirection = 'd';
                direction = 'n';
            }
            else rb.velocity = new Vector2(0f, speed);
        }
        else if (direction == 'd')
        {
            if (Physics2D.Raycast(transform.position, Vector2.down).distance < 1f)
            {
                prevDirection = 'u';
                direction = 'n';
            }
            else rb.velocity = new Vector2(0f, -1 * speed);
        }
        else if (direction == 'l')
        {
            if (Physics2D.Raycast(transform.position, Vector2.left).distance < 1f)
            {
                prevDirection = 'r';
                direction = 'n';
            }
            else rb.velocity = new Vector2(-1 * speed, 0f);
        }
        else /*direction == 'r'*/
        {
            if (Physics2D.Raycast(transform.position, Vector2.right).distance < 1f)
            {
                prevDirection = 'l';
                direction = 'n';
            }
            else rb.velocity = new Vector2(speed, 0f);
        }
    }
}