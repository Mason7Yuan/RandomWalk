using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Default of transform.postion is a 3-D vector, but it can regard as 2-D vector as in 2-D methods.

public class RandomWalk : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    private Vector2 originalPosition;
    private float nowDistance;
    private float nowDelayTime;
    private bool startDelay;

    private float walkingVelocity = 1f;
    private float maximumRange = 1f;
    private float delayTime = 0.3f;
    #endregion

    #region Main Gaming functions
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        originalPosition = new Vector2(transform.position.x, transform.position.y);
        nowDelayTime = delayTime;
        startDelay = true;
    }

    // Update is called once per frame
    void Update()
    {
        nowDistance = DistanceCalculator2D(transform.position, originalPosition);

        if (startDelay)
        {
            if (nowDistance > maximumRange)
            {
                rb.velocity = NormalizedDirection2D(originalPosition, transform.position) * walkingVelocity;
            }
            else
                rb.velocity = RandomEightDirectionGenerator2D() * walkingVelocity;

            startDelay = false;
        }
        else
        {
            nowDelayTime -= Time.deltaTime;

            if (nowDelayTime < 0)
            {
                startDelay = true;
                nowDelayTime = delayTime;
            }
        }
    }
    #endregion

    #region Vector2 methods
    private float DistanceCalculator2D(Vector2 positionA, Vector2 positionB)
    {
        return Mathf.Sqrt(Mathf.Pow(positionA.x - positionB.x, 2) + Mathf.Pow(positionA.y - positionB.y, 2));
    }

    private Vector2 NormalizedDirection2D(Vector2 positionA, Vector2 positionB)
    {
        return new Vector2(positionA.x - positionB.x, positionA.y - positionB.y).normalized;
    }

    private Vector2 RandomEightDirectionGenerator2D()
    {
        float randomInt = Mathf.Ceil(Random.Range(0f, 8f));
        Vector2 dir = new Vector2(0f, 0f);

        switch (randomInt)
        {
            case 1:
                dir = new Vector2(0f, 1f);
                break;
            case 2:
                dir = new Vector2(1f, 1f).normalized;
                break;
            case 3:
                dir = new Vector2(1f, 0f);
                break;
            case 4:
                dir = new Vector2(1f, -1f).normalized;
                break;
            case 5:
                dir = new Vector2(0f, -1f);
                break;
            case 6:
                dir = new Vector2(-1f, -1f).normalized;
                break;
            case 7:
                dir = new Vector2(-1f, 0f);
                break;
            case 8:
                dir = new Vector2(-1f, 1f).normalized;
                break;
        }
        return dir;
    }
    #endregion
}
