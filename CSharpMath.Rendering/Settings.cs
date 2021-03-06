namespace CSharpMath {
  using System.Collections.Generic;
  public static class Settings {
    public static bool DisableEnhancedTextPainterColors {
      get => Rendering.TextSource.NoEnhancedColors;
      set => Rendering.TextSource.NoEnhancedColors = value;
    }
    public static Rendering.Typefaces GlobalTypefaces => Rendering.Fonts.GlobalTypefaces;

    public static BiDictionary<string, Structures.Color> PredefinedColors =>
      Structures.Color.PredefinedColors;
    public static MultiDictionary<string, string> PredefinedBoundaryDelimiters =>
      Atoms.MathAtoms.BoundaryDelimiters;
    public static MultiDictionary<string, Enumerations.FontStyle> PredefinedFontStyles =>
      Enumerations.FontStyleExtensions.FontStyles;
    public static BiDictionary<string, Atoms.MathAtom> PredefinedLaTeXSymbols =>
      Atoms.MathAtoms.Commands;
    public static Dictionary<string, string> PredefinedLaTeXSymbolAliases =>
      Atoms.MathAtoms.Aliases;
    public static Dictionary<string, Structures.Space> PredefinedLengthUnits =>
      Structures.Space.PredefinedLengthUnits;
  }
}
