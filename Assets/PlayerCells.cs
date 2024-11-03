using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCells : MonoBehaviour
{
    [SerializeField] private List<Transform> _cells = new List<Transform>();

    public Vector3 MeanPosition()
    {
        if ( _cells.Count == 0) { return Vector3.zero;}
        Vector3 sum = Vector3.zero;
        foreach (Transform cell in _cells)
        {
            sum += cell.position;
        }
        return sum / _cells.Count;
    }
    public Vector3 CenterPosition()
    {
        if (_cells.Count == 0) { return Vector3.zero; }

        Bounds bounds = new Bounds(_cells[0].position, Vector3.zero);

        foreach (Transform cell in _cells)
        {
            bounds.Encapsulate(cell.position);
        }

        return bounds.center;
    }


#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int childCount = transform.childCount;
        _cells.Clear();

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.GetComponent<PlayerCell>() != null)
            {
                _cells.Add(child);
            }
        }
    }

#endif

}
