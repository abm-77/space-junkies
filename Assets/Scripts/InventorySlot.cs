using R3;
using R3.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private Image _displayImage;
    [SerializeField] private GameObject _selectedIcon;
    [SerializeField] private TextMeshProUGUI _massText;

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
        Item.Subscribe(item =>
            {
                _displayImage.sprite = item ? item.GetComponent<SpriteRenderer>().sprite : _emptySprite;
                _displayImage.color = item ? item.GetComponent<SpriteRenderer>().color : Color.white;
                var newScale = item ? (item.mass - 1f) * .3f + 1f : 1f;
                _displayImage.GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1);
                _massText.text = item ? item.mass.ToString() : "";
            })
            .AddTo(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _inventory.SelectedSlot.Value = this;
    }
}