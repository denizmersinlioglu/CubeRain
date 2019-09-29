using UnityEngine;

public class Line : MonoBehaviour {
    public float lineWidth = 0.2f;
    public string sortingLayer;
    
    private LineRenderer lineRenderer;

    public Transform leftEnd;
    public Transform rightEnd;

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, leftEnd.position);
        lineRenderer.SetPosition(1, rightEnd.position);
        lineRenderer.enabled = false;
    }

    private void Update() {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, leftEnd.position);
        lineRenderer.SetPosition(1, rightEnd.position);
    }
}
