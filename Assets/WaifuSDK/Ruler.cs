
#if UNITY_EDITOR
using UnityEngine;


using UnityEditor;

public class Ruler : MonoBehaviour
{
    public float MaxHeight = 3f;
    public bool UseMetricUnits = true;

    private const float metersToFeet = 3.28084f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Get the origin position in local space
        Vector3 origin = transform.position;

        // Draw the line from the origin to the maximum height
        Gizmos.DrawLine(origin, origin + Vector3.up * MaxHeight);

        // Draw ticks every 1 unit up to the maximum height
        float tickInterval = UseMetricUnits ? 1f : 1f / metersToFeet;
        float maxHeight = UseMetricUnits ? MaxHeight : MaxHeight * metersToFeet;
        for (float i = tickInterval; i <= maxHeight; i += tickInterval)
        {
            Vector3 tickPositionWorld = origin + Vector3.up * i;
            Gizmos.DrawLine(tickPositionWorld, tickPositionWorld + Vector3.right * 0.1f);

            // Add a label for the tick position
            string label = UseMetricUnits ? i.ToString("F0") + "m" : Mathf.Round(i * metersToFeet) + "ft";
            Handles.Label(tickPositionWorld + Vector3.right * 0.2f, label);
        }
    }
}

#endif