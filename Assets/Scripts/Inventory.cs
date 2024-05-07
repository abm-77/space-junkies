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

    [SerializeField]
    public Rigidbody2D big_rock;

    [SerializeField]
    public Rigidbody2D small_rock;

    private void Awake()
    {
        _playerBaseMass = _player.mass;
        _inventorySlots = GetComponentsInChildren<InventorySlot>();
        PlayerTotalMass = Observable.CombineLatest(_inventorySlots.Select(slot => slot.Item))
            .Select(items => items.Sum(item => item ? item.mass : 0f) + _playerBaseMass);
        PlayerTotalMass.Subscribe(mass => _player.mass = mass);
    }

    public void AddBigRock()
    {
        foreach (InventorySlot slot in _inventorySlots)
        {
            if (slot.Item.Value == null)
            {
                slot.Item.Value = big_rock;
                break;
            }
        }
    }

    public void AddSmallRock()
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (_inventorySlots[i].Item.Value == null)
            {
                _inventorySlots[i].Item.Value = small_rock;
                break;
            }
        }
    }
    
    private void Start()
    {
        SelectedSlot.Value = _inventorySlots[0];
    }
}