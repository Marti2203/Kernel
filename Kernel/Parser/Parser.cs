using System;
using System.Linq;
using System.Numerics;
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
			IParseTree tree = parser.expression();

			KernelVisitor walker = new KernelVisitor(parser);
			return walker.Visit(tree);
		}
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
		};

		static readonly IDictionary<char, int> bases = new Dictionary<char, int>
		{
			{'b', 2}, {'o',8}, {'d', 10}, {'x',16}
		};

		static Regex complexRegex = new Regex(@"([+-]??.*?)??([+-].*)i");

		readonly KernelParser parser;
		public KernelVisitor(KernelParser parser)
		{
			this.parser = parser;
		}

		public override Object VisitKeywords([NotNull] KernelParser.KeywordsContext context)
		=> keywords[context.GetText()];

		//public override Object VisitComplexLiteral([NotNull] KernelParser.ComplexLiteralContext context)
		//{
		//	Match result = complexRegex.Match(context.GetText());
		//	string imaginary = result.Groups[2].Value;
		//	string real = result.Groups[1].Value;                                                   
		//	if (result.Groups[1].Value.Length == 0)
		//		real = "0";
		//	if (result.Groups[2].Value.Length == 1)
		//		imaginary = result.Groups[2].Value + "1";
		//	return Arithmetic.Complex.Get(real, imaginary);
		//}

		//public override Object VisitIntLiteral([NotNull] KernelParser.IntLiteralContext context)
		//{
		//	string text = context.GetText();
		//	if (text[0] == '#')
		//	{
		//		return Integer.Get(text.Substring(2), bases[text[1]]);
		//	}
		//	return Integer.Get(text);
		//}

		public override Object VisitPair([NotNull] KernelParser.PairContext context)
		{
			if (context.star != null)
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

		//public override Object VisitRationalLiteral([NotNull] KernelParser.RationalLiteralContext context)
		//{
		//	string[] parts = context.GetText().Split('/');
		//	return Rational.Get(parts[0], parts[1]);
		//}


		//public override Object VisitRealLiteral([NotNull] KernelParser.RealLiteralContext context)
		//=> Real.Get(context.GetText());

		public override Object VisitString([NotNull] KernelParser.StringContext context)
		{
			string text = context.GetText();
			return new String(text.Substring(1, text.Length - 2));
		}

		public override Object VisitSymbol([NotNull] KernelParser.SymbolContext context)
		=> Symbol.Get(context.GetText());

		public override Object VisitErrorNode(IErrorNode node)
		=> throw new ArgumentException(node.GetText());

	}
}