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
  public readonly struct Direction2D
  {
    /// <summary>
    /// No direction.
    /// </summary>
    public static readonly Direction2D None = new Direction2D(Nibble.Zero);

    /// <summary>
    /// Right direction.
    /// </summary>
    public static readonly Direction2D Right = new Direction2D((Nibble)CONST_RIGHT);

    /// <summary>
    /// Up-right direction.
    /// </summary>
    public static readonly Direction2D UpRight = new Direction2D((Nibble)(CONST_UP | CONST_RIGHT));

    /// <summary>
    /// Up direction.
    /// </summary>
    public static readonly Direction2D Up = new Direction2D((Nibble)CONST_UP);

    /// <summary>
    /// Up-left direction.
    /// </summary>
    public static readonly Direction2D UpLeft = new Direction2D((Nibble)(CONST_UP | CONST_LEFT));

    /// <summary>
    /// Left direction.
    /// </summary>
    public static readonly Direction2D Left = new Direction2D((Nibble)CONST_LEFT);

    /// <summary>
    /// Down-left direction.
    /// </summary>
    public static readonly Direction2D DownLeft = new Direction2D((Nibble)(CONST_DOWN | CONST_LEFT));

    /// <summary>
    /// Down direction.
    /// </summary>
    public static readonly Direction2D Down = new Direction2D((Nibble)CONST_DOWN);

    /// <summary>
    /// Down-right direction.
    /// </summary>
    public static readonly Direction2D DownRight = new Direction2D((Nibble)(CONST_DOWN | CONST_RIGHT));

    /// <summary>
    /// Right direction.
    /// </summary>
    public static readonly Direction2D East = Right;

    /// <summary>
    /// Up-right direction.
    /// </summary>
    public static readonly Direction2D NorthEast = UpRight;

    /// <summary>
    /// Up direction.
    /// </summary>
    public static readonly Direction2D North = Up;

    /// <summary>
    /// Up-left direction.
    /// </summary>
    public static readonly Direction2D NorthWest = UpLeft;

    /// <summary>
    /// Left direction.
    /// </summary>
    public static readonly Direction2D West = Left;

    /// <summary>
    /// Down-left direction.
    /// </summary>
    public static readonly Direction2D SouthWest = DownLeft;

    /// <summary>
    /// Down direction.
    /// </summary>
    public static readonly Direction2D South = Down;

    /// <summary>
    /// Down-right direction.
    /// </summary>
    public static readonly Direction2D SouthEast = DownRight;

    /// <summary>
    /// Up-right direction.
    /// </summary>
    public static readonly Direction2D ForwardRight = UpRight;

    /// <summary>
    /// Up direction.
    /// </summary>
    public static readonly Direction2D Forward = Up;

    /// <summary>
    /// Up-left direction.
    /// </summary>
    public static readonly Direction2D ForwardLeft = UpLeft;

    /// <summary>
    /// Down-left direction.
    /// </summary>
    public static readonly Direction2D BackLeft = DownLeft;

    /// <summary>
    /// Down direction.
    /// </summary>
    public static readonly Direction2D Back = Down;

    /// <summary>
    /// Down-right direction.
    /// </summary>
    public static readonly Direction2D BackRight = DownRight;

    /// <summary>
    /// Right constant.
    /// </summary>
    private const byte CONST_RIGHT = 0x01;

    /// <summary>
    /// Up constant.
    /// </summary>
    private const byte CONST_UP = 0x02;

    /// <summary>
    /// Left constant.
    /// </summary>
    private const byte CONST_LEFT = 0x04;

    /// <summary>
    /// Down constant.
    /// </summary>
    private const byte CONST_DOWN = 0x08;

    /// <summary>
    /// Radian lookup dictionary.
    /// </summary>
    private static readonly Dictionary<Nibble, Radians> RadianLookup = new Dictionary<Nibble, Radians>
    {
      { (Nibble)CONST_RIGHT,                new Radians(0) },
      { (Nibble)(CONST_UP | CONST_RIGHT),   new Radians(Mathf.PI / 4) },
      { (Nibble)CONST_UP,                   new Radians(Mathf.PI / 2) },
      { (Nibble)(CONST_UP | CONST_LEFT),    new Radians((3 * Mathf.PI) / 4) },
      { (Nibble)CONST_LEFT,                 new Radians(Mathf.PI) },
      { (Nibble)(CONST_DOWN | CONST_LEFT),  new Radians((5 * Mathf.PI) / 4) },
      { (Nibble)CONST_DOWN,                 new Radians((3 * Mathf.PI) / 2) },
      { (Nibble)(CONST_DOWN | CONST_RIGHT), new Radians((7 * Mathf.PI) / 4) }
    };

    /// <summary>
    /// Horizontal conflict constant.
    /// </summary>
    private static readonly Nibble HorizontalConflict = (Nibble)(CONST_RIGHT | CONST_LEFT);

    /// <summary>
    /// Vertical conflict constant.
    /// </summary>
    private static readonly Nibble VerticalConflict = (Nibble)(CONST_UP | CONST_DOWN);

    /// <summary>
    /// Internal direction value.
    /// </summary>
    private readonly Nibble value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Direction2D"/> struct.
    /// </summary>
    /// <param name="vector">Vector encoding direction.</param>
    public Direction2D(Vector2 vector)
    {
      if (vector == Vector2.zero)
      {
        this.value = Nibble.Zero;
        return;
      }

      this.value = new Direction2D(new Radians(vector)).value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Direction2D"/> struct.
    /// </summary>
    /// <param name="angle">Angle.</param>
    public Direction2D(Degrees angle)
      : this(angle.AsRadians())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Direction2D"/> struct.
    /// </summary>
    /// <param name="angle">Angle.</param>
    public Direction2D(Radians angle)
    {
      float radians = angle.FloatAsRadians();
      float increment = (radians / Mathf.PI) * 8;
      var nibble = Nibble.Zero;

      if (increment < 3 || increment > 13)
      {
        nibble |= (Nibble)CONST_RIGHT;
      }

      if (increment < 7 && increment > 1)
      {
        nibble |= (Nibble)CONST_UP;
      }

      if (increment < 11 && increment > 5)
      {
        nibble |= (Nibble)CONST_LEFT;
      }

      if (increment < 15 && increment > 9)
      {
        nibble |= (Nibble)CONST_DOWN;
      }

      this.value = nibble;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Direction2D"/> struct.
    /// </summary>
    /// <param name="directionValue">Direction value.</param>
    private Direction2D(Nibble directionValue)
    {
      this.value = directionValue;
    }

    public static bool operator ==(Direction2D left, Direction2D right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(Direction2D left, Direction2D right)
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
    public Direction2D RotateClockwise90()
    {
      return new Direction2D(this.value.RotateRight());
    }

    /// <summary>
    /// Generates a screen direction 2D that has been rotated counter clockwise 90 degrees.
    /// </summary>
    /// <returns>Rotated screen direction.</returns>
    public Direction2D RotateCounterClockwise90()
    {
      return new Direction2D(this.value.RotateLeft());
    }

    /// <summary>
    /// Generates a screen direction 2D that has been rotated clockwise 45 degrees.
    /// </summary>
    /// <returns>Rotated screen direction.</returns>
    public Direction2D RotateClockwise45()
    {
      return new Direction2D(this.value.RotateRight() | this.value);
    }

    /// <summary>
    /// Generates a screen direction 2D that has been rotated counter clockwise 45 degrees.
    /// </summary>
    /// <returns>Rotated screen direction.</returns>
    public Direction2D RotateCounterClockwise45()
    {
      return new Direction2D(this.value.RotateLeft() | this.value);
    }

    /// <summary>
    /// Determines whether this direction contains provided direction.
    /// </summary>
    /// <param name="screenDirection">Screen direction.</param>
    /// <returns>Whether this direction contains provided direction.</returns>
    public bool ContainsDirection(Direction2D screenDirection)
    {
      return this.value.ContainsAllFlags(screenDirection.value);
    }

    /// <summary>
    /// Adds direction component.
    /// </summary>
    /// <param name="directionComponent">Direction component.</param>
    /// <returns>New screen direction with given component added.</returns>
    public Direction2D AddDirectionComponent(Direction2D directionComponent)
    {
      return new Direction2D(this.AddDirectionComponent(directionComponent.value));
    }

    /// <summary>
    /// Removes direction component.
    /// </summary>
    /// <param name="directionComponent">Direction component.</param>
    /// <returns>New screen direction with given component removed.</returns>
    public Direction2D RemoveDirectionComponent(Direction2D directionComponent)
    {
      return new Direction2D(this.RemoveDirectionComponent(directionComponent.value));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return this.value.GetHashCode();
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      return obj is Direction2D dir && dir.value == this.value;
    }

    /// <summary>
    /// Gets value as radians.
    /// </summary>
    /// <returns>Radians.</returns>
    public Radians ToRadians()
    {
      if (this.value == UpRight.value)
      {
        return new Radians(Mathf.PI / 4);
      }

      if (this.value == Up.value)
      {
        return new Radians(Mathf.PI / 2);
      }

      if (this.value == UpLeft.value)
      {
        return new Radians((3 * Mathf.PI) / 4);
      }

      if (this.value == Left.value)
      {
        return new Radians(Mathf.PI);
      }

      if (this.value == DownLeft.value)
      {
        return new Radians((5 * Mathf.PI) / 4);
      }

      if (this.value == Down.value)
      {
        return new Radians((3 * Mathf.PI) / 2);
      }

      if (this.value == DownRight.value)
      {
        return new Radians((7 * Mathf.PI) / 4);
      }

      return new Radians(0);
    }

    /// <summary>
    /// Gets value as degrees.
    /// </summary>
    /// <returns>Degrees.</returns>
    public Degrees ToDegrees()
    {
      return this.ToRadians().AsDegrees();
    }

    /// <summary>
    /// Adds direction component.
    /// </summary>
    /// <param name="direction">Direction component.</param>
    /// <returns>New direction value.</returns>
    private Nibble AddDirectionComponent(Nibble direction)
    {
      Nibble value = this.value | direction;
      if (value.ContainsAllFlags(HorizontalConflict))
      {
        value &= ~HorizontalConflict;
      }

      if (value.ContainsAllFlags(VerticalConflict))
      {
        value &= ~VerticalConflict;
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