using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints = new Transform[3];
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private bool showGizmo = true;
    [SerializeField] private float arrowSize = 0.5f;
    [SerializeField] private float arrowAmount = 1;

    private int currentWaypoint = 0;
    private float minDistance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = waypoints[currentWaypoint].position;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < minDistance)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    private void DrawArrow(Vector3 start, Vector3 end)
    {
        Vector3 direction = (end - start).normalized;
        float pathLength = Vector3.Distance(start, end);
        int arrowCount = Mathf.Max(1, Mathf.FloorToInt(pathLength * arrowAmount));

        for (int i=0; i < arrowCount; i++)
        {
            float t = ((float)i + 0.5f) / arrowCount;
            Vector3 arrowPos = Vector3.Lerp(start, end, t);
            Vector3 right = Vector3.Cross(Vector3.up, direction).normalized;

            Vector3 arrowTip = arrowPos + direction * (arrowSize * 0.5f);
            Vector3 arrowBase = arrowPos - direction * (arrowSize * 0.5f);
            Vector3 arrowRight = arrowBase + right * (arrowSize * 0.25f);
            Vector3 arrowLeft = arrowBase - right * (arrowSize * 0.25f);

            Gizmos.DrawLine(arrowBase, arrowTip);
            Gizmos.DrawLine(arrowRight, arrowTip);
            Gizmos.DrawLine(arrowLeft, arrowTip);
        }
    }

    private void OnDrawGizmos()
    {
        if (!showGizmo || waypoints == null || waypoints.Length != 3)
        {
            return;
        }

        for (int i=0; i< waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                //gizmo spheres
                Gizmos.color = (i == currentWaypoint)? Color.green : Color.red;
                Gizmos.DrawWireSphere(waypoints[i].position, 0.25f);

                //checking if being viewed from unity editor
#if (UNITY_EDITOR)
                
                    Vector3 textPosition = waypoints[i].position + Vector3.up * 0.5f;
                    Handles.Label(textPosition, (i+1).ToString(), new GUIStyle()
                    {
                        normal = new GUIStyleState() { textColor = (i == currentWaypoint) ? Color.green : Color.white },
                        fontSize = 20,
                        fontStyle = FontStyle.Bold,
                        alignment = TextAnchor.MiddleCenter
                    });
#endif

                if (i < waypoints.Length - 1 && waypoints[i + 1] != null)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                    DrawArrow(waypoints[i].position, waypoints[i + 1].position);
                }
                else if (waypoints[0] != null && waypoints[waypoints.Length-1])
                {
                    Gizmos.DrawLine(waypoints[waypoints.Length-1].position, waypoints[0].position);
                    DrawArrow(waypoints[waypoints.Length - 1].position, waypoints[0].position);
                }
                
            }
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }
}
