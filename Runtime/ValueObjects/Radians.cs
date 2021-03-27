// MIT Licensed.

using System.Runtime.CompilerServices;
using UnityEngine;

#nullable enable

namespace GrowlingPigeon.Math
{
  /// <summary>
  /// Angle in degrees.
  /// </summary>
  public readonly struct Radians
  {
    /// <summary>
    /// Full rotation.
    /// </summary>
    private const float FULL_ROTATION = Mathf.PI * 2;

    /// <summary>
    /// Internal value.
    /// </summary>
    private readonly float value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Degrees"/> struct.
    /// </summary>
    /// <param name="value">Value.</param>
    public Radians(float value)
    {
      this.value = NormalizeValue(value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Degrees"/> struct.
    /// </summary>
    /// <param name="angle">Angle.</param>
    public Radians(Degrees angle)
    {
      this.value = angle.FloatAsRadians();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Degrees"/> struct.
    /// </summary>
    /// <param name="angle">Angle.</param>
    public Radians(Radians angle)
    {
      this.value = angle.FloatAsRadians();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Radians"/> struct.
    /// </summary>
    /// <param name="vector">Vector that encodes angle.</param>
    public Radians(Vector2 vector)
      : this(Mathf.Atan2(vector.y, vector.x))
    {
    }

    /// <summary>
    /// Casts radians to radians.
    /// </summary>
    /// <param name="angle">Angle.</param>
    public static explicit operator Radians(Degrees angle)
    {
      return new Radians(angle);
    }

    /// <summary>
    /// Casts vector to radians.
    /// </summary>
    /// <param name="vector">Vector to cast.</param>
    public static explicit operator Radians(Vector2 vector)
    {
      return new Radians(vector);
    }

    public static Radians operator +(Radians left, Radians right)
    {
      return new Radians(left.FloatAsRadians() + right.FloatAsRadians());
    }

    public static Radians operator +(Radians left, Degrees right)
    {
      return new Radians(left.FloatAsRadians() + right.FloatAsRadians());
    }

    public static Radians operator -(Radians left, Radians right)
    {
      return new Radians(left.FloatAsRadians() - right.FloatAsRadians());
    }

    public static Radians operator -(Radians left, Degrees right)
    {
      return new Radians(left.FloatAsRadians() - right.FloatAsRadians());
    }

    public static Radians operator *(Radians left, Radians right)
    {
      return new Radians(left.FloatAsRadians() * right.FloatAsRadians());
    }

    public static Radians operator *(Radians left, Degrees right)
    {
      return new Radians(left.FloatAsRadians() * right.FloatAsRadians());
    }

    public static Radians operator /(Radians left, Radians right)
    {
      return new Radians(left.FloatAsRadians() / right.FloatAsRadians());
    }

    public static Radians operator /(Radians left, Degrees right)
    {
      return new Radians(left.FloatAsRadians() / right.FloatAsRadians());
    }

    /// <summary>
    /// Internal value as degrees.
    /// </summary>
    /// <returns>Degrees.</returns>
    public float FloatAsDegrees()
    {
      return this.value * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Internal value as radians.
    /// </summary>
    /// <returns>Radians.</returns>
    public float FloatAsRadians()
    {
      return this.value;
    }

    /// <summary>
    /// Casts to degrees.
    /// </summary>
    /// <returns>Degrees.</returns>
    public Degrees AsDegrees()
    {
      return (Degrees)this;
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