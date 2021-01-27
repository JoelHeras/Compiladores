%{
	/*Para generar un códgio en flex es necesario definir 3 
	  espacios o areas para que flex los identifique y 
	  trabaje con base en ellas.*/
	/*Primer sección: Sección de definición,definiciones del 
	  lenguaje C y definiciones regulares(Conjuntos)
	  Segunda seccion:Reglas para definir una expresión regular*/
	/*Un analizador lexico se define o se especifica 
	  usando expresiones regulares.
	  Expresión regular es una notación simplificada 
	  para lenguajes regulares: conjuntos finitos y operaciones.
	  OPeraciones: Union, concatenacion y la ceradura*/

	/*Código de C*/
	
	#include<stdio.h>
%}
digito [0-9] 
vocales [aeiou]
espacios [ \n]

%%
x	{ printf("Acabo de ver una x\n");}
int	{ printf("Acabo de ver una palabra reservada int\n");}
char 	{ printf("Acabo de ver una palabra reservada\n");}
x|y|z	{ printf("Acabo de ver una letra del final del alfabeto\n");}
{digito}+	{ printf("Acabo de ver un digito\n");}


%%

