// Generated from Kernel.g4 by ANTLR 4.7.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link KernelParser}.
 */
public interface KernelListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link KernelParser#file}.
	 * @param ctx the parse tree
	 */
	void enterFile(KernelParser.FileContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#file}.
	 * @param ctx the parse tree
	 */
	void exitFile(KernelParser.FileContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterExpression(KernelParser.ExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitExpression(KernelParser.ExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Keyword}
	 * labeled alternative in {@link KernelParser#atom}.
	 * @param ctx the parse tree
	 */
	void enterKeyword(KernelParser.KeywordContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Keyword}
	 * labeled alternative in {@link KernelParser#atom}.
	 * @param ctx the parse tree
	 */
	void exitKeyword(KernelParser.KeywordContext ctx);
	/**
	 * Enter a parse tree produced by the {@code String}
	 * labeled alternative in {@link KernelParser#atom}.
	 * @param ctx the parse tree
	 */
	void enterString(KernelParser.StringContext ctx);
	/**
	 * Exit a parse tree produced by the {@code String}
	 * labeled alternative in {@link KernelParser#atom}.
	 * @param ctx the parse tree
	 */
	void exitString(KernelParser.StringContext ctx);
	/**
	 * Enter a parse tree produced by the {@code NumberLiteral}
	 * labeled alternative in {@link KernelParser#atom}.
	 * @param ctx the parse tree
	 */
	void enterNumberLiteral(KernelParser.NumberLiteralContext ctx);
	/**
	 * Exit a parse tree produced by the {@code NumberLiteral}
	 * labeled alternative in {@link KernelParser#atom}.
	 * @param ctx the parse tree
	 */
	void exitNumberLiteral(KernelParser.NumberLiteralContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Symbol}
	 * labeled alternative in {@link KernelParser#atom}.
	 * @param ctx the parse tree
	 */
	void enterSymbol(KernelParser.SymbolContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Symbol}
	 * labeled alternative in {@link KernelParser#atom}.
	 * @param ctx the parse tree
	 */
	void exitSymbol(KernelParser.SymbolContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#pair}.
	 * @param ctx the parse tree
	 */
	void enterPair(KernelParser.PairContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#pair}.
	 * @param ctx the parse tree
	 */
	void exitPair(KernelParser.PairContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#number}.
	 * @param ctx the parse tree
	 */
	void enterNumber(KernelParser.NumberContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#number}.
	 * @param ctx the parse tree
	 */
	void exitNumber(KernelParser.NumberContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#numBin}.
	 * @param ctx the parse tree
	 */
	void enterNumBin(KernelParser.NumBinContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#numBin}.
	 * @param ctx the parse tree
	 */
	void exitNumBin(KernelParser.NumBinContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#numOct}.
	 * @param ctx the parse tree
	 */
	void enterNumOct(KernelParser.NumOctContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#numOct}.
	 * @param ctx the parse tree
	 */
	void exitNumOct(KernelParser.NumOctContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#numDec}.
	 * @param ctx the parse tree
	 */
	void enterNumDec(KernelParser.NumDecContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#numDec}.
	 * @param ctx the parse tree
	 */
	void exitNumDec(KernelParser.NumDecContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#numHex}.
	 * @param ctx the parse tree
	 */
	void enterNumHex(KernelParser.NumHexContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#numHex}.
	 * @param ctx the parse tree
	 */
	void exitNumHex(KernelParser.NumHexContext ctx);
	/**
	 * Enter a parse tree produced by the {@code BinaryNumber}
	 * labeled alternative in {@link KernelParser#complexBin}.
	 * @param ctx the parse tree
	 */
	void enterBinaryNumber(KernelParser.BinaryNumberContext ctx);
	/**
	 * Exit a parse tree produced by the {@code BinaryNumber}
	 * labeled alternative in {@link KernelParser#complexBin}.
	 * @param ctx the parse tree
	 */
	void exitBinaryNumber(KernelParser.BinaryNumberContext ctx);
	/**
	 * Enter a parse tree produced by the {@code BinaryPolar}
	 * labeled alternative in {@link KernelParser#complexBin}.
	 * @param ctx the parse tree
	 */
	void enterBinaryPolar(KernelParser.BinaryPolarContext ctx);
	/**
	 * Exit a parse tree produced by the {@code BinaryPolar}
	 * labeled alternative in {@link KernelParser#complexBin}.
	 * @param ctx the parse tree
	 */
	void exitBinaryPolar(KernelParser.BinaryPolarContext ctx);
	/**
	 * Enter a parse tree produced by the {@code BinaryComplex}
	 * labeled alternative in {@link KernelParser#complexBin}.
	 * @param ctx the parse tree
	 */
	void enterBinaryComplex(KernelParser.BinaryComplexContext ctx);
	/**
	 * Exit a parse tree produced by the {@code BinaryComplex}
	 * labeled alternative in {@link KernelParser#complexBin}.
	 * @param ctx the parse tree
	 */
	void exitBinaryComplex(KernelParser.BinaryComplexContext ctx);
	/**
	 * Enter a parse tree produced by the {@code OctalNumber}
	 * labeled alternative in {@link KernelParser#complexOct}.
	 * @param ctx the parse tree
	 */
	void enterOctalNumber(KernelParser.OctalNumberContext ctx);
	/**
	 * Exit a parse tree produced by the {@code OctalNumber}
	 * labeled alternative in {@link KernelParser#complexOct}.
	 * @param ctx the parse tree
	 */
	void exitOctalNumber(KernelParser.OctalNumberContext ctx);
	/**
	 * Enter a parse tree produced by the {@code OtalPolar}
	 * labeled alternative in {@link KernelParser#complexOct}.
	 * @param ctx the parse tree
	 */
	void enterOtalPolar(KernelParser.OtalPolarContext ctx);
	/**
	 * Exit a parse tree produced by the {@code OtalPolar}
	 * labeled alternative in {@link KernelParser#complexOct}.
	 * @param ctx the parse tree
	 */
	void exitOtalPolar(KernelParser.OtalPolarContext ctx);
	/**
	 * Enter a parse tree produced by the {@code OctalComplex}
	 * labeled alternative in {@link KernelParser#complexOct}.
	 * @param ctx the parse tree
	 */
	void enterOctalComplex(KernelParser.OctalComplexContext ctx);
	/**
	 * Exit a parse tree produced by the {@code OctalComplex}
	 * labeled alternative in {@link KernelParser#complexOct}.
	 * @param ctx the parse tree
	 */
	void exitOctalComplex(KernelParser.OctalComplexContext ctx);
	/**
	 * Enter a parse tree produced by the {@code DecimalNumber}
	 * labeled alternative in {@link KernelParser#complexDec}.
	 * @param ctx the parse tree
	 */
	void enterDecimalNumber(KernelParser.DecimalNumberContext ctx);
	/**
	 * Exit a parse tree produced by the {@code DecimalNumber}
	 * labeled alternative in {@link KernelParser#complexDec}.
	 * @param ctx the parse tree
	 */
	void exitDecimalNumber(KernelParser.DecimalNumberContext ctx);
	/**
	 * Enter a parse tree produced by the {@code DecimalPolar}
	 * labeled alternative in {@link KernelParser#complexDec}.
	 * @param ctx the parse tree
	 */
	void enterDecimalPolar(KernelParser.DecimalPolarContext ctx);
	/**
	 * Exit a parse tree produced by the {@code DecimalPolar}
	 * labeled alternative in {@link KernelParser#complexDec}.
	 * @param ctx the parse tree
	 */
	void exitDecimalPolar(KernelParser.DecimalPolarContext ctx);
	/**
	 * Enter a parse tree produced by the {@code DecimalComplex}
	 * labeled alternative in {@link KernelParser#complexDec}.
	 * @param ctx the parse tree
	 */
	void enterDecimalComplex(KernelParser.DecimalComplexContext ctx);
	/**
	 * Exit a parse tree produced by the {@code DecimalComplex}
	 * labeled alternative in {@link KernelParser#complexDec}.
	 * @param ctx the parse tree
	 */
	void exitDecimalComplex(KernelParser.DecimalComplexContext ctx);
	/**
	 * Enter a parse tree produced by the {@code HexadecimalNumber}
	 * labeled alternative in {@link KernelParser#complexHex}.
	 * @param ctx the parse tree
	 */
	void enterHexadecimalNumber(KernelParser.HexadecimalNumberContext ctx);
	/**
	 * Exit a parse tree produced by the {@code HexadecimalNumber}
	 * labeled alternative in {@link KernelParser#complexHex}.
	 * @param ctx the parse tree
	 */
	void exitHexadecimalNumber(KernelParser.HexadecimalNumberContext ctx);
	/**
	 * Enter a parse tree produced by the {@code HexadecimalPolar}
	 * labeled alternative in {@link KernelParser#complexHex}.
	 * @param ctx the parse tree
	 */
	void enterHexadecimalPolar(KernelParser.HexadecimalPolarContext ctx);
	/**
	 * Exit a parse tree produced by the {@code HexadecimalPolar}
	 * labeled alternative in {@link KernelParser#complexHex}.
	 * @param ctx the parse tree
	 */
	void exitHexadecimalPolar(KernelParser.HexadecimalPolarContext ctx);
	/**
	 * Enter a parse tree produced by the {@code HexadecimalComplex}
	 * labeled alternative in {@link KernelParser#complexHex}.
	 * @param ctx the parse tree
	 */
	void enterHexadecimalComplex(KernelParser.HexadecimalComplexContext ctx);
	/**
	 * Exit a parse tree produced by the {@code HexadecimalComplex}
	 * labeled alternative in {@link KernelParser#complexHex}.
	 * @param ctx the parse tree
	 */
	void exitHexadecimalComplex(KernelParser.HexadecimalComplexContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#realBin}.
	 * @param ctx the parse tree
	 */
	void enterRealBin(KernelParser.RealBinContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#realBin}.
	 * @param ctx the parse tree
	 */
	void exitRealBin(KernelParser.RealBinContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#realOct}.
	 * @param ctx the parse tree
	 */
	void enterRealOct(KernelParser.RealOctContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#realOct}.
	 * @param ctx the parse tree
	 */
	void exitRealOct(KernelParser.RealOctContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#realDec}.
	 * @param ctx the parse tree
	 */
	void enterRealDec(KernelParser.RealDecContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#realDec}.
	 * @param ctx the parse tree
	 */
	void exitRealDec(KernelParser.RealDecContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#realHex}.
	 * @param ctx the parse tree
	 */
	void enterRealHex(KernelParser.RealHexContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#realHex}.
	 * @param ctx the parse tree
	 */
	void exitRealHex(KernelParser.RealHexContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#imagBin}.
	 * @param ctx the parse tree
	 */
	void enterImagBin(KernelParser.ImagBinContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#imagBin}.
	 * @param ctx the parse tree
	 */
	void exitImagBin(KernelParser.ImagBinContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#imagOct}.
	 * @param ctx the parse tree
	 */
	void enterImagOct(KernelParser.ImagOctContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#imagOct}.
	 * @param ctx the parse tree
	 */
	void exitImagOct(KernelParser.ImagOctContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#imagDec}.
	 * @param ctx the parse tree
	 */
	void enterImagDec(KernelParser.ImagDecContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#imagDec}.
	 * @param ctx the parse tree
	 */
	void exitImagDec(KernelParser.ImagDecContext ctx);
	/**
	 * Enter a parse tree produced by {@link KernelParser#imagHex}.
	 * @param ctx the parse tree
	 */
	void enterImagHex(KernelParser.ImagHexContext ctx);
	/**
	 * Exit a parse tree produced by {@link KernelParser#imagHex}.
	 * @param ctx the parse tree
	 */
	void exitImagHex(KernelParser.ImagHexContext ctx);
	/**
	 * Enter a parse tree produced by the {@code BinaryInteger}
	 * labeled alternative in {@link KernelParser#urealBin}.
	 * @param ctx the parse tree
	 */
	void enterBinaryInteger(KernelParser.BinaryIntegerContext ctx);
	/**
	 * Exit a parse tree produced by the {@code BinaryInteger}
	 * labeled alternative in {@link KernelParser#urealBin}.
	 * @param ctx the parse tree
	 */
	void exitBinaryInteger(KernelParser.BinaryIntegerContext ctx);
	/**
	 * Enter a parse tree produced by the {@code BinaryRational}
	 * labeled alternative in {@link KernelParser#urealBin}.
	 * @param ctx the parse tree
	 */
	void enterBinaryRational(KernelParser.BinaryRationalContext ctx);
	/**
	 * Exit a parse tree produced by the {@code BinaryRational}
	 * labeled alternative in {@link KernelParser#urealBin}.
	 * @param ctx the parse tree
	 */
	void exitBinaryRational(KernelParser.BinaryRationalContext ctx);
	/**
	 * Enter a parse tree produced by the {@code OctalInteger}
	 * labeled alternative in {@link KernelParser#urealOct}.
	 * @param ctx the parse tree
	 */
	void enterOctalInteger(KernelParser.OctalIntegerContext ctx);
	/**
	 * Exit a parse tree produced by the {@code OctalInteger}
	 * labeled alternative in {@link KernelParser#urealOct}.
	 * @param ctx the parse tree
	 */
	void exitOctalInteger(KernelParser.OctalIntegerContext ctx);
	/**
	 * Enter a parse tree produced by the {@code OctalRational}
	 * labeled alternative in {@link KernelParser#urealOct}.
	 * @param ctx the parse tree
	 */
	void enterOctalRational(KernelParser.OctalRationalContext ctx);
	/**
	 * Exit a parse tree produced by the {@code OctalRational}
	 * labeled alternative in {@link KernelParser#urealOct}.
	 * @param ctx the parse tree
	 */
	void exitOctalRational(KernelParser.OctalRationalContext ctx);
	/**
	 * Enter a parse tree produced by the {@code DecimalInteger}
	 * labeled alternative in {@link KernelParser#urealDec}.
	 * @param ctx the parse tree
	 */
	void enterDecimalInteger(KernelParser.DecimalIntegerContext ctx);
	/**
	 * Exit a parse tree produced by the {@code DecimalInteger}
	 * labeled alternative in {@link KernelParser#urealDec}.
	 * @param ctx the parse tree
	 */
	void exitDecimalInteger(KernelParser.DecimalIntegerContext ctx);
	/**
	 * Enter a parse tree produced by the {@code DecimalRational}
	 * labeled alternative in {@link KernelParser#urealDec}.
	 * @param ctx the parse tree
	 */
	void enterDecimalRational(KernelParser.DecimalRationalContext ctx);
	/**
	 * Exit a parse tree produced by the {@code DecimalRational}
	 * labeled alternative in {@link KernelParser#urealDec}.
	 * @param ctx the parse tree
	 */
	void exitDecimalRational(KernelParser.DecimalRationalContext ctx);
	/**
	 * Enter a parse tree produced by the {@code DecimalReal}
	 * labeled alternative in {@link KernelParser#urealDec}.
	 * @param ctx the parse tree
	 */
	void enterDecimalReal(KernelParser.DecimalRealContext ctx);
	/**
	 * Exit a parse tree produced by the {@code DecimalReal}
	 * labeled alternative in {@link KernelParser#urealDec}.
	 * @param ctx the parse tree
	 */
	void exitDecimalReal(KernelParser.DecimalRealContext ctx);
	/**
	 * Enter a parse tree produced by the {@code HexadecimalInteger}
	 * labeled alternative in {@link KernelParser#urealHex}.
	 * @param ctx the parse tree
	 */
	void enterHexadecimalInteger(KernelParser.HexadecimalIntegerContext ctx);
	/**
	 * Exit a parse tree produced by the {@code HexadecimalInteger}
	 * labeled alternative in {@link KernelParser#urealHex}.
	 * @param ctx the parse tree
	 */
	void exitHexadecimalInteger(KernelParser.HexadecimalIntegerContext ctx);
	/**
	 * Enter a parse tree produced by the {@code HexadecimalRational}
	 * labeled alternative in {@link KernelParser#urealHex}.
	 * @param ctx the parse tree
	 */
	void enterHexadecimalRational(KernelParser.HexadecimalRationalContext ctx);
	/**
	 * Exit a parse tree produced by the {@code HexadecimalRational}
	 * labeled alternative in {@link KernelParser#urealHex}.
	 * @param ctx the parse tree
	 */
	void exitHexadecimalRational(KernelParser.HexadecimalRationalContext ctx);
}