using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class CustomBar : VisualElement
{
    [UxmlAttribute, CreateProperty]
    public int value;

    [UxmlAttribute, CreateProperty]
    public int minValue;

    [UxmlAttribute]
    public int maxValue;

    [UxmlAttribute, CreateProperty]
    public string label;


    public bool editable = false;


    readonly VisualElement track;
    readonly VisualElement fill;
    readonly Label textLabel;
    

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

        // TEXT
        textLabel = new Label
        {
            name = "text",
            text = label
        };
        textLabel.AddToClassList("custom-bar__text");
        textLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
        textLabel.style.position = Position.Absolute;
        textLabel.style.left = 0;
        textLabel.style.right = 0;
        textLabel.style.top = 0;
        textLabel.style.bottom = 0;
        track.Add(textLabel);


        RegisterCallback<GeometryChangedEvent>(_ => UpdateFill());
        RegisterCallback<AttachToPanelEvent>(_ => UpdateFill());

        UpdateFill();
    }

    void UpdateFill()
    {
        float percent = (float)value / maxValue;
        fill.style.width = Length.Percent(percent * 100f);
        textLabel.text = label;
    }
}