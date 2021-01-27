%{
	
	#include<stdio.h>
%}
digito [0-9] 
letras [a-zA-Z]
espacios [ ]
salto [\n]
separador [,]
operadores [*+-/]
%%
'{letras}*{digito}*'	{ printf("<Expresion literal>");}
{digito}*	{printf("<Digito>");}
'yyyy-mm-dd'|'hh:mm:ss'|'yyyy-mm-dd_hh:mm:ss'|'{digito}{4}-{digito}{2}-{digito}{2}'|'{digito}{2}:{digito}{2}:{digito}{2}'|'{digito}{4}-{digito}{2}-{digito}{2}_{digito}{2}:{digito}{2}:{digito}{2}'	{ printf("<Expresion literal de Fecha>");}

{salto} 	{printf("\n");}
INT8|BIGINT|BINARY|CHAR(ACTER)?|NUMERIC|DEC|DECIMAL|FLOAT8|DOUBLEDATE|DATETIME|DAY_HOUR|DAY_MICROSECOND|DAY_MINUTE|DAY_SECOND|INT4?|INTEGER|INT2|SMALLINT|INT1|TINYINT|YEAR|YEAR_MONTH|VARBINARY|VARCHAR|FLOAT|INT|BIT|VARCHAR({digito}*)	{printf("<Tipo de dato>");}
SELECT|USE|UPDATE|CREATE_DATABASE|CREATE_TABLE|DELETE_DATABASE|DELETE_TABLE|DROP_DATABASE|CREATE_DATABASES|DELETE_DATABASES|DROP_DATABASES|DROP_TABLE|ALTER|INSERT_INTO	{printf("<Sentencia>");}
CASE	{printf("<Expresion condicional>");}
WHEN|WHILE 	{printf("<Operador>");}
AND|OR|NOT|{operadores}	{printf("<Operador logico>");}
WHERE|FROM|GROUP_BY|ORDER_BY|OFFSET|FETCH|VALUES|COLUMN|JOIN|ALL|TABLE|DATABASE	{printf("<Clausula>");}
BETWEEN|IN|IS_NULL|LIKE	{printf("<Operador logico>");}
AUTO_INCREMENT|CONSTRAINT|NOT_NULL|UNIQUE|CHECK|UNSIGNED	{printf("<Caracteristica de dato>");}
CURRENT_DATE|CURRENT_TIME|CURRENT_TIMESTAMP|CURRENT_USER|ADD|ALL|ALTER|ANALYZE	{printf("<Funcion>");}
TIMESTAMP	{printf("<Funcion de tiempo>");}
FOREIGN_KEY|PRIMARY_KEY	{printf("<Llave>");}

%%

