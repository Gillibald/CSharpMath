﻿using SkiaSharp;
using System.Drawing;
using Typography.OpenFont;
using TFont = CSharpMath.SkiaSharp.SkiaMathFont;
using CSharpMath.FrontEnd;
using System.Linq;

namespace CSharpMath.SkiaSharp {
  public class SkiaGraphicsContext : IGraphicsContext<TFont, Glyph> {
    protected SKPaint glyphPaint = new SKPaint { IsStroke = true, StrokeCap = SKStrokeCap.Round };

    public SKCanvas Canvas { get; set; }
    public bool DrawGlyphBoxes { get; set; }
    public SKColor Color { get => glyphPaint.Color; set => glyphPaint.Color = value; }
    public SKPaintStyle PaintStyle { get => glyphPaint.Style; set => glyphPaint.Style = value; }

    public PointF TextPosition { get; set; }
    void IGraphicsContext<TFont, Glyph>.SetTextPosition(PointF position) {
      Debug($"TextPosition ({position.X}, {position.Y})");
      TextPosition = position;
    }

    public void DrawGlyphsAtPoints(Glyph[] glyphs, TFont font, PointF[] points) {
      Debug($"Glyphs {string.Join("; ", glyphs.Zip(points, (g, p) => $"{g.GetCff1GlyphData().Name} ({p.X}, {p.Y})"))} ");

      var typeface = font.Typeface;
      var pathBuilder = new SkiaGlyphPathBuilder(typeface);
      var path = new SkiaGlyphPath();
      Canvas.Save();
      Canvas.Translate(TextPosition.X, TextPosition.Y);
      if (DrawGlyphBoxes) {
        RectangleF[] rects = new SkiaGlyphBoundsProvider().GetBoundingRectsForGlyphs(font, glyphs);
        for (int i = 0; i < rects.Length; i++) {
          var rect = rects[i];
          var point = points[i];
          Canvas.DrawRect(point.X + rect.X, point.Y + rect.Y, rect.Width, rect.Height, new SKPaint() { Color = SKColors.Red, Style = SKPaintStyle.Stroke });
        }
      }
      for (int i = 0; i < glyphs.Length; i++) {
        pathBuilder.BuildFromGlyphIndex(glyphs[i].GlyphIndex, font.PointSize);
        pathBuilder.ReadShapes(path);
        Canvas.Save();
        
        Canvas.Translate(points[i].X, points[i].Y);
        Canvas.DrawPath(path.Path, glyphPaint);
        Canvas.Restore();
        path.Clear();
      }
      Canvas.Restore();
    }

    public void DrawLine(float x1, float y1, float x2, float y2, float lineThickness) {
      Debug($"Line ({x1}, {y1}) -> ({x2}, {y2})");
      var paint = glyphPaint.Clone();
      paint.StrokeWidth = lineThickness;
      Canvas.DrawLine(x1, y1, x2, y2, paint);
    }

    public void DrawGlyphRunWithOffset(Display.Text.AttributedGlyphRun<TFont, Glyph> run, PointF offset, float maxWidth = float.NaN) {
      Debug($"Text {run.Text} ({offset.X}, {offset.Y})");
      var textPosition = TextPosition.Plus(offset);

      if (DrawGlyphBoxes) {
        var box = run.Font.GlyphLayout.LayoutAndMeasureString(run.Text.ToCharArray(), 0, run.Text.Length, run.Font.PointSize);
        Canvas.DrawRect(textPosition.X, textPosition.Y, box.width + run.KernedGlyphs.Sum(g => g.KernAfterGlyph), box.btbd, new SKPaint() { Color = SKColors.Blue, Style = SKPaintStyle.Stroke });
      }

      var typeface = run.Font.Typeface;
      var glyphs = run.KernedGlyphs;
      var pointSize = run.Font.PointSize;
      var layout = run.Font.GlyphLayout;
      var pathBuilder = new SkiaGlyphPathBuilder(typeface);
      var path = new SkiaGlyphPath();
      var scale = typeface.CalculateScaleToPixelFromPointSize(pointSize);
      Canvas.Save();
      Canvas.Translate(textPosition.X, textPosition.Y);
      for (int i = 0; i < glyphs.Length; i++) {
        var index = glyphs[i].Glyph.GlyphIndex;
        pathBuilder.BuildFromGlyphIndex(index, pointSize);
        pathBuilder.ReadShapes(path);
        Canvas.DrawPath(path.Path, glyphPaint);
        Canvas.Translate(typeface.GetHAdvanceWidthFromGlyphIndex(index) * scale + glyphs[i].KernAfterGlyph, 0);
        path.Clear();
      }
      Canvas.Restore();
    }

    public void RestoreState() {
      Debug("Restore");
      Canvas.Restore();
    }

    public void SaveState() {
      Debug("Save");
      Canvas.Save();
    }

    public void Translate(PointF dxy) {
      Debug($"Translate ({dxy.X}, {dxy.Y})");
      Canvas.Translate(dxy.X, dxy.Y);
    }
    
    [System.Diagnostics.DebuggerStepThrough, System.Diagnostics.Conditional("DEBUG")]
    private void Debug(string message) {
      System.Diagnostics.Debug.WriteLine(message); //comment out to avoid spamming the debug output
    }
  }
}
