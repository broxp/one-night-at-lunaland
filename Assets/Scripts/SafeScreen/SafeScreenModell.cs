using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeScreenModell : MonoBehaviour {

    public float maxSpeed;
    public float maxScale, minScale;

    SafeScreenController safeScreenController;
    SafeScreenView safeScreenView;
    Transform maxYPositionTransform, minYPositionTransform;
    Vector2 originalScale;
    Rigidbody2D rigidbody2d;
    bool facingRight = true;
    float maxYPosition, minYPosition;
    GameObject box;



    void Start ()
    {
        safeScreenController = GetComponent<SafeScreenController>();
        safeScreenView = GetComponent<SafeScreenView>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        maxYPositionTransform = GameObject.FindGameObjectWithTag("MaxY").transform;
        minYPositionTransform = GameObject.FindGameObjectWithTag("MinY").transform;
        originalScale = transform.localScale;
        maxYPosition = maxYPositionTransform.position.y;
        minYPosition = minYPositionTransform.position.y;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        box = collision.gameObject;
        safeScreenController.Kick();
    }

    public void KickBox()
    {
        box.GetComponent<BoxScript>().KickBox(this);
    }

    public void Move(float xInput, float yInput)
    {
        rigidbody2d.velocity = new Vector2(xInput * maxSpeed, yInput * maxSpeed);

        if ((facingRight && xInput < 0) || (!facingRight && xInput > 0))
        {
            facingRight = !facingRight;
        }

        AdjustScale();
    }

    private void AdjustScale()
    {
        float currentYPosition = transform.position.y;
        float relativeYPosition = Mathf.InverseLerp(minYPosition, maxYPosition, currentYPosition);
        float newScale = Mathf.Lerp(maxScale, minScale, relativeYPosition);

        Vector2 newScaleVector = new Vector2(newScale * originalScale.x, newScale * originalScale.y);

        transform.localScale = newScaleVector;
    }

    /*private void GetMaxAndMinY()
    {
        PolygonCollider2D polygonCollider2D = GetComponent<PolygonCollider2D>();

        foreach(Vector2 point in polygonCollider2D.points)
        {
            if(point.y > maxYPosition)
            {
                maxYPosition = point.y;
            }

            if(point.y < minYPosition)
            {
                minYPosition = point.y;
            }
        }
    }*/
}
