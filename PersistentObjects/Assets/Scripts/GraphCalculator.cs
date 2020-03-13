using UnityEngine;

public class GraphCalculator : MonoBehaviour
{
    public Transform pointPrefab;
    Transform[] points;

    [Range(10,100)]
    public int resolution = 10;

    public GraphFunctionName function;

    const float pi = Mathf.PI;

    private void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one *step;
        Vector3 position;
        position.z = 0f;
        position.y = 0f;

        points = new Transform[resolution* resolution];

        for (int i = 0, z = 0; z < resolution; z++)
        {
            position.z = (z + 0.5f) * step - 1f;

            for (int x = 0; x < resolution; x++, i++)
            {
                if (x == resolution)
                {
                    x = 0;
                    z++;
                }

                Transform point = Instantiate(pointPrefab);
                points[i] = point;
                position.x = (x + 0.5f) * step - 1f;
                point.localPosition = position;
                point.localScale = scale;
                point.SetParent(transform, false);
            }
        }
    }

    private void Update()
    {
        float t = Time.time;

        GraphFunctions[] functions = { SineFunction, Sine2dFunction, SineWave, MultiSineFunction, MultiSine2DFunction, Ripple, Eggbasket };
        
        GraphFunctions f = functions[(int)function];

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;

            position.y = f(position.x, position.z, t);
            point.localPosition = position;
        }                
    }

    float SineFunction(float x, float z, float t)
    {
        return Mathf.Sin(pi * (x + t));
    }

    float MultiSineFunction(float x, float z, float t)
    {
        float y = Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(2f * pi * (x + t)) / 2f;
        y*= 2f/3f;
        return y;
    }

    float MultiSine2DFunction(float x, float z, float t)
    {
        float y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
        y += Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        y *= 1f / 5.5f;
        return y;
    }

    float Eggbasket(float x, float z, float t)
    {
        float y = Mathf.Pow(3, (pi*-x)) * Mathf.Cos(10*pi*(x+t));
        //y += Mathf.Cos(y);
        return y;
    }

    float Sine2dFunction(float x, float z, float t)
    {
        return Mathf.Sin(pi * (x + z + t));
    }

    float SineWave(float x, float z, float t)
    {
        return (Mathf.Sin(pi * (x + t)) + Mathf.Sin(pi * (z + t)))*0.5f;
    }

    float Ripple(float x, float z, float t)
    {
        float d = Mathf.Sqrt(x * x + z * z);
        return d;
    }
}
