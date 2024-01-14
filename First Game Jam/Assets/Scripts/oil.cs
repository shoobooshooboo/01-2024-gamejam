using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oil : MonoBehaviour
{
    public Rigidbody2D player;
    public Vector2 position1;
    public Vector2 position2;
    private BoolHolder boolHolder;
    private bool atPosition1;

    // Start is called before the first frame update
    void Start()
    {
        boolHolder = GetComponentInParent<BoolHolder>();
        float rand = Random.value;
        if(rand < .5f)
        {
            transform.position = position1 + new Vector2(-3, 12.4f);
            atPosition1 = true;
        }
        else
        {
            transform.position = position2 + new Vector2(-3, 12.4f);
            atPosition1 = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(player.position, new Vector2(transform.position.x, transform.position.y)) < .5f)
        {
            boolHolder.oilCount++;
            if (atPosition1)
            {
                transform.position = position2 + new Vector2(-3, 12.4f);
                atPosition1 = false;
            }
            else
            {
                transform.position = position1 + new Vector2(-3, 12.4f);
                atPosition1 = true;
            }
        }
    }
}
