using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private UIDocument uiDocument;
    private VisualElement rootElement;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeUIDocument();
    }

    private void InitializeUIDocument()
    {
        if (uiDocument == null)
        {
            uiDocument = GetComponent<UIDocument>();
        }

        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component not found on UIController GameObject!");
            return;
        }

        rootElement = uiDocument.rootVisualElement;
        if (rootElement == null)
        {
            Debug.LogError("Root visual element not found!");
            return;
        }
    }

    public void SetDataSource(object dataSource)
    {
        if (rootElement != null)
        {
            rootElement.dataSource = dataSource;
        }
    }

    public VisualElement GetRootElement()
    {
        return rootElement;
    }

    public UIDocument GetUIDocument()
    {
        return uiDocument;
    }
}
