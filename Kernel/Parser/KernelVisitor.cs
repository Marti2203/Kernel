//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Kernel.g4 by ANTLR 4.7.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="KernelParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public interface IKernelVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.file"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFile([NotNull] KernelParser.FileContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.expressionLine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpressionLine([NotNull] KernelParser.ExpressionLineContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] KernelParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Keyword</c>
	/// labeled alternative in <see cref="KernelParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitKeyword([NotNull] KernelParser.KeywordContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>String</c>
	/// labeled alternative in <see cref="KernelParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitString([NotNull] KernelParser.StringContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>SymbolLiteral</c>
	/// labeled alternative in <see cref="KernelParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSymbolLiteral([NotNull] KernelParser.SymbolLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>NumberLiteral</c>
	/// labeled alternative in <see cref="KernelParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumberLiteral([NotNull] KernelParser.NumberLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.pair"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPair([NotNull] KernelParser.PairContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumber([NotNull] KernelParser.NumberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.numBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumBin([NotNull] KernelParser.NumBinContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.numOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumOct([NotNull] KernelParser.NumOctContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.numDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumDec([NotNull] KernelParser.NumDecContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.numHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumHex([NotNull] KernelParser.NumHexContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BinaryPolar</c>
	/// labeled alternative in <see cref="KernelParser.complexBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinaryPolar([NotNull] KernelParser.BinaryPolarContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BinaryComplex</c>
	/// labeled alternative in <see cref="KernelParser.complexBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinaryComplex([NotNull] KernelParser.BinaryComplexContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BinaryNumber</c>
	/// labeled alternative in <see cref="KernelParser.complexBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinaryNumber([NotNull] KernelParser.BinaryNumberContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OctalPolar</c>
	/// labeled alternative in <see cref="KernelParser.complexOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOctalPolar([NotNull] KernelParser.OctalPolarContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OctalComplex</c>
	/// labeled alternative in <see cref="KernelParser.complexOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOctalComplex([NotNull] KernelParser.OctalComplexContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OctalNumber</c>
	/// labeled alternative in <see cref="KernelParser.complexOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOctalNumber([NotNull] KernelParser.OctalNumberContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DecimalPolar</c>
	/// labeled alternative in <see cref="KernelParser.complexDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecimalPolar([NotNull] KernelParser.DecimalPolarContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DecimalComplex</c>
	/// labeled alternative in <see cref="KernelParser.complexDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecimalComplex([NotNull] KernelParser.DecimalComplexContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DecimalNumber</c>
	/// labeled alternative in <see cref="KernelParser.complexDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecimalNumber([NotNull] KernelParser.DecimalNumberContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>HexadecimalPolar</c>
	/// labeled alternative in <see cref="KernelParser.complexHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHexadecimalPolar([NotNull] KernelParser.HexadecimalPolarContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>HexadecimalComplex</c>
	/// labeled alternative in <see cref="KernelParser.complexHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHexadecimalComplex([NotNull] KernelParser.HexadecimalComplexContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>HexadecimalNumber</c>
	/// labeled alternative in <see cref="KernelParser.complexHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHexadecimalNumber([NotNull] KernelParser.HexadecimalNumberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.realBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRealBin([NotNull] KernelParser.RealBinContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.realOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRealOct([NotNull] KernelParser.RealOctContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.realDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRealDec([NotNull] KernelParser.RealDecContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.realHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRealHex([NotNull] KernelParser.RealHexContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.imagBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitImagBin([NotNull] KernelParser.ImagBinContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.imagOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitImagOct([NotNull] KernelParser.ImagOctContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.imagDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitImagDec([NotNull] KernelParser.ImagDecContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.imagHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitImagHex([NotNull] KernelParser.ImagHexContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BinaryRational</c>
	/// labeled alternative in <see cref="KernelParser.urealBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinaryRational([NotNull] KernelParser.BinaryRationalContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BinaryInteger</c>
	/// labeled alternative in <see cref="KernelParser.urealBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinaryInteger([NotNull] KernelParser.BinaryIntegerContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OctalRational</c>
	/// labeled alternative in <see cref="KernelParser.urealOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOctalRational([NotNull] KernelParser.OctalRationalContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OctalInteger</c>
	/// labeled alternative in <see cref="KernelParser.urealOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOctalInteger([NotNull] KernelParser.OctalIntegerContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DecimalRational</c>
	/// labeled alternative in <see cref="KernelParser.urealDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecimalRational([NotNull] KernelParser.DecimalRationalContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DecimalInteger</c>
	/// labeled alternative in <see cref="KernelParser.urealDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecimalInteger([NotNull] KernelParser.DecimalIntegerContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DecimalReal</c>
	/// labeled alternative in <see cref="KernelParser.urealDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecimalReal([NotNull] KernelParser.DecimalRealContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>HexadecimalRational</c>
	/// labeled alternative in <see cref="KernelParser.urealHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHexadecimalRational([NotNull] KernelParser.HexadecimalRationalContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>HexadecimalInteger</c>
	/// labeled alternative in <see cref="KernelParser.urealHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHexadecimalInteger([NotNull] KernelParser.HexadecimalIntegerContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="KernelParser.symbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSymbol([NotNull] KernelParser.SymbolContext context);
}
