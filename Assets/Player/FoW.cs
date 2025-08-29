using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoW : MonoBehaviour {

    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public float meshResolution;

    private void Update()
    {
        DrawFieldofView();
    }

    public Vector3 DirFromAngle(float angleinDegrees) {
        if(viewAngle == 360)
        {
            angleinDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleinDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleinDegrees * Mathf.Deg2Rad));
    }

    void DrawFieldofView()
    {
        int stepcount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAnleSize = viewAngle / stepcount;

        for (int i = 0; i <= stepcount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAnleSize * i;
            Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle) * viewRadius, Color.red);
        }
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius)) {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
}
