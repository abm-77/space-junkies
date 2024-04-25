using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private Image _displayImage;
    [SerializeField] private GameObject _selectedIcon;

    [Header("attach desired prefab in scene:")] [SerializeField]
    private Rigidbody2D _item;

    private Inventory _inventory;
    public ReactiveProperty<Rigidbody2D> Item = new();


    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        _inventory.SelectedSlot
            .Subscribe(slot => _selectedIcon.SetActive(slot == this))
            .AddTo(this);
        Item.Value = _item;
        Item.Subscribe(item => _displayImage.sprite = item ? item.GetComponent<SpriteRenderer>().sprite : _emptySprite)
            .AddTo(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _inventory.SelectedSlot.Value = this;
    }
}