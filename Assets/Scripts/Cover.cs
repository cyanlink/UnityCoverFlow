using System;
using UnityEngine;

public class Cover : MonoBehaviour
{
    private Renderer _pRenderer;
    private Material _pMaterial;
    
    public EventHandler onClickEvent;
    public bool IsActive => gameObject.activeSelf;
    public bool IsInit
    {
        get
        {
            if (_pRenderer == null)
                _pRenderer = GetComponent<Renderer>();
            if (_pMaterial == null && _pRenderer != null)
                _pMaterial = _pRenderer.material;
            return _pRenderer != null && _pMaterial != null;
        }
    }
    public int Index { get; set; }
    public float FlowIndexOffset { get; set; }

    
    public Cover Create()
    {
        return Instantiate(this);
    }

    public void Setup(Color pColor)
    {
        if (!IsInit) return;
        _pMaterial.color = pColor;
        _pRenderer.material = _pMaterial;
    }

    public void Setup(Texture pTexture)
    {
        if (!IsInit) return;
        _pMaterial.mainTexture = pTexture;
        _pRenderer.material = _pMaterial;
    }

    public void OnMouseUpAsButton()
    {
        onClickEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnClick()
    {
        onClickEvent?.Invoke(this, EventArgs.Empty);
    }

    public void SetActive(bool bActive, GameObject pOwner = null)
    {
        if (bActive)
        {
            if (pOwner != null)
                transform.parent = pOwner.transform;
            gameObject.SetActive(true);
        }
        else
        {
            onClickEvent = null;
            Index = 0;
            FlowIndexOffset = 0f;
            gameObject.SetActive(false);
        }
    }
}