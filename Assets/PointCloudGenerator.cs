using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloudGenerator : MonoBehaviour
{
    public void GeneratePointCloud() {
        Camera camera = GetComponentInChildren<Camera>();

        // Zivid Two parameters
        // 1944x1200 pixels
        // 1 mm is 1 pixel at z-axis at 1800mm
        for(int x = -972; x < 972; x++) {
            for(int y = -600; y < 600; y++) {
                Vector3 endPoint = new Vector3(x,-1800,y);
                endPoint -= camera.transform.position*10;
                
                RaycastHit hit;
                if(Physics.Raycast(camera.transform.position,
                    endPoint, out hit, 100)) {
                    Gizmos.DrawSphere(hit.point, 0.1f);
                }
            }
        }
    }
}
