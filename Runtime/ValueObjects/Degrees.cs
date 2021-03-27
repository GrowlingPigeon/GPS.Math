// MIT Licensed.

using System.Runtime.CompilerServices;
using UnityEngine;

#nullable enable

namespace GrowlingPigeon.Math
{
  /// <summary>
  /// Angle in degrees.
  /// </summary>
  public readonly struct Degrees
  {
    /// <summary>
    /// Full rotation.
    /// </summary>
    private const float FULL_ROTATION = 360;

    /// <summary>
    /// Internal value.
    /// </summary>
    private readonly float value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Degrees"/> struct.
    /// </summary>
    /// <param name="value">Value.</param>
    public Degrees(float value)
    {
      this.value = NormalizeValue(value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Degrees"/> struct.
    /// </summary>
    /// <param name="angle">Angle.</param>
    public Degrees(Degrees angle)
    {
      this.value = angle.FloatAsDegrees();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Degrees"/> struct.
    /// </summary>
    /// <param name="angle">Angle.</param>
    public Degrees(Radians angle)
    {
      this.value = angle.FloatAsDegrees();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Degrees"/> struct.
    /// </summary>
    /// <param name="vector">Vector that encodes angle.</param>
    public Degrees(Vector2 vector)
      : this(Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg)
    {
    }

    /// <summary>
    /// Casts radians to degrees.
    /// </summary>
    /// <param name="angle">Angle.</param>
    public static explicit operator Degrees(Radians angle)
    {
      return new Degrees(angle);
    }

    /// <summary>
    /// Casts vector to degrees.
    /// </summary>
    /// <param name="vector">Vector to cast.</param>
    public static explicit operator Degrees(Vector2 vector)
    {
      return new Degrees(vector);
    }

    public static Degrees operator +(Degrees left, Degrees right)
    {
      return new Degrees(left.FloatAsDegrees() + right.FloatAsDegrees());
    }

    public static Degrees operator +(Degrees left, Radians right)
    {
      return new Degrees(left.FloatAsDegrees() + right.FloatAsDegrees());
    }

    public static Degrees operator -(Degrees left, Degrees right)
    {
      return new Degrees(left.FloatAsDegrees() - right.FloatAsDegrees());
    }

    public static Degrees operator -(Degrees left, Radians right)
    {
      return new Degrees(left.FloatAsDegrees() - right.FloatAsDegrees());
    }

    public static Degrees operator *(Degrees left, Degrees right)
    {
      return new Degrees(left.FloatAsDegrees() * right.FloatAsDegrees());
    }

    public static Degrees operator *(Degrees left, Radians right)
    {
      return new Degrees(left.FloatAsDegrees() * right.FloatAsDegrees());
    }

    public static Degrees operator /(Degrees left, Degrees right)
    {
      return new Degrees(left.FloatAsDegrees() / right.FloatAsDegrees());
    }

    public static Degrees operator /(Degrees left, Radians right)
    {
      return new Degrees(left.FloatAsDegrees() / right.FloatAsDegrees());
    }

    /// <summary>
    /// Internal value as degrees.
    /// </summary>
    /// <returns>Degrees.</returns>
    public float FloatAsDegrees()
    {
      return this.value;
    }

    /// <summary>
    /// Internal value as radians.
    /// </summary>
    /// <returns>Radians.</returns>
    public float FloatAsRadians()
    {
      return this.value * Mathf.Deg2Rad;
    }

    /// <summary>
    /// Casts to radians.
    /// </summary>
    /// <returns>Radians.</returns>
    public Radians AsRadians()
    {
      return (Radians)this;
    }

    /// <summary>
    /// Converts value to Vector2 which encodes angle.
    /// </summary>
    /// <returns>Vector.</returns>
    public Vector2 ToVector2()
    {
      return new Vector2
      {
        x = GPSMath.Cos(this),
        y = GPSMath.Sin(this)
      };
    }

    /// <summary>
    /// Normalizes value.
    /// </summary>
    /// <param name="value">Value to normalize.</param>
    /// <returns>Normalized value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float NormalizeValue(float value)
    {
      if (value < 0)
      {
        return FULL_ROTATION - (-value) % FULL_ROTATION;
      }
      else
      {
        return value % FULL_ROTATION;
      }
    }
  }
}