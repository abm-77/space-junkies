using System;
using R3;
using TMPro;
using UnityEngine;

public class MassDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        FindObjectOfType<Inventory>().PlayerTotalMass.Subscribe(mass => _text.text = $"Total Mass: {mass}").AddTo(this);
    }
}