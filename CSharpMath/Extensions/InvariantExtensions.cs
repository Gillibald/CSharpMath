using System;
using System.Globalization;

namespace CSharpMath {
  public static class InvariantExtensions {
    public static string ToStringInvariant<T>(this T value) where T : IFormattable =>
      value.ToString(null, CultureInfo.InvariantCulture);
  }
}