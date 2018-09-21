grammar Kernel;  
file       : expression+;
expression : atom | pair;

atom : keywords       
     | string 
     | number        
     | symbol     
    ;

symbol: Identifier;

pair: '(' expression (expression)* (star='.' expression)? ')' ;

keywords  : '#t' | '#f'| '#ignore' | '#inert' | '()';

string:  '"' ( EscapeSequence | ~('\\'|'"') )* '"';

EscapeSequence: '\\' ('b'|'t'|'n'|'f'|'r'|'"'|'\''|'\\');

number: Integer | Rational | Real | Complex;



Integer     : ('#'[ied])? [+-]? [0-9]+
            | '#b'        [+-]? [0-1]+
            | '#o'        [+-]? [0-7]+
            | '#x'        [+-]? [0-9a-fA-F]+;

Rational    : [+-]? [0-9a-fA-F]+ '/' [0-9a-fA-F]+;

Real        : [+-]? FloatingNumber;

Complex     : [+-]? FloatingNumber [+-] FloatingNumber 'i';

fragment FloatingNumber: [0-9a-fA-F]+ '.' [0-9a-fA-F]*
                       |              '.' [0-9a-fA-F]+;

Identifier   : '+' 
         | '-'
         | [a-zA-Z!$%&*+\-./:<=>?@^_~] [0-9a-zA-Z!$%&*+\-./:<=>?@^_~]*;
COMMENT  :   ';' .*? ('\r'?'\n') -> skip;
WHITESPACE : [ \t\r\n]+ -> skip;
// RESERVED: [\[\]{}|]+  -> channel(RESERVED);
// ILLEGAL : [’‘,]|',@'  -> channel(ILLEGAL);
