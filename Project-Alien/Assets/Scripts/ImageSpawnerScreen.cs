using System.Collections;
using UnityEngine;

public class ImageSpawnerScreen : MonoBehaviour
{
    public float waitTime = 8;
    public float despawnTime = 20;
    public GameObject imageToSpawn;

    public RectTransform panel;

    void Start()
    {
        StartCoroutine(SpawnImage());
    }


    IEnumerator SpawnImage()
    {
        while (true)
        {
            RectTransform img = Instantiate(imageToSpawn, transform)
                .GetComponent<RectTransform>();

            Vector2 randomPos = new Vector2(
                Random.Range(panel.rect.xMin, panel.rect.xMax),
                Random.Range(panel.rect.yMin, panel.rect.yMax)
            );

            img.localPosition = randomPos;

            Destroy(img.gameObject, despawnTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}

