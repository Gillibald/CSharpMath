using System;
using System.Collections.Generic;
using CSharpMath.Atoms;
using System.Diagnostics;
using System.Linq;

namespace CSharpMath.Display.Text {
  /// <summary>Like an attributed string, but the attributes other than Kern are required to be fixed
  /// over the whole string. We use KernedGlyph objects instead of Glyphs to
  /// allow us to set kern on a per-glyph basis.</summary>
  public class AttributedGlyphRun<TFont, TGlyph>
    where TFont : MathFont<TGlyph> {
    public bool Placeholder { get; set; }

    public IEnumerable<TGlyph> Glyphs => KernedGlyphs.Select(g => g.Glyph);
    public KernedGlyph<TGlyph>[] KernedGlyphs { get; set; }
    public string Text { get; set; }

    public int Length => KernedGlyphs.Length;
    public TFont Font { get; set; }

    public override string ToString() => $"AttributedGlyphRun {KernedGlyphs.Length} glyphs";

    public AttributedGlyphRun() {
    }
  }

 
  public static class AttributedGlyphRunExtensions {
    public static bool AttributesMatch<TFont, TGlyph>(this AttributedGlyphRun<TFont, TGlyph> run1, AttributedGlyphRun<TFont, TGlyph> run2)
      where TFont: MathFont<TGlyph> {
      if (run1 is null || run2 is null) {
        return false;
      }
      return run1.Font.Equals(run2.Font);
    }
  }
}
