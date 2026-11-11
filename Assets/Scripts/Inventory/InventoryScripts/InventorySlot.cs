using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemData itemData;    //Reference to the data of what makes an item;
    [SerializeField] private int stackSize;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(InventoryItemData source, int amount)  //Constructor to make an occupied inventory slot;
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot()  //This constructor makes an empty inventory slot;
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1; //Essentially thats null as well;
    }

    public void AssignItem(InventorySlot invSlot)   //Assigns an item to the slot;
    {
        if (itemData == invSlot.itemData)   //Checks if the slot contains the same item we try to assign
        {
            AddToStack(invSlot.stackSize);
        }
        else
        {
            //We overwrite the slot with the inventory slot that we're passing in;
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }
    public void UpdateInventorySlot(InventoryItemData data, int amount) //Updates the slot directly;
    {
        itemData = data;
        stackSize = amount;
    }

    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = itemData.maxStackSize - stackSize;

        return EnoughRoomLeftInStack(amountToAdd);
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        
        if(itemData == null || itemData != null && stackSize + amountToAdd <= itemData.maxStackSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if (stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(itemData, halfStack);    //Creates a copy of this slot with half the stack size;
        return true;
    }
}
