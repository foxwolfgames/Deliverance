using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; set; }
    [SerializeField] private List<GameObject> inventorySlots;
    [SerializeField] private GameObject activeInventorySlot;

    public int ammo = 20;

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

    private void Start () {
        activeInventorySlot = inventorySlots[0];
    }

    private void Update()
    {
        // making sure slot get set inactive when active slot changes
        foreach (GameObject slot in inventorySlots) {
            if (slot == activeInventorySlot) 
            {
                slot.SetActive(true);
            }
            else
            {
                slot.SetActive(false);
            }
        }

        // TODO: refactor into input manager and refactor if statements to be better
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchInventorySlots(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchInventorySlots(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchInventorySlots(2);
        }
    }

    public void PickupObject(GameObject item) 
    {
        // add to inventory
        AddInteractableToInventory(item);
    }

    private void AddInteractableToInventory(GameObject item)
    {
        // TODO: check if there isn't already an item in the firstaid slot
        if (item.GetComponent<FirstAidKit>())
        {
            item.transform.SetParent(inventorySlots[2].transform, false);

            // TODO: remove first aid kit from original position and add to inventory slot
            print("First aid kit added to inventory");
        }

        if (item.GetComponent<AmmoBox>())
        {
            ammo += 200;
        }
    }

    // Switch between inventory slots function
    private void SwitchInventorySlots(int slotIndex)
    {
        // checking to see if weapon is in active slot and disable it if it is
        if (activeInventorySlot.transform.childCount > 0)
        {
            GameObject activeObject = activeInventorySlot.transform.GetChild(0).gameObject;
            if (activeObject.GetComponent<Weapon>()) {
                activeObject.GetComponent<Weapon>().isActiveWeapon = false;
            }
        }
        activeInventorySlot = inventorySlots[slotIndex];

        // checking to see if weapon is in new active slot and enable it if it is
        if (activeInventorySlot.transform.childCount > 0)
        {
            GameObject activeObject = activeInventorySlot.transform.GetChild(0).gameObject;
            if (activeObject.GetComponent<Weapon>()) {
                activeObject.GetComponent<Weapon>().isActiveWeapon = true;
            }
        }
    }

    // Ammo
    public void UpdateAmmo(int amount)
    {
        ammo += amount;
        if (ammo < 0)
        {
            ammo = 0;
        }
    }
    public int CheckAmmoLeft()
    {
        // We don't have other weapon models, so this is a placeholder in case we evolve our game with multiple weapon models
        return ammo;
    }

}
