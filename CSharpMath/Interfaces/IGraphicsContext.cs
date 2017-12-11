﻿using System;
using System.Drawing;
using CSharpMath.FrontEnd;

namespace CSharpMath {
  /// <summary>Represents a front-end graphics context.  NOT related to IContext, CalculatorContext, etc.</summary>
  public interface IGraphicsContext<TGlyph> {
    void DrawLine(float x1, float y1, float x2, float y2);
    void DrawTextAtPoint(string text, PointF point, float maxWidth = float.NaN);
    void DrawGlyphsAtPoint(TGlyph[] glyphs, PointF point, float maxWidth = float.NaN);
    void Translate(PointF dxy);
    void SaveState();
    void RestoreState();
    void SetTextPosition(PointF position);
    IGlyphFinder<TGlyph> GlyphFinder { get; }
  }

}