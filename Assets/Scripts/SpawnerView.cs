using TMPro;
using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private SpawnerInfo _spawnerInfo;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _textCreatedCount;
    [SerializeField] private TextMeshProUGUI _textSpawnedCount;
    [SerializeField] private TextMeshProUGUI _createAtivedCount;

    private void OnEnable()
    {
        _spawnerInfo.ObjectsCreated += ShowCreateCount;
        _spawnerInfo.ObjectsSpawned += ShowSpawnCount;
        _spawnerInfo.ObjectsActived += ShowActiveCount;
    }

    private void OnDisable()
    {
        _spawnerInfo.ObjectsCreated -= ShowCreateCount;
        _spawnerInfo.ObjectsSpawned -= ShowSpawnCount;
        _spawnerInfo.ObjectsActived -= ShowActiveCount;
    }

    private void Start()
    {
        _name.text = _spawnerInfo.gameObject.name;
        ShowCreateCount(0);
        ShowSpawnCount(0);
        ShowActiveCount(0);
    }

    private void ShowCreateCount(int count)
    {
        _textCreatedCount.text = ($"Created: {count}");
    }

    private void ShowSpawnCount(int count)
    {
        _textSpawnedCount.text = ($"Spawned: {count}");
    }

    private void ShowActiveCount(int count)
    {
        _createAtivedCount.text = ($"Actived: {count}");
    }
}
