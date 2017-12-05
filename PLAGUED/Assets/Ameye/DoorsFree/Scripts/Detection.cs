using UnityEngine.UI;
using UnityEngine;

public class Detection : MonoBehaviour
{
    // GENERAL SETTINGS
    [Header("General Settings")]
    [Tooltip("How close the player has to be in order to be able to open/close the door.")]
    public float Reach = 4.0F;
    [HideInInspector] public bool InReach;
    public string Character = "e";

    // UI SETTINGS
    [Header("UI Settings")]
    [Tooltip("The image or text that will be shown whenever the player is in reach of the door.")]
    public GameObject TextPrefab; // A text element to display when the player is in reach of the door
	public GameObject QuestPanel;
	public GameObject QuestManager;
    [HideInInspector] public GameObject TextPrefabInstance; // A copy of the text prefab to prevent data corruption
    [HideInInspector] public bool TextActive;


    // DEBUG SETTINGS
    [Header("Debug Settings")]
    [Tooltip("The color of the debugray that will be shown in the scene view window when playing the game.")]
    public Color DebugRayColor;
    [Tooltip("The opacity of the debugray.")]
    [Range(0.0F, 1.0F)]
    public float Opacity = 1.0F;

    void Start()
    {
        if (TextPrefab == null) Debug.Log("<color=yellow><b>No TextPrefab was found.</b></color>"); // Return an error if no text element was specified

        DebugRayColor.a = Opacity; // Set the alpha value of the DebugRayColor
    }

    void Update()
    {
        // Set origin of ray to 'center of screen' and direction of ray to 'cameraview'
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));

        RaycastHit hit; // Variable reading information about the collider hit

        // Cast ray from center of the screen towards where the player is looking
        if (Physics.Raycast(ray, out hit, Reach))
        {
			if (hit.collider.tag == "Door") 
			{
				InReach = true;

				// Display the UI element when the player is in reach of the door
				if (TextActive == false && TextPrefab != null) 
				{
					TextPrefabInstance = Instantiate (TextPrefab);
					TextActive = true;
					TextPrefabInstance.transform.SetParent (transform, true); // Make the player the parent object of the text element
				}

				// Give the object that was hit the name 'Door'
				GameObject Door = hit.transform.gameObject;

				// Get access to the 'Door' script attached to the object that was hit
				Door dooropening = Door.GetComponent<Door> ();

				if (Input.GetKey (Character)) 
				{
					//if door requires key, and quest finished then open
					if (dooropening.requiresKey && QuestManager.activeSelf)
					{
						//print (GameObject.Find ("QuestManager").GetComponent<QuestLog> ().questLog [0].GetComponent<Quest> ().state);
						if (GameObject.Find ("QuestManager").GetComponent<QuestLog> ().questLog [0].GetComponent<Quest> ().state == Quest.QuestState.COMPLETE) 
						{
							if (dooropening.RotationPending == false)
								StartCoroutine (hit.collider.GetComponent<Door> ().Move ());
						}
					} 
					else if(!dooropening.requiresKey)
					{ 
						//if just normal door and key is not needed
						if (dooropening.RotationPending == false)
							StartCoroutine (hit.collider.GetComponent<Door> ().Move ());
					}
				}
			} 
			else if (hit.collider.tag == "NPC") 
			{
				print ("NPC");
				InReach = true;

				if (TextActive == false && TextPrefab != null) 
				{
					TextPrefabInstance = Instantiate (TextPrefab);
					TextActive = true;
					TextPrefabInstance.transform.SetParent (transform, true);
				}

				if (Input.GetKey (Character)) 
				{
					Quest inactiveQuest = null;
					QuestPanel.SetActive (true);
					for (int i = 0; i < QuestManager.GetComponent<QuestLog> ().questLog.Length; i++) 
					{
						print (QuestManager.GetComponent<QuestLog> ().questLog [i].GetComponent<Quest> ().state);
						if (QuestManager.GetComponent<QuestLog> ().questLog [i].GetComponent<Quest> ().state == Quest.QuestState.ACTIVE) 
						{
							inactiveQuest = QuestManager.GetComponent<QuestLog> ().questLog [i].GetComponent<Quest>();
							break;
						}
					}
					QuestPanel.transform.Find ("QuestTitle").GetComponent<Text> ().text = inactiveQuest.name;
					QuestPanel.transform.Find ("QuestDescription").GetComponent<Text> ().text = inactiveQuest.description;
				}
			}
            else
            {
                InReach = false;

                // Destroy the UI element when Player is no longer in reach of the door
                if (TextActive == true)
                {
                    DestroyImmediate(TextPrefabInstance);
                    TextActive = false;
                }
            }
        }
        else
        {
            InReach = false;

            // Destroy the UI element when Player is no longer in reach of the door
            if (TextActive == true)
            {
                DestroyImmediate(TextPrefabInstance);
                TextActive = false;
            }
        }

        //Draw the ray as a colored line for debugging purposes.
        Debug.DrawRay(ray.origin, ray.direction * Reach, DebugRayColor);
    }
}
