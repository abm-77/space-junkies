using System;
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
    private bool updated = false;

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
    
    private void Update()
    {
        if (!updated)
        {
            SelectedSlot.Value = _inventorySlots[0];
            updated = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedSlot.Value = _inventorySlots[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedSlot.Value = _inventorySlots[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectedSlot.Value = _inventorySlots[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectedSlot.Value = _inventorySlots[3];
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectedSlot.Value = _inventorySlots[4];
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectedSlot.Value = _inventorySlots[5];
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectedSlot.Value = _inventorySlots[6];
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectedSlot.Value = _inventorySlots[7];
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectedSlot.Value = _inventorySlots[8];
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SelectedSlot.Value = _inventorySlots[9];
        }
    }
}