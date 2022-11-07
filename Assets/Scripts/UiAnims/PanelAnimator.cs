using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PanelAnimator : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    [SerializeField, Range(0,1)] private float startScale;
    [SerializeField] private float speedInTime;

    private Tweener appearTweener;
    // Start is called before the first frame update
    void Awake()
    {
        transform.position = startPosition.position;
    }

    public void Appear()
    {
        appearTweener?.Kill();
        appearTweener = transform.DOMove(endPosition.position, speedInTime);
    }

    public void Disappear()
    {
        appearTweener?.Kill();
        appearTweener = transform.DOMove(startPosition.position, speedInTime).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
