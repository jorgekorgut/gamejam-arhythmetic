using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    public static List<Color> ColorList = new List<Color>
    {
        ColorFromHex("#E7D562"),
        ColorFromHex("#A36ACB"),
        ColorFromHex("#3ACAD7"),
        ColorFromHex("#936A47"),
        ColorFromHex("#DE8AA3"),
        ColorFromHex("#A9284C"),

        ColorFromHex("#D2D5C1"),
        ColorFromHex("#988982"),
    };

    public static Color ColorFromHex(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color color);
        return color;
    }
}
