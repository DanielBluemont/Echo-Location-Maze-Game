using System.Collections;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class BrokenVase : MonoBehaviour
{
    [SerializeField] private Rigidbody[] rigidbodies = new Rigidbody[10];
    [SerializeField] private float force, radius, fadeTime;
    private Material[] materials = new Material[10];
    Color emCol, col;
    private void Start() 
    {
        for (int i = 0; i<10; i++)
        {
            materials[i] = rigidbodies[i].GetComponent<Renderer>().material;
            materials[i].EnableKeyword("_EMISSION");
        }
        col = materials[0].GetColor("_EmissionColor");
        Shatter();
    }
    private void Shatter()
    {
        foreach (Rigidbody body in rigidbodies)
        {
            body.AddExplosionForce(force, transform.position,radius);
        }
        StartCoroutine(FadeFragments());
    }
    IEnumerator FadeFragments()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime/fadeTime;
            float intensity = Mathf.Lerp(1f, 0f, t);
            foreach (Material material in materials)
            {
                Color newColor = col * intensity;
                material.SetColor("_EmissionColor", newColor);
            }
            yield return null;
        }
        foreach (Rigidbody r in rigidbodies)
        {
            Destroy(r.gameObject);
        }
        Destroy(this.gameObject);
    }
}
