using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;
using System;

public class ButtonScript : MonoBehaviour
{
    public event Action OnButtonClicked;

    [SerializeField] private TextMeshPro title;
    [SerializeField] private Interactable interactable;

    public void Initialize(NavPoint navPoint)
    {
        title.text = navPoint.Name;
        interactable.OnClick.AddListener(ProccessClick);
    }

    public void ProccessClick()
    {
        OnButtonClicked?.Invoke();
    }
}
