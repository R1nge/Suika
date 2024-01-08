﻿using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PolygonColliderOptimizer : MonoBehaviour
{
    private PolygonCollider2D _polygonCollider2D;
    private readonly List<List<Vector2>> _initPaths = new();

    public void GetInitPaths(PolygonCollider2D polygonCollider2D)
    {
        _polygonCollider2D = polygonCollider2D;
        for (int i = 0; i < _polygonCollider2D.pathCount; i++)
        {
            List<Vector2> path = new List<Vector2>(_polygonCollider2D.GetPath(i));
            _initPaths.Add(path);
        }
    }

    public void OptimizePolygonCollider(float tolerance)
    {
        float toleranceNormalized = tolerance / _initPaths.Count;

        if (toleranceNormalized == 0)
        {
            for (int i = 0; i < _initPaths.Count; i++)
            {
                List<Vector2> path = _initPaths[i];
                _polygonCollider2D.SetPath(i, path.ToArray());
            }

            return;
        }

        for (int i = 0; i < _initPaths.Count; i++)
        {
            List<Vector2> path = _initPaths[i];
            path = DouglasPeuckerReduction.DouglasPeuckerReductionPoints(path, toleranceNormalized);
            _polygonCollider2D.SetPath(i, path.ToArray());
        }
    }

    public void Reset() => OptimizePolygonCollider(0);
}