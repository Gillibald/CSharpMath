﻿using CSharpMath.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMath.Display.Text;

namespace CSharpMath.Tests {
  public static class IDisplayTestExtensions {
    public static string StringText(this TextLineDisplay<MathFont<char>, char> display)
      => new string(display.Text.ToArray());
  }
}
