//Code based on https://www.youtube.com/watch?v=aLpixrPvlB8

using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Camera))]
public class cameraViewAll : MonoBehaviour {
    public List<Transform> keepTheseInView;
    public Vector3 offset;
    public float smoothTime;
    private Vector3 velocity;

    private void Awake() {
        smoothTime = 0.25f;
    }

    private void LateUpdate() {
        Bounds findCenter = new Bounds(keepTheseInView[0].position, Vector3.zero);
        for (int i = 0; i < keepTheseInView.Count; ++i) {
            findCenter.Encapsulate(keepTheseInView[i].position);
        }
        this.gameObject.transform.position = Vector3.SmoothDamp(this.gameObject.transform.position, (offset + findCenter.center), ref velocity, smoothTime);
    }
}