using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using static System.Globalization.NumberStyles;
using System.Text;
using Kernel.Arithmetic;
namespace Kernel
{
    public static class Parser
    {
        //Todo Add Exactness and Inexactness. May be important
        static readonly IDictionary<string, Object> constants = new Dictionary<string, Object>
        {
            { "#t", Boolean.True},
            { "#f", Boolean.False},
            { "#ignore", Ignore.Instance},
            { "#inert", Inert.Instance},
            { "()", Null.Instance},
        };
        static readonly string[] illegalLexemes = { ",", "`", "'", ",@" };
        static readonly Regex IntegerPattern = new Regex(@"^(#[bodx])?([+-]?\d+)$");
        static readonly Regex RationalPattern = new Regex(@"^([+-]?\d*)/(\d+)$");
        static readonly Regex RealPattern = new Regex(@"^[+-]?\d+$");
        static readonly Regex ComplexPattern = new Regex(@"^([+-]?\d+(?:\.\d*|e\d+)?)([+-]\d+(?:\.\d*|e\d+)?)i$");
        static readonly Regex PairPattern = new Regex(@"\(([^()\s]+)\s+\.\s+([^()\s]+)\)$");
        static readonly Regex StringPattern = new Regex(@"""(.*)""$");
        static readonly Regex CharacterPattern = new Regex(@"#\\(.*)$");
        static readonly Regex SchemyPattern = new Regex(@"^(""(?:[\\].|[^\\""])*""|;.*|[^\s('"",;)]*)(.*)");
        internal static Object ParseToken(string input)
        {
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
                        case 'x':
                            @base = 16;
                            break;
                    }
                return new Integer(Convert.ToInt64(input, @base));
            }

            match = RationalPattern.Match(input);
            if (match.Success)
                return new Rational(long.Parse(match.Groups[2].Value),
                                               long.Parse(match.Groups[3].Value));

            match = RealPattern.Match(input);
            if (match.Success)
                return new Real(decimal.Parse(input, Any));

            match = ComplexPattern.Match(input);
            if (match.Success)
                return new Complex(decimal.Parse(match.Groups[2].Value, Any),
                                              decimal.Parse(match.Groups[3].Value, Any));

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
            Stack<List> lists = new Stack<List>();
            bool inString = false;

            StringBuilder buffer = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '(':
                        if (inString) goto default;
                        lists.Push(new List());
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
                        List temp = lists.Pop();
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
                            if (lists.Count == 0)  return ParseToken(buffer.ToString());
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
                        if (inString || buffer.Length!=0 || input[i+1] != ' ' || input[i+1] != ')') goto default;
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


        public static void ParseExperimental(string line)
        {
            Match res = SchemyPattern.Match(line);
        }
    }
}