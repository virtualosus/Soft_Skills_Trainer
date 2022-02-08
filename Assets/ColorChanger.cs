using System;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    /// <summary>
    /// Sets the color of the specified transform.
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="color"></param>
    private void SetColor(Transform trans, Color color)
    {
        trans.GetComponent<Renderer>().material.color = color;
    }

    /// <summary>
    /// Updates the color of GameObject with the names specified in the input values.
    /// </summary>
    /// <param name="values"></param>
    public void UpdateColor(string[] values)
    {
        var colorString = values[0];
        var shapeString = values[1];

        if (!ColorUtility.TryParseHtmlString(colorString, out var color)) return;
        if (string.IsNullOrEmpty(shapeString)) return;

        foreach (Transform child in transform) // iterate through all children of the gameObject.
        {
            if (child.name.IndexOf(shapeString, StringComparison.OrdinalIgnoreCase) != -1) // if the name exists
            {
                SetColor(child, color);
                return;
            }
        }
    }
}