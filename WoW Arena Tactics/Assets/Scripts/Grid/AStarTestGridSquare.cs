using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AStarTestGridSquare : GridSquare
{
    [SerializeField] private Material unwalkableMaterial;

    private Renderer rend;
    private Material defaultMaterial;

    protected override void Awake()
    {
        base.Awake();
        rend = GetComponent<Renderer>();
        defaultMaterial = rend.material;
    }

    public override void OnPointerMiddleClick()
    {
        Walkable = !Walkable;
        rend.material = Walkable ? defaultMaterial : unwalkableMaterial;
        base.OnPointerMiddleClick();
    }
}
