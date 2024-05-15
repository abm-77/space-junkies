using UnityEngine;
using System.Collections.Generic;

public class AudioPersist : MonoBehaviour
{
    private static readonly Dictionary<string, AudioPersist> _allAudios = new();

    void Awake()
    {
        _allAudios.TryAdd(name, null);

        if (_allAudios[name] != null && _allAudios[name] != this) {
            Destroy(gameObject);
            return;
        }

        _allAudios[name] = this;
        DontDestroyOnLoad(gameObject);
    }
}