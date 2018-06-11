﻿using System.Drawing;
using System.Linq;
using Typography.OpenFont;
using static Typography.Contours.MyMath;
using CSharpMath.Display.Text;
using CSharpMath.FrontEnd;
using TFont = CSharpMath.Rendering.MathFont;

namespace CSharpMath.Rendering {
  public class GlyphBoundsProvider: IGlyphBoundsProvider<TFont, Glyph> {

    public (float[] Advances, float Total) GetAdvancesForGlyphs(TFont font, Glyph[] glyphs) {
      var typeface = font.Typeface;
      var nGlyphs = glyphs.Length;
      var advanceSizes = new float[nGlyphs];
      var scale = typeface.CalculateScaleToPixelFromPointSize(font.PointSize);
      var total = 0f;
      for (int i = 0; i < nGlyphs; i++) total += advanceSizes[i] = font.Typeface.GetHAdvanceWidthFromGlyphIndex(glyphs[i].GlyphIndex) * scale;
      return (advanceSizes, total);
    }

    public RectangleF[] GetBoundingRectsForGlyphs(TFont font, Glyph[] glyphs)
    {
      var scale = font.Typeface.CalculateScaleToPixelFromPointSize(font.PointSize);
      var rects = new RectangleF[glyphs.Length];
      for (int i = 0; i < glyphs.Length; i++) {
        var bounds = glyphs[i].Bounds;
        var obounds = glyphs[i].GetOriginalBounds();
        rects[i] = RectangleF.FromLTRB(obounds.XMin * scale, bounds.YMin * scale, obounds.XMax * scale, bounds.YMax * scale);
      }
      return rects;
    }

    public float GetTypographicWidth(TFont font, AttributedGlyphRun<TFont, Glyph> run) {
      var stringBox = font.GlyphLayout.LayoutAndMeasureString(run.Text.ToCharArray(), 0, run.Text.Length, font.PointSize);
      return stringBox.width + run.KernedGlyphs.Sum(g => g.KernAfterGlyph);
    }
  }
}