// Generated from Kernel.g4 by ANTLR 4.7.1
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class KernelLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.7.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		KEYWORD=10, STRING=11, UintegerBin=12, UintegerOct=13, UintegerDec=14, 
		UintegerHex=15, PrefixBin=16, PrefixOct=17, PrefixDec=18, PrefixHex=19, 
		Decimal=20, Sign=21, Suffix=22, SYMBOL=23, WS=24;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"KEYWORD", "STRING", "EscapeSequence", "OctalEscape", "UnicodeEscape", 
		"UintegerBin", "UintegerOct", "UintegerDec", "UintegerHex", "PrefixBin", 
		"PrefixOct", "PrefixDec", "PrefixHex", "Decimal", "Sign", "Suffix", "Exactness", 
		"ExponentMarker", "DigitBin", "DigitOct", "DigitDec", "DigitHex", "RadixBin", 
		"RadixOct", "RadixDec", "RadixHex", "SYMBOL", "WS"
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


		int base = 10;


	public KernelLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "Kernel.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	@Override
	public void action(RuleContext _localctx, int ruleIndex, int actionIndex) {
		switch (ruleIndex) {
		case 18:
			PrefixBin_action((RuleContext)_localctx, actionIndex);
			break;
		case 19:
			PrefixOct_action((RuleContext)_localctx, actionIndex);
			break;
		case 20:
			PrefixDec_action((RuleContext)_localctx, actionIndex);
			break;
		case 21:
			PrefixHex_action((RuleContext)_localctx, actionIndex);
			break;
		case 23:
			Sign_action((RuleContext)_localctx, actionIndex);
			break;
		}
	}
	private void PrefixBin_action(RuleContext _localctx, int actionIndex) {
		switch (actionIndex) {
		case 0:
			base=2;
			break;
		}
	}
	private void PrefixOct_action(RuleContext _localctx, int actionIndex) {
		switch (actionIndex) {
		case 1:
			base=8;
			break;
		}
	}
	private void PrefixDec_action(RuleContext _localctx, int actionIndex) {
		switch (actionIndex) {
		case 2:
			base=10;
			break;
		}
	}
	private void PrefixHex_action(RuleContext _localctx, int actionIndex) {
		switch (actionIndex) {
		case 3:
			base=16;
			break;
		}
	}
	private void Sign_action(RuleContext _localctx, int actionIndex) {
		switch (actionIndex) {
		case 4:
			System.out.println("HELLO");
			break;
		}
	}
	@Override
	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 27:
			return DigitBin_sempred((RuleContext)_localctx, predIndex);
		case 28:
			return DigitOct_sempred((RuleContext)_localctx, predIndex);
		case 29:
			return DigitDec_sempred((RuleContext)_localctx, predIndex);
		case 30:
			return DigitHex_sempred((RuleContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean DigitBin_sempred(RuleContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return base == 2;
		}
		return true;
	}
	private boolean DigitOct_sempred(RuleContext _localctx, int predIndex) {
		switch (predIndex) {
		case 1:
			return base == 8;
		}
		return true;
	}
	private boolean DigitDec_sempred(RuleContext _localctx, int predIndex) {
		switch (predIndex) {
		case 2:
			return base == 10;
		}
		return true;
	}
	private boolean DigitHex_sempred(RuleContext _localctx, int predIndex) {
		switch (predIndex) {
		case 3:
			return base ==16;
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\32\u0156\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31"+
		"\t\31\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t"+
		" \4!\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\3\2\3\2\3\3\3\3\3\4\3\4\3\5\3"+
		"\5\3\6\3\6\3\7\3\7\3\b\3\b\3\t\3\t\3\n\3\n\3\13\3\13\3\13\3\13\3\13\3"+
		"\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\5"+
		"\13s\n\13\3\f\3\f\3\f\7\fx\n\f\f\f\16\f{\13\f\3\f\3\f\3\r\3\r\3\r\3\r"+
		"\5\r\u0083\n\r\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\5\16\u008e"+
		"\n\16\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3\20\6\20\u0098\n\20\r\20\16"+
		"\20\u0099\3\20\7\20\u009d\n\20\f\20\16\20\u00a0\13\20\3\21\6\21\u00a3"+
		"\n\21\r\21\16\21\u00a4\3\21\7\21\u00a8\n\21\f\21\16\21\u00ab\13\21\3\22"+
		"\6\22\u00ae\n\22\r\22\16\22\u00af\3\22\7\22\u00b3\n\22\f\22\16\22\u00b6"+
		"\13\22\3\23\6\23\u00b9\n\23\r\23\16\23\u00ba\3\23\7\23\u00be\n\23\f\23"+
		"\16\23\u00c1\13\23\3\24\3\24\5\24\u00c5\n\24\3\24\3\24\3\24\5\24\u00ca"+
		"\n\24\3\24\3\24\3\25\3\25\5\25\u00d0\n\25\3\25\3\25\3\25\5\25\u00d5\n"+
		"\25\3\25\3\25\3\26\3\26\5\26\u00db\n\26\3\26\3\26\5\26\u00df\n\26\5\26"+
		"\u00e1\n\26\3\26\3\26\3\27\3\27\5\27\u00e7\n\27\3\27\3\27\3\27\5\27\u00ec"+
		"\n\27\3\27\3\27\3\30\3\30\5\30\u00f2\n\30\3\30\5\30\u00f5\n\30\3\30\3"+
		"\30\7\30\u00f9\n\30\f\30\16\30\u00fc\13\30\3\30\7\30\u00ff\n\30\f\30\16"+
		"\30\u0102\13\30\3\30\5\30\u0105\n\30\3\30\6\30\u0108\n\30\r\30\16\30\u0109"+
		"\3\30\6\30\u010d\n\30\r\30\16\30\u010e\3\30\3\30\7\30\u0113\n\30\f\30"+
		"\16\30\u0116\13\30\3\30\5\30\u0119\n\30\5\30\u011b\n\30\3\31\3\31\3\31"+
		"\5\31\u0120\n\31\3\32\3\32\5\32\u0124\n\32\3\32\6\32\u0127\n\32\r\32\16"+
		"\32\u0128\3\33\3\33\3\33\3\33\5\33\u012f\n\33\3\34\3\34\3\35\3\35\3\35"+
		"\3\36\3\36\3\36\3\37\3\37\3\37\3 \3 \3 \3!\3!\3!\3\"\3\"\3\"\3#\3#\3#"+
		"\3$\3$\3$\3%\6%\u014c\n%\r%\16%\u014d\3&\6&\u0151\n&\r&\16&\u0152\3&\3"+
		"&\2\2\'\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\2\33\2\35"+
		"\2\37\16!\17#\20%\21\'\22)\23+\24-\25/\26\61\27\63\30\65\2\67\29\2;\2"+
		"=\2?\2A\2C\2E\2G\2I\31K\32\3\2\27\4\2VVvv\4\2HHhh\4\2KKkk\4\2IIii\4\2"+
		"PPpp\4\2QQqq\4\2TTtt\4\2GGgg\4\2$$^^\16\2$$DDHHPPTTVV^^ddhhppttvv\3\2"+
		"\62\65\3\2\629\b\2FHNNUUfhnnuu\3\2\62\63\3\2\62;\5\2\62;CHch\4\2DDdd\4"+
		"\2FFff\4\2ZZzz\4\2C\\c|\5\2\13\f\17\17\"\"\2\u0174\2\3\3\2\2\2\2\5\3\2"+
		"\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21"+
		"\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\37\3\2\2\2\2!\3\2\2"+
		"\2\2#\3\2\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2"+
		"\2/\3\2\2\2\2\61\3\2\2\2\2\63\3\2\2\2\2I\3\2\2\2\2K\3\2\2\2\3M\3\2\2\2"+
		"\5O\3\2\2\2\7Q\3\2\2\2\tS\3\2\2\2\13U\3\2\2\2\rW\3\2\2\2\17Y\3\2\2\2\21"+
		"[\3\2\2\2\23]\3\2\2\2\25r\3\2\2\2\27t\3\2\2\2\31\u0082\3\2\2\2\33\u008d"+
		"\3\2\2\2\35\u008f\3\2\2\2\37\u0097\3\2\2\2!\u00a2\3\2\2\2#\u00ad\3\2\2"+
		"\2%\u00b8\3\2\2\2\'\u00c9\3\2\2\2)\u00d4\3\2\2\2+\u00e0\3\2\2\2-\u00eb"+
		"\3\2\2\2/\u011a\3\2\2\2\61\u011f\3\2\2\2\63\u0121\3\2\2\2\65\u012e\3\2"+
		"\2\2\67\u0130\3\2\2\29\u0132\3\2\2\2;\u0135\3\2\2\2=\u0138\3\2\2\2?\u013b"+
		"\3\2\2\2A\u013e\3\2\2\2C\u0141\3\2\2\2E\u0144\3\2\2\2G\u0147\3\2\2\2I"+
		"\u014b\3\2\2\2K\u0150\3\2\2\2MN\7*\2\2N\4\3\2\2\2OP\7\60\2\2P\6\3\2\2"+
		"\2QR\7+\2\2R\b\3\2\2\2ST\7-\2\2T\n\3\2\2\2UV\7/\2\2V\f\3\2\2\2WX\7B\2"+
		"\2X\16\3\2\2\2YZ\7k\2\2Z\20\3\2\2\2[\\\7K\2\2\\\22\3\2\2\2]^\7\61\2\2"+
		"^\24\3\2\2\2_`\7%\2\2`s\t\2\2\2ab\7%\2\2bs\t\3\2\2cd\7%\2\2de\t\4\2\2"+
		"ef\t\5\2\2fg\t\6\2\2gh\t\7\2\2hi\t\b\2\2is\t\t\2\2jk\7%\2\2kl\t\4\2\2"+
		"lm\t\6\2\2mn\t\t\2\2no\t\b\2\2os\t\2\2\2pq\7*\2\2qs\7+\2\2r_\3\2\2\2r"+
		"a\3\2\2\2rc\3\2\2\2rj\3\2\2\2rp\3\2\2\2s\26\3\2\2\2ty\7$\2\2ux\5\31\r"+
		"\2vx\n\n\2\2wu\3\2\2\2wv\3\2\2\2x{\3\2\2\2yw\3\2\2\2yz\3\2\2\2z|\3\2\2"+
		"\2{y\3\2\2\2|}\7$\2\2}\30\3\2\2\2~\177\7^\2\2\177\u0083\t\13\2\2\u0080"+
		"\u0083\5\35\17\2\u0081\u0083\5\33\16\2\u0082~\3\2\2\2\u0082\u0080\3\2"+
		"\2\2\u0082\u0081\3\2\2\2\u0083\32\3\2\2\2\u0084\u0085\7^\2\2\u0085\u0086"+
		"\t\f\2\2\u0086\u0087\t\r\2\2\u0087\u008e\t\r\2\2\u0088\u0089\7^\2\2\u0089"+
		"\u008a\t\r\2\2\u008a\u008e\t\r\2\2\u008b\u008c\7^\2\2\u008c\u008e\t\r"+
		"\2\2\u008d\u0084\3\2\2\2\u008d\u0088\3\2\2\2\u008d\u008b\3\2\2\2\u008e"+
		"\34\3\2\2\2\u008f\u0090\7^\2\2\u0090\u0091\7w\2\2\u0091\u0092\5? \2\u0092"+
		"\u0093\5? \2\u0093\u0094\5? \2\u0094\u0095\5? \2\u0095\36\3\2\2\2\u0096"+
		"\u0098\59\35\2\u0097\u0096\3\2\2\2\u0098\u0099\3\2\2\2\u0099\u0097\3\2"+
		"\2\2\u0099\u009a\3\2\2\2\u009a\u009e\3\2\2\2\u009b\u009d\7%\2\2\u009c"+
		"\u009b\3\2\2\2\u009d\u00a0\3\2\2\2\u009e\u009c\3\2\2\2\u009e\u009f\3\2"+
		"\2\2\u009f \3\2\2\2\u00a0\u009e\3\2\2\2\u00a1\u00a3\5;\36\2\u00a2\u00a1"+
		"\3\2\2\2\u00a3\u00a4\3\2\2\2\u00a4\u00a2\3\2\2\2\u00a4\u00a5\3\2\2\2\u00a5"+
		"\u00a9\3\2\2\2\u00a6\u00a8\7%\2\2\u00a7\u00a6\3\2\2\2\u00a8\u00ab\3\2"+
		"\2\2\u00a9\u00a7\3\2\2\2\u00a9\u00aa\3\2\2\2\u00aa\"\3\2\2\2\u00ab\u00a9"+
		"\3\2\2\2\u00ac\u00ae\5=\37\2\u00ad\u00ac\3\2\2\2\u00ae\u00af\3\2\2\2\u00af"+
		"\u00ad\3\2\2\2\u00af\u00b0\3\2\2\2\u00b0\u00b4\3\2\2\2\u00b1\u00b3\7%"+
		"\2\2\u00b2\u00b1\3\2\2\2\u00b3\u00b6\3\2\2\2\u00b4\u00b2\3\2\2\2\u00b4"+
		"\u00b5\3\2\2\2\u00b5$\3\2\2\2\u00b6\u00b4\3\2\2\2\u00b7\u00b9\5? \2\u00b8"+
		"\u00b7\3\2\2\2\u00b9\u00ba\3\2\2\2\u00ba\u00b8\3\2\2\2\u00ba\u00bb\3\2"+
		"\2\2\u00bb\u00bf\3\2\2\2\u00bc\u00be\7%\2\2\u00bd\u00bc\3\2\2\2\u00be"+
		"\u00c1\3\2\2\2\u00bf\u00bd\3\2\2\2\u00bf\u00c0\3\2\2\2\u00c0&\3\2\2\2"+
		"\u00c1\u00bf\3\2\2\2\u00c2\u00c4\5A!\2\u00c3\u00c5\5\65\33\2\u00c4\u00c3"+
		"\3\2\2\2\u00c4\u00c5\3\2\2\2\u00c5\u00ca\3\2\2\2\u00c6\u00c7\5\65\33\2"+
		"\u00c7\u00c8\5A!\2\u00c8\u00ca\3\2\2\2\u00c9\u00c2\3\2\2\2\u00c9\u00c6"+
		"\3\2\2\2\u00ca\u00cb\3\2\2\2\u00cb\u00cc\b\24\2\2\u00cc(\3\2\2\2\u00cd"+
		"\u00cf\5C\"\2\u00ce\u00d0\5\65\33\2\u00cf\u00ce\3\2\2\2\u00cf\u00d0\3"+
		"\2\2\2\u00d0\u00d5\3\2\2\2\u00d1\u00d2\5\65\33\2\u00d2\u00d3\5C\"\2\u00d3"+
		"\u00d5\3\2\2\2\u00d4\u00cd\3\2\2\2\u00d4\u00d1\3\2\2\2\u00d5\u00d6\3\2"+
		"\2\2\u00d6\u00d7\b\25\3\2\u00d7*\3\2\2\2\u00d8\u00da\5E#\2\u00d9\u00db"+
		"\5\65\33\2\u00da\u00d9\3\2\2\2\u00da\u00db\3\2\2\2\u00db\u00e1\3\2\2\2"+
		"\u00dc\u00de\5\65\33\2\u00dd\u00df\5E#\2\u00de\u00dd\3\2\2\2\u00de\u00df"+
		"\3\2\2\2\u00df\u00e1\3\2\2\2\u00e0\u00d8\3\2\2\2\u00e0\u00dc\3\2\2\2\u00e1"+
		"\u00e2\3\2\2\2\u00e2\u00e3\b\26\4\2\u00e3,\3\2\2\2\u00e4\u00e6\5G$\2\u00e5"+
		"\u00e7\5\65\33\2\u00e6\u00e5\3\2\2\2\u00e6\u00e7\3\2\2\2\u00e7\u00ec\3"+
		"\2\2\2\u00e8\u00e9\5\65\33\2\u00e9\u00ea\5G$\2\u00ea\u00ec\3\2\2\2\u00eb"+
		"\u00e4\3\2\2\2\u00eb\u00e8\3\2\2\2\u00ec\u00ed\3\2\2\2\u00ed\u00ee\b\27"+
		"\5\2\u00ee.\3\2\2\2\u00ef\u00f1\5#\22\2\u00f0\u00f2\5\63\32\2\u00f1\u00f0"+
		"\3\2\2\2\u00f1\u00f2\3\2\2\2\u00f2\u011b\3\2\2\2\u00f3\u00f5\5=\37\2\u00f4"+
		"\u00f3\3\2\2\2\u00f4\u00f5\3\2\2\2\u00f5\u00f6\3\2\2\2\u00f6\u00fa\7\60"+
		"\2\2\u00f7\u00f9\5=\37\2\u00f8\u00f7\3\2\2\2\u00f9\u00fc\3\2\2\2\u00fa"+
		"\u00f8\3\2\2\2\u00fa\u00fb\3\2\2\2\u00fb\u0100\3\2\2\2\u00fc\u00fa\3\2"+
		"\2\2\u00fd\u00ff\7%\2\2\u00fe\u00fd\3\2\2\2\u00ff\u0102\3\2\2\2\u0100"+
		"\u00fe\3\2\2\2\u0100\u0101\3\2\2\2\u0101\u0104\3\2\2\2\u0102\u0100\3\2"+
		"\2\2\u0103\u0105\5\63\32\2\u0104\u0103\3\2\2\2\u0104\u0105\3\2\2\2\u0105"+
		"\u011b\3\2\2\2\u0106\u0108\5=\37\2\u0107\u0106\3\2\2\2\u0108\u0109\3\2"+
		"\2\2\u0109\u0107\3\2\2\2\u0109\u010a\3\2\2\2\u010a\u010c\3\2\2\2\u010b"+
		"\u010d\7%\2\2\u010c\u010b\3\2\2\2\u010d\u010e\3\2\2\2\u010e\u010c\3\2"+
		"\2\2\u010e\u010f\3\2\2\2\u010f\u0110\3\2\2\2\u0110\u0114\7\60\2\2\u0111"+
		"\u0113\7%\2\2\u0112\u0111\3\2\2\2\u0113\u0116\3\2\2\2\u0114\u0112\3\2"+
		"\2\2\u0114\u0115\3\2\2\2\u0115\u0118\3\2\2\2\u0116\u0114\3\2\2\2\u0117"+
		"\u0119\5\63\32\2\u0118\u0117\3\2\2\2\u0118\u0119\3\2\2\2\u0119\u011b\3"+
		"\2\2\2\u011a\u00ef\3\2\2\2\u011a\u00f4\3\2\2\2\u011a\u0107\3\2\2\2\u011b"+
		"\60\3\2\2\2\u011c\u0120\7-\2\2\u011d\u011e\7/\2\2\u011e\u0120\b\31\6\2"+
		"\u011f\u011c\3\2\2\2\u011f\u011d\3\2\2\2\u0120\62\3\2\2\2\u0121\u0123"+
		"\5\67\34\2\u0122\u0124\5\61\31\2\u0123\u0122\3\2\2\2\u0123\u0124\3\2\2"+
		"\2\u0124\u0126\3\2\2\2\u0125\u0127\5=\37\2\u0126\u0125\3\2\2\2\u0127\u0128"+
		"\3\2\2\2\u0128\u0126\3\2\2\2\u0128\u0129\3\2\2\2\u0129\64\3\2\2\2\u012a"+
		"\u012b\7%\2\2\u012b\u012f\t\4\2\2\u012c\u012d\7%\2\2\u012d\u012f\t\t\2"+
		"\2\u012e\u012a\3\2\2\2\u012e\u012c\3\2\2\2\u012f\66\3\2\2\2\u0130\u0131"+
		"\t\16\2\2\u01318\3\2\2\2\u0132\u0133\t\17\2\2\u0133\u0134\6\35\2\2\u0134"+
		":\3\2\2\2\u0135\u0136\t\r\2\2\u0136\u0137\6\36\3\2\u0137<\3\2\2\2\u0138"+
		"\u0139\t\20\2\2\u0139\u013a\6\37\4\2\u013a>\3\2\2\2\u013b\u013c\t\21\2"+
		"\2\u013c\u013d\6 \5\2\u013d@\3\2\2\2\u013e\u013f\7%\2\2\u013f\u0140\t"+
		"\22\2\2\u0140B\3\2\2\2\u0141\u0142\7%\2\2\u0142\u0143\t\7\2\2\u0143D\3"+
		"\2\2\2\u0144\u0145\7%\2\2\u0145\u0146\t\23\2\2\u0146F\3\2\2\2\u0147\u0148"+
		"\7%\2\2\u0148\u0149\t\24\2\2\u0149H\3\2\2\2\u014a\u014c\t\25\2\2\u014b"+
		"\u014a\3\2\2\2\u014c\u014d\3\2\2\2\u014d\u014b\3\2\2\2\u014d\u014e\3\2"+
		"\2\2\u014eJ\3\2\2\2\u014f\u0151\t\26\2\2\u0150\u014f\3\2\2\2\u0151\u0152"+
		"\3\2\2\2\u0152\u0150\3\2\2\2\u0152\u0153\3\2\2\2\u0153\u0154\3\2\2\2\u0154"+
		"\u0155\b&\7\2\u0155L\3\2\2\2)\2rwy\u0082\u008d\u0099\u009e\u00a4\u00a9"+
		"\u00af\u00b4\u00ba\u00bf\u00c4\u00c9\u00cf\u00d4\u00da\u00de\u00e0\u00e6"+
		"\u00eb\u00f1\u00f4\u00fa\u0100\u0104\u0109\u010e\u0114\u0118\u011a\u011f"+
		"\u0123\u0128\u012e\u014d\u0152\b\3\24\2\3\25\3\3\26\4\3\27\5\3\31\6\b"+
		"\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}