// MIT Licensed.

#nullable enable

namespace GrowlingPigeonStudio.Math
{
  /// <summary>
  /// Nibble (half of a byte).
  /// </summary>
  public readonly struct Nibble
  {
    /// <summary>
    /// Nibble mask.
    /// </summary>
    private const byte NIBBLE_MASK = 0x0F;

    /// <summary>
    /// Internal value.
    /// </summary>
    private readonly byte value;

    /// <summary>
    /// Zero nibble.
    /// </summary>
    public static Nibble Zero => new Nibble(0);

    /// <summary>
    /// Nibble with lowest value (0).
    /// </summary>
    public static Nibble MinValue => new Nibble(0);

    /// <summary>
    /// Nibble with highest value (15).
    /// </summary>
    public static Nibble MaxValue => new Nibble(15);

    /// <summary>
    /// Initializes a new instance of the <see cref="Nibble"/> struct.
    /// </summary>
    /// <param name="value">Value.</param>
    private Nibble(int value)
      : this((byte)value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Nibble"/> struct.
    /// </summary>
    /// <param name="value">Value.</param>
    private Nibble(short value)
      : this((byte)value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Nibble"/> struct.
    /// </summary>
    /// <param name="value">Value.</param>
    private Nibble(byte value)
    {
      this.value = (byte)(value & NIBBLE_MASK);
    }

    public static bool operator ==(Nibble left, Nibble right)
    {
      return left.value == right.value;
    }

    public static bool operator !=(Nibble left, Nibble right)
    {
      return left.value != right.value;
    }

    public static explicit operator Nibble(byte value)
    {
      return new Nibble(value);
    }

    public static explicit operator Nibble(short value)
    {
      return new Nibble(value);
    }

    public static explicit operator Nibble(int value)
    {
      return new Nibble(value);
    }

    public static explicit operator byte(Nibble nibble)
    {
      return nibble.value;
    }

    public static explicit operator short(Nibble nibble)
    {
      return nibble.value;
    }

    public static explicit operator int(Nibble nibble)
    {
      return nibble.value;
    }

    public static Nibble operator &(Nibble left, Nibble right)
    {
      return (Nibble)(left.value & right.value);
    }

    public static Nibble operator ~(Nibble nibble)
    {
      return (Nibble)~nibble.value;
    }

    public static bool operator >(Nibble left, Nibble right)
    {
      return left.value > right.value;
    }

    public static bool operator <(Nibble left, Nibble right)
    {
      return left.value < right.value;
    }

    /// <summary>
    /// Gets byte representation of nibble.
    /// </summary>
    /// <returns></returns>
    public byte AsByte()
    {
      return this.value;
    }

    /// <summary>
    /// Determines whether all provided flags appear in this nibble.
    /// </summary>
    /// <param name="flags">Flags.</param>
    /// <returns>Whether all provided flags appear in this nibble.</returns>
    public bool ContainsAllFlags(Nibble flags)
    {
      return (this & flags) == flags;
    }

    /// <summary>
    /// Determines whether at least one provided flag appears in this nibble.
    /// </summary>
    /// <param name="flags">Flags.</param>
    /// <returns>Whether at least one provided flag appears in this nibble.</returns>
    public bool ContainsOneFlag(Nibble flags)
    {
      return (this & flags) > (Nibble)0;
    }

    public override bool Equals(object? obj)
    {
      return obj is Nibble nibble && this.value == nibble.value;
    }

    public override int GetHashCode()
    {
      return -1584136870 + this.value.GetHashCode();
    }

    /// <summary>
    /// Returns a new nibble rotated right.
    /// </summary>
    /// <returns>Rotated nibble.</returns>
    public Nibble RotateRight()
    {
      byte value = this.value;
      int bottomBit = (value & 1) << 3;
      return new Nibble((value >> 1) | bottomBit);
    }

    /// <summary>
    /// Returns a new nibble rotated right.
    /// </summary>
    /// <returns>Rotated nibble.</returns>
    public Nibble RotateLeft()
    {
      byte value = this.value;
      int topBit = (value & 8) >> 3;
      return new Nibble((value << 1) | topBit);
    }

    public static Nibble operator |(Nibble left, Nibble right)
    {
      return (Nibble)(left.value | right.value);
    }
  }
}