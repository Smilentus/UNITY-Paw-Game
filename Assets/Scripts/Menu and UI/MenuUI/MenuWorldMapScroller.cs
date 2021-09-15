using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWorldMapScroller : MonoBehaviour
{
    private Vector3 startPosition;
    private Camera camera;

    private Vector2 targetPosition;

    [Header("Ограничения на движение камеры")]
    public Vector2 minLimits;
    public Vector2 maxLimits;

    [Header("Плавность движения")]
    public float smoothing = 7f;

    void Start()
    {
        camera = GetComponent<Camera>();
        targetPosition = transform.position;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 position = new Vector2(
                camera.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x, 
                camera.ScreenToWorldPoint(Input.mousePosition).y - startPosition.y);

            targetPosition = new Vector2(
                Mathf.Clamp(transform.position.x - position.x, minLimits.x, maxLimits.x),
                Mathf.Clamp(transform.position.y - position.y, minLimits.y, maxLimits.y));

            //transform.position = new Vector3(
            //    Mathf.Clamp(transform.position.x - posX, minLimits.x, maxLimits.x), 
            //    Mathf.Clamp(transform.position.y - posY, minLimits.y, maxLimits.y), 
            //    this.transform.position.z);
        }
        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, targetPosition.x, smoothing * Time.deltaTime),
            Mathf.Lerp(transform.position.y, targetPosition.y, smoothing * Time.deltaTime), 
            transform.position.z);
    }
}
