using System.Collections;
using UnityEngine;

public class ImageSpawnerScreen : MonoBehaviour
{
    [SerializeField] private float _waitTime;
    [SerializeField] private int _maxImages;
    [SerializeField] private GameObject _imageToSpawn;

    [SerializeField] private RectTransform _panel;
    [SerializeField] private float _offset;

    void Start()
    {
        StartCoroutine(SpawnImage());
    }


    IEnumerator SpawnImage()
    {
        for (int i = 0; i < _maxImages; i++)
        {
            RectTransform img = Instantiate(_imageToSpawn, transform)
                .GetComponent<RectTransform>();

            Vector2 randomPos = new Vector2(
                Random.Range(_panel.rect.xMin + _offset, _panel.rect.xMax - _offset),
                Random.Range(_panel.rect.yMin + _offset, _panel.rect.yMax - _offset)
            );

            img.localPosition = randomPos;

            yield return new WaitForSeconds(_waitTime);
        }
    }
}

