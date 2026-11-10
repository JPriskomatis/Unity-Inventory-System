using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;

    public int InventorySize => inventorySlots.Count;

    //Event that gets fired when we add an item to the inventory;
    public UnityAction<InventorySlot> OnInventorySlotChanged;

    //We make a new inventory system and we pass the size of that system;
    public InventorySystem(int size)
    {
        
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            //Makes enough inventory slots to fill the system;
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if(ContainsItem(itemToAdd, out List<InventorySlot> invSlot))  //Checks whether the item already exists in the inventory;
        {
            foreach(var slot in invSlot)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
            
        }
        else if(HasFreeSlot(out InventorySlot freeSlot))    //Gets the first available slot;
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot)
    {
        //checks all our inventory slots, and it creates a list of inventorySlots to fill it, where the itemData is equal to the item we want to add;
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();

        //if it contains the item it returns true, otherwise false;
        return inventorySlots.Count > 1 ? true : false;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = null;
        return false;
    }
}
