using UnityEngine;
using UnityEngine.InputSystem;

public class SelectPOI : MonoBehaviour
{
    [SerializeField] private RectTransform _centerRect;
    [SerializeField] private RectTransform _pointRect;


    private Vector3[] aCorners = new Vector3[4];
    private Vector3[] bCorners = new Vector3[4];

    public bool RectOverlaps(RectTransform a, RectTransform b)
    {
        a.GetWorldCorners(aCorners);
        b.GetWorldCorners(bCorners);

        Rect rectA = new Rect(
            aCorners[0],
            aCorners[2] - aCorners[0]
        );

        Rect rectB = new Rect(
            bCorners[0],
            bCorners[2] - bCorners[0]
        );

        return rectA.Overlaps(rectB);
    }


    public void Scan(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject[] pois = GameObject.FindGameObjectsWithTag("POI");

            foreach (var poi in pois)
            {
                RectTransform rt = poi.GetComponent<RectTransform>();
                if (RectOverlaps(rt, _centerRect))
                {
                    // Debug.Log(poi.name);
                }
            }
        }
    }

}
