using System;
using DG.Tweening;
using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    private Vector3 scale;
    private float time = 0.5f;

    private void Awake()
    {
        scale = transform.localScale;
    }

    public void OnHover(bool hovering)
    {
        transform.DOScale(hovering ? scale * 2 : scale, time);
    }
}
