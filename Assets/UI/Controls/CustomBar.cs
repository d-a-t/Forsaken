using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class CustomBar : VisualElement
{
    [UxmlAttribute]
    public float value { get; set;}

    [UxmlAttribute]
    public float minValue {get; set;}

    [UxmlAttribute]
    public float maxValue { get; set { m_LowValue = value; UpdateFill(); }}

    [UxmlAttribute]
    public string visualName { get; set;}

    public float lowValue
    {
        get => m_LowValue;
        set { m_LowValue = value; UpdateFill(); }
    }

    public float highValue
    {
        get => m_HighValue;
        set { m_HighValue = value; UpdateFill(); }
    }

    public float step
    {
        get => m_Step;
        set => m_Step = Mathf.Max(0f, value);
    }

    public bool editable = false;

    public float value
    {
        get => m_Value;
        set
        {
            float clamped = Mathf.Clamp(value, lowValue, highValue);

            // Snap to step if step > 0
            if (m_Step > 0f)
                clamped = Mathf.Round((clamped - lowValue) / m_Step) * m_Step + lowValue;

            m_Value = clamped;
            UpdateFill();
        }
    }

    readonly VisualElement track;
    readonly VisualElement fill;

    public CustomBar()
    {
        AddToClassList("custom-bar");

        // TRACK
        track = new VisualElement { name = "track" };
        track.AddToClassList("custom-bar__track");
        track.style.position = Position.Relative;
        hierarchy.Add(track);

        // FILL
        fill = new VisualElement { name = "fill" };
        fill.AddToClassList("custom-bar__fill");
        fill.style.position = Position.Absolute;
        track.Add(fill);

        RegisterCallback<GeometryChangedEvent>(_ => UpdateFill());
        RegisterCallback<AttachToPanelEvent>(_ => UpdateFill());

        RegisterCallback<PointerDownEvent>(OnPointer);
        RegisterCallback<PointerMoveEvent>(OnPointer);

        UpdateFill();
    }

    void OnPointer(IPointerEvent evt)
    {
        if (!editable || track.contentRect.width <= 0f)
            return;

        float percent = Mathf.Clamp01(evt.localPosition.x / track.contentRect.width);
        value = Mathf.Lerp(lowValue, highValue, percent);
    }

    void UpdateFill()
    {
        if (highValue <= lowValue)
            return;

        float percent = Mathf.InverseLerp(lowValue, highValue, m_Value);
        fill.style.width = Length.Percent(percent * 100f);
    }
}