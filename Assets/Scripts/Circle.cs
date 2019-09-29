using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour {

    public int VertexCount = 40;
    public float LineWidth = 0.01f;
    public float Radius;
    public bool CircleFillScreen;

    private LineRenderer lineRenderer;


    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        SetupCircle();
    }

    private void SetupCircle() {

        if (CircleFillScreen) {
            float yMax = Camera.main.pixelRect.yMax;
            float yMin = Camera.main.pixelRect.yMin;
            Vector3 maxPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, yMax, 0f));
            Vector3 minPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, yMin, 0f));
            Radius = Vector3.Distance(maxPoint, minPoint)/2;
        }

        float deltaTheta = (2f * Mathf.PI) / (float)VertexCount;
        float theta = 0f;

        lineRenderer.positionCount = VertexCount;

        for (int i = 0; i < lineRenderer.positionCount; i++) {
            Vector3 pos = Radius * new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0f);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        float deltaTheta = (2f * Mathf.PI) / (float) VertexCount;
        float theta = 0f;

        Vector3 oldPos = Vector3.zero;

        for (int i = 0; i < VertexCount + 1; i++) {
            Vector3 pos = Radius * new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0f);
            Vector3 newPos = transform.position + pos;
            Gizmos.DrawLine(oldPos, newPos);
            oldPos = newPos;

            theta += deltaTheta;
        }
    }

#endif
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
