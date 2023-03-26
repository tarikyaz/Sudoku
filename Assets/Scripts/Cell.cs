using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    public Item[] ItemsArray = new Item[9];
    public void Init(int itemSize)
    {
        float itemPadding = (float)(gridLayoutGroup.spacing.magnitude / gridLayoutGroup.cellSize.magnitude);
        itemPadding *= itemSize;
        itemSize = Mathf.FloorToInt( itemSize- itemPadding);
        itemPadding = Mathf.FloorToInt(itemPadding);
        gridLayoutGroup.cellSize = new Vector2(itemSize, itemSize);
        gridLayoutGroup.spacing = new Vector2(itemPadding, itemPadding);
    }
}
