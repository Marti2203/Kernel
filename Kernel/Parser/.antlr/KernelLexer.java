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
		Identifier=16, COMMENT=17, WHITESPACE=18, ILLEGAL=19;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "EscapeSequence", "Integer", "Rational", "Real", "Complex", "FloatingNumber", 
		"Identifier", "COMMENT", "WHITESPACE", "ILLEGAL"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'('", "'.'", "')'", "'#t'", "'#f'", "'#ignore'", "'#inert'", "'()'", 
		"'\"'", "'\\'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, null, null, null, null, null, null, null, null, null, null, "EscapeSequence", 
		"Integer", "Rational", "Real", "Complex", "Identifier", "COMMENT", "WHITESPACE", 
		"ILLEGAL"
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\25\u00d2\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\4\25\t\25\3\2\3\2\3\3\3\3\3\4\3\4\3\5\3\5\3"+
		"\5\3\6\3\6\3\6\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\b\3\b\3\b\3\b\3\b\3\b"+
		"\3\b\3\t\3\t\3\t\3\n\3\n\3\13\3\13\3\f\3\f\3\f\3\r\3\r\5\rS\n\r\3\r\5"+
		"\rV\n\r\3\r\6\rY\n\r\r\r\16\rZ\3\r\3\r\3\r\3\r\5\ra\n\r\3\r\6\rd\n\r\r"+
		"\r\16\re\3\r\3\r\3\r\3\r\5\rl\n\r\3\r\6\ro\n\r\r\r\16\rp\3\r\3\r\3\r\3"+
		"\r\5\rw\n\r\3\r\6\rz\n\r\r\r\16\r{\5\r~\n\r\3\16\5\16\u0081\n\16\3\16"+
		"\6\16\u0084\n\16\r\16\16\16\u0085\3\16\3\16\6\16\u008a\n\16\r\16\16\16"+
		"\u008b\3\17\5\17\u008f\n\17\3\17\3\17\3\20\5\20\u0094\n\20\3\20\3\20\3"+
		"\20\3\20\3\20\3\21\6\21\u009c\n\21\r\21\16\21\u009d\3\21\3\21\7\21\u00a2"+
		"\n\21\f\21\16\21\u00a5\13\21\3\21\3\21\6\21\u00a9\n\21\r\21\16\21\u00aa"+
		"\5\21\u00ad\n\21\3\22\3\22\3\22\7\22\u00b2\n\22\f\22\16\22\u00b5\13\22"+
		"\5\22\u00b7\n\22\3\23\3\23\7\23\u00bb\n\23\f\23\16\23\u00be\13\23\3\23"+
		"\5\23\u00c1\n\23\3\23\3\23\3\23\3\23\3\24\6\24\u00c8\n\24\r\24\16\24\u00c9"+
		"\3\24\3\24\3\25\3\25\3\25\5\25\u00d1\n\25\3\u00bc\2\26\3\3\5\4\7\5\t\6"+
		"\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17\35\20\37\21!\2#\22%\23"+
		"\'\24)\25\3\2\r\n\2$$))^^ddhhppttvv\4\2fgkk\4\2--//\3\2\62;\3\2\62\63"+
		"\3\2\629\5\2\62;CHch\13\2##&(,-/\61<<>\\`ac|\u0080\u0080\n\2##&(,-/<>"+
		"\\`ac|\u0080\u0080\5\2\13\f\17\17\"\"\4\2..\u201a\u201b\2\u00eb\2\3\3"+
		"\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2"+
		"\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3"+
		"\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2#\3\2\2\2\2%\3\2\2\2\2"+
		"\'\3\2\2\2\2)\3\2\2\2\3+\3\2\2\2\5-\3\2\2\2\7/\3\2\2\2\t\61\3\2\2\2\13"+
		"\64\3\2\2\2\r\67\3\2\2\2\17?\3\2\2\2\21F\3\2\2\2\23I\3\2\2\2\25K\3\2\2"+
		"\2\27M\3\2\2\2\31}\3\2\2\2\33\u0080\3\2\2\2\35\u008e\3\2\2\2\37\u0093"+
		"\3\2\2\2!\u00ac\3\2\2\2#\u00b6\3\2\2\2%\u00b8\3\2\2\2\'\u00c7\3\2\2\2"+
		")\u00d0\3\2\2\2+,\7*\2\2,\4\3\2\2\2-.\7\60\2\2.\6\3\2\2\2/\60\7+\2\2\60"+
		"\b\3\2\2\2\61\62\7%\2\2\62\63\7v\2\2\63\n\3\2\2\2\64\65\7%\2\2\65\66\7"+
		"h\2\2\66\f\3\2\2\2\678\7%\2\289\7k\2\29:\7i\2\2:;\7p\2\2;<\7q\2\2<=\7"+
		"t\2\2=>\7g\2\2>\16\3\2\2\2?@\7%\2\2@A\7k\2\2AB\7p\2\2BC\7g\2\2CD\7t\2"+
		"\2DE\7v\2\2E\20\3\2\2\2FG\7*\2\2GH\7+\2\2H\22\3\2\2\2IJ\7$\2\2J\24\3\2"+
		"\2\2KL\7^\2\2L\26\3\2\2\2MN\7^\2\2NO\t\2\2\2O\30\3\2\2\2PQ\7%\2\2QS\t"+
		"\3\2\2RP\3\2\2\2RS\3\2\2\2SU\3\2\2\2TV\t\4\2\2UT\3\2\2\2UV\3\2\2\2VX\3"+
		"\2\2\2WY\t\5\2\2XW\3\2\2\2YZ\3\2\2\2ZX\3\2\2\2Z[\3\2\2\2[~\3\2\2\2\\]"+
		"\7%\2\2]^\7d\2\2^`\3\2\2\2_a\t\4\2\2`_\3\2\2\2`a\3\2\2\2ac\3\2\2\2bd\t"+
		"\6\2\2cb\3\2\2\2de\3\2\2\2ec\3\2\2\2ef\3\2\2\2f~\3\2\2\2gh\7%\2\2hi\7"+
		"q\2\2ik\3\2\2\2jl\t\4\2\2kj\3\2\2\2kl\3\2\2\2ln\3\2\2\2mo\t\7\2\2nm\3"+
		"\2\2\2op\3\2\2\2pn\3\2\2\2pq\3\2\2\2q~\3\2\2\2rs\7%\2\2st\7z\2\2tv\3\2"+
		"\2\2uw\t\4\2\2vu\3\2\2\2vw\3\2\2\2wy\3\2\2\2xz\t\b\2\2yx\3\2\2\2z{\3\2"+
		"\2\2{y\3\2\2\2{|\3\2\2\2|~\3\2\2\2}R\3\2\2\2}\\\3\2\2\2}g\3\2\2\2}r\3"+
		"\2\2\2~\32\3\2\2\2\177\u0081\t\4\2\2\u0080\177\3\2\2\2\u0080\u0081\3\2"+
		"\2\2\u0081\u0083\3\2\2\2\u0082\u0084\t\b\2\2\u0083\u0082\3\2\2\2\u0084"+
		"\u0085\3\2\2\2\u0085\u0083\3\2\2\2\u0085\u0086\3\2\2\2\u0086\u0087\3\2"+
		"\2\2\u0087\u0089\7\61\2\2\u0088\u008a\t\b\2\2\u0089\u0088\3\2\2\2\u008a"+
		"\u008b\3\2\2\2\u008b\u0089\3\2\2\2\u008b\u008c\3\2\2\2\u008c\34\3\2\2"+
		"\2\u008d\u008f\t\4\2\2\u008e\u008d\3\2\2\2\u008e\u008f\3\2\2\2\u008f\u0090"+
		"\3\2\2\2\u0090\u0091\5!\21\2\u0091\36\3\2\2\2\u0092\u0094\t\4\2\2\u0093"+
		"\u0092\3\2\2\2\u0093\u0094\3\2\2\2\u0094\u0095\3\2\2\2\u0095\u0096\5!"+
		"\21\2\u0096\u0097\t\4\2\2\u0097\u0098\5!\21\2\u0098\u0099\7k\2\2\u0099"+
		" \3\2\2\2\u009a\u009c\t\b\2\2\u009b\u009a\3\2\2\2\u009c\u009d\3\2\2\2"+
		"\u009d\u009b\3\2\2\2\u009d\u009e\3\2\2\2\u009e\u009f\3\2\2\2\u009f\u00a3"+
		"\7\60\2\2\u00a0\u00a2\t\b\2\2\u00a1\u00a0\3\2\2\2\u00a2\u00a5\3\2\2\2"+
		"\u00a3\u00a1\3\2\2\2\u00a3\u00a4\3\2\2\2\u00a4\u00ad\3\2\2\2\u00a5\u00a3"+
		"\3\2\2\2\u00a6\u00a8\7\60\2\2\u00a7\u00a9\t\b\2\2\u00a8\u00a7\3\2\2\2"+
		"\u00a9\u00aa\3\2\2\2\u00aa\u00a8\3\2\2\2\u00aa\u00ab\3\2\2\2\u00ab\u00ad"+
		"\3\2\2\2\u00ac\u009b\3\2\2\2\u00ac\u00a6\3\2\2\2\u00ad\"\3\2\2\2\u00ae"+
		"\u00b7\t\4\2\2\u00af\u00b3\t\t\2\2\u00b0\u00b2\t\n\2\2\u00b1\u00b0\3\2"+
		"\2\2\u00b2\u00b5\3\2\2\2\u00b3\u00b1\3\2\2\2\u00b3\u00b4\3\2\2\2\u00b4"+
		"\u00b7\3\2\2\2\u00b5\u00b3\3\2\2\2\u00b6\u00ae\3\2\2\2\u00b6\u00af\3\2"+
		"\2\2\u00b7$\3\2\2\2\u00b8\u00bc\7=\2\2\u00b9\u00bb\13\2\2\2\u00ba\u00b9"+
		"\3\2\2\2\u00bb\u00be\3\2\2\2\u00bc\u00bd\3\2\2\2\u00bc\u00ba\3\2\2\2\u00bd"+
		"\u00c0\3\2\2\2\u00be\u00bc\3\2\2\2\u00bf\u00c1\7\17\2\2\u00c0\u00bf\3"+
		"\2\2\2\u00c0\u00c1\3\2\2\2\u00c1\u00c2\3\2\2\2\u00c2\u00c3\7\f\2\2\u00c3"+
		"\u00c4\3\2\2\2\u00c4\u00c5\b\23\2\2\u00c5&\3\2\2\2\u00c6\u00c8\t\13\2"+
		"\2\u00c7\u00c6\3\2\2\2\u00c8\u00c9\3\2\2\2\u00c9\u00c7\3\2\2\2\u00c9\u00ca"+
		"\3\2\2\2\u00ca\u00cb\3\2\2\2\u00cb\u00cc\b\24\2\2\u00cc(\3\2\2\2\u00cd"+
		"\u00d1\t\f\2\2\u00ce\u00cf\7.\2\2\u00cf\u00d1\7B\2\2\u00d0\u00cd\3\2\2"+
		"\2\u00d0\u00ce\3\2\2\2\u00d1*\3\2\2\2\34\2RUZ`ekpv{}\u0080\u0085\u008b"+
		"\u008e\u0093\u009d\u00a3\u00aa\u00ac\u00b3\u00b6\u00bc\u00c0\u00c9\u00d0"+
		"\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}