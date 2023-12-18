using Avalonia.Media;

namespace Speculator.Core;

public static class FrameBuffer
{
    private unsafe static Span<byte> GetPixel(byte* framePtr, int frameBufferRowBytes, int x, int y)
    {
        const int bytesPerPixel = 4;
        var offset = frameBufferRowBytes * y + bytesPerPixel * x;
        return new Span<byte>(framePtr + offset, bytesPerPixel);
    }

    public unsafe static void SetPixel(byte* framePtr, int frameBufferRowBytes, int x, int y, Color color)
    {
        var pixel = GetPixel(framePtr, frameBufferRowBytes, x, y);
        var alpha = color.A / 255.0;
        pixel[0] = (byte)(color.R * alpha);
        pixel[1] = (byte)(color.G * alpha);
        pixel[2] = (byte)(color.B * alpha);
        pixel[3] = color.A;
    }
}