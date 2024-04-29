using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// TODO: classes must pass parameters, when it need, and update, delete, or add new parameters info
public class CharacterDebugIController : MonoBehaviour
{
    [SerializeField] 
    private VisualTreeAsset labeledFieldElement;

    private GroupBox _groupBox;

    private readonly List<(Label name, Label value)> _labeledFields = new();
    

    private void InstantiateLabeledField()
    {
        var a = labeledFieldElement.Instantiate();
        _groupBox.Add(a);

        (Label name, Label value) b = (a.Q<Label>("ParameterName"), a.Q<Label>("ParameterValue"));
        _labeledFields.Add(b);
    }
}
