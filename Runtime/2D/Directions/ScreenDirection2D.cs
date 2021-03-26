// MIT licenced.

using System.Collections.Generic;
using GrowlingPigeon.Math;
using UnityEngine;

#nullable enable

namespace GrowlingPigeon.Math2D
{
  /// <summary>
  /// Screen-space direction in 2D.
  /// </summary>
  public readonly struct ScreenDirection2D
  {
    /// <summary>
    /// No direction.
    /// </summary>
    public static readonly ScreenDirection2D None = new ScreenDirection2D(Nibble.Zero);

    /// <summary>
    /// Right direction.
    /// </summary>
    public static readonly ScreenDirection2D Right = new ScreenDirection2D(CONST_RIGHT);

    /// <summary>
    /// Up-right direction.
    /// </summary>
    public static readonly ScreenDirection2D UpRight = new ScreenDirection2D(CONST_UP | CONST_RIGHT);

    /// <summary>
    /// Up direction.
    /// </summary>
    public static readonly ScreenDirection2D Up = new ScreenDirection2D(CONST_UP);

    /// <summary>
    /// Up-left direction.
    /// </summary>
    public static readonly ScreenDirection2D UpLeft = new ScreenDirection2D(CONST_UP | CONST_LEFT);

    /// <summary>
    /// Left direction.
    /// </summary>
    public static readonly ScreenDirection2D Left = new ScreenDirection2D(CONST_LEFT);

    /// <summary>
    /// Down-left direction.
    /// </summary>
    public static readonly ScreenDirection2D DownLeft = new ScreenDirection2D(CONST_DOWN | CONST_LEFT);

    /// <summary>
    /// Down direction.
    /// </summary>
    public static readonly ScreenDirection2D Down = new ScreenDirection2D(CONST_DOWN);

    /// <summary>
    /// Down-right direction.
    /// </summary>
    public static readonly ScreenDirection2D DownRight = new ScreenDirection2D(CONST_DOWN | CONST_RIGHT);

    /// <summary>
    /// Radian lookup dictionary.
    /// </summary>
    private static readonly Dictionary<Nibble, Radians> RadianLookup = new Dictionary<Nibble, Radians>
    {
      { CONST_LEFT,               new Radians(0) },
      { CONST_UP | CONST_LEFT,    new Radians(Mathf.PI / 4) },
      { CONST_UP,                 new Radians(Mathf.PI / 2) },
      { CONST_UP | CONST_RIGHT,   new Radians((3 * Mathf.PI) / 4) },
      { CONST_RIGHT,              new Radians(Mathf.PI) },
      { CONST_DOWN | CONST_RIGHT, new Radians((5 * Mathf.PI) / 4) },
      { CONST_DOWN,               new Radians((3 * Mathf.PI) / 2) },
      { CONST_DOWN | CONST_LEFT,  new Radians((7 * Mathf.PI) / 4) }
    };

    /// <summary>
    /// Local constant right.
    /// </summary>
    private static readonly Nibble CONST_RIGHT = (Nibble)0x01;

    /// <summary>
    /// Local constant up.
    /// </summary>
    private static readonly Nibble CONST_UP = (Nibble)0x02;

    /// <summary>
    /// Local constant left.
    /// </summary>
    private static readonly Nibble CONST_LEFT = (Nibble)0x04;

    /// <summary>
    /// Local constant down.
    /// </summary>
    private static readonly Nibble CONST_DOWN = (Nibble)0x08;

    /// <summary>
    /// Horizontal conflict constant.
    /// </summary>
    private static readonly Nibble HORIZONTAL_CONFLICT = CONST_RIGHT | CONST_LEFT;

    /// <summary>
    /// Vertical conflict constant.
    /// </summary>
    private static readonly Nibble VERTICAL_CONFLICT = CONST_UP | CONST_DOWN;

    /// <summary>
    /// Internal direction value.
    /// </summary>
    private readonly Nibble value;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScreenDirection2D"/> struct.
    /// </summary>
    /// <param name="angle">Angle.</param>
    public ScreenDirection2D(Degrees angle)
      : this(angle.AsRadians())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScreenDirection2D"/> struct.
    /// </summary>
    /// <param name="angle">Angle.</param>
    public ScreenDirection2D(Radians angle)
    {
      float radians = angle.FloatAsRadians();
      float increment = (radians / Mathf.PI) * 8;
      var nibble = Nibble.Zero;

      if (increment < 3 || increment > 13)
      {
        nibble |= CONST_RIGHT;
      }

      if (increment < 7 && increment > 1)
      {
        nibble |= CONST_UP;
      }

      if (increment < 11 && increment > 5)
      {
        nibble |= CONST_LEFT;
      }

      if (increment < 15 && increment > 9)
      {
        nibble |= CONST_DOWN;
      }

      this.value = nibble;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScreenDirection2D"/> struct.
    /// </summary>
    /// <param name="directionValue">Direction value.</param>
    private ScreenDirection2D(Nibble directionValue)
    {
      this.value = directionValue;
    }

    public static bool operator ==(ScreenDirection2D left, ScreenDirection2D right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(ScreenDirection2D left, ScreenDirection2D right)
    {
      return !(left == right);
    }

    /// <summary>
    /// Gets direction as degrees.
    /// </summary>
    /// <returns>Degrees.</returns>
    public Degrees AsDegrees()
    {
      return this.AsRadians().AsDegrees();
    }

    /// <summary>
    /// Gets direction as radians.
    /// </summary>
    /// <returns>Radians.</returns>
    public Radians AsRadians()
    {
      return RadianLookup[this.value];
    }

    /// <summary>
    /// Generates a screen direction 2D that has been rotated clockwise 90 degrees.
    /// </summary>
    /// <returns>Rotated screen direction.</returns>
    public ScreenDirection2D RotateClockwise90()
    {
      return new ScreenDirection2D(this.value.RotateRight());
    }

    /// <summary>
    /// Generates a screen direction 2D that has been rotated counter clockwise 90 degrees.
    /// </summary>
    /// <returns>Rotated screen direction.</returns>
    public ScreenDirection2D RotateCounterClockwise90()
    {
      return new ScreenDirection2D(this.value.RotateLeft());
    }

    /// <summary>
    /// Generates a screen direction 2D that has been rotated clockwise 45 degrees.
    /// </summary>
    /// <returns>Rotated screen direction.</returns>
    public ScreenDirection2D RotateClockwise45()
    {
      return new ScreenDirection2D(this.value.RotateRight() | this.value);
    }

    /// <summary>
    /// Generates a screen direction 2D that has been rotated counter clockwise 45 degrees.
    /// </summary>
    /// <returns>Rotated screen direction.</returns>
    public ScreenDirection2D RotateCounterClockwise45()
    {
      return new ScreenDirection2D(this.value.RotateLeft() | this.value);
    }

    /// <summary>
    /// Determines whether this direction contains provided direction.
    /// </summary>
    /// <param name="screenDirection">Screen direction.</param>
    /// <returns>Whether this direction contains provided direction.</returns>
    public bool ContainsDirection(ScreenDirection2D screenDirection)
    {
      return this.value.ContainsAllFlags(screenDirection.value);
    }

    /// <summary>
    /// Adds direction component.
    /// </summary>
    /// <param name="directionComponent">Direction component.</param>
    /// <returns>New screen direction with given component added.</returns>
    public ScreenDirection2D AddDirectionComponent(ScreenDirection2D directionComponent)
    {
      return new ScreenDirection2D(this.AddDirectionComponent(directionComponent.value));
    }

    /// <summary>
    /// Removes direction component.
    /// </summary>
    /// <param name="directionComponent">Direction component.</param>
    /// <returns>New screen direction with given component removed.</returns>
    public ScreenDirection2D RemoveDirectionComponent(ScreenDirection2D directionComponent)
    {
      return new ScreenDirection2D(this.RemoveDirectionComponent(directionComponent.value));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return this.value.GetHashCode();
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      return obj is ScreenDirection2D dir && dir.value == this.value;
    }

    /// <summary>
    /// Adds direction component.
    /// </summary>
    /// <param name="direction">Direction component.</param>
    /// <returns>New direction value.</returns>
    private Nibble AddDirectionComponent(Nibble direction)
    {
      Nibble value = this.value | direction;
      if (value.ContainsAllFlags(HORIZONTAL_CONFLICT))
      {
        value &= ~HORIZONTAL_CONFLICT;
      }

      if (value.ContainsAllFlags(VERTICAL_CONFLICT))
      {
        value &= ~VERTICAL_CONFLICT;
      }

      return value;
    }

    /// <summary>
    /// Removes direction component.
    /// </summary>
    /// <param name="direction">Direction component.</param>
    /// <returns>New direction value.</returns>
    private Nibble RemoveDirectionComponent(Nibble direction)
    {
      return this.value & ~direction;
    }
  }
}