using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeScreenModell : MonoBehaviour {

    public float maxSpeed;
    public Transform maxYPositionTransform, minYPositionTransform;
    public float maxScale, minScale;

    Vector2 originalScale;
    Rigidbody2D rigidbody2d;
    bool facingRight = true;
    float maxYPosition, minYPosition;



    void Start ()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        maxYPosition = maxYPositionTransform.position.y;
        minYPosition = minYPositionTransform.position.y;
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
