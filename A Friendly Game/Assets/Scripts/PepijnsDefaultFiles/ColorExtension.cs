using UnityEngine;
using System.Collections;

//
// Made By Jonathan van hove
// https://github.com/joonjoonjoon
// https://twitter.com/joonturbo
//
public static class ColorExtension {
	public static Color SetV(this Color parent, float newV)
	{
	    float h,s,v;
	    ColorToHSV(parent, out h, out s, out v);
	    v = newV;
	    return ColorFromHSV(h, s, v, parent.a);
	}

    public static Color SetH(this Color parent, float newH)
    {
        float h, s, v;
        ColorToHSV(parent, out h, out s, out v);
        if (newH > 360) newH = newH %360;
        h = newH;
        return ColorFromHSV(h, s, v, parent.a);
    }

	public static float GetV(this Color parent)
	{
	    float h, s, v;
	    ColorToHSV(parent, out h, out s, out v);
	    return v;
	}

    public static float GetH(this Color parent)
    {
        float h, s, v;
        ColorToHSV(parent, out h, out s, out v);
        return h;
    }

	public static Color SetS(this Color parent, float newS)
	{
		float h,s,v;
		ColorToHSV(parent, out h, out s, out v);
		s = newS;
		return ColorFromHSV(h, s, v, parent.a);
	}
		
	public static float GetS(this Color parent)
	{
		float h, s, v;
		ColorToHSV(parent, out h, out s, out v);
		return s;
	}

	public static Color SetA(this Color parent, float newA)
	{
	    return new Color(parent.r, parent.g, parent.b, newA);
	}

    public static Color SetR(this Color parent, float newR)
    {
        return new Color(newR, parent.g, parent.b, parent.a);
    }

	public static Color ColorFromHSV(float h, float s, float v, float a = 1)
	{
	    // no saturation, we can return the value across the board (grayscale)
	    if (s == 0)
	        return new Color(v, v, v, a);

	    // which chunk of the rainbow are we in?
	    float sector = h / 60;

	    // split across the decimal (ie 3.87 into 3 and 0.87)
	    int i = (int)sector;
	    float f = sector - i;

	    float p = v * (1 - s);
	    float q = v * (1 - s * f);
	    float t = v * (1 - s * (1 - f));

	    // build our rgb color
	    Color color = new Color(0, 0, 0, a);

	    switch (i)
	    {
	        case 0:
	            color.r = v;
	            color.g = t;
	            color.b = p;
	            break;

	        case 1:
	            color.r = q;
	            color.g = v;
	            color.b = p;
	            break;

	        case 2:
	            color.r = p;
	            color.g = v;
	            color.b = t;
	            break;

	        case 3:
	            color.r = p;
	            color.g = q;
	            color.b = v;
	            break;

	        case 4:
	            color.r = t;
	            color.g = p;
	            color.b = v;
	            break;

	        default:
	            color.r = v;
	            color.g = p;
	            color.b = q;
	            break;
	    }

	    return color;
	}

	public static void ColorToHSV(Color color, out float h, out float s, out float v)
	{
	    float min = Mathf.Min(Mathf.Min(color.r, color.g), color.b);
	    float max = Mathf.Max(Mathf.Max(color.r, color.g), color.b);
	    float delta = max - min;

	    // value is our max color
	    v = max;

	    // saturation is percent of max
	    if (!Mathf.Approximately(max, 0))
	        s = delta / max;
	    else
	    {
	        // all colors are zero, no saturation and hue is undefined
	        s = 0;
	        h = -1;
	        return;
	    }

	    // grayscale image if min and max are the same
	    if (Mathf.Approximately(min, max))
	    {
	        v = max;
	        s = 0;
	        h = -1;
	        return;
	    }

	    // hue depends which color is max (this creates a rainbow effect)
	    if (color.r == max)
	        h = (color.g - color.b) / delta;         	// between yellow & magenta
	    else if (color.g == max)
	        h = 2 + (color.b - color.r) / delta; 		// between cyan & yellow
	    else
	        h = 4 + (color.r - color.g) / delta; 		// between magenta & cyan

	    // turn hue into 0-360 degrees
	    h *= 60;
	    if (h < 0)
	        h += 360;
	}

    // Note that Color32 and Color implictly convert to each other. You may pass a Color object to this method without first casting it.
    public static string colorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    public static Color hexToColor(string hex)
    {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }
}
