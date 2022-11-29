using UnityEngine;
using UnityEngine.UIElements;
 
public class UiController : MonoBehaviour
{
    private Label label;
    private VisualElement frame;
    private Button button;
    public string NewText;

    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        var rootVisualElement = uiDocument.rootVisualElement;

        //frame = rootVisualElement.Q<VisualElement>("Frame");
        button = frame.Q<Button>("Random");

        button.RegisterCallback<ClickEvent>(ev => SetText());
    }

    public void SetText()
    {
        Debug.Log("ASSASAS");
    }
}