using System;
using System.Linq;
using Antlr4.Runtime;
using Kernel.Arithmetic;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Kernel.Parser
{
    public static class Parser
    {
        public static Object Parse(string input)
        {
            KernelLexer lexer = new KernelLexer(CharStreams.fromstring(input));
            CommonTokenStream stream = new CommonTokenStream(lexer);
            KernelParser parser = new KernelParser(stream);
            IParseTree tree = parser.expressionLine();

            KernelVisitor walker = new KernelVisitor();
            return walker.Visit(tree);
        }

        class KernelVisitor : KernelBaseVisitor<Object>
        {
            static readonly IDictionary<string, Object> keywords = new Dictionary<string, Object>
        {
            { "#t", Boolean.True},
            { "#f", Boolean.False},
            { "#ignore", Ignore.Instance},
            { "#inert", Inert.Instance},
            { "()", Null.Instance},
            { "+inf.0", Real.PositiveInfinity },
            { "-inf.0", Real.NegativeInfinity },
        };

            #region Signing Numbers

            public override Object VisitRealBin([NotNull] KernelParser.RealBinContext context)
            => (Visit(context.urealBin()) as Number) * (context.Sign()?.GetText() == "-" ? -1 : 1);

            public override Object VisitRealOct([NotNull] KernelParser.RealOctContext context)
            => (Visit(context.urealOct()) as Number) * (context.Sign()?.GetText() == "-" ? -1 : 1);

            public override Object VisitRealDec([NotNull] KernelParser.RealDecContext context)
            => (Visit(context.urealDec()) as Number) * (context.Sign()?.GetText() == "-" ? -1 : 1);

            public override Object VisitRealHex([NotNull] KernelParser.RealHexContext context)
            => (Visit(context.urealHex()) as Number) * (context.Sign()?.GetText() == "-" ? -1 : 1);

            public override Object VisitBinaryNumber([NotNull] KernelParser.BinaryNumberContext context)
            => (Visit(context.urealBin()) as Number) * (context.sign?.Text == "-" ? -1 : 1);

            public override Object VisitOctalNumber([NotNull] KernelParser.OctalNumberContext context)
            => (Visit(context.urealOct()) as Number) * (context.sign?.Text == "-" ? -1 : 1);

            public override Object VisitDecimalNumber([NotNull] KernelParser.DecimalNumberContext context)
            => (Visit(context.urealDec()) as Number) * (context.sign?.Text == "-" ? -1 : 1);

            public override Object VisitHexadecimalNumber([NotNull] KernelParser.HexadecimalNumberContext context)
            => (Visit(context.urealHex()) as Number) * (context.sign?.Text == "-" ? -1 : 1);

            #endregion

            #region Integer

            Object VisitInteger(string number, int @base)
            => number.EndsWith("#", StringComparison.InvariantCultureIgnoreCase) ?
                     Real.Get(number, @base) : (Object)Integer.Get(number, @base);
            public override Object VisitBinaryInteger([NotNull] KernelParser.BinaryIntegerContext context)
            => VisitInteger(context.UintegerBin().GetText(), 2);
            public override Object VisitOctalInteger([NotNull] KernelParser.OctalIntegerContext context)
            => VisitInteger(context.UintegerOct().GetText(), 8);

            public override Object VisitDecimalInteger([NotNull] KernelParser.DecimalIntegerContext context)
            => VisitInteger(context.UintegerDec().GetText(), 10);

            public override Object VisitHexadecimalInteger([NotNull] KernelParser.HexadecimalIntegerContext context)
            => VisitInteger(context.UintegerHex().GetText(), 16);
            #endregion

            #region Rational

            Object VisitRational(string numerator, string denominator, int @base)
            => (VisitInteger(numerator, @base) as Number) / (VisitInteger(denominator, @base) as Number);

            public override Object VisitBinaryRational([NotNull] KernelParser.BinaryRationalContext context)
            {
                string numerator = context.UintegerBin(0).GetText();
                string denominator = context.UintegerBin(1).GetText();
                return VisitRational(numerator, denominator, 2);
            }
            public override Object VisitOctalRational([NotNull] KernelParser.OctalRationalContext context)
            {
                string numerator = context.UintegerOct(0).GetText();
                string denominator = context.UintegerOct(1).GetText();
                return VisitRational(numerator, denominator, 8);
            }
            public override Object VisitDecimalRational([NotNull] KernelParser.DecimalRationalContext context)
            {
                string numerator = context.UintegerDec(0).GetText();
                string denominator = context.UintegerDec(1).GetText();
                return VisitRational(numerator, denominator, 10);
            }
            public override Object VisitHexadecimalRational([NotNull] KernelParser.HexadecimalRationalContext context)
            {
                string numerator = context.UintegerHex(0).GetText();
                string denominator = context.UintegerHex(1).GetText();
                return VisitRational(numerator, denominator, 16);
            }
            #endregion

            #region Decimal
            static readonly Regex replacer = new Regex("[esdfl]", RegexOptions.IgnoreCase);
            public override Object VisitDecimalReal([NotNull] KernelParser.DecimalRealContext context)
            {
                string text = context.Decimal().GetText();
                if (replacer.Matches(text).Count != 0)
                {
                    string replaced = replacer.Replace(text, "e");
                    try
                    {
                        return long.Parse(text.Split('e')[1]) > 28
                            ? Real.Get(double.Parse(replaced, System.Globalization.NumberStyles.Any))
                            : Real.Get(decimal.Parse(replaced, System.Globalization.NumberStyles.Any));
                    }
                    catch (OverflowException)
                    {
                        throw new ArgumentException($"Value {text} out of range.");
                    }
                }
                return Real.Get(decimal.Parse(text, System.Globalization.NumberStyles.Any));
            }
            #endregion

            #region Complex
            public override Object VisitBinaryComplex([NotNull] KernelParser.BinaryComplexContext context)
            => Complex.Get(VisitOrDefault(context.realBin(), Integer.Zero) as Number, VisitOrDefault(context.imagBin(), Integer.Zero) as Number);

            public override Object VisitOctalComplex([NotNull] KernelParser.OctalComplexContext context)
            => Complex.Get(VisitOrDefault(context.realOct(), Integer.Zero) as Number, VisitOrDefault(context.imagOct(), Integer.Zero) as Number);

            public override Object VisitDecimalComplex([NotNull] KernelParser.DecimalComplexContext context)
            => Complex.Get(VisitOrDefault(context.realDec(), Integer.Zero) as Number, VisitOrDefault(context.imagDec(), Integer.Zero) as Number);

            public override Object VisitHexadecimalComplex([NotNull] KernelParser.HexadecimalComplexContext context)
            => Complex.Get(VisitOrDefault(context.realHex(), Integer.Zero) as Number, VisitOrDefault(context.imagHex(), Integer.Zero) as Number);

            public override Object VisitImagBin([NotNull] KernelParser.ImagBinContext context)
            => VisitOrDefault(context.urealBin(), Integer.One);

            public override Object VisitImagOct([NotNull] KernelParser.ImagOctContext context)
            => VisitOrDefault(context.urealOct(), Integer.One);

            public override Object VisitImagDec([NotNull] KernelParser.ImagDecContext context)
            => VisitOrDefault(context.urealDec(), Integer.One);

            public override Object VisitImagHex([NotNull] KernelParser.ImagHexContext context)
            => VisitOrDefault(context.urealHex(), Integer.One);

            public override Object VisitBinaryPolar([NotNull] KernelParser.BinaryPolarContext context)
            => Complex.GetPolar(Visit(context.realBin(0)) as Number, Visit(context.realBin(1)) as Number);

            public override Object VisitOctalPolar([NotNull] KernelParser.OctalPolarContext context)
            => Complex.GetPolar(Visit(context.realOct(0)) as Number, Visit(context.realOct(1)) as Number);

            public override Object VisitDecimalPolar([NotNull] KernelParser.DecimalPolarContext context)
            => Complex.GetPolar(Visit(context.realDec(0)) as Number, Visit(context.realDec(1)) as Number);

            public override Object VisitHexadecimalPolar([NotNull] KernelParser.HexadecimalPolarContext context)
            => Complex.GetPolar(Visit(context.realHex(0)) as Number, Visit(context.realHex(1)) as Number);
            #endregion

            #region String,Keyword,Symbol

            public override Object VisitKeyword([NotNull] KernelParser.KeywordContext context)
            => keywords[context.GetText()];

            public override Object VisitPair([NotNull] KernelParser.PairContext context)
            {
                if (context.dot != null)
                {
                    Object[] expressions = context.expression().Select(VisitExpression).ToArray();
                    Pair start, pair;
                    start = pair = new Pair(expressions[0]);
                    for (int i = 1; i < expressions.Length - 1; i++)
                        pair = pair.Append(expressions[i]);
                    pair.Cdr = expressions[expressions.Length - 1];
                    return start;
                }
                return new Pair(context.expression().Select(VisitExpression));
            }

            public override Object VisitString([NotNull] KernelParser.StringContext context)
            {
                string text = context.GetText();
                return String.Get(text.Substring(1, text.Length - 2));
            }

            public override Object VisitSymbolLiteral([NotNull] KernelParser.SymbolLiteralContext context)
            => Symbol.Get(context.GetText());
            #endregion

            public override Object VisitExpressionLine([NotNull] KernelParser.ExpressionLineContext context)
            => Visit(context.expression());

            public override Object VisitErrorNode(IErrorNode node)
            => throw new ArgumentException(node.GetText());

            Object VisitOrDefault(IParseTree context, Object @default)
            => context == null ? @default : Visit(context);
        }
    }
}