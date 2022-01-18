using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float backgroundScrollSpeed = 0.15f;
    private Material material;
    private Vector2 offSet;
    private void Start()
    {
        material = GetComponent<Renderer>().material;
        offSet = new Vector2(0, backgroundScrollSpeed);
    }
    private void Update()
    {
        material.mainTextureOffset += offSet * Time.deltaTime;
    }
}
