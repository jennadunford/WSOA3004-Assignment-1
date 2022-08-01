using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eightDirectionMovement : MonoBehaviour
{
    public Rigidbody2D playerBody;

    public SpriteRenderer playerSprites;

    public Sprite fSprite;
    public Sprite bSprite;
    public Sprite lSprite;
    public Sprite rSprite;
    public Sprite flSprite;
    public Sprite frSprite;
    public Sprite blSprite;
    public Sprite brSprite;

    public float walkSpeed;

    Vector2 direction;

    //https://www.youtube.com/watch?v=NQN3rYGqqP8&ab_channel=AdamCYounis

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        playerBody.velocity = direction * walkSpeed;

        flip();

        setSprite();
      
   

        
    }

    void flip()
    {
        if (!playerSprites.flipX && direction.x < 0)
        {
            playerSprites.flipX = true;
        }
        else
       if (playerSprites.flipX && direction.x > 0)
        {
            playerSprites.flipX = false;
        }
    }

    void setSprite()
    {
        Sprite selectedSprite = null;
        if(direction.y > 0) //back
        {
            if(Mathf.Abs(direction.x) >0) //right or left
            {
                selectedSprite = brSprite;
                playerSprites.sprite = selectedSprite;
            }
            else
            {
                selectedSprite = bSprite;
                playerSprites.sprite = selectedSprite;
            }


        }else if(direction.y < 0) //forward
        {
            if (Mathf.Abs(direction.x) > 0) //right or left
            {
                selectedSprite = frSprite;
                playerSprites.sprite = selectedSprite;
            }
            else
            {
                selectedSprite = fSprite;
                playerSprites.sprite = selectedSprite;
            }

        }
        else //neutral
        {
           
                selectedSprite = rSprite;
            playerSprites.sprite = selectedSprite;


        }
        
    }
}
