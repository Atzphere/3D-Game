using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

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

                //print("Is Interactable");
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

    private bool hasWon()
    {
        return buttonStatus[4];
    }

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

    private void PlayerMovement()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        Vector3 offset = new Vector3(moveHoriz, 0, moveVert);
        rb.AddForce(offset * speed);
        float step = lookSpeed * Time.deltaTime;
        camera.transform.rotation = rb.transform.rotation;
    }

    private void SetButton(int buttonIndex)
    {
        if (buttonIndex == 0 || buttonStatus[buttonIndex - 1])
        {
            buttonStatus[buttonIndex] = true;
        }
    }





    }

// http://docs.unity3d.com/ScriptReference/Physics.Raycast.html
// http://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html
