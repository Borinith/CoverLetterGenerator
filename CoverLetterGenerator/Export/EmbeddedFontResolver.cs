using PdfSharp.Fonts;
using System;

namespace CoverLetterGenerator.Export
{
    public sealed class EmbeddedFontResolver : IFontResolver
    {
        private const string FaceName = "Arimo";
        private const string ResourceName = "CoverLetterGenerator.Fonts.Arimo-Regular.ttf";

        public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
        {
            return new FontResolverInfo(FaceName);
        }

        public byte[]? GetFont(string faceName)
        {
            using var stream = typeof(EmbeddedFontResolver).Assembly.GetManifestResourceStream(ResourceName)
                ?? throw new InvalidOperationException($"Embedded font resource '{ResourceName}' not found.");

            var bytes = new byte[stream.Length];
            stream.ReadExactly(bytes);

            return bytes;
        }
    }
}