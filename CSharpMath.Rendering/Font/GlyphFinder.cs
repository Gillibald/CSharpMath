using System.Linq;
using CSharpMath.FrontEnd;

namespace CSharpMath.Rendering {
  public class GlyphFinder : IGlyphFinder<Fonts, Glyph> {
    private GlyphFinder() { }

    //http://unicode.org/charts/PDF/U25A0.pdf
    //U+25A1 WHITE SQUARE may be used to represent a missing ideograph
    //The glyph of this character is in the Latin Modern Math font
    public const char GlyphNotFound = '□';

    public static GlyphFinder Instance { get; } = new GlyphFinder();
    
    public Glyph Lookup(Fonts fonts, int codepoint) {
      foreach (var font in fonts) {
        var g = font.Lookup(codepoint);
        if (g.GlyphIndex != 0) return new Glyph(font, g);
      }
      return new Glyph(fonts.MathTypeface, fonts.MathTypeface.Lookup(GlyphNotFound));
    }

    public Glyph FindGlyphForCharacterAtIndex(Fonts fonts, int index, string str) {
      var codepoint = char.ConvertToUtf32(str, index - (char.IsLowSurrogate(str[index]) ? 1 : 0));
      return Lookup(fonts, codepoint);
    }

    public Glyph[] FindGlyphs(Fonts fonts, string str) =>
      Typography.OpenFont.StringUtils.GetCodepoints(str.ToCharArray()).Select(c => Lookup(fonts, c)).ToArray();

    public bool GlyphIsEmpty(Glyph glyph) => glyph.IsEmpty;
  }
}
