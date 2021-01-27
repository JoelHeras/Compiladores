%{

	#include "calculadora.tab.h"
%}

%%
[0-9]+	{
		yylval=atoi(yytext);/*yylval sera $n*/
		return NUMERO;
	}

[\n]	return '\n';

[+] 	return '+';

[*]	return '*';

[-]	return '-';

[/]	return '/';

%%



