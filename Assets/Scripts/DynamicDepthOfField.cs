using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DynamicDepthOfField : MonoBehaviour
{
    public PostProcessVolume volume;
	Ray raycast;
	RaycastHit hit;
    DepthOfField depthOfField;
	float hitDistance;
	public float maxFocusDist;

	[Range(2, 14)]
	public float focusSpeed = 8f;

	void Start()
	{
		volume.profile.TryGetSettings(out depthOfField);
	}

	void Update()
	{
		raycast = new Ray(transform.position, transform.forward * maxFocusDist);
		if (Physics.Raycast(raycast, out hit, maxFocusDist))
		{
			hitDistance = Vector3.Distance(transform.position, hit.point);
		} else
		{
			if (hitDistance < maxFocusDist)
				hitDistance++;
		}

		SetFocus();
	}

	void SetFocus()
	{
		depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, hitDistance, Time.deltaTime * focusSpeed);
	}
}
