/*La extencion .y se refiere al lenguaje YACC
	YACC=Yet Another Compiler of Compilers

La version libre es Bison (GNU)
*/

%{
	#include<stdio.h>
	#include<stdlib.h>
	int yylex(void);//Cabecera

	/*Manejador de errores*/
	void yyerror(char *mensaje){
		printf("ERROR: %s",mensaje);
		exit(0);
	}
%}

%token NUMERO

%%
programa:
;
programa: linea programa
;
linea: '\n'
;			/*Pila*/			
linea: expresion '\n'	{printf("Valor= %d\n",$1);}
;    
expresion: NUMERO	{$$=$1;}
;
expresion: expresion '+' expresion {$$=$1+$3;}
;	
expresion: expresion '-' expresion {$$=$1 - $3;}
;
expresion: expresion '*' expresion {$$=$1 *$3;}
;
expresion: expresion '/' expresion {
	 			if($3!=0){
	 				$$=$1/$3;
				}else{
					printf("Mi loco dele pa fuera\n");
//					exit(0);
				}
				}
;

%%
