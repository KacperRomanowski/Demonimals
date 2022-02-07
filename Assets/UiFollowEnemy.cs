using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFollowEnemy : MonoBehaviour
{
    public Transform objectToFollow;
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (objectToFollow != null) {
            rectTransform.anchoredPosition = objectToFollow.localPosition;
        }   
    }
}
