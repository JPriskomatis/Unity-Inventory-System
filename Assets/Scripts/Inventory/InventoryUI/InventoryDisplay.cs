using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlotUI, InventorySlot> slotDictionary;


    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlotUI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach(var slot in SlotDictionary)
        {
            if(slot.Value == updatedSlot)   //This is basically the inventory slot in the backend;
            {
                slot.Key.UpdateUISlot(updatedSlot);    //This is the slot key, the UI repressentation of the value;
            }
        }
    }

    public void SlotClicked(InventorySlotUI clickedUISlot)
    {
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;


        //Clicked slot has an item and the mouse doesn't have an item, we need to pick up that item;
        if(clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.assignedInventorySlot.ItemData == null)
        {
            //Check if the player is holding shift key, then we want to split the stack;
            if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot)) //Split the stack
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            }
            else
            {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();

                return;
            }

        }

        //If the clicked slot doesnt have an item and the mouse does have one, then place the mouse item into the empty slot;

        if(clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.assignedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.assignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearShot();
            return;
        }

        //Check if both slots have an item, and if they do decide what to do;
            //If both items are the same, then combine them
                //Is the slot stack size + mouse stack size > slot Max Stack Size? if so, take from mouse.
            //If they are different, then swap the items;

        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.assignedInventorySlot.ItemData != null)
        {
            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.assignedInventorySlot.ItemData;

            if (isSameItem && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.assignedInventorySlot.StackSize))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.assignedInventorySlot);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.ClearShot();
            } 
            else if(isSameItem && !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.assignedInventorySlot.StackSize, out int leftInTheStack))
            {
                if (leftInTheStack < 1) SwapSlots(clickedUISlot);   //Stack is full, so swap the items;
                else //Slow is not at max, so take whats needed from the mouse inventory;
                {
                    int remainingOnMouse = mouseInventoryItem.assignedInventorySlot.StackSize - leftInTheStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInTheStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.assignedInventorySlot.ItemData, remainingOnMouse);
                    mouseInventoryItem.ClearShot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                    return;
                }

            }
            else if(!isSameItem)
            {
                SwapSlots(clickedUISlot);
                return;
            }
        }
    }

    private void SwapSlots(InventorySlotUI clickedUISlot)
    {
        var clonedSlot = new InventorySlot(mouseInventoryItem.assignedInventorySlot.ItemData, mouseInventoryItem.assignedInventorySlot.StackSize);
        mouseInventoryItem.ClearShot();

        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.ClearSlot();

        clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }

}
