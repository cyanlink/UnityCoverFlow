using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;

public class CoverSpawner : MonoBehaviour
{
    [SerializeField] private float animationTime = 1f;
    [SerializeField] private int maxCells = 16;
    [SerializeField] private float sideGaps = 0.2f;
    [SerializeField] private float centralGap = 1f;
    [SerializeField] private float sideDepth = 0f;
    [SerializeField] private float centerDepth = 1f;
    [SerializeField] private float sideAngles = 90f;
    
    public float CurrentIndex { get; set; }

    public EventHandle onCellClick;
    private List<Cover> _pCoverPools;

    public void Layout()
    {
        foreach (Cover pCover in _pCoverPools)
        {
            float fIndexOffset = pCover.Index - CurrentIndex;
            pCover.FlowIndexOffset = fIndexOffset;
            Vector3 pPos = Position(fIndexOffset, pCover.Index);
            Vector3 pRotation = EulerAngle(fIndexOffset, pCover.Index);
            Vector3 pScale = Scale(fIndexOffset, pCover.Index);
            pPos += transform.position;
            
            // TODO: iTween 사용한 이동 구현. Legacy 코드 참조
        }
    }

    private int Sign(float fX) => fX == 0 ? 0 : (fX < 0) ? -1 : 1;

    public Vector3 EulerAngle(float fOffset, int nIndex) => new Vector3(0, RotationAngle(fOffset), 0);

    public Vector3 Position(float fOffset, int nIndex) => new Vector3(TranslationX(fOffset), 0, TranslationZ(fOffset));

    public Vector3 Scale(float fOffset, int nIndex) => new Vector3(1, 1, 1);

    private float RotationAngle(float fOffset) => fOffset is >= 1 or <= -1
        ? Sign(fOffset) * sideAngles
        : Math.Abs(fOffset) * Sign(fOffset) * sideAngles;

    private float TranslationX(float fOffset) => fOffset is >= 1 or <= -1
        ? fOffset * sideGaps + Sign(fOffset) * centralGap
        : Sign(fOffset) * centralGap * Math.Abs(fOffset);

    private float TranslationZ(float fOffset) => fOffset is >= 1 or <= -1
        ? sideDepth
        : centerDepth - Math.Abs(fOffset) * (centerDepth - sideDepth);
}