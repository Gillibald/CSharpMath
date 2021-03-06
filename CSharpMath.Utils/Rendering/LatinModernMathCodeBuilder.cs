using System;
using System.IO;
using System.Text;

namespace CSharpMath.DevUtils.Rendering {
  static class LatinModernMathCodeBuilder {
    /// <summary>
    /// Builds the LatinModernMath.cs file located at CSharpMath\CSharpMath.SkiaSharp\Font Reference\LatinModernMath.cs
    /// from latinmodern-math.otf located at CSharpMath\CSharpMath.SkiaSharp\Font Reference\latinmodern-math.otf
    /// </summary>
    public static void Build() {
      const int numbersPerLine = 25;
      var bytes = File.ReadAllBytes(Paths.LatinModernMathSource);
      var b = new StringBuilder()
        .AppendLine("namespace CSharpMath.Rendering {")
        .AppendLine("  //Do not modify this file directly. Instead, modify this at")
        .AppendLine("  //CSharpMath\\CSharpMath.Utils\\Rendering\\LatinModernMathCodeBuilder.cs and re-generate")
        .AppendLine("  //this file by executing the method in that file in the CSharpMath.Utils project.")
        .AppendLine("  [System.Diagnostics.DebuggerNonUserCode, System.Runtime.CompilerServices.CompilerGenerated]")
        .AppendLine("  internal static partial class Resources {")
        .AppendLine("    public static byte[] LatinModernMath { get; } = new byte[] {");
      int i = 0;
      for (int l = bytes.Length - numbersPerLine; i < l; i += numbersPerLine)
        b.Append("      ").AppendJoin(", ", new ArraySegment<byte>(bytes, i, numbersPerLine)).AppendLine(", ");
      b
        .Append("      ").AppendJoin(", ", new ArraySegment<byte>(bytes, i, bytes.Length - i)).AppendLine()
        .AppendLine("    };")
        .AppendLine("  }")
        .AppendLine("}");

      File.WriteAllText(Paths.LatinModernMathCodeFile, b.ToString());
    }
  }
}