using UnityEngine;

public class GraphCalculator : MonoBehaviour
{
    public Transform pointPrefab;
    Transform[] points;

    [Range(10, 100)]
    public int resolution = 10;

    public GraphFunctionName function;

    const float pi = Mathf.PI;
    


    private void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;

        points = new Transform[resolution * resolution];

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    private void Update()
    {
        float t = Time.time;

        GraphFunctions[] functions = { SineFunction, Sine2dFunction, SineWave, MultiSineFunction, MultiSine2DFunction, Ripple, Cylinder, Sphere, Eggbasket };

        GraphFunctions f = functions[(int)function];
        float step = 2f / resolution;
        for (int i = 0, z = 0; z < resolution; z++)
        {
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++)
            {
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, t);
            }
        }
    }

    static Vector3 SineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.z = z;
        return p;
    }

    static Vector3 Sine2dFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(pi * (z + t));
        p.y *= 0.5f;
        p.z = z;
        return p;
    }

    static Vector3 MultiSineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
        p.y += Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        p.y *= 1f / 5.5f;
        p.z = z;
        return p;
    }

    static Vector3 MultiSine2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = 4f * Mathf.Sin(pi * (x + z + t / 2f));
        p.y += Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        p.y *= 1f / 5.5f;
        p.z = z;
        return p;
    }

    static Vector3 Eggbasket(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(x + t) + Mathf.Cos(z + t);
        p.z = z;
        return p;
    }

    static Vector3 SineWave(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = (Mathf.Sin(pi * (x + t)) + Mathf.Sin(pi * (z + t))) * 0.5f;
        p.z = z;
        return p;
    }

    static Vector3 Ripple(float u, float v, float t)
    {
        Vector3 p;
        float d = Mathf.Sqrt(u * u + v * v);
        p.x = u;
        p.y = Mathf.Sin(pi * (4f * d - t));
        p.y /= 1f + 10f * d;
        p.z = v;
        return p;
    }

    static Vector3 Cylinder(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(pi * (6f * u + 2f* v+t)) * 0.2f;
        p.x = r * Mathf.Sin(pi*u);
        p.y = v;
        p.z = r * Mathf.Cos(pi*u);
        return p;
    }
    static Vector3 Sphere(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(pi * (6f * u + t)) * 0.1f ;
        r += Mathf.Sin(pi*(4f * v + t)) * 0.1f;
        float s = r * Mathf.Cos(pi * v * 0.5f);
        p.x = s * Mathf.Sin(pi * u);
        p.y = r *Mathf.Sin(pi * v * 0.5f);
        p.z = s * Mathf.Cos(pi * u);
        return p;
    }
}
