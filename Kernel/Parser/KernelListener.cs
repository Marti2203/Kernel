//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Kernel.g4 by ANTLR 4.7.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Kernel.Parser {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="KernelParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.2")]
public interface IKernelListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.file"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFile([NotNull] KernelParser.FileContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.file"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFile([NotNull] KernelParser.FileContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.expressionLine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpressionLine([NotNull] KernelParser.ExpressionLineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.expressionLine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpressionLine([NotNull] KernelParser.ExpressionLineContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] KernelParser.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] KernelParser.ExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Keyword</c>
	/// labeled alternative in <see cref="KernelParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterKeyword([NotNull] KernelParser.KeywordContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Keyword</c>
	/// labeled alternative in <see cref="KernelParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitKeyword([NotNull] KernelParser.KeywordContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>StringLiteral</c>
	/// labeled alternative in <see cref="KernelParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStringLiteral([NotNull] KernelParser.StringLiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>StringLiteral</c>
	/// labeled alternative in <see cref="KernelParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStringLiteral([NotNull] KernelParser.StringLiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>SymbolLiteral</c>
	/// labeled alternative in <see cref="KernelParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSymbolLiteral([NotNull] KernelParser.SymbolLiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>SymbolLiteral</c>
	/// labeled alternative in <see cref="KernelParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSymbolLiteral([NotNull] KernelParser.SymbolLiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>NumberLiteral</c>
	/// labeled alternative in <see cref="KernelParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumberLiteral([NotNull] KernelParser.NumberLiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>NumberLiteral</c>
	/// labeled alternative in <see cref="KernelParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumberLiteral([NotNull] KernelParser.NumberLiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.pair"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPair([NotNull] KernelParser.PairContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.pair"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPair([NotNull] KernelParser.PairContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.string"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterString([NotNull] KernelParser.StringContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.string"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitString([NotNull] KernelParser.StringContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumber([NotNull] KernelParser.NumberContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumber([NotNull] KernelParser.NumberContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.numBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumBin([NotNull] KernelParser.NumBinContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.numBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumBin([NotNull] KernelParser.NumBinContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.numOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumOct([NotNull] KernelParser.NumOctContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.numOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumOct([NotNull] KernelParser.NumOctContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.numDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumDec([NotNull] KernelParser.NumDecContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.numDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumDec([NotNull] KernelParser.NumDecContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.numHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumHex([NotNull] KernelParser.NumHexContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.numHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumHex([NotNull] KernelParser.NumHexContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>BinaryPolar</c>
	/// labeled alternative in <see cref="KernelParser.complexBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinaryPolar([NotNull] KernelParser.BinaryPolarContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>BinaryPolar</c>
	/// labeled alternative in <see cref="KernelParser.complexBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinaryPolar([NotNull] KernelParser.BinaryPolarContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>BinaryComplex</c>
	/// labeled alternative in <see cref="KernelParser.complexBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinaryComplex([NotNull] KernelParser.BinaryComplexContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>BinaryComplex</c>
	/// labeled alternative in <see cref="KernelParser.complexBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinaryComplex([NotNull] KernelParser.BinaryComplexContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>BinaryNumber</c>
	/// labeled alternative in <see cref="KernelParser.complexBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinaryNumber([NotNull] KernelParser.BinaryNumberContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>BinaryNumber</c>
	/// labeled alternative in <see cref="KernelParser.complexBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinaryNumber([NotNull] KernelParser.BinaryNumberContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>OctalPolar</c>
	/// labeled alternative in <see cref="KernelParser.complexOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOctalPolar([NotNull] KernelParser.OctalPolarContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>OctalPolar</c>
	/// labeled alternative in <see cref="KernelParser.complexOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOctalPolar([NotNull] KernelParser.OctalPolarContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>OctalComplex</c>
	/// labeled alternative in <see cref="KernelParser.complexOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOctalComplex([NotNull] KernelParser.OctalComplexContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>OctalComplex</c>
	/// labeled alternative in <see cref="KernelParser.complexOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOctalComplex([NotNull] KernelParser.OctalComplexContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>OctalNumber</c>
	/// labeled alternative in <see cref="KernelParser.complexOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOctalNumber([NotNull] KernelParser.OctalNumberContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>OctalNumber</c>
	/// labeled alternative in <see cref="KernelParser.complexOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOctalNumber([NotNull] KernelParser.OctalNumberContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>DecimalPolar</c>
	/// labeled alternative in <see cref="KernelParser.complexDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDecimalPolar([NotNull] KernelParser.DecimalPolarContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>DecimalPolar</c>
	/// labeled alternative in <see cref="KernelParser.complexDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDecimalPolar([NotNull] KernelParser.DecimalPolarContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>DecimalComplex</c>
	/// labeled alternative in <see cref="KernelParser.complexDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDecimalComplex([NotNull] KernelParser.DecimalComplexContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>DecimalComplex</c>
	/// labeled alternative in <see cref="KernelParser.complexDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDecimalComplex([NotNull] KernelParser.DecimalComplexContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>DecimalNumber</c>
	/// labeled alternative in <see cref="KernelParser.complexDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDecimalNumber([NotNull] KernelParser.DecimalNumberContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>DecimalNumber</c>
	/// labeled alternative in <see cref="KernelParser.complexDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDecimalNumber([NotNull] KernelParser.DecimalNumberContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>HexadecimalPolar</c>
	/// labeled alternative in <see cref="KernelParser.complexHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterHexadecimalPolar([NotNull] KernelParser.HexadecimalPolarContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>HexadecimalPolar</c>
	/// labeled alternative in <see cref="KernelParser.complexHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitHexadecimalPolar([NotNull] KernelParser.HexadecimalPolarContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>HexadecimalComplex</c>
	/// labeled alternative in <see cref="KernelParser.complexHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterHexadecimalComplex([NotNull] KernelParser.HexadecimalComplexContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>HexadecimalComplex</c>
	/// labeled alternative in <see cref="KernelParser.complexHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitHexadecimalComplex([NotNull] KernelParser.HexadecimalComplexContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>HexadecimalNumber</c>
	/// labeled alternative in <see cref="KernelParser.complexHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterHexadecimalNumber([NotNull] KernelParser.HexadecimalNumberContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>HexadecimalNumber</c>
	/// labeled alternative in <see cref="KernelParser.complexHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitHexadecimalNumber([NotNull] KernelParser.HexadecimalNumberContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.realBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRealBin([NotNull] KernelParser.RealBinContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.realBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRealBin([NotNull] KernelParser.RealBinContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.realOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRealOct([NotNull] KernelParser.RealOctContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.realOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRealOct([NotNull] KernelParser.RealOctContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.realDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRealDec([NotNull] KernelParser.RealDecContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.realDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRealDec([NotNull] KernelParser.RealDecContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.realHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRealHex([NotNull] KernelParser.RealHexContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.realHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRealHex([NotNull] KernelParser.RealHexContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.imagBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterImagBin([NotNull] KernelParser.ImagBinContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.imagBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitImagBin([NotNull] KernelParser.ImagBinContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.imagOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterImagOct([NotNull] KernelParser.ImagOctContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.imagOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitImagOct([NotNull] KernelParser.ImagOctContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.imagDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterImagDec([NotNull] KernelParser.ImagDecContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.imagDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitImagDec([NotNull] KernelParser.ImagDecContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.imagHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterImagHex([NotNull] KernelParser.ImagHexContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.imagHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitImagHex([NotNull] KernelParser.ImagHexContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>BinaryRational</c>
	/// labeled alternative in <see cref="KernelParser.urealBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinaryRational([NotNull] KernelParser.BinaryRationalContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>BinaryRational</c>
	/// labeled alternative in <see cref="KernelParser.urealBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinaryRational([NotNull] KernelParser.BinaryRationalContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>BinaryInteger</c>
	/// labeled alternative in <see cref="KernelParser.urealBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinaryInteger([NotNull] KernelParser.BinaryIntegerContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>BinaryInteger</c>
	/// labeled alternative in <see cref="KernelParser.urealBin"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinaryInteger([NotNull] KernelParser.BinaryIntegerContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>OctalRational</c>
	/// labeled alternative in <see cref="KernelParser.urealOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOctalRational([NotNull] KernelParser.OctalRationalContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>OctalRational</c>
	/// labeled alternative in <see cref="KernelParser.urealOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOctalRational([NotNull] KernelParser.OctalRationalContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>OctalInteger</c>
	/// labeled alternative in <see cref="KernelParser.urealOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOctalInteger([NotNull] KernelParser.OctalIntegerContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>OctalInteger</c>
	/// labeled alternative in <see cref="KernelParser.urealOct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOctalInteger([NotNull] KernelParser.OctalIntegerContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>DecimalRational</c>
	/// labeled alternative in <see cref="KernelParser.urealDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDecimalRational([NotNull] KernelParser.DecimalRationalContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>DecimalRational</c>
	/// labeled alternative in <see cref="KernelParser.urealDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDecimalRational([NotNull] KernelParser.DecimalRationalContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>DecimalInteger</c>
	/// labeled alternative in <see cref="KernelParser.urealDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDecimalInteger([NotNull] KernelParser.DecimalIntegerContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>DecimalInteger</c>
	/// labeled alternative in <see cref="KernelParser.urealDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDecimalInteger([NotNull] KernelParser.DecimalIntegerContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>DecimalReal</c>
	/// labeled alternative in <see cref="KernelParser.urealDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDecimalReal([NotNull] KernelParser.DecimalRealContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>DecimalReal</c>
	/// labeled alternative in <see cref="KernelParser.urealDec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDecimalReal([NotNull] KernelParser.DecimalRealContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>HexadecimalRational</c>
	/// labeled alternative in <see cref="KernelParser.urealHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterHexadecimalRational([NotNull] KernelParser.HexadecimalRationalContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>HexadecimalRational</c>
	/// labeled alternative in <see cref="KernelParser.urealHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitHexadecimalRational([NotNull] KernelParser.HexadecimalRationalContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>HexadecimalInteger</c>
	/// labeled alternative in <see cref="KernelParser.urealHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterHexadecimalInteger([NotNull] KernelParser.HexadecimalIntegerContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>HexadecimalInteger</c>
	/// labeled alternative in <see cref="KernelParser.urealHex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitHexadecimalInteger([NotNull] KernelParser.HexadecimalIntegerContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KernelParser.symbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSymbol([NotNull] KernelParser.SymbolContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KernelParser.symbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSymbol([NotNull] KernelParser.SymbolContext context);
}
} // namespace Kernel.Parser
