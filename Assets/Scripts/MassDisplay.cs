using System;
using R3;
using TMPro;
using UnityEngine;

public class MassDisplay : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _inventory.PlayerTotalMass.Subscribe(mass => _text.text = $"Mass: {mass}").AddTo(this);
    }
}