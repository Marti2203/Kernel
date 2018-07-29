using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using static System.Globalization.NumberStyles;
using System.Text;
using System.Numerics;
using Kernel.Arithmetic;
namespace Kernel
{
	public static class Parser
	{

		static readonly string digits = "0123456789abcdef";
		static readonly Dictionary<char, int> values = digits.ToDictionary(c => c, digits.IndexOf);
		static BigInteger ParseBigInteger(string value, int baseOfValue)
		=> value.Aggregate(new BigInteger(), (current, digit) => current * baseOfValue + values[digit]);

		//Todo Add Exactness and Inexactness. Is important
		static readonly IDictionary<string, Object> constants = new Dictionary<string, Object>
		{
			{ "#t", Boolean.True},
			{ "#f", Boolean.False},
			{ "#ignore", Ignore.Instance},
			{ "#inert", Inert.Instance},
			{ "()", Null.Instance},
		};
		static readonly string[] illegalLexemes = { ",", "`", "'", ",@" };
		static readonly Regex IntegerPattern = new Regex(@"^(#[bodx])?([+-])?(\d+)$");
		static readonly Regex RationalPattern = new Regex(@"^([+-]?\d*)/(\d+)$");
		static readonly Regex RealPattern = new Regex(@"^[+-]?\d*\.\d+$");
		static readonly Regex ComplexPattern = new Regex(@"^([+-]?\d+(?:\.\d*|e\d+)?)([+-]\d+(?:\.\d*|e\d+)?)i$");
		static readonly Regex PairPattern = new Regex(@"\(([^()\s]+)\s+\.\s+([^()\s]+)\)$");
		static readonly Regex StringPattern = new Regex(@"""(.*)""$");
		static readonly Regex CharacterPattern = new Regex(@"#\\(.*)$");
		internal static Object ParseToken(string input)
		{
			//TODO Refactor
			if (illegalLexemes.Contains(input))
				throw new ArgumentException("Illegal Lexeme");

			if (constants.ContainsKey(input))
				return constants[input];

			Match match = IntegerPattern.Match(input);
			if (match.Success)
			{
				int @base = 10;
				if (match.Groups[1].Success)
					switch (match.Groups[1].Value[1])
					{
						case 'b':
							@base = 2;
							break;
						case 'o':
							@base = 8;
							break;
						case 'd':
							@base = 10;
							break;
						case 'x':
							@base = 16;
							break;
					}
				return new Integer(ParseBigInteger(match.Groups[3].Value.ToLower(), @base) *
								   (match.Groups[2].Success && match.Groups[2].Value[0] == '-' ? -1 : 1));
			}

			match = RationalPattern.Match(input);
			if (match.Success)
				return new Rational(BigInteger.Parse(match.Groups[1].Value),
											   BigInteger.Parse(match.Groups[2].Value));

			match = RealPattern.Match(input);
			if (match.Success)
				return new Real(decimal.Parse(input, Any));

			match = ComplexPattern.Match(input);
			if (match.Success)
				return new Arithmetic.Complex(decimal.Parse(match.Groups[1].Value, Any),
											  decimal.Parse(match.Groups[2].Value, Any));

			match = PairPattern.Match(input);
			if (match.Success)
				return new Pair(ParseToken(match.Groups[1].Value),
								ParseToken(match.Groups[2].Value));

			match = StringPattern.Match(input);
			if (match.Success)
				return new String(match.Groups[1].Value);

			return Symbol.Get(input);
		}

		//Todo Parse may be better and Pairs are not complete
		public static Object Parse(string input)
		{
			if (constants.ContainsKey(input))
				return constants[input];

			Stack<Pair> lists = new Stack<Pair>();
			bool inString = false;

			StringBuilder buffer = new StringBuilder();
			for (int i = 0; i < input.Length; i++)
			{
				switch (input[i])
				{
					case '(':
						if (inString) goto default;
						lists.Push(new Pair());
						break;
					case ')':
						if (inString)
							goto default;
						if (lists.Count == 0)
							throw new ArgumentException("No opening parenthesis");
						if (buffer.Length != 0)
							lists.Peek().Append(ParseToken(buffer.ToString()));

						buffer.Clear();

						if (lists.Count == 1) return lists.Pop();
						Pair temp = lists.Pop();
						lists.Peek().Append(temp);
						break;
					case '"':
						Object result = null;
						if (inString)
							result = new String(buffer.ToString());
						else if (buffer.Length != 0)
							result = ParseToken(buffer.ToString());
						buffer.Clear();

						if (lists.Count == 0) return result;

						lists.Peek().Append(result);

						inString = !inString;
						break;
					case '\\':
						if (inString)
							i++;
						else buffer.Append(input[i]);
						break;
					case ' ':
						if (inString) goto default;
						if (buffer.Length != 0)
						{
							if (lists.Count == 0) return ParseToken(buffer.ToString());
							lists.Peek().Append(ParseToken(buffer.ToString()));
							buffer.Clear();
						}
						break;
					case ';':
						if (inString) goto default;
						while (i < input.Length && input[i] != '\n')
							i++;
						break;
					case '.':
						if (inString || buffer.Length != 0 || input[i + 1] != ' ' || input[i + 1] != ')') goto default;
						break;
					default:
						buffer.Append(input[i]);
						break;
				}
			}
			if (lists.Count != 0)
				throw new ArgumentException($"{nameof(input)} has no closing parenthesis");
			if (buffer.Length == 0)
				throw new InvalidOperationException("Wtf?!");
			return ParseToken(buffer.ToString());
		}


		static readonly Regex SchemyPattern = new Regex(@"^(""(?:[\\].|[^\\""])*""|;.*|[^\s('"",;)]*)(.*)");
		public static void ParseExperimental(string line)
		{
			Match res = SchemyPattern.Match(line);
		}
	}
}