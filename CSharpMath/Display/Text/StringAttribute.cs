﻿using CSharpMath.Atoms;
using System;
using System.Collections.Generic;

namespace CSharpMath.Display.Text {
  public class StringAttribute<T> {
    private List<(int index, T t)> _values = new List<(int index, T t)>(); // the integer is the character at which the string starts having the value for the attribute. The list is always kept sorted by index.
    public T this[int index] {
      get {
        T r = default;
        foreach ((int i, T t) in _values) {
          if (i <= index) {
            r = t;
          } else {
            break;
          }
        }
        return r;
      }
    }
    public StringAttribute(T t) {
      _values.Add((0, t));
    }
    public void Set(T value, Range range) {
      var endValue = this[range.End];
      int n;
      // Remove any stops that are inside the range, and insert the new one.
      for (n = _values.Count; n>=0; n--) {
        if (_values[n].index < range.Location) {
          _values.Insert(n, (range.Location, value));
          break;
        }
        if (_values[n].index == range.Location) {
          _values[n] = (range.Location, value);
          break;
        }
        if (_values[n].index < range.End) { // and also range.Location <= _values[n].index, but this is implied by the previous "if" statements.
          _values.RemoveAt(n);
        } 
      }
      // n is now the index where we inserted the start for our range. We still
      // have to remove the end one.
      if (_values.Count == n+1) {
        _values.Add((range.End, endValue));
      } else {
        if (_values[n + 1].index == range.End) {
          _values.RemoveAt(n + 1);
        }
        _values.Insert(n + 1, (range.End, endValue));
      }
    }
    public void Append(StringAttribute<T> other, int myLength) {
      T endValue = this[myLength];
      bool first = false;
      foreach (var (index, t) in other._values) {
        if (first && t.Equals(endValue)) {
          continue;
        }
        else {
          _values.Add((myLength + index, t));
        }
      }
    }
  }
  public static class StringAttributeExtensions {
    public static StringAttribute<T> Combine<T>(this StringAttribute<T> attr1, StringAttribute<T> attr2, int length1)
    where T : IEquatable<T> {
      if (attr1 == null) {
        return attr2;
      } if (attr2 == null) {
        return attr1;
      }
      attr1.Append(attr2, length1);
      return attr1;
    }
  }
}
