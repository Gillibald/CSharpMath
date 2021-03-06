using CSharpMath.Enumerations;
using CSharpMath.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpMath.Atoms {
  public class Space : MathAtom, IMathAtom, ISpace {
    private Structures.Space _space;

    public float Length => _space.Length;

    public bool IsMu => _space.IsMu;

    public Space(Structures.Space space) : base(MathAtomType.Space, string.Empty) =>
      _space = space;

    public Space(Space cloneMe, bool finalize) : base(cloneMe, finalize) =>
      _space = cloneMe._space;

    public float ActualLength<TFont, TGlyph>(FrontEnd.FontMathTable<TFont, TGlyph> mathTable, TFont font)
      where TFont : Display.MathFont<TGlyph> => _space.ActualLength(mathTable, font);

    public override string StringValue => " ";

    public override bool Equals(object obj) => obj is Space s ? EqualsSpace(s) : false;

    public bool EqualsSpace(Space otherSpace) =>
      EqualsAtom(otherSpace)
      && Length == otherSpace.Length
      && IsMu == otherSpace.IsMu;

    public override int GetHashCode() =>
      unchecked(base.GetHashCode() + 3 * _space.GetHashCode());

    public override T Accept<T, THelper>(IMathAtomVisitor<T, THelper> visitor, THelper helper)
      => visitor.Visit(this, helper);
  }
}