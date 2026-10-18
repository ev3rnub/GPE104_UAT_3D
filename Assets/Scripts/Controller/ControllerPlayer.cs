//Course: GPE104 
//Prof: Matthew Henry 
//Student: Chad V

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class ControllerPlayer : MyController
{
    [Header("Camera")]
    private Camera cam; 

    [Header("KeyBinds")]
    public Key moveFwd = Key.W;
    public Key moveBak = Key.S;
    public Key rotateLeft = Key.A;
    public Key rotateRight = Key.D;
    public Key rotateUp = Key.R;
    public Key rotateDown = Key.V;
    public Key rotateShipRight = Key.E;
    public Key rotateShipLeft = Key.Q;
    public Key teleport = Key.T;
    public Key strafeLeft = Key.LeftArrow;
    public Key strafeRight = Key.RightArrow;
    public Key strafeUp = Key.UpArrow;
    public Key strafeDown = Key.DownArrow;
    public Key quitGame = Key.Escape;
    public Key shoot = Key.Space;
    public Key options = Key.O;
    public Key Brake = Key.X;

    [Header("Turbo")]
    public float turboMultiplier = 2f;
    public float someMaxWidth = Screen.width;
    public float someMaxHeight = Screen.height;

    //not sure where I should put this. 
    // oneshot sounds for teleport
    // [Header("Teleport Audio Settings")] //header for unity properties tab to set a category to keep organized. 
    // public AudioSource audioSource;
    // public AudioClip teleportSound;
    // public float soundVolume = 1f;
    // public bool optionsGUIvisible = false;
    // public GameObject optionsGUI;

    // like init, but called start!
    void Start()
    {
        cam = Camera.main; // get ref to main camera
        // optionsGUI.SetActive(false); //hide optionsGUI.
        if (GameManager.someGameManager != null)
        {
            GameManager.someGameManager.controllerPlayer = this;
        }
    }

    // called once per frame
    void Update()
    {
        if (somePawn != null)
        {
            // Check if either shift key is held
            bool turboHeld = Keyboard.current.leftShiftKey.isPressed
                          || Keyboard.current.rightShiftKey.isPressed;

            // check if W is being pressed, and if either of the shifts are being held.
            if (Keyboard.current[moveFwd].isPressed)
            {
                //Debug.Log("w was pressed!");
                // move fwd
                Vector3 direction = somePawn.transform.forward;
                if (turboHeld)
                {
                    somePawn.Move(direction * turboMultiplier);
                }
                else
                {
                    somePawn.Move(direction);    
                }
                
            }

            // check if s is beig pressed. 
            if (Keyboard.current[moveBak].isPressed)
            {
                //Debug.Log("s was pressed!");
                // move sprite, etc.
                somePawn.Move(-somePawn.transform.forward);
            }
            // check if a is being pressed.
            if (Keyboard.current[rotateLeft].wasPressedThisFrame)
            {
                //Debug.Log("a was pressed!");
                // move sprite, etc.
                somePawn.Rotate3D(-0.05f);
            }
            // check if d is being pressed.
            if (Keyboard.current[rotateRight].wasPressedThisFrame)
            {
                //Debug.Log("d was pressed!");
                // rotate.
                somePawn.Rotate3D(0.05f);
            }

            // check if R or V keys are pressed. 
            if (Keyboard.current[rotateUp].wasPressedThisFrame)
            {
                Debug.Log("R was pressed");
                somePawn.Pitch3D(-0.05f);
            }

            if (Keyboard.current[rotateDown].wasPressedThisFrame)
            {
                somePawn.Pitch3D(0.05f);
            }

            // check if Q or E is pressed.
            // ROLL right
            if (Keyboard.current[rotateShipRight].wasPressedThisFrame)
            {
                somePawn.Roll3D(-0.05f);
            }

            // ROLL left
            if (Keyboard.current[rotateShipLeft].wasPressedThisFrame)
            {
                somePawn.Roll3D(0.05f);
            }



            if (Keyboard.current[Brake].isPressed)
            {
                somePawn.Brake();
            }



            // check if escape was pressed.
            if (Keyboard.current[quitGame].isPressed)
            {
                //Debug.Log("ESCAPE was pressed!");
                Application.Quit();

                // If running inside the Unity Editor
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #endif
            }
            // shooting?
           if (Keyboard.current[shoot].wasPressedThisFrame)
            {
                Debug.Log("PEW PEW");
                somePawn.Shoot();
            }

            // check if teleport, T was pressed.
            // if (Keyboard.current[teleport].wasPressedThisFrame)
            // {
            //     //Debug.Log("T was pressed");
            //     //move at random
            //     // get screen/viewport resolution
            //     //Debug.Log($"Current Screen width/height = ({someMaxWidth}/{someMaxHeight})");
            //     // set random ranges from max width/height
            //     float randomPixelX = Random.Range(0f, someMaxWidth);
            //     float randomPixelY = Random.Range(0f, someMaxHeight);
            //     //Debug.Log($"X = {randomPixelX}");
            //     //Debug.Log($"Y = {randomPixelY}");

            //     Vector3 somePos = new Vector3(randomPixelX, randomPixelY, 0f);
            //     // 3. Convert screen (pixel) space → world space
            //     // z = 0 because we're in 2D, however worldPos is set to randomPixelX, randomPixelY, -10f due to the cam
            //     // IF i dont use ScreenToWorldPoint the sprite gets moved off of the screen.
            //     Vector3 worldPos = cam.ScreenToWorldPoint(
            //         new Vector3(randomPixelX, randomPixelY, 0f)
            //     );

            //     //Vector3 worldPos = new Vector3(randomPixelX, randomPixelY, 0f);

            //     //Debug.Log($"Current World Pos to move to: {worldPos}");

            //     // 4. Move the attached sprite via transform.
            //     worldPos.z = 0f; // [RESEARCH: "Why cam.ScreenToWorldPoint sets worldPos z to -10f, resetting it to 0."]
            //     //Debug.Log($"NEW World Pos to move to: {worldPos}");
            //     somePawn.Teleport(worldPos);
            //     audioSource.PlayOneShot(teleportSound, soundVolume);
            // }

            // check arrow keys for presses. Controls directional thrusters up down left right, straf
            if (Keyboard.current[strafeUp].isPressed)
            {
                somePawn.StrafeUp(0.5f);
                
            }
            // down
            if (Keyboard.current[strafeDown].isPressed)
            {
                somePawn.StrafeDown(0.5f);
                
            }
            // left
            if (Keyboard.current[strafeLeft].isPressed)
            {
                somePawn.StrafeLeft(0.5f);
                
            }        
            // right
            if (Keyboard.current[strafeRight].isPressed)
            {
                somePawn.StrafeRight(0.5f);
            }
            //Options window

            // if (Keyboard.current[options].wasPressedThisFrame)
            // {
            //     if (optionsGUIvisible == true)
            //     {
            //         optionsGUI.SetActive(false);
            //         optionsGUIvisible = false;
            //     }
            //     else
            //     {
            //         optionsGUI.SetActive(true);
            //         optionsGUIvisible = true;
            //     }
            // }
        }       
    }
}
