using UnityEngine;
[System.Serializable]
public static class Easing
{
    public static float EaseInSine(float t) => 1 - Mathf.Cos((t * Mathf.PI) / 2);
    public static float EaseOutSine(float t) => Mathf.Sin((t * Mathf.PI) / 2);
    public static float EaseInOutSine(float t) => -(Mathf.Cos(Mathf.PI * t) - 1) / 2;

    public static float EaseInQuad(float t) => t * t;
    public static float EaseOutQuad(float t) => t * (2 - t);
    public static float EaseInOutQuad(float t) => t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;

    public static float EaseInCubic(float t) => t * t * t;
    public static float EaseOutCubic(float t) => (--t) * t * t + 1;
    public static float EaseInOutCubic(float t) => t < 0.5f ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;

    public static float EaseInQuart(float t) => t * t * t * t;
    public static float EaseOutQuart(float t) => 1 - (--t) * t * t * t;
    public static float EaseInOutQuart(float t) => t < 0.5f ? 8 * t * t * t * t : 1 - 8 * (--t) * t * t * t;

    public static float EaseInQuint(float t) => t * t * t * t * t;
    public static float EaseOutQuint(float t) => 1 + (--t) * t * t * t * t;
    public static float EaseInOutQuint(float t) => t < 0.5f ? 16 * t * t * t * t * t : 1 + 16 * (--t) * t * t * t * t;

    public static float EaseInExpo(float t) => t == 0 ? 0 : Mathf.Pow(2, 10 * t - 10);
    public static float EaseOutExpo(float t) => t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);
    public static float EaseInOutExpo(float t) => t == 0 ? 0 : t == 1 ? 1 : t < 0.5 ? Mathf.Pow(2, 20 * t - 10) / 2 : (2 - Mathf.Pow(2, -20 * t + 10)) / 2;

    public static float EaseInCirc(float t) => 1 - Mathf.Sqrt(1 - t * t);
    public static float EaseOutCirc(float t) => Mathf.Sqrt(1 - (--t) * t);
    public static float EaseInOutCirc(float t) => t < 0.5f ? (1 - Mathf.Sqrt(1 - 2 * t * 2 * t)) / 2 : (Mathf.Sqrt(1 - (2 * t - 2) * (2 * t - 2)) + 1) / 2;

    public static float EaseInBack(float t) => 2.70158f * t * t * t - 1.70158f * t * t;
    public static float EaseOutBack(float t) => 1 + 2.70158f * (--t) * t * t + 1.70158f * t * t;
    public static float EaseInOutBack(float t) => t < 0.5f ? (Mathf.Pow(2 * t, 2) * ((2.59491f + 1) * 2 * t - 2.59491f)) / 2 : (Mathf.Pow(2 * t - 2, 2) * ((2.59491f + 1) * (t * 2 - 2) + 2.59491f) + 2) / 2;

    public static float EaseInElastic(float t) => t == 0 ? 0 : t == 1 ? 1 : -Mathf.Pow(2, 10 * t - 10) * Mathf.Sin((t * 10 - 10.75f) * (2 * Mathf.PI) / 3);
    public static float EaseOutElastic(float t) => t == 0 ? 0 : t == 1 ? 1 : Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10 - 0.75f) * (2 * Mathf.PI) / 3) + 1;
    public static float EaseInOutElastic(float t) => t == 0 ? 0 : t == 1 ? 1 : t < 0.5 ? -(Mathf.Pow(2, 20 * t - 10) * Mathf.Sin((20 * t - 11.125f) * (2 * Mathf.PI) / 4.5f)) / 2 : (Mathf.Pow(2, -20 * t + 10) * Mathf.Sin((20 * t - 11.125f) * (2 * Mathf.PI) / 4.5f)) / 2 + 1;

    public static float EaseInBounce(float t) => 1 - EaseOutBounce(1 - t);
    public static float EaseOutBounce(float t)
    {
        if (t < 1 / 2.75f) return 7.5625f * t * t;
        else if (t < 2 / 2.75f) return 7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f;
        else if (t < 2.5 / 2.75) return 7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f;
        else return 7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f;
    }
    public static float EaseInOutBounce(float t) => t < 0.5f ? (1 - EaseOutBounce(1 - 2 * t)) / 2 : (1 + EaseOutBounce(2 * t - 1)) / 2;
    public static float Cut(float start, float end, int easingType, float progress)
    {
        float t = progress;
        float easedT = t;

        switch (easingType)
        {
            case 1: easedT = Easing.EaseInSine(t); break;
            case 2: easedT = Easing.EaseOutSine(t); break;
            case 3: easedT = Easing.EaseInOutSine(t); break;
            case 4: easedT = Easing.EaseInQuad(t); break;
            case 5: easedT = Easing.EaseOutQuad(t); break;
            case 6: easedT = Easing.EaseInOutQuad(t); break;
            case 7: easedT = Easing.EaseInCubic(t); break;
            case 8: easedT = Easing.EaseOutCubic(t); break;
            case 9: easedT = Easing.EaseInOutCubic(t); break;
            case 10: easedT = Easing.EaseInQuart(t); break;
            case 11: easedT = Easing.EaseOutQuart(t); break;
            case 12: easedT = Easing.EaseInOutQuart(t); break;
            case 13: easedT = Easing.EaseInQuint(t); break;
            case 14: easedT = Easing.EaseOutQuint(t); break;
            case 15: easedT = Easing.EaseInOutQuint(t); break;
            case 16: easedT = Easing.EaseInExpo(t); break;
            case 17: easedT = Easing.EaseOutExpo(t); break;
            case 18: easedT = Easing.EaseInOutExpo(t); break;
            case 19: easedT = Easing.EaseInCirc(t); break;
            case 20: easedT = Easing.EaseOutCirc(t); break;
            case 21: easedT = Easing.EaseInOutCirc(t); break;
            case 22: easedT = Easing.EaseInBack(t); break;
            case 23: easedT = Easing.EaseOutBack(t); break;
            case 24: easedT = Easing.EaseInOutBack(t); break;
            case 25: easedT = Easing.EaseInElastic(t); break;
            case 26: easedT = Easing.EaseOutElastic(t); break;
            case 27: easedT = Easing.EaseInOutElastic(t); break;
            case 28: easedT = Easing.EaseInBounce(t); break;
            case 29: easedT = Easing.EaseOutBounce(t); break;
            case 30: easedT = Easing.EaseInOutBounce(t); break;
            default: easedT = t; break;
        }
        return start + (end - start) * easedT;
    }
}