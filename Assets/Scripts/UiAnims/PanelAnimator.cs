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
    
    public PanelState ActiveState { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        transform.position = startPosition.position;
        ActiveState = PanelState.Down;
    }

    public void Appear()
    {
        appearTweener?.Kill();
        ActiveState = PanelState.AnimatingUp;
        appearTweener = transform.DOMove(endPosition.position, speedInTime).OnComplete(() =>
        {
            ActiveState = PanelState.Up;
        });
    }

    public void Disappear()
    {
        appearTweener?.Kill();
        ActiveState = PanelState.AnimatingDown;
        appearTweener = transform.DOMove(startPosition.position, speedInTime).OnComplete(() =>
        {
            ActiveState = PanelState.Down;
            gameObject.SetActive(false);
        });
    }
}

public enum PanelState
{
    Up = 0,
    Down = 1,
    AnimatingUp = 2,
    AnimatingDown = 3
}
