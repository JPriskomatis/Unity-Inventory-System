using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "InventorySystem/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int id;
    public string displayName;
    [TextArea(4,4)]
    public string description;
    public Sprite icon;
    public int maxStackSize;
}
