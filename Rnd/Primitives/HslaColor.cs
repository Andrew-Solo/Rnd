namespace Rnd.Primitives;

public record struct HslaColor
{
    public HslaColor(
        short hue,
        byte saturation = 100,
        byte lightness = 100,
        byte alpha = 100
    )
    {
        Hue = hue > 360 ? (short) 360 : hue;
        Saturation = saturation > 100 ? (byte) 100 : saturation;
        Lightness = lightness > 100 ? (byte) 100 : lightness;
        Alpha = alpha > 100 ? (byte) 100 : alpha;
    }

    public short Hue { get; }
    public byte Saturation { get; }
    public byte Lightness { get; }
    public byte Alpha { get; }

    public short[] ToArray()
    {
        return new[] {Hue, Saturation, Lightness, Alpha};
    }
}