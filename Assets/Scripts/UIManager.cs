using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Grid Generation Panel Items")]
    [SerializeField] private TMP_InputField rowInput;
    [SerializeField] private TMP_InputField columnInput;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private Button createButton;
    [SerializeField] private Button solveButton;

    private bool isRowEven;
    private bool isColumnEven;

    public int RowNumber { get; private set; }
    public int ColumnNumber { get; private set; }

    private void Start()
    {
        rowInput.onValueChanged.AddListener(OnRowInputChange);
        columnInput.onValueChanged.AddListener(OnColumnInputChange);
    }

    private void OnRowInputChange(string inputText)
    {
        if (int.TryParse(inputText, out int number))
        {
            isRowEven = number % 2 == 0 && number > 0;
            RowNumber = number;
        }
        CreateButtonOnOff();
    }

    private void OnColumnInputChange(string inputText)
    {
        if (int.TryParse(inputText, out int number))
        {
            isColumnEven = number % 2 == 0 && number > 0;
            ColumnNumber = number;
        }
        CreateButtonOnOff();
    }

    // Make create and solve buttons interactable if both numbers are even
    private void CreateButtonOnOff()
    {
        createButton.interactable = isColumnEven && isRowEven;
        solveButton.interactable = isColumnEven && isRowEven;
    }
}
