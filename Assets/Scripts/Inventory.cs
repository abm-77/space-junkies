﻿using System;
using System.Linq;
using R3;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ReactiveProperty<InventorySlot> SelectedSlot = new();
    [SerializeField] private Rigidbody2D _player;
    private float _playerBaseMass;
    private InventorySlot[] _inventorySlots;

    public Observable<float> PlayerTotalMass;

    private void Awake()
    {
        _playerBaseMass = _player.mass;
        _inventorySlots = GetComponentsInChildren<InventorySlot>();
        PlayerTotalMass = Observable.CombineLatest(_inventorySlots.Select(slot => slot.Item))
            .Select(items => items.Sum(item => item ? item.mass : 0f) + _playerBaseMass);
        PlayerTotalMass.Subscribe(mass => _player.mass = mass);
    }

    public void AddItem(Rigidbody2D itemPrefab)
    {
        foreach (InventorySlot slot in _inventorySlots)
        {
            if (slot.Item.Value == null)
            {
                slot.Item.Value = itemPrefab;
                break;
            }
        }
    }

    private void Start()
    {
        SelectedSlot.Value = _inventorySlots[0];
    }
}