using PdfSharp.Fonts;
using System;

namespace CoverLetterGenerator.Export
{
    public sealed class EmbeddedFontResolver : IFontResolver
    {
        private const string FACE_NAME = "Arimo";
        private const string RESOURCE_NAME = "CoverLetterGenerator.Fonts.Arimo-Regular.ttf";

        public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
        {
            return new FontResolverInfo(FACE_NAME);
        }

        public byte[]? GetFont(string faceName)
        {
            using var stream = typeof(EmbeddedFontResolver).Assembly.GetManifestResourceStream(RESOURCE_NAME)
                ?? throw new InvalidOperationException($"Embedded font resource '{RESOURCE_NAME}' not found.");

            var bytes = new byte[stream.Length];
            stream.ReadExactly(bytes);

            return bytes;
        }
    }
}