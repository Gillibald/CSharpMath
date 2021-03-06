using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpMath.SkiaSharp {
  public class SkiaPath : Rendering.IPath {
    public SkiaPath(SkiaCanvas owner) => _owner = owner;

    private SkiaCanvas _owner;
    private global::SkiaSharp.SKPath _path = new global::SkiaSharp.SKPath();
    public void BeginRead(int contourCount) { }
    public void EndRead() {
      _owner.Canvas.DrawPath(_path, _owner.Paint);
      _path.Dispose();
      _path = null;
    }
    public void CloseContour() => _path.Close();
    public void Curve3(float x1, float y1, float x2, float y2) => _path.QuadTo(x1, y1, x2, y2);
    public void Curve4(float x1, float y1, float x2, float y2, float x3, float y3) => _path.CubicTo(x1, y1, x2, y2, x3, y3);
    public void LineTo(float x1, float y1) => _path.LineTo(x1, y1);
    public void MoveTo(float x0, float y0) { _path.Close(); _path.MoveTo(x0, y0); }
  }
}
