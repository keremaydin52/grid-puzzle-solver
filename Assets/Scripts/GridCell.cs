using TMPro;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public static Pooler gridCellPool;

    private int value;
    public int Value 
    {  
        get { return value; }
        set 
        { 
            this.value = value;
            text.text = value.ToString();
        }
    }
}
