using System;
using System.Linq;
using R3;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Switch[] _switchesToOpen;

    private void Start()
    {
        Observable.CombineLatest(_switchesToOpen.Select(s => s.Hit))
            .Where(hits => hits.All(x => x))
            .Subscribe(_ =>
            {
                Destroy(gameObject);
            })
            .AddTo(this);
    }
}