using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//imported stuff
public class PlayerController : MonoBehaviour
{
    //all variables used by the code
    public float speed;
    public GameObject camera;
    public GameObject frontdoor;
    public GameObject lockedDoor1;
    public GameObject trickBookShelf;
    public float lookSpeed;

    private static string INTERACTABLE_TAG = "Interactable";
    private Rigidbody rb;
    private float spin;
    private bool[] buttonStatus; 

    public Text resultText;
    public Text timeRemainingText;
    public Text playerText;
    public float totalTime = 300;

    private float currentTime;
    private float timeRemaining;
    private bool gameDone = false;
    private LostReason endReason;

    private Ray ray;
    private RaycastHit hit;

    private enum LostReason
    {
        fall,
        time,
        enemy
    }
    //initialisation of certain variables, eg. the timer
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerText.text = "";
        resultText.text = "";
        currentTime = 0;
        buttonStatus = new bool[5];
        
    }

    void FixedUpdate()
    {
        if (!gameDone)
        {
            UpdateTime();
            DetermineResult();
        }
    }
    //detects what the player is looking at, and does stuff based on that. Happens once every game update
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        playerText.text = "";
        if (Physics.Raycast(ray, out hit))
        {
            GameObject rayobject = hit.collider.gameObject;
            print(hit.collider.name);
            if (rayobject.CompareTag(INTERACTABLE_TAG))
            {

                switch (rayobject.name)
                {
                    case "test1":
                        {
                            print("Is Test1");
                            break;
                        }
                    case "button1":
                        {
                            if (Input.GetKey(KeyCode.E))
                            {
                                print("Button 1");
                                SetButton(0);
                                DoorRotate rotator = (DoorRotate)lockedDoor1.AddComponent<DoorRotate>();
                                rotator.finalDegree = 90;
                                playerText.text = "What was that noise?";
                            }

                            break;
                        }
                    case "button2":
                        {
                            print("Button 2");
                            if (Input.GetKey(KeyCode.E))
                            {
                                SetButton(1);
                                if (buttonStatus[1])
                                {
                                    playerText.text = "A noise came from upstairs!";
                                    if (trickBookShelf != null)
                                    {
                                        Destroy(trickBookShelf);
                                    }

                                }
                            }

                            break;
                        }
                    case "button3":
                        {
                            print("Button 3");
                            SetButton(2);
                            break;
                        }
                    case "button4":
                        {
                            print("Button 4");
                            SetButton(3);
                            break;
                        }
                    case "button5":
                        {
                            print("Button 5");
                            SetButton(4);
                            break;
                        }

                    case "test2":
                        {
                            print("Is Test2");
                            break;
                        }
                    case "Button1Door":
                        {
                            if (Input.GetKey(KeyCode.E))
                            {
                                playerText.text = "I can't open this door like this...";
                            }

                            break;
                        }
                    case "FrontDoor":
                        {

                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                DoorRotate rotator = (DoorRotate) rayobject.GetComponent<DoorRotate>();
                                if (rotator == null)
                                {
                                    rotator = (DoorRotate) rayobject.AddComponent<DoorRotate>();
                                    rotator.finalDegree = 90;
                                }

                                else 
                                {
                                    rotator.ChangeDirection();
                                }

                            }
                        break;
                        }

                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //print("e");
                }
            }
        }
    }


    void LateUpdate()
    {
        if (!gameDone)
        {
            PlayerMovement();
        }

    }
    //timer function for the screen
    private void UpdateTime ()
    {
        int prettyTime = 0;

        currentTime = currentTime + Time.deltaTime;
        timeRemaining = totalTime - currentTime;
        if(timeRemaining > 0)
        {
            prettyTime = (int)System.Math.Ceiling(timeRemaining);
        }
        timeRemainingText.text = "Time Remaining: " + prettyTime.ToString();
    }
    //determines the result of the game using hasWon(), and hasLost()
    private void DetermineResult ()
    {
        if (hasWon())
        {
            resultText.text = "Victory!!";
            gameDone = true;
        }
        if (hasLost())
        {
  
            switch (endReason)
            {
                case LostReason.time:
                    {
                        resultText.text = "You Ran Out Of Time!";
                        break;
                    }
                case LostReason.fall:
                    {
                        resultText.text = "You Fell Out!";
                        break;
                    }
                case LostReason.enemy:
                    {
                        resultText.text = "An Enemy Killed You!";
                        break;
                    }
            }

            gameDone = true;
        }
    }
    //returns whether or not the player has won or not
    private bool hasWon()
    {
        return buttonStatus[4];
    }  
    //returns whether or not the player has lost, based on things such as dying from an enemy, and falling out of the world
    private bool hasLost()
    {
        bool hasLost = false;
        if (timeRemaining <= 0)
        {
            endReason = LostReason.time;
            hasLost = true;
        }

        else if (rb.position.y <= -10)
        {
            endReason = LostReason.fall;
            hasLost = true;
        }

        else if (endReason == LostReason.enemy)
        {
            hasLost = true;
        }
        return hasLost;
    }
    //controls player movement
    private void PlayerMovement()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        Vector3 offset = new Vector3(moveHoriz, 0, moveVert);
        rb.AddForce(offset * speed);
        float step = lookSpeed * Time.deltaTime;
        camera.transform.rotation = rb.transform.rotation;
    }
    //controls the order of the buttons used to progress the game
    private void SetButton(int buttonIndex)
    {
        if (buttonIndex == 0 || buttonStatus[buttonIndex - 1])
        {
            buttonStatus[buttonIndex] = true;
        }
    }

}
