using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FortuneWheelManager : MonoBehaviour
{
    private bool isStarted;//isWheelRotating
    private float[] sectorsAngles;
    private float finalAngle;
    private static float startAngle = 0;
    private float currentRotationTime;
    public Button TurnButton;
    public GameObject Circle; 			// Rotatable Object with rewards

    //Swipe control variables
    Vector3 startPosition, endPosition;
    float swipeHeight, minimumSwipeHeight;

    void Start()
    {
        //Higher the value,Longer will be minimum swipe height
        minimumSwipeHeight = 45f;
    }


    public static void GiveAwardByAngle ()
    {
    	// Here you can set up rewards for every sector of wheel
    	switch (Mathf.RoundToInt(startAngle)) {
            case -30:
                GameManager.setNextBallScore(0);
                break;
            case -90:
                GameManager.setNextBallScore(6);
                break;
            case -150:
                GameManager.setNextBallScore(4);
                break;
            case -210:
                GameManager.setNextBallScore(3);
                break;
            case -270:
                GameManager.setNextBallScore(2);
                break;
            case -330:
                GameManager.setNextBallScore(1);
                break;
        }
    }

    void Update ()
    {
    	if (!isStarted)
    	    return;

    	float maxRotationTime = 4f;
    
    	// increment timer once per frame
    	currentRotationTime += Time.deltaTime;

        //Stop rotation if rotation time goes greater than maximum or reaches final angle
    	if (currentRotationTime > maxRotationTime || Circle.transform.eulerAngles.z == finalAngle) {
    	    currentRotationTime = maxRotationTime;
    	    isStarted = false;
    	    startAngle = finalAngle % 360;
    
    	    GiveAwardByAngle ();
    	}
    
    	// Calculate current position using linear interpolation
    	float t = currentRotationTime / maxRotationTime;
    
    	// This formulae allows to speed up at start and speed down at the end of rotation.
    	// Try to change this values to customize the speed
    	t = t * t * t * (t * (6f * t - 15f) + 10f);
    
    	float angle = Mathf.Lerp (startAngle,finalAngle, t);
    	Circle.transform.eulerAngles = new Vector3 (0, 0, angle);
    }
    

    //Button or swipe calls this function
    public void TurnWheel()
    {
        currentRotationTime = 0f;

        // Fill the necessary angles (for example if you want to have 6 sectors you need to fill the angles with 60 degrees step and -30 as a padding)
        sectorsAngles = new float[] { 30, 90, 150, 210, 270, 330 };

        //How many times to rotate
        int fullCircles = 5;

        //Which angle to finish rotation
        float randomFinalAngle = sectorsAngles[Random.Range(0,sectorsAngles.Length)];

        // Here we set up how many circles our wheel should rotate before stop
        finalAngle = -(fullCircles * 360 + randomFinalAngle);
        
        //Start the rotation
        isStarted = true;
    }

    public void dragStart()
    {
        startPosition = Input.mousePosition;
    }

    public void dragEnd()
    {
        endPosition = Input.mousePosition;
        swipeHeight = startPosition.y - endPosition.y;
        if(swipeHeight>=minimumSwipeHeight)
        {
            Invoke("TurnWheel",0.0000001f);//2nd argument is time delay to execute function,0 causes bug
        }
    }
}
