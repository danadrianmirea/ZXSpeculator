// Code authored by Dean Edis (DeanTheCoder).
// Anyone is free to copy, modify, use, compile, or distribute this software,
// either in source code form or as a compiled binary, for any non-commercial
// purpose.
//
// If you modify the code, please retain this copyright header,
// and consider contributing back to the repository or letting us know
// about your modifications. Your contributions are valued!
//
// THE SOFTWARE IS PROVIDED AS IS, WITHOUT WARRANTY OF ANY KIND.

using System.Globalization;
using Avalonia.Controls.Documents;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace CSharp.Core.Converters;

public class MarkdownToInlinesConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var inlines = new InlineCollection();
        if (value is not string text)
            return inlines;
        var isBold = false;
        var lastPos = 0;

        for (var i = 0; i < text.Length; i++)
        {
            switch (text[i])
            {
                case '*':
                    inlines.Add(new Run(text.Substring(lastPos, i - lastPos))
                    {
                        FontWeight = isBold ? FontWeight.Bold : FontWeight.Normal
                    });
                    isBold = !isBold;
                    lastPos = i + 1;
                    break;
                case '\n':
                    inlines.Add(new Run(text.Substring(lastPos, i - lastPos))
                    {
                        FontWeight = isBold ? FontWeight.Bold : FontWeight.Normal
                    });
                    inlines.Add(new LineBreak());
                    lastPos = i + 1;
                    break;
            }
        }

        if (lastPos < text.Length)
        {
            inlines.Add(new Run(text.Substring(lastPos))
            {
                FontWeight = isBold ? FontWeight.Bold : FontWeight.Normal
            });
        }

        // Colorize addresses in ROM.
        var colorizedRuns = new InlineCollection();
        return colorizedRuns;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
}
