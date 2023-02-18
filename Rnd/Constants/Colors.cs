using System.Drawing;

namespace Rnd.Constants;

public static class Colors
{
    public static readonly Color[] Default = {
        Color.Silver,
        Color.Brown,
        Color.Salmon,
        Color.RosyBrown,
        Color.Orange,
        Color.SandyBrown,
        Color.Gold,
        Color.Khaki,
        Color.GreenYellow,
        Color.ForestGreen,
        Color.Teal,
        Color.Lavender,
        Color.Aquamarine,
        Color.SkyBlue,
        Color.DodgerBlue,
        Color.Navy,
        Color.MediumSlateBlue,
        Color.Purple,
        Color.Orchid,
        Color.MediumVioletRed,
    };

    public static Color GetRandom() => Default[Rand.Get.Next(0, Default.Length)];
    public static string GetRandomHtml() => ColorTranslator.ToHtml(GetRandom());
}