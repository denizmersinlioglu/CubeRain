using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour {

    public int vertexCount = 40;
    public float lineWidth = 0.01f;
    public float radius;
    public bool circleFillScreen;

    private LineRenderer lineRenderer;
    public Transform leftCog;
    public Transform rightCog;


    #region LifeCycle

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        SetupCircle();
    }

    private void Start() {
        leftCog.position = new Vector3(-radius, 0f, 0f);
        rightCog.position = new Vector3(radius, 0f, 0f);
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            Vector3 leftPos = PlaceOnCircle(Input.mousePosition);
            leftCog.position = leftPos;
        }

        if (Input.GetMouseButton(1)) {
            Vector3 rightPos = PlaceOnCircle(Input.mousePosition);
            rightCog.position = rightPos;
        }
    }

    #endregion

    #region Setup Methods

    private Vector3 PlaceOnCircle(Vector3 position) {
        Ray ray = Camera.main.ScreenPointToRay(position);
        Vector3 pos = ray.GetPoint(0);

        pos = transform.InverseTransformDirection(pos);
        float angle = Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg;
        pos.x = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = 0f;

        return pos;

    }

    private void SetupCircle() {

        if (circleFillScreen) {
            float yMax = Camera.main.pixelRect.yMax;
            float yMin = Camera.main.pixelRect.yMin;
            Vector3 maxPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, yMax, 0f));
            Vector3 minPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, yMin, 0f));
            radius = Vector3.Distance(maxPoint, minPoint)/2;
        }

        float deltaTheta = (2f * Mathf.PI) / (float)vertexCount;
        float theta = 0f;

        lineRenderer.positionCount = vertexCount;

        for (int i = 0; i < lineRenderer.positionCount; i++) {
            Vector3 pos = radius * new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0f);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }

    #endregion

    #region GUI Methods

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        float deltaTheta = (2f * Mathf.PI) / (float) vertexCount;
        float theta = 0f;

        Vector3 oldPos = Vector3.zero;

        for (int i = 0; i < vertexCount + 1; i++) {
            Vector3 pos = radius * new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0f);
            Vector3 newPos = transform.position + pos;
            Gizmos.DrawLine(oldPos, newPos);
            oldPos = newPos;

            theta += deltaTheta;
        }
    }

#endif

    #endregion

}
