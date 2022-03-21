using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lerptest : MonoBehaviour
{
    float timeElapsed;
    float lerpDuration = 3;
    float startValue = 1;
    float endValue = 0;
    float valueToLerp;
    private RawImage image;

	private void Start()
	{
        image = GetComponent<RawImage>();
    }
	void Update()
    {
        if (timeElapsed < lerpDuration)
        {
            Color end = image.color;
            end.a = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            Debug.Log(valueToLerp);
            image.color = end;
            timeElapsed += Time.deltaTime;
        }
    }
}
