﻿#if UNITY_EDITOR
#define BITSTACK_DEBUG
#endif

#if NET_4_6 && !BITSTACK_DISABLE_INLINE
#define BITSTACK_METHOD_INLINE
#endif

#if BITSTACK_METHOD_INLINE
using System.Runtime.CompilerServices;
#endif

namespace BitStack {

	/**
	 * Contains useful extension methods for the uint Value datatype.
	 * uint is an unsigned 32 bit value.
	 * 
	 * For more info visit https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/uint
	 *
	 * NOTICE ABOUT PERFORMANCE
	 * 
	 * UNITY_EDITOR or DEBUG flags ensure that common errors are caught. These
	 * flags are removed in production mode so don't rely on try/catch methods.
	 * If performing benchmarks, ensure that the flags are not taken into account.
	 * The flags ensure that common problems are caught in code and taken care of.
	 *
	 * CRITICAL CHANGES
	 * 20/12/2018 - for .NET 4.6 targets, all functions are hinted to use AggressiveInlining
	 */
	public static sealed class ValueUIntExtensions {

		/**
		 * Simple method to get a simple true/false value from data
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static bool Bool(this uint data) {
			return data > 0;
		}

		/**
		 * Return the state of the bit (either 1 or 0) at provided
		 * position. position value must be between [0, 31]
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static int BitAt(this uint data, int pos) {
			#if BITSTACK_DEBUG
				if (pos < 0 || pos > 31) {
					BitDebug.Exception("uint.BitAt(int) - position must be between 0 and 31 but was " + pos);
				}
			#endif
			return (int)((data >> pos) & 1);
		}
		
		/**
		 * Return the inverted state of the bit (either 1 or 0) at provided
		 * position. position value must be between [0, 31]
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static int BitInvAt(this uint data, int pos) {
			#if BITSTACK_DEBUG
				if (pos < 0 || pos > 31) {
					BitDebug.Exception("uint.BitInvAt(int) - position must be between 0 and 31 but was " + pos);
				}
			#endif
			return 1 - (int)((data >> pos) & 1);
		}

		/**
		 * Sets the state of the bit into the ON/1 at provided
		 * position. position value must be between [0, 31]
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static uint SetBitAt(this uint data, int pos) {
			#if BITSTACK_DEBUG
				if (pos < 0 || pos > 31) {
					BitDebug.Exception("uint.SetBitAt(int) - position must be between 0 and 31 but was " + pos);
				}
			#endif
			return (data | 1u << pos);
		}

		/**
		 * Sets the state of the bit into the OFF/0 at provided
		 * position. position value must be between [0, 31]
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static uint UnsetBitAt(this uint data, int pos) {
			#if BITSTACK_DEBUG
				if (pos < 0 || pos > 31) {
					BitDebug.Exception("uint.UnsetBitAt(int) - position must be between 0 and 31 but was " + pos);
				}
			#endif
			return (data & ~(1u << pos));
		}

		/**
		 * Toggles the state of the bit into the ON/1 or OFF/0 at provided
		 * position. position value must be between [0, 31].
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static uint ToggleBitAt(this uint data, int pos) {
			#if BITSTACK_DEBUG
				if (pos < 0 || pos > 31) {
					BitDebug.Exception("uint.ToggleBitAt(int) - position must be between 0 and 31 but was " + pos);
				}
			#endif
			return (data ^ (1u << pos));
		}
		
		/**
		 * Sets the state of the bit into the OFF/0 or ON/1 at provided
		 * position. position value must be between [0, 31]
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static uint SetBit(this uint data, int pos, int bit) {
			#if BITSTACK_DEBUG
				if (pos < 0 || pos > 31) {
					BitDebug.Exception("uint.SetBit(int, int) - position must be between 0 and 31 but was " + pos);
				}
				
				if (bit != 0 && bit != 1) {
					BitDebug.Exception("uint.SetBit(int, int) - bit value must be either 0 or 1 but was " + bit);
				}
			#endif
			uint mask = 1u << pos;
			uint m1 = ((uint)bit << pos) & mask;
			uint m2 = data & ~mask;
			
			return m2 | m1;
		}

		/**
		 * Count the number of set bits in the provided uint value (32 bits)
		 * A general purpose Hamming Weight or popcount function which returns the number of
		 * set bits in the argument.
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static int PopCount(this uint data) {
			uint data0 = data - ((data >> 1) & 0x55555555);
			uint data1 = (data0 & 0x33333333) + ((data0 >> 2) & 0x33333333);

			return (int)((((data1 + (data1 >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24);
		}

		/**
		 * Checks if the provided value is a power of 2.
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static bool IsPowerOfTwo(this uint value) {
			return value != 0 && (value & value - 1) == 0;
		}
		
		/**
		 * Returns the byte (8 bits) at provided position index
		 * Position value must be between [0, 3]
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static byte ByteAt(this uint data, int pos) {
			#if BITSTACK_DEBUG
				if (pos < 0 || pos > 3) {
					BitDebug.Exception("uint.ByteAt(int) - position must be between 0 and 3 but was " + pos);
				}
			#endif
			return (byte)(data >> (24 - (pos * 8)));
		}

		/**
		 * Sets and returns the byte (8 bits) at provided position index
		 * Position value must be between [0, 3]
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static uint SetByteAt(this uint data, byte newData, int pos) {
			#if BITSTACK_DEBUG
				if (pos < 0 || pos > 3) {
					BitDebug.Exception("uint.SetByteAt(int) - position must be between 0 and 3 but was " + pos);
				}
			#endif
			int shift = 24 - (pos * 8);
			uint mask = (uint)(0xFF << shift);
			uint m1 = ((uint)newData << shift) & mask;
			uint m2 = data & ~mask;
			
			return m2 | m1;
		}

		/**
		 * Returns the String representation of the Bit Sequence from the provided
		 * Int. The String will contain 32 characters of 1 or 0 for each bit position
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static string BitString(this uint value) {
#pragma warning disable XS0001 // Find APIs marked as TODO in Mono
			var stringBuilder = new System.Text.StringBuilder(32);
#pragma warning restore XS0001 // Find APIs marked as TODO in Mono

			for (int i = 31; i >= 0; i--) {
				stringBuilder.Append(value.BitAt(i));
			}

			return stringBuilder.ToString();
		}

		/**
		 * Given a string in binary form ie (10110101) convert into
		 * a byte and return. Will only look at the first 8 characters
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static uint UIntFromBitString(this string data, int readIndex) {
			#if BITSTACK_DEBUG
				if ((readIndex + 32) > data.Length) {
					BitDebug.Exception("string.UIntFromBitString(int) - read index and uint length is less than the string size");
				}
			#endif
			uint value = 0;

			for (int i = readIndex, j = 31; i < 32; i++, j--) {
				value = data[i] == '1' ? value.SetBitAt(j) : value.UnsetBitAt(j);
			}

			return value;
		}

		/**
		 * Given a string in binary form ie (10110101) convert into
		 * a byte and return. Will only look at the first 8 characters
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static uint UIntFromBitString(this string data) {
			return data.UIntFromBitString(0);
		}

		/**
		 * Returns the Hex Value as a String.
		 */
		#if BITSTACK_METHOD_INLINE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		#endif
		public static string HexString(this uint value) {
			return value.ToString("X");
		}
	}
}
