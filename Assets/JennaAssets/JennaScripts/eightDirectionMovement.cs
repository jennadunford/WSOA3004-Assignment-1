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

        // playerBody.velocity = direction * walkSpeed;

        //flip();

        //setSprite();
        // setSpriteIso();

        rotateForMovement();



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
        if(direction.y > 0) //up
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


        }else if(direction.y < 0) //down
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
           
            selectedSprite = frSprite;
            playerSprites.sprite = selectedSprite;


        }
        
    }

    void setSpriteIso()
    {
        Sprite selectedSprite = null;
        if(direction.y > 0) //up
        {
            if((direction.x) > 0) //right or left
            {
                //Debug.Log("straight up");
                selectedSprite = bSprite;
            
                playerSprites.sprite = selectedSprite;
            }
            else if ((direction.x) < 0)
            {
                //Debug.Log("straight left");
                selectedSprite = lSprite;
            
                playerSprites.sprite = selectedSprite;
            }
            else
            {
                //Debug.Log("up left");
                selectedSprite = blSprite;
                playerSprites.sprite = selectedSprite;
            }
        }else if(direction.y < 0) //down
        {
            if ((direction.x) > 0) //right or left
            {
                //Debug.Log("straight right");

                selectedSprite = rSprite;
            
                playerSprites.sprite = selectedSprite;
            }
            else if ((direction.x) < 0)
            {
                //Debug.Log("straight down");
                selectedSprite = fSprite;

                playerSprites.sprite = selectedSprite;
            }else
            {
                Debug.Log("down right");
                selectedSprite = frSprite;
                playerSprites.sprite = selectedSprite;
            }

        }
        if(direction.x == -1 && direction.y == 0)
        {
            //down and left
            selectedSprite = flSprite;
            playerSprites.sprite = selectedSprite;
        }
        if(direction.x == 1 && direction.y == 0)
        {
            //Up right
            selectedSprite = brSprite;
            playerSprites.sprite = selectedSprite;
        }
      

    }

    void rotateForMovement()
    {
        Vector3 rotation;
       
        if (direction.y > 0) //up
        {
            if ((direction.x) > 0) //right or left
            {
                rotation = new Vector3(0, 0, 90);
                //Debug.Log("straight up");
               // transform.Rotate(rotation* Time.deltaTime);
                playerSprites.transform.rotation = Quaternion.Euler(rotation);


            }
            else if ((direction.x) < 0)
            {
                //Debug.Log("straight left");
                rotation = new Vector3(0, 0, 180);
               // transform.Rotate(rotation * Time.deltaTime);
                playerSprites.transform.rotation = Quaternion.Euler(rotation);
            }
            else
            {
                //Debug.Log("up left");
                rotation = new Vector3(0, 0, 135);
                //transform.Rotate(rotation * Time.deltaTime);
                playerSprites.transform.rotation = Quaternion.Euler(rotation);

            }
        }
        else if (direction.y < 0) //down
        {
            if ((direction.x) > 0) //right or left
            {
                //Debug.Log("straight right");
                rotation = new Vector3(0, 0, 0);
                //transform.Rotate(rotation * Time.deltaTime);
                playerSprites.transform.rotation = Quaternion.Euler(rotation);

            }
            else if ((direction.x) < 0)
            {
                //Debug.Log("straight down");
                rotation = new Vector3(0, 0, 270);
                //transform.Rotate(rotation * Time.deltaTime);
                playerSprites.transform.rotation = Quaternion.Euler(rotation);


            }
            else
            {
                //Debug.Log("down right");
                rotation = new Vector3(0, 0, 315);
                //transform.Rotate(rotation * Time.deltaTime);
                playerSprites.transform.rotation = Quaternion.Euler(rotation);

            }

        }
        if (direction.x == -1 && direction.y == 0)
        {
            //down and left
            rotation = new Vector3(0, 0, 225);
            //transform.Rotate(rotation * Time.deltaTime);
            playerSprites.transform.rotation = Quaternion.Euler(rotation);

        }
        if (direction.x == 1 && direction.y == 0)
        {
            //Up right
            rotation = new Vector3(0, 0, 45);
           // transform.Rotate(rotation * Time.deltaTime);
            playerSprites.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
