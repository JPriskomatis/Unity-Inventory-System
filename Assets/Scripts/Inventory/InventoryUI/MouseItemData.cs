using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseItemData : MonoBehaviour
{
    public Image itemSprite;
    public TextMeshProUGUI itemCount;

    private void Awake()
    {
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }
}
