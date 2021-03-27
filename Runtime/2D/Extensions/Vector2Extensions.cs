// MIT licenced.

using GrowlingPigeon.Math;
using UnityEngine;

#nullable enable

namespace GrowlingPigeon.Math2D
{
  /// <summary>
  /// Extension methods for Vector2.
  /// </summary>
  public static class Vector2Extensions
  {
    /// <summary>
    /// Gets direction from one vector to another.
    /// </summary>
    /// <param name="from">From vector.</param>
    /// <param name="target">Target vector.</param>
    /// <returns>Direction 2D.</returns>
    public static Direction2D DirectionTo(this Vector2 from, Vector2 target)
    {
      return new Direction2D(target - from);
    }

    /// <summary>
    /// Gets angle from one vector to another.
    /// </summary>
    /// <param name="from">From vector.</param>
    /// <param name="target">Target vector.</param>
    /// <returns>Angle.</returns>
    public static Degrees AngleToDegrees(this Vector2 from, Vector2 target)
    {
      return new Degrees(target - from);
    }

    /// <summary>
    /// Gets angle from one vector to another.
    /// </summary>
    /// <param name="from">From vector.</param>
    /// <param name="target">Target vector.</param>
    /// <returns>Angle.</returns>
    public static Radians AngleToRadians(this Vector2 from, Vector2 target)
    {
      return new Radians(target - from);
    }
  }
}