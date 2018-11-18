// Generated from Kernel.g4 by ANTLR 4.7.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class KernelParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.7.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		KEYWORD=10, STRING=11, UintegerBin=12, UintegerOct=13, UintegerDec=14, 
		UintegerHex=15, PrefixBin=16, PrefixOct=17, PrefixDec=18, PrefixHex=19, 
		Decimal=20, Sign=21, Suffix=22, SYMBOL=23, WS=24;
	public static final int
		RULE_file = 0, RULE_expression = 1, RULE_atom = 2, RULE_pair = 3, RULE_number = 4, 
		RULE_numBin = 5, RULE_numOct = 6, RULE_numDec = 7, RULE_numHex = 8, RULE_complexBin = 9, 
		RULE_complexOct = 10, RULE_complexDec = 11, RULE_complexHex = 12, RULE_realBin = 13, 
		RULE_realOct = 14, RULE_realDec = 15, RULE_realHex = 16, RULE_imagBin = 17, 
		RULE_imagOct = 18, RULE_imagDec = 19, RULE_imagHex = 20, RULE_urealBin = 21, 
		RULE_urealOct = 22, RULE_urealDec = 23, RULE_urealHex = 24;
	public static final String[] ruleNames = {
		"file", "expression", "atom", "pair", "number", "numBin", "numOct", "numDec", 
		"numHex", "complexBin", "complexOct", "complexDec", "complexHex", "realBin", 
		"realOct", "realDec", "realHex", "imagBin", "imagOct", "imagDec", "imagHex", 
		"urealBin", "urealOct", "urealDec", "urealHex"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'('", "'.'", "')'", "'+'", "'-'", "'@'", "'i'", "'I'", "'/'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, null, null, null, null, null, null, null, null, null, "KEYWORD", 
		"STRING", "UintegerBin", "UintegerOct", "UintegerDec", "UintegerHex", 
		"PrefixBin", "PrefixOct", "PrefixDec", "PrefixHex", "Decimal", "Sign", 
		"Suffix", "SYMBOL", "WS"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "Kernel.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public KernelParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}
	public static class FileContext extends ParserRuleContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public FileContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_file; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterFile(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitFile(this);
		}
	}

	public final FileContext file() throws RecognitionException {
		FileContext _localctx = new FileContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_file);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(51); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(50);
				expression();
				}
				}
				setState(53); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__3) | (1L << T__4) | (1L << KEYWORD) | (1L << STRING) | (1L << UintegerDec) | (1L << PrefixBin) | (1L << PrefixOct) | (1L << PrefixDec) | (1L << PrefixHex) | (1L << Decimal) | (1L << Sign) | (1L << SYMBOL))) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpressionContext extends ParserRuleContext {
		public AtomContext atom() {
			return getRuleContext(AtomContext.class,0);
		}
		public PairContext pair() {
			return getRuleContext(PairContext.class,0);
		}
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterExpression(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitExpression(this);
		}
	}

	public final ExpressionContext expression() throws RecognitionException {
		ExpressionContext _localctx = new ExpressionContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_expression);
		try {
			setState(57);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__3:
			case T__4:
			case KEYWORD:
			case STRING:
			case UintegerDec:
			case PrefixBin:
			case PrefixOct:
			case PrefixDec:
			case PrefixHex:
			case Decimal:
			case Sign:
			case SYMBOL:
				enterOuterAlt(_localctx, 1);
				{
				setState(55);
				atom();
				}
				break;
			case T__0:
				enterOuterAlt(_localctx, 2);
				{
				setState(56);
				pair();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AtomContext extends ParserRuleContext {
		public AtomContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_atom; }
	 
		public AtomContext() { }
		public void copyFrom(AtomContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class KeywordContext extends AtomContext {
		public TerminalNode KEYWORD() { return getToken(KernelParser.KEYWORD, 0); }
		public KeywordContext(AtomContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterKeyword(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitKeyword(this);
		}
	}
	public static class SymbolContext extends AtomContext {
		public TerminalNode SYMBOL() { return getToken(KernelParser.SYMBOL, 0); }
		public SymbolContext(AtomContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterSymbol(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitSymbol(this);
		}
	}
	public static class StringContext extends AtomContext {
		public TerminalNode STRING() { return getToken(KernelParser.STRING, 0); }
		public StringContext(AtomContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterString(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitString(this);
		}
	}
	public static class NumberLiteralContext extends AtomContext {
		public NumberContext number() {
			return getRuleContext(NumberContext.class,0);
		}
		public NumberLiteralContext(AtomContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterNumberLiteral(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitNumberLiteral(this);
		}
	}

	public final AtomContext atom() throws RecognitionException {
		AtomContext _localctx = new AtomContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_atom);
		try {
			setState(63);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case KEYWORD:
				_localctx = new KeywordContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(59);
				match(KEYWORD);
				}
				break;
			case STRING:
				_localctx = new StringContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(60);
				match(STRING);
				}
				break;
			case T__3:
			case T__4:
			case UintegerDec:
			case PrefixBin:
			case PrefixOct:
			case PrefixDec:
			case PrefixHex:
			case Decimal:
			case Sign:
				_localctx = new NumberLiteralContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(61);
				number();
				}
				break;
			case SYMBOL:
				_localctx = new SymbolContext(_localctx);
				enterOuterAlt(_localctx, 4);
				{
				setState(62);
				match(SYMBOL);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PairContext extends ParserRuleContext {
		public Token dot;
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public PairContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pair; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterPair(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitPair(this);
		}
	}

	public final PairContext pair() throws RecognitionException {
		PairContext _localctx = new PairContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_pair);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(65);
			match(T__0);
			setState(67); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(66);
				expression();
				}
				}
				setState(69); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__3) | (1L << T__4) | (1L << KEYWORD) | (1L << STRING) | (1L << UintegerDec) | (1L << PrefixBin) | (1L << PrefixOct) | (1L << PrefixDec) | (1L << PrefixHex) | (1L << Decimal) | (1L << Sign) | (1L << SYMBOL))) != 0) );
			setState(73);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__1) {
				{
				setState(71);
				((PairContext)_localctx).dot = match(T__1);
				setState(72);
				expression();
				}
			}

			setState(75);
			match(T__2);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class NumberContext extends ParserRuleContext {
		public NumBinContext numBin() {
			return getRuleContext(NumBinContext.class,0);
		}
		public NumOctContext numOct() {
			return getRuleContext(NumOctContext.class,0);
		}
		public NumDecContext numDec() {
			return getRuleContext(NumDecContext.class,0);
		}
		public NumHexContext numHex() {
			return getRuleContext(NumHexContext.class,0);
		}
		public NumberContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_number; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterNumber(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitNumber(this);
		}
	}

	public final NumberContext number() throws RecognitionException {
		NumberContext _localctx = new NumberContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_number);
		try {
			setState(81);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case PrefixBin:
				enterOuterAlt(_localctx, 1);
				{
				setState(77);
				numBin();
				}
				break;
			case PrefixOct:
				enterOuterAlt(_localctx, 2);
				{
				setState(78);
				numOct();
				}
				break;
			case T__3:
			case T__4:
			case UintegerDec:
			case PrefixDec:
			case Decimal:
			case Sign:
				enterOuterAlt(_localctx, 3);
				{
				setState(79);
				numDec();
				}
				break;
			case PrefixHex:
				enterOuterAlt(_localctx, 4);
				{
				setState(80);
				numHex();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class NumBinContext extends ParserRuleContext {
		public TerminalNode PrefixBin() { return getToken(KernelParser.PrefixBin, 0); }
		public ComplexBinContext complexBin() {
			return getRuleContext(ComplexBinContext.class,0);
		}
		public NumBinContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_numBin; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterNumBin(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitNumBin(this);
		}
	}

	public final NumBinContext numBin() throws RecognitionException {
		NumBinContext _localctx = new NumBinContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_numBin);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(83);
			match(PrefixBin);
			setState(84);
			complexBin();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class NumOctContext extends ParserRuleContext {
		public TerminalNode PrefixOct() { return getToken(KernelParser.PrefixOct, 0); }
		public ComplexOctContext complexOct() {
			return getRuleContext(ComplexOctContext.class,0);
		}
		public NumOctContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_numOct; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterNumOct(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitNumOct(this);
		}
	}

	public final NumOctContext numOct() throws RecognitionException {
		NumOctContext _localctx = new NumOctContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_numOct);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(86);
			match(PrefixOct);
			setState(87);
			complexOct();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class NumDecContext extends ParserRuleContext {
		public ComplexDecContext complexDec() {
			return getRuleContext(ComplexDecContext.class,0);
		}
		public TerminalNode PrefixDec() { return getToken(KernelParser.PrefixDec, 0); }
		public NumDecContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_numDec; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterNumDec(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitNumDec(this);
		}
	}

	public final NumDecContext numDec() throws RecognitionException {
		NumDecContext _localctx = new NumDecContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_numDec);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(90);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==PrefixDec) {
				{
				setState(89);
				match(PrefixDec);
				}
			}

			setState(92);
			complexDec();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class NumHexContext extends ParserRuleContext {
		public TerminalNode PrefixHex() { return getToken(KernelParser.PrefixHex, 0); }
		public ComplexHexContext complexHex() {
			return getRuleContext(ComplexHexContext.class,0);
		}
		public NumHexContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_numHex; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterNumHex(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitNumHex(this);
		}
	}

	public final NumHexContext numHex() throws RecognitionException {
		NumHexContext _localctx = new NumHexContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_numHex);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(94);
			match(PrefixHex);
			setState(95);
			complexHex();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ComplexBinContext extends ParserRuleContext {
		public ComplexBinContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_complexBin; }
	 
		public ComplexBinContext() { }
		public void copyFrom(ComplexBinContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class BinaryNumberContext extends ComplexBinContext {
		public UrealBinContext urealBin() {
			return getRuleContext(UrealBinContext.class,0);
		}
		public BinaryNumberContext(ComplexBinContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterBinaryNumber(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitBinaryNumber(this);
		}
	}
	public static class BinaryPolarContext extends ComplexBinContext {
		public List<RealBinContext> realBin() {
			return getRuleContexts(RealBinContext.class);
		}
		public RealBinContext realBin(int i) {
			return getRuleContext(RealBinContext.class,i);
		}
		public BinaryPolarContext(ComplexBinContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterBinaryPolar(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitBinaryPolar(this);
		}
	}
	public static class BinaryComplexContext extends ComplexBinContext {
		public RealBinContext realBin() {
			return getRuleContext(RealBinContext.class,0);
		}
		public ImagBinContext imagBin() {
			return getRuleContext(ImagBinContext.class,0);
		}
		public BinaryComplexContext(ComplexBinContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterBinaryComplex(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitBinaryComplex(this);
		}
	}

	public final ComplexBinContext complexBin() throws RecognitionException {
		ComplexBinContext _localctx = new ComplexBinContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_complexBin);
		int _la;
		try {
			setState(111);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,8,_ctx) ) {
			case 1:
				_localctx = new BinaryNumberContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(98);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__3 || _la==T__4) {
					{
					setState(97);
					_la = _input.LA(1);
					if ( !(_la==T__3 || _la==T__4) ) {
					_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					}
				}

				setState(100);
				urealBin();
				}
				break;
			case 2:
				_localctx = new BinaryPolarContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(101);
				realBin();
				setState(102);
				match(T__5);
				setState(103);
				realBin();
				}
				break;
			case 3:
				_localctx = new BinaryComplexContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(105);
				realBin();
				setState(106);
				_la = _input.LA(1);
				if ( !(_la==T__3 || _la==T__4) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(107);
				imagBin();
				}
				break;
			case 4:
				_localctx = new BinaryComplexContext(_localctx);
				enterOuterAlt(_localctx, 4);
				{
				setState(109);
				_la = _input.LA(1);
				if ( !(_la==T__3 || _la==T__4) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(110);
				imagBin();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ComplexOctContext extends ParserRuleContext {
		public ComplexOctContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_complexOct; }
	 
		public ComplexOctContext() { }
		public void copyFrom(ComplexOctContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class OctalComplexContext extends ComplexOctContext {
		public RealOctContext realOct() {
			return getRuleContext(RealOctContext.class,0);
		}
		public ImagOctContext imagOct() {
			return getRuleContext(ImagOctContext.class,0);
		}
		public OctalComplexContext(ComplexOctContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterOctalComplex(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitOctalComplex(this);
		}
	}
	public static class OtalPolarContext extends ComplexOctContext {
		public List<RealOctContext> realOct() {
			return getRuleContexts(RealOctContext.class);
		}
		public RealOctContext realOct(int i) {
			return getRuleContext(RealOctContext.class,i);
		}
		public OtalPolarContext(ComplexOctContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterOtalPolar(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitOtalPolar(this);
		}
	}
	public static class OctalNumberContext extends ComplexOctContext {
		public UrealOctContext urealOct() {
			return getRuleContext(UrealOctContext.class,0);
		}
		public OctalNumberContext(ComplexOctContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterOctalNumber(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitOctalNumber(this);
		}
	}

	public final ComplexOctContext complexOct() throws RecognitionException {
		ComplexOctContext _localctx = new ComplexOctContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_complexOct);
		int _la;
		try {
			setState(127);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,10,_ctx) ) {
			case 1:
				_localctx = new OctalNumberContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(114);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__3 || _la==T__4) {
					{
					setState(113);
					_la = _input.LA(1);
					if ( !(_la==T__3 || _la==T__4) ) {
					_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					}
				}

				setState(116);
				urealOct();
				}
				break;
			case 2:
				_localctx = new OtalPolarContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(117);
				realOct();
				setState(118);
				match(T__5);
				setState(119);
				realOct();
				}
				break;
			case 3:
				_localctx = new OctalComplexContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(121);
				realOct();
				setState(122);
				_la = _input.LA(1);
				if ( !(_la==T__3 || _la==T__4) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(123);
				imagOct();
				}
				break;
			case 4:
				_localctx = new OctalComplexContext(_localctx);
				enterOuterAlt(_localctx, 4);
				{
				setState(125);
				_la = _input.LA(1);
				if ( !(_la==T__3 || _la==T__4) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(126);
				imagOct();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ComplexDecContext extends ParserRuleContext {
		public ComplexDecContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_complexDec; }
	 
		public ComplexDecContext() { }
		public void copyFrom(ComplexDecContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class DecimalNumberContext extends ComplexDecContext {
		public UrealDecContext urealDec() {
			return getRuleContext(UrealDecContext.class,0);
		}
		public DecimalNumberContext(ComplexDecContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterDecimalNumber(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitDecimalNumber(this);
		}
	}
	public static class DecimalComplexContext extends ComplexDecContext {
		public RealDecContext realDec() {
			return getRuleContext(RealDecContext.class,0);
		}
		public ImagDecContext imagDec() {
			return getRuleContext(ImagDecContext.class,0);
		}
		public DecimalComplexContext(ComplexDecContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterDecimalComplex(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitDecimalComplex(this);
		}
	}
	public static class DecimalPolarContext extends ComplexDecContext {
		public List<RealDecContext> realDec() {
			return getRuleContexts(RealDecContext.class);
		}
		public RealDecContext realDec(int i) {
			return getRuleContext(RealDecContext.class,i);
		}
		public DecimalPolarContext(ComplexDecContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterDecimalPolar(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitDecimalPolar(this);
		}
	}

	public final ComplexDecContext complexDec() throws RecognitionException {
		ComplexDecContext _localctx = new ComplexDecContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_complexDec);
		int _la;
		try {
			setState(143);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,12,_ctx) ) {
			case 1:
				_localctx = new DecimalNumberContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(130);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__3 || _la==T__4) {
					{
					setState(129);
					_la = _input.LA(1);
					if ( !(_la==T__3 || _la==T__4) ) {
					_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					}
				}

				setState(132);
				urealDec();
				}
				break;
			case 2:
				_localctx = new DecimalPolarContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(133);
				realDec();
				setState(134);
				match(T__5);
				setState(135);
				realDec();
				}
				break;
			case 3:
				_localctx = new DecimalComplexContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(137);
				realDec();
				setState(138);
				_la = _input.LA(1);
				if ( !(_la==T__3 || _la==T__4) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(139);
				imagDec();
				}
				break;
			case 4:
				_localctx = new DecimalComplexContext(_localctx);
				enterOuterAlt(_localctx, 4);
				{
				setState(141);
				_la = _input.LA(1);
				if ( !(_la==T__3 || _la==T__4) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(142);
				imagDec();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ComplexHexContext extends ParserRuleContext {
		public ComplexHexContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_complexHex; }
	 
		public ComplexHexContext() { }
		public void copyFrom(ComplexHexContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class HexadecimalComplexContext extends ComplexHexContext {
		public RealHexContext realHex() {
			return getRuleContext(RealHexContext.class,0);
		}
		public ImagHexContext imagHex() {
			return getRuleContext(ImagHexContext.class,0);
		}
		public HexadecimalComplexContext(ComplexHexContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterHexadecimalComplex(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitHexadecimalComplex(this);
		}
	}
	public static class HexadecimalNumberContext extends ComplexHexContext {
		public UrealHexContext urealHex() {
			return getRuleContext(UrealHexContext.class,0);
		}
		public HexadecimalNumberContext(ComplexHexContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterHexadecimalNumber(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitHexadecimalNumber(this);
		}
	}
	public static class HexadecimalPolarContext extends ComplexHexContext {
		public List<RealHexContext> realHex() {
			return getRuleContexts(RealHexContext.class);
		}
		public RealHexContext realHex(int i) {
			return getRuleContext(RealHexContext.class,i);
		}
		public HexadecimalPolarContext(ComplexHexContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterHexadecimalPolar(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitHexadecimalPolar(this);
		}
	}

	public final ComplexHexContext complexHex() throws RecognitionException {
		ComplexHexContext _localctx = new ComplexHexContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_complexHex);
		int _la;
		try {
			setState(159);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,14,_ctx) ) {
			case 1:
				_localctx = new HexadecimalNumberContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(146);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__3 || _la==T__4) {
					{
					setState(145);
					_la = _input.LA(1);
					if ( !(_la==T__3 || _la==T__4) ) {
					_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					}
				}

				setState(148);
				urealHex();
				}
				break;
			case 2:
				_localctx = new HexadecimalPolarContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(149);
				realHex();
				setState(150);
				match(T__5);
				setState(151);
				realHex();
				}
				break;
			case 3:
				_localctx = new HexadecimalComplexContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(153);
				realHex();
				setState(154);
				_la = _input.LA(1);
				if ( !(_la==T__3 || _la==T__4) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(155);
				imagHex();
				}
				break;
			case 4:
				_localctx = new HexadecimalComplexContext(_localctx);
				enterOuterAlt(_localctx, 4);
				{
				setState(157);
				_la = _input.LA(1);
				if ( !(_la==T__3 || _la==T__4) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(158);
				imagHex();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class RealBinContext extends ParserRuleContext {
		public UrealBinContext urealBin() {
			return getRuleContext(UrealBinContext.class,0);
		}
		public TerminalNode Sign() { return getToken(KernelParser.Sign, 0); }
		public RealBinContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_realBin; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterRealBin(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitRealBin(this);
		}
	}

	public final RealBinContext realBin() throws RecognitionException {
		RealBinContext _localctx = new RealBinContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_realBin);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(162);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Sign) {
				{
				setState(161);
				match(Sign);
				}
			}

			setState(164);
			urealBin();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class RealOctContext extends ParserRuleContext {
		public UrealOctContext urealOct() {
			return getRuleContext(UrealOctContext.class,0);
		}
		public TerminalNode Sign() { return getToken(KernelParser.Sign, 0); }
		public RealOctContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_realOct; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterRealOct(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitRealOct(this);
		}
	}

	public final RealOctContext realOct() throws RecognitionException {
		RealOctContext _localctx = new RealOctContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_realOct);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(167);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Sign) {
				{
				setState(166);
				match(Sign);
				}
			}

			setState(169);
			urealOct();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class RealDecContext extends ParserRuleContext {
		public UrealDecContext urealDec() {
			return getRuleContext(UrealDecContext.class,0);
		}
		public TerminalNode Sign() { return getToken(KernelParser.Sign, 0); }
		public RealDecContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_realDec; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterRealDec(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitRealDec(this);
		}
	}

	public final RealDecContext realDec() throws RecognitionException {
		RealDecContext _localctx = new RealDecContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_realDec);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(172);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Sign) {
				{
				setState(171);
				match(Sign);
				}
			}

			setState(174);
			urealDec();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class RealHexContext extends ParserRuleContext {
		public UrealHexContext urealHex() {
			return getRuleContext(UrealHexContext.class,0);
		}
		public TerminalNode Sign() { return getToken(KernelParser.Sign, 0); }
		public RealHexContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_realHex; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterRealHex(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitRealHex(this);
		}
	}

	public final RealHexContext realHex() throws RecognitionException {
		RealHexContext _localctx = new RealHexContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_realHex);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(177);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Sign) {
				{
				setState(176);
				match(Sign);
				}
			}

			setState(179);
			urealHex();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ImagBinContext extends ParserRuleContext {
		public UrealBinContext urealBin() {
			return getRuleContext(UrealBinContext.class,0);
		}
		public ImagBinContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_imagBin; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterImagBin(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitImagBin(this);
		}
	}

	public final ImagBinContext imagBin() throws RecognitionException {
		ImagBinContext _localctx = new ImagBinContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_imagBin);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(182);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==UintegerBin) {
				{
				setState(181);
				urealBin();
				}
			}

			setState(184);
			_la = _input.LA(1);
			if ( !(_la==T__6 || _la==T__7) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ImagOctContext extends ParserRuleContext {
		public UrealOctContext urealOct() {
			return getRuleContext(UrealOctContext.class,0);
		}
		public ImagOctContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_imagOct; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterImagOct(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitImagOct(this);
		}
	}

	public final ImagOctContext imagOct() throws RecognitionException {
		ImagOctContext _localctx = new ImagOctContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_imagOct);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(187);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==UintegerOct) {
				{
				setState(186);
				urealOct();
				}
			}

			setState(189);
			_la = _input.LA(1);
			if ( !(_la==T__6 || _la==T__7) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ImagDecContext extends ParserRuleContext {
		public UrealDecContext urealDec() {
			return getRuleContext(UrealDecContext.class,0);
		}
		public ImagDecContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_imagDec; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterImagDec(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitImagDec(this);
		}
	}

	public final ImagDecContext imagDec() throws RecognitionException {
		ImagDecContext _localctx = new ImagDecContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_imagDec);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(192);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==UintegerDec || _la==Decimal) {
				{
				setState(191);
				urealDec();
				}
			}

			setState(194);
			_la = _input.LA(1);
			if ( !(_la==T__6 || _la==T__7) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ImagHexContext extends ParserRuleContext {
		public UrealHexContext urealHex() {
			return getRuleContext(UrealHexContext.class,0);
		}
		public ImagHexContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_imagHex; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterImagHex(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitImagHex(this);
		}
	}

	public final ImagHexContext imagHex() throws RecognitionException {
		ImagHexContext _localctx = new ImagHexContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_imagHex);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(197);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==UintegerHex) {
				{
				setState(196);
				urealHex();
				}
			}

			setState(199);
			_la = _input.LA(1);
			if ( !(_la==T__6 || _la==T__7) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class UrealBinContext extends ParserRuleContext {
		public UrealBinContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_urealBin; }
	 
		public UrealBinContext() { }
		public void copyFrom(UrealBinContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class BinaryIntegerContext extends UrealBinContext {
		public TerminalNode UintegerBin() { return getToken(KernelParser.UintegerBin, 0); }
		public BinaryIntegerContext(UrealBinContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterBinaryInteger(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitBinaryInteger(this);
		}
	}
	public static class BinaryRationalContext extends UrealBinContext {
		public List<TerminalNode> UintegerBin() { return getTokens(KernelParser.UintegerBin); }
		public TerminalNode UintegerBin(int i) {
			return getToken(KernelParser.UintegerBin, i);
		}
		public BinaryRationalContext(UrealBinContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterBinaryRational(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitBinaryRational(this);
		}
	}

	public final UrealBinContext urealBin() throws RecognitionException {
		UrealBinContext _localctx = new UrealBinContext(_ctx, getState());
		enterRule(_localctx, 42, RULE_urealBin);
		try {
			setState(205);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,23,_ctx) ) {
			case 1:
				_localctx = new BinaryIntegerContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(201);
				match(UintegerBin);
				}
				break;
			case 2:
				_localctx = new BinaryRationalContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				{
				setState(202);
				match(UintegerBin);
				setState(203);
				match(T__8);
				setState(204);
				match(UintegerBin);
				}
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class UrealOctContext extends ParserRuleContext {
		public UrealOctContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_urealOct; }
	 
		public UrealOctContext() { }
		public void copyFrom(UrealOctContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class OctalIntegerContext extends UrealOctContext {
		public TerminalNode UintegerOct() { return getToken(KernelParser.UintegerOct, 0); }
		public OctalIntegerContext(UrealOctContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterOctalInteger(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitOctalInteger(this);
		}
	}
	public static class OctalRationalContext extends UrealOctContext {
		public List<TerminalNode> UintegerOct() { return getTokens(KernelParser.UintegerOct); }
		public TerminalNode UintegerOct(int i) {
			return getToken(KernelParser.UintegerOct, i);
		}
		public OctalRationalContext(UrealOctContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterOctalRational(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitOctalRational(this);
		}
	}

	public final UrealOctContext urealOct() throws RecognitionException {
		UrealOctContext _localctx = new UrealOctContext(_ctx, getState());
		enterRule(_localctx, 44, RULE_urealOct);
		try {
			setState(211);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,24,_ctx) ) {
			case 1:
				_localctx = new OctalIntegerContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(207);
				match(UintegerOct);
				}
				break;
			case 2:
				_localctx = new OctalRationalContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(208);
				match(UintegerOct);
				setState(209);
				match(T__8);
				setState(210);
				match(UintegerOct);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class UrealDecContext extends ParserRuleContext {
		public UrealDecContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_urealDec; }
	 
		public UrealDecContext() { }
		public void copyFrom(UrealDecContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class DecimalRationalContext extends UrealDecContext {
		public List<TerminalNode> UintegerDec() { return getTokens(KernelParser.UintegerDec); }
		public TerminalNode UintegerDec(int i) {
			return getToken(KernelParser.UintegerDec, i);
		}
		public DecimalRationalContext(UrealDecContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterDecimalRational(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitDecimalRational(this);
		}
	}
	public static class DecimalIntegerContext extends UrealDecContext {
		public TerminalNode UintegerDec() { return getToken(KernelParser.UintegerDec, 0); }
		public DecimalIntegerContext(UrealDecContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterDecimalInteger(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitDecimalInteger(this);
		}
	}
	public static class DecimalRealContext extends UrealDecContext {
		public TerminalNode Decimal() { return getToken(KernelParser.Decimal, 0); }
		public DecimalRealContext(UrealDecContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterDecimalReal(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitDecimalReal(this);
		}
	}

	public final UrealDecContext urealDec() throws RecognitionException {
		UrealDecContext _localctx = new UrealDecContext(_ctx, getState());
		enterRule(_localctx, 46, RULE_urealDec);
		try {
			setState(218);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,25,_ctx) ) {
			case 1:
				_localctx = new DecimalIntegerContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(213);
				match(UintegerDec);
				}
				break;
			case 2:
				_localctx = new DecimalRationalContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(214);
				match(UintegerDec);
				setState(215);
				match(T__8);
				setState(216);
				match(UintegerDec);
				}
				break;
			case 3:
				_localctx = new DecimalRealContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(217);
				match(Decimal);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class UrealHexContext extends ParserRuleContext {
		public UrealHexContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_urealHex; }
	 
		public UrealHexContext() { }
		public void copyFrom(UrealHexContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class HexadecimalRationalContext extends UrealHexContext {
		public List<TerminalNode> UintegerHex() { return getTokens(KernelParser.UintegerHex); }
		public TerminalNode UintegerHex(int i) {
			return getToken(KernelParser.UintegerHex, i);
		}
		public HexadecimalRationalContext(UrealHexContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterHexadecimalRational(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitHexadecimalRational(this);
		}
	}
	public static class HexadecimalIntegerContext extends UrealHexContext {
		public TerminalNode UintegerHex() { return getToken(KernelParser.UintegerHex, 0); }
		public HexadecimalIntegerContext(UrealHexContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).enterHexadecimalInteger(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof KernelListener ) ((KernelListener)listener).exitHexadecimalInteger(this);
		}
	}

	public final UrealHexContext urealHex() throws RecognitionException {
		UrealHexContext _localctx = new UrealHexContext(_ctx, getState());
		enterRule(_localctx, 48, RULE_urealHex);
		try {
			setState(224);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,26,_ctx) ) {
			case 1:
				_localctx = new HexadecimalIntegerContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(220);
				match(UintegerHex);
				}
				break;
			case 2:
				_localctx = new HexadecimalRationalContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(221);
				match(UintegerHex);
				setState(222);
				match(T__8);
				setState(223);
				match(UintegerHex);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\32\u00e5\4\2\t\2"+
		"\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\3\2\6\2\66\n\2\r\2\16\2\67\3\3\3\3\5\3<\n\3\3\4\3\4\3\4\3\4"+
		"\5\4B\n\4\3\5\3\5\6\5F\n\5\r\5\16\5G\3\5\3\5\5\5L\n\5\3\5\3\5\3\6\3\6"+
		"\3\6\3\6\5\6T\n\6\3\7\3\7\3\7\3\b\3\b\3\b\3\t\5\t]\n\t\3\t\3\t\3\n\3\n"+
		"\3\n\3\13\5\13e\n\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13"+
		"\3\13\5\13r\n\13\3\f\5\fu\n\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f"+
		"\3\f\5\f\u0082\n\f\3\r\5\r\u0085\n\r\3\r\3\r\3\r\3\r\3\r\3\r\3\r\3\r\3"+
		"\r\3\r\3\r\5\r\u0092\n\r\3\16\5\16\u0095\n\16\3\16\3\16\3\16\3\16\3\16"+
		"\3\16\3\16\3\16\3\16\3\16\3\16\5\16\u00a2\n\16\3\17\5\17\u00a5\n\17\3"+
		"\17\3\17\3\20\5\20\u00aa\n\20\3\20\3\20\3\21\5\21\u00af\n\21\3\21\3\21"+
		"\3\22\5\22\u00b4\n\22\3\22\3\22\3\23\5\23\u00b9\n\23\3\23\3\23\3\24\5"+
		"\24\u00be\n\24\3\24\3\24\3\25\5\25\u00c3\n\25\3\25\3\25\3\26\5\26\u00c8"+
		"\n\26\3\26\3\26\3\27\3\27\3\27\3\27\5\27\u00d0\n\27\3\30\3\30\3\30\3\30"+
		"\5\30\u00d6\n\30\3\31\3\31\3\31\3\31\3\31\5\31\u00dd\n\31\3\32\3\32\3"+
		"\32\3\32\5\32\u00e3\n\32\3\32\2\2\33\2\4\6\b\n\f\16\20\22\24\26\30\32"+
		"\34\36 \"$&(*,.\60\62\2\4\3\2\6\7\3\2\t\n\2\u00f3\2\65\3\2\2\2\4;\3\2"+
		"\2\2\6A\3\2\2\2\bC\3\2\2\2\nS\3\2\2\2\fU\3\2\2\2\16X\3\2\2\2\20\\\3\2"+
		"\2\2\22`\3\2\2\2\24q\3\2\2\2\26\u0081\3\2\2\2\30\u0091\3\2\2\2\32\u00a1"+
		"\3\2\2\2\34\u00a4\3\2\2\2\36\u00a9\3\2\2\2 \u00ae\3\2\2\2\"\u00b3\3\2"+
		"\2\2$\u00b8\3\2\2\2&\u00bd\3\2\2\2(\u00c2\3\2\2\2*\u00c7\3\2\2\2,\u00cf"+
		"\3\2\2\2.\u00d5\3\2\2\2\60\u00dc\3\2\2\2\62\u00e2\3\2\2\2\64\66\5\4\3"+
		"\2\65\64\3\2\2\2\66\67\3\2\2\2\67\65\3\2\2\2\678\3\2\2\28\3\3\2\2\29<"+
		"\5\6\4\2:<\5\b\5\2;9\3\2\2\2;:\3\2\2\2<\5\3\2\2\2=B\7\f\2\2>B\7\r\2\2"+
		"?B\5\n\6\2@B\7\31\2\2A=\3\2\2\2A>\3\2\2\2A?\3\2\2\2A@\3\2\2\2B\7\3\2\2"+
		"\2CE\7\3\2\2DF\5\4\3\2ED\3\2\2\2FG\3\2\2\2GE\3\2\2\2GH\3\2\2\2HK\3\2\2"+
		"\2IJ\7\4\2\2JL\5\4\3\2KI\3\2\2\2KL\3\2\2\2LM\3\2\2\2MN\7\5\2\2N\t\3\2"+
		"\2\2OT\5\f\7\2PT\5\16\b\2QT\5\20\t\2RT\5\22\n\2SO\3\2\2\2SP\3\2\2\2SQ"+
		"\3\2\2\2SR\3\2\2\2T\13\3\2\2\2UV\7\22\2\2VW\5\24\13\2W\r\3\2\2\2XY\7\23"+
		"\2\2YZ\5\26\f\2Z\17\3\2\2\2[]\7\24\2\2\\[\3\2\2\2\\]\3\2\2\2]^\3\2\2\2"+
		"^_\5\30\r\2_\21\3\2\2\2`a\7\25\2\2ab\5\32\16\2b\23\3\2\2\2ce\t\2\2\2d"+
		"c\3\2\2\2de\3\2\2\2ef\3\2\2\2fr\5,\27\2gh\5\34\17\2hi\7\b\2\2ij\5\34\17"+
		"\2jr\3\2\2\2kl\5\34\17\2lm\t\2\2\2mn\5$\23\2nr\3\2\2\2op\t\2\2\2pr\5$"+
		"\23\2qd\3\2\2\2qg\3\2\2\2qk\3\2\2\2qo\3\2\2\2r\25\3\2\2\2su\t\2\2\2ts"+
		"\3\2\2\2tu\3\2\2\2uv\3\2\2\2v\u0082\5.\30\2wx\5\36\20\2xy\7\b\2\2yz\5"+
		"\36\20\2z\u0082\3\2\2\2{|\5\36\20\2|}\t\2\2\2}~\5&\24\2~\u0082\3\2\2\2"+
		"\177\u0080\t\2\2\2\u0080\u0082\5&\24\2\u0081t\3\2\2\2\u0081w\3\2\2\2\u0081"+
		"{\3\2\2\2\u0081\177\3\2\2\2\u0082\27\3\2\2\2\u0083\u0085\t\2\2\2\u0084"+
		"\u0083\3\2\2\2\u0084\u0085\3\2\2\2\u0085\u0086\3\2\2\2\u0086\u0092\5\60"+
		"\31\2\u0087\u0088\5 \21\2\u0088\u0089\7\b\2\2\u0089\u008a\5 \21\2\u008a"+
		"\u0092\3\2\2\2\u008b\u008c\5 \21\2\u008c\u008d\t\2\2\2\u008d\u008e\5("+
		"\25\2\u008e\u0092\3\2\2\2\u008f\u0090\t\2\2\2\u0090\u0092\5(\25\2\u0091"+
		"\u0084\3\2\2\2\u0091\u0087\3\2\2\2\u0091\u008b\3\2\2\2\u0091\u008f\3\2"+
		"\2\2\u0092\31\3\2\2\2\u0093\u0095\t\2\2\2\u0094\u0093\3\2\2\2\u0094\u0095"+
		"\3\2\2\2\u0095\u0096\3\2\2\2\u0096\u00a2\5\62\32\2\u0097\u0098\5\"\22"+
		"\2\u0098\u0099\7\b\2\2\u0099\u009a\5\"\22\2\u009a\u00a2\3\2\2\2\u009b"+
		"\u009c\5\"\22\2\u009c\u009d\t\2\2\2\u009d\u009e\5*\26\2\u009e\u00a2\3"+
		"\2\2\2\u009f\u00a0\t\2\2\2\u00a0\u00a2\5*\26\2\u00a1\u0094\3\2\2\2\u00a1"+
		"\u0097\3\2\2\2\u00a1\u009b\3\2\2\2\u00a1\u009f\3\2\2\2\u00a2\33\3\2\2"+
		"\2\u00a3\u00a5\7\27\2\2\u00a4\u00a3\3\2\2\2\u00a4\u00a5\3\2\2\2\u00a5"+
		"\u00a6\3\2\2\2\u00a6\u00a7\5,\27\2\u00a7\35\3\2\2\2\u00a8\u00aa\7\27\2"+
		"\2\u00a9\u00a8\3\2\2\2\u00a9\u00aa\3\2\2\2\u00aa\u00ab\3\2\2\2\u00ab\u00ac"+
		"\5.\30\2\u00ac\37\3\2\2\2\u00ad\u00af\7\27\2\2\u00ae\u00ad\3\2\2\2\u00ae"+
		"\u00af\3\2\2\2\u00af\u00b0\3\2\2\2\u00b0\u00b1\5\60\31\2\u00b1!\3\2\2"+
		"\2\u00b2\u00b4\7\27\2\2\u00b3\u00b2\3\2\2\2\u00b3\u00b4\3\2\2\2\u00b4"+
		"\u00b5\3\2\2\2\u00b5\u00b6\5\62\32\2\u00b6#\3\2\2\2\u00b7\u00b9\5,\27"+
		"\2\u00b8\u00b7\3\2\2\2\u00b8\u00b9\3\2\2\2\u00b9\u00ba\3\2\2\2\u00ba\u00bb"+
		"\t\3\2\2\u00bb%\3\2\2\2\u00bc\u00be\5.\30\2\u00bd\u00bc\3\2\2\2\u00bd"+
		"\u00be\3\2\2\2\u00be\u00bf\3\2\2\2\u00bf\u00c0\t\3\2\2\u00c0\'\3\2\2\2"+
		"\u00c1\u00c3\5\60\31\2\u00c2\u00c1\3\2\2\2\u00c2\u00c3\3\2\2\2\u00c3\u00c4"+
		"\3\2\2\2\u00c4\u00c5\t\3\2\2\u00c5)\3\2\2\2\u00c6\u00c8\5\62\32\2\u00c7"+
		"\u00c6\3\2\2\2\u00c7\u00c8\3\2\2\2\u00c8\u00c9\3\2\2\2\u00c9\u00ca\t\3"+
		"\2\2\u00ca+\3\2\2\2\u00cb\u00d0\7\16\2\2\u00cc\u00cd\7\16\2\2\u00cd\u00ce"+
		"\7\13\2\2\u00ce\u00d0\7\16\2\2\u00cf\u00cb\3\2\2\2\u00cf\u00cc\3\2\2\2"+
		"\u00d0-\3\2\2\2\u00d1\u00d6\7\17\2\2\u00d2\u00d3\7\17\2\2\u00d3\u00d4"+
		"\7\13\2\2\u00d4\u00d6\7\17\2\2\u00d5\u00d1\3\2\2\2\u00d5\u00d2\3\2\2\2"+
		"\u00d6/\3\2\2\2\u00d7\u00dd\7\20\2\2\u00d8\u00d9\7\20\2\2\u00d9\u00da"+
		"\7\13\2\2\u00da\u00dd\7\20\2\2\u00db\u00dd\7\26\2\2\u00dc\u00d7\3\2\2"+
		"\2\u00dc\u00d8\3\2\2\2\u00dc\u00db\3\2\2\2\u00dd\61\3\2\2\2\u00de\u00e3"+
		"\7\21\2\2\u00df\u00e0\7\21\2\2\u00e0\u00e1\7\13\2\2\u00e1\u00e3\7\21\2"+
		"\2\u00e2\u00de\3\2\2\2\u00e2\u00df\3\2\2\2\u00e3\63\3\2\2\2\35\67;AGK"+
		"S\\dqt\u0081\u0084\u0091\u0094\u00a1\u00a4\u00a9\u00ae\u00b3\u00b8\u00bd"+
		"\u00c2\u00c7\u00cf\u00d5\u00dc\u00e2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}