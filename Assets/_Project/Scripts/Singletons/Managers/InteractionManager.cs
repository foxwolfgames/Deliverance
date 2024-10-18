using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public GameObject interactableObject = null;

    private void Awake()
    {
        if (Instance != null && Instance!= this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            if (objectHitByRaycast.GetComponent<Outline>())
            {
                interactableObject = objectHitByRaycast;
                interactableObject.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F) && interactableObject)
                {
                InventoryManager.Instance.PickupObject(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                if (interactableObject)
                {
                    interactableObject.GetComponent<Outline>().enabled = false;
                    interactableObject = null;
                }
            }
        }

    }
}
