using UnityEngine;

public class Circle
{
    private float squaredRadius;

    public Circle(float radius)
    {
        squaredRadius = radius*radius;
    }

    public Vector2 GetRandomPointInCircle()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);

        float randomRadius = Mathf.Sqrt(Random.Range(0f, squaredRadius));

        float x = randomRadius * Mathf.Cos(angle);
        float y = randomRadius * Mathf.Sin(angle);

        return new Vector2(x, y);
    }
}