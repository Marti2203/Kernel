// Generated from /home/martin/Projects/Kernel/Kernel/Parser/Kernel.g4 by ANTLR 4.7.1
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
		T__9=10, EscapeSequence=11, Integer=12, Rational=13, Real=14, Complex=15, 
		Symbol=16, COMMENT=17, WHITESPACE=18;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "EscapeSequence", "Integer", "Rational", "Real", "Complex", "FloatingNumber", 
		"Symbol", "COMMENT", "WHITESPACE"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'('", "'.'", "')'", "'#t'", "'#f'", "'#ignore'", "'#inert'", "'()'", 
		"'\"'", "'\\'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, null, null, null, null, null, null, null, null, null, null, "EscapeSequence", 
		"Integer", "Rational", "Real", "Complex", "Symbol", "COMMENT", "WHITESPACE"
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

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\24\u00ca\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\3\2\3\2\3\3\3\3\3\4\3\4\3\5\3\5\3\5\3\6\3\6"+
		"\3\6\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\b\3\b\3\b\3\b\3\b\3\b\3\b\3\t\3"+
		"\t\3\t\3\n\3\n\3\13\3\13\3\f\3\f\3\f\3\r\3\r\5\rQ\n\r\3\r\5\rT\n\r\3\r"+
		"\6\rW\n\r\r\r\16\rX\3\r\3\r\3\r\3\r\5\r_\n\r\3\r\6\rb\n\r\r\r\16\rc\3"+
		"\r\3\r\3\r\3\r\5\rj\n\r\3\r\6\rm\n\r\r\r\16\rn\3\r\3\r\3\r\3\r\5\ru\n"+
		"\r\3\r\6\rx\n\r\r\r\16\ry\5\r|\n\r\3\16\5\16\177\n\16\3\16\6\16\u0082"+
		"\n\16\r\16\16\16\u0083\3\16\3\16\6\16\u0088\n\16\r\16\16\16\u0089\3\17"+
		"\5\17\u008d\n\17\3\17\3\17\3\20\5\20\u0092\n\20\3\20\3\20\3\20\3\20\3"+
		"\20\3\21\6\21\u009a\n\21\r\21\16\21\u009b\3\21\3\21\7\21\u00a0\n\21\f"+
		"\21\16\21\u00a3\13\21\3\21\3\21\6\21\u00a7\n\21\r\21\16\21\u00a8\5\21"+
		"\u00ab\n\21\3\22\3\22\3\22\6\22\u00b0\n\22\r\22\16\22\u00b1\5\22\u00b4"+
		"\n\22\3\23\3\23\7\23\u00b8\n\23\f\23\16\23\u00bb\13\23\3\23\5\23\u00be"+
		"\n\23\3\23\3\23\3\23\3\23\3\24\6\24\u00c5\n\24\r\24\16\24\u00c6\3\24\3"+
		"\24\3\u00b9\2\25\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31"+
		"\16\33\17\35\20\37\21!\2#\22%\23\'\24\3\2\f\n\2$$))^^ddhhppttvv\4\2fg"+
		"kk\4\2--//\3\2\62;\3\2\62\63\3\2\629\5\2\62;CHch\13\2##&(,-/\61<<>\\`"+
		"ac|\u0080\u0080\n\2##&(,-/<>\\`ac|\u0080\u0080\5\2\13\f\17\17\"\"\2\u00e2"+
		"\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2"+
		"\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2"+
		"\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2#\3\2\2\2\2%\3\2"+
		"\2\2\2\'\3\2\2\2\3)\3\2\2\2\5+\3\2\2\2\7-\3\2\2\2\t/\3\2\2\2\13\62\3\2"+
		"\2\2\r\65\3\2\2\2\17=\3\2\2\2\21D\3\2\2\2\23G\3\2\2\2\25I\3\2\2\2\27K"+
		"\3\2\2\2\31{\3\2\2\2\33~\3\2\2\2\35\u008c\3\2\2\2\37\u0091\3\2\2\2!\u00aa"+
		"\3\2\2\2#\u00b3\3\2\2\2%\u00b5\3\2\2\2\'\u00c4\3\2\2\2)*\7*\2\2*\4\3\2"+
		"\2\2+,\7\60\2\2,\6\3\2\2\2-.\7+\2\2.\b\3\2\2\2/\60\7%\2\2\60\61\7v\2\2"+
		"\61\n\3\2\2\2\62\63\7%\2\2\63\64\7h\2\2\64\f\3\2\2\2\65\66\7%\2\2\66\67"+
		"\7k\2\2\678\7i\2\289\7p\2\29:\7q\2\2:;\7t\2\2;<\7g\2\2<\16\3\2\2\2=>\7"+
		"%\2\2>?\7k\2\2?@\7p\2\2@A\7g\2\2AB\7t\2\2BC\7v\2\2C\20\3\2\2\2DE\7*\2"+
		"\2EF\7+\2\2F\22\3\2\2\2GH\7$\2\2H\24\3\2\2\2IJ\7^\2\2J\26\3\2\2\2KL\7"+
		"^\2\2LM\t\2\2\2M\30\3\2\2\2NO\7%\2\2OQ\t\3\2\2PN\3\2\2\2PQ\3\2\2\2QS\3"+
		"\2\2\2RT\t\4\2\2SR\3\2\2\2ST\3\2\2\2TV\3\2\2\2UW\t\5\2\2VU\3\2\2\2WX\3"+
		"\2\2\2XV\3\2\2\2XY\3\2\2\2Y|\3\2\2\2Z[\7%\2\2[\\\7d\2\2\\^\3\2\2\2]_\t"+
		"\4\2\2^]\3\2\2\2^_\3\2\2\2_a\3\2\2\2`b\t\6\2\2a`\3\2\2\2bc\3\2\2\2ca\3"+
		"\2\2\2cd\3\2\2\2d|\3\2\2\2ef\7%\2\2fg\7q\2\2gi\3\2\2\2hj\t\4\2\2ih\3\2"+
		"\2\2ij\3\2\2\2jl\3\2\2\2km\t\7\2\2lk\3\2\2\2mn\3\2\2\2nl\3\2\2\2no\3\2"+
		"\2\2o|\3\2\2\2pq\7%\2\2qr\7z\2\2rt\3\2\2\2su\t\4\2\2ts\3\2\2\2tu\3\2\2"+
		"\2uw\3\2\2\2vx\t\b\2\2wv\3\2\2\2xy\3\2\2\2yw\3\2\2\2yz\3\2\2\2z|\3\2\2"+
		"\2{P\3\2\2\2{Z\3\2\2\2{e\3\2\2\2{p\3\2\2\2|\32\3\2\2\2}\177\t\4\2\2~}"+
		"\3\2\2\2~\177\3\2\2\2\177\u0081\3\2\2\2\u0080\u0082\t\b\2\2\u0081\u0080"+
		"\3\2\2\2\u0082\u0083\3\2\2\2\u0083\u0081\3\2\2\2\u0083\u0084\3\2\2\2\u0084"+
		"\u0085\3\2\2\2\u0085\u0087\7\61\2\2\u0086\u0088\t\b\2\2\u0087\u0086\3"+
		"\2\2\2\u0088\u0089\3\2\2\2\u0089\u0087\3\2\2\2\u0089\u008a\3\2\2\2\u008a"+
		"\34\3\2\2\2\u008b\u008d\t\4\2\2\u008c\u008b\3\2\2\2\u008c\u008d\3\2\2"+
		"\2\u008d\u008e\3\2\2\2\u008e\u008f\5!\21\2\u008f\36\3\2\2\2\u0090\u0092"+
		"\t\4\2\2\u0091\u0090\3\2\2\2\u0091\u0092\3\2\2\2\u0092\u0093\3\2\2\2\u0093"+
		"\u0094\5!\21\2\u0094\u0095\t\4\2\2\u0095\u0096\5!\21\2\u0096\u0097\7k"+
		"\2\2\u0097 \3\2\2\2\u0098\u009a\t\b\2\2\u0099\u0098\3\2\2\2\u009a\u009b"+
		"\3\2\2\2\u009b\u0099\3\2\2\2\u009b\u009c\3\2\2\2\u009c\u009d\3\2\2\2\u009d"+
		"\u00a1\7\60\2\2\u009e\u00a0\t\b\2\2\u009f\u009e\3\2\2\2\u00a0\u00a3\3"+
		"\2\2\2\u00a1\u009f\3\2\2\2\u00a1\u00a2\3\2\2\2\u00a2\u00ab\3\2\2\2\u00a3"+
		"\u00a1\3\2\2\2\u00a4\u00a6\7\60\2\2\u00a5\u00a7\t\b\2\2\u00a6\u00a5\3"+
		"\2\2\2\u00a7\u00a8\3\2\2\2\u00a8\u00a6\3\2\2\2\u00a8\u00a9\3\2\2\2\u00a9"+
		"\u00ab\3\2\2\2\u00aa\u0099\3\2\2\2\u00aa\u00a4\3\2\2\2\u00ab\"\3\2\2\2"+
		"\u00ac\u00b4\t\4\2\2\u00ad\u00af\t\t\2\2\u00ae\u00b0\t\n\2\2\u00af\u00ae"+
		"\3\2\2\2\u00b0\u00b1\3\2\2\2\u00b1\u00af\3\2\2\2\u00b1\u00b2\3\2\2\2\u00b2"+
		"\u00b4\3\2\2\2\u00b3\u00ac\3\2\2\2\u00b3\u00ad\3\2\2\2\u00b4$\3\2\2\2"+
		"\u00b5\u00b9\7=\2\2\u00b6\u00b8\13\2\2\2\u00b7\u00b6\3\2\2\2\u00b8\u00bb"+
		"\3\2\2\2\u00b9\u00ba\3\2\2\2\u00b9\u00b7\3\2\2\2\u00ba\u00bd\3\2\2\2\u00bb"+
		"\u00b9\3\2\2\2\u00bc\u00be\7\17\2\2\u00bd\u00bc\3\2\2\2\u00bd\u00be\3"+
		"\2\2\2\u00be\u00bf\3\2\2\2\u00bf\u00c0\7\f\2\2\u00c0\u00c1\3\2\2\2\u00c1"+
		"\u00c2\b\23\2\2\u00c2&\3\2\2\2\u00c3\u00c5\t\13\2\2\u00c4\u00c3\3\2\2"+
		"\2\u00c5\u00c6\3\2\2\2\u00c6\u00c4\3\2\2\2\u00c6\u00c7\3\2\2\2\u00c7\u00c8"+
		"\3\2\2\2\u00c8\u00c9\b\24\2\2\u00c9(\3\2\2\2\33\2PSX^cinty{~\u0083\u0089"+
		"\u008c\u0091\u009b\u00a1\u00a8\u00aa\u00b1\u00b3\u00b9\u00bd\u00c6\3\b"+
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