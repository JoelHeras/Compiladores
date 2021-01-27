
%{
    #include<stdlib.h>
    /* Automata de pila */
    #include "calc.h" // Incluimos funciones para la tabla de simbolos
    #include "mfcalc.tab.h" // exportamos la declaraciones de bison
%}



%%

[ \t]  {}   // No hacemos nada en espacios y tabulaciones
[0-9]+("."[0-9]+)?                              { yylval.NUM = atof( yytext );return NUM; } // Regresa numeros flotantes
[sin|cos|tan|atan|asin|acos|log|exp|sqrt|abs]   {
                                                    symrec *s;
                                                    s = getsym(yytext);
                                                    if(s == 0)
                                                        s = putsym (yytext, FNCT);
                                                    *((symrec**) &yylval) = s;
                                                    return s->type;                                                    
                                                }
[a-zA-Z][a-zA-Z0-9]*                            {
                                                    symrec *s;
                                                    s = getsym(yytext);
                                                    if(s == 0)
                                                        s = putsym (yytext, VAR);
                                                    *((symrec**) &yylval) = s;
                                                    return s->type;                                                    
                                                }
[-|+|*|/|\n|=|(|)|^]                            { return *yytext; }    // Regresa operador
.                                               { fprintf (stderr, "ERROR:\tCaracter desconocido\n"); }

%%

