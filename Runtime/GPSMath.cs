// MIT Licensed.

using UnityEngine;

#nullable enable

namespace GrowlingPigeon.Math
{
  /// <summary>
  /// GPS owned Math collection.
  /// </summary>
  public static class GPSMath
  {
    public static Radians Acos(float cosine)
    {
      return new Radians(Mathf.Acos(cosine));
    }

    public static Radians Asin(float sine)
    {
      return new Radians(Mathf.Asin(sine));
    }

    public static Radians Atan(float tan)
    {
      return new Radians(Mathf.Atan(tan));
    }

    public static Radians Atan2(float y, float x)
    {
      return new Radians(Mathf.Atan2(y, x));
    }

    public static float Cos(Radians angle)
    {
      return Mathf.Cos(angle.FloatAsRadians());
    }

    public static float Cos(Degrees angle)
    {
      return Mathf.Cos(angle.FloatAsRadians());
    }

    public static Degrees DeltaAngle(Degrees current, Degrees target)
    {
      return new Degrees(Mathf.DeltaAngle(current.FloatAsDegrees(), target.FloatAsDegrees()));
    }

    public static Radians DeltaAngle(Radians current, Radians target)
    {
      return new Degrees(Mathf.DeltaAngle(current.FloatAsDegrees(), target.FloatAsDegrees())).AsRadians();
    }

    public static Degrees LerpAngle(Degrees minAngle, Degrees maxAngle, float time)
    {
      return new Degrees(Mathf.LerpAngle(minAngle.FloatAsDegrees(), maxAngle.FloatAsDegrees(), time));
    }

    public static Radians LerpAngle(Radians minAngle, Radians maxAngle, float time)
    {
      return new Degrees(Mathf.LerpAngle(minAngle.FloatAsDegrees(), maxAngle.FloatAsDegrees(), time)).AsRadians();
    }

    public static Degrees MoveTowardsAngle(Degrees current, Degrees target, float maxDelta)
    {
      return new Degrees(Mathf.MoveTowardsAngle(current.FloatAsDegrees(), target.FloatAsDegrees(), maxDelta));
    }

    public static Radians MoveTowardsAngle(Radians current, Radians target, float maxDelta)
    {
      return new Degrees(Mathf.MoveTowardsAngle(current.FloatAsDegrees(), target.FloatAsDegrees(), maxDelta)).AsRadians();
    }

    public static float Sin(Radians angle)
    {
      return Mathf.Sin(angle.FloatAsRadians());
    }

    public static float Sin(Degrees angle)
    {
      return Mathf.Sin(angle.FloatAsRadians());
    }

    public static Degrees SmoothDampAngle(
      Degrees current,
      Degrees target,
      ref float currentVelocity,
      float smoothTime,
      float maxSpeed,
      float deltaTime)
    {
      return new Degrees(
        Mathf.SmoothDampAngle(
          current.FloatAsDegrees(),
          target.FloatAsDegrees(),
          ref currentVelocity,
          smoothTime,
          maxSpeed,
          deltaTime));
    }

    public static Radians SmoothDampAngle(
      Radians current,
      Radians target,
      ref float currentVelocity,
      float smoothTime,
      float maxSpeed,
      float deltaTime)
    {
      return new Degrees(
        Mathf.SmoothDampAngle(
          current.FloatAsDegrees(),
          target.FloatAsDegrees(),
          ref currentVelocity,
          smoothTime,
          maxSpeed,
          deltaTime)).AsRadians();
    }

    public static float Tan(Radians angle)
    {
      return Mathf.Tan(angle.FloatAsRadians());
    }

    public static float Tan(Degrees angle)
    {
      return Mathf.Tan(angle.FloatAsRadians());
    }
  }
}