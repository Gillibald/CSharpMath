﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CSharpMath {
  public static class PointFExtensions {
    public static PointF Plus(this PointF point1, PointF point2)
      => new PointF(point1.X + point2.X, point1.Y + point2.Y);
  }
}
