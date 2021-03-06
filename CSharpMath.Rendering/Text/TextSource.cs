namespace CSharpMath.Rendering {
  public readonly struct TextSource : ISource {
    public static bool NoEnhancedColors { get; set; } = false;

    public TextSource(string text) {
      Text = text;
      var result = TextBuilder.Build(text, !NoEnhancedColors);
      if (result.Error != null) {
        ErrorMessage = result.Error;
        Atom = null;
      } else {
        Atom = result.Value;
        ErrorMessage = null;
      }
    }
    public TextSource(TextAtom atom) {
      Atom = atom;
      Text = TextBuilder.Unbuild(atom, new System.Text.StringBuilder()).ToString();
      ErrorMessage = null;
    }

    public string Text { get; }
    public TextAtom Atom { get; }
    public string ErrorMessage { get; }
    public bool IsValid => Atom != null;
  }
}
