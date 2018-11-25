grammar Kernel;
@lexer::members {
 int @base = 10;
}
file: expression+ EOF;
expressionLine: expression EOF;
expression: atom | pair;

atom:
    KEYWORD     # Keyword
    | STRING    # String
    | symbol    # SymbolLiteral
    | number    # NumberLiteral;
pair: '(' expression+ (dot = '.' expression)? ')';

KEYWORD:
    '#' [tT]
    | '#' [fF]
    | '#' [Ii][Gg][Nn][Oo][Rr][Ee]
    | '#' [Ii][Nn][Ee][Rr][Tt]
    | '(' ')';

STRING: '"' (EscapeSequence | ~('\\' | '"'))* '"';

fragment EscapeSequence:
    '\\' [btnfrBTNFR"\\]
    | UnicodeEscape
    | OctalEscape;

fragment OctalEscape:
    '\\' [0-3] [0-7] [0-7]
    | '\\' [0-7] [0-7]
    | '\\' [0-7];

fragment UnicodeEscape:
    '\\' 'u' DigitHex DigitHex DigitHex DigitHex;

number: numBin | numOct | numDec | numHex;

numBin: PrefixBin complexBin;
numOct: PrefixOct complexOct;
numDec: (PrefixDec)? complexDec;
numHex: PrefixHex complexHex;

complexBin:
    realBin '@' realBin                     # BinaryPolar
    | realBin sign = ('+' | '-') imagBin    # BinaryComplex
    | sign = ('+' | '-')? urealBin          # BinaryNumber
    | sign = ('+' | '-') imagBin            # BinaryComplex;

complexOct:
    realOct '@' realOct                     # OctalPolar
    | realOct sign = ('+' | '-') imagOct    # OctalComplex
    | sign = ('+' | '-')? urealOct          # OctalNumber
    | sign = ('+' | '-') imagOct            # OctalComplex;

complexDec:
    realDec '@' realDec                     # DecimalPolar
    | realDec sign = ('+' | '-') imagDec    # DecimalComplex
    | sign = ('+' | '-')? urealDec          # DecimalNumber
    | sign = ('+' | '-') imagDec            # DecimalComplex;

complexHex:
    realHex '@' realHex                     # HexadecimalPolar
    | realHex sign = ('+' | '-') imagHex    # HexadecimalComplex
    | sign = ('+' | '-')? urealHex          # HexadecimalNumber
    | sign = ('+' | '-') imagHex            # HexadecimalComplex;

realBin: Sign? urealBin;
realOct: Sign? urealOct;
realDec: Sign? urealDec;
realHex: Sign? urealHex;

imagBin: urealBin? ('i' | 'I');
imagOct: urealOct? ('i' | 'I');
imagDec: urealDec? ('i' | 'I');
imagHex: urealHex? ('i' | 'I');
urealBin:
    UintegerBin '/' UintegerBin # BinaryRational
    | UintegerBin               # BinaryInteger;
urealOct:
    UintegerOct '/' UintegerOct # OctalRational
    | UintegerOct               # OctalInteger;
urealDec:
    UintegerDec '/' UintegerDec # DecimalRational
    | UintegerDec               # DecimalInteger
    | Decimal                   # DecimalReal;
urealHex:
    UintegerHex '/' UintegerHex # HexadecimalRational
    | UintegerHex               # HexadecimalInteger;
UintegerBin: DigitBin+ '#'*;
UintegerOct: DigitOct+ '#'*;
UintegerDec: DigitDec+ '#'*;
UintegerHex: DigitHex+ '#'*;

PrefixBin: (RadixBin Exactness? | Exactness RadixBin) {@base=2;};
PrefixOct: (RadixOct Exactness? | Exactness RadixOct) {@base=8;};
PrefixDec: (RadixDec Exactness? | Exactness RadixDec?) {@base=10;};
PrefixHex: (RadixHex Exactness? | Exactness RadixHex) {@base=16;};

Decimal:
    UintegerDec Suffix?
    | (DigitDec? '.' DigitDec* '#'* Suffix?)
    | (DigitDec+ '#'+ '.' '#'* Suffix?);

Sign: '+' | '-';
Suffix: ExponentMarker Sign? DigitDec+;
fragment Exactness: '#' [iI] | '#' [eE];
fragment ExponentMarker: [esfdlESFDL];

fragment DigitBin: [0-1]{@base == 2}?;
fragment DigitOct: [0-7]{@base == 8}?;
fragment DigitDec: [0-9]{@base == 10}?;
fragment DigitHex: [0-9a-fA-F]{@base ==16}?;

fragment RadixBin: '#' [bB];
fragment RadixOct: '#' [oO];
fragment RadixDec: '#' [dD];
fragment RadixHex: '#' [xX];

symbol: '+' | '-' | '/' | SYMBOL;
SYMBOL: [a-zA-Z!$%&*.:<=>?^_~] [a-zA-Z!$#%&+*\-./:<=>?@^_~0-9]*;
WS: [ \t\r\n]+ -> skip;