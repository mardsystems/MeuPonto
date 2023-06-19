# language: pt-br

Funcionalidade: Fechar Folha
	Para guardar a apuração mensal
	E não permitir futuras alterações do ponto
	Enquanto trabalhador
	Eu quero fechar a folha de ponto

Contexto:
	Dado que o trabalhador tem uma folha de ponto aberta na competência '2022/11'
	E que o ano/mês é '2022/11'

Regra: O fechamento deve ser representado por um status

Cenário: Trabalhador confirma que uma folha de ponto aberta foi fechada
	Quando o trabalhador fechar a folha de ponto
	Então a folha de ponto deverá ser fechada
	E o status da folha de ponto deverá ser 'Fechada'

Regra: Guarda da apuração

Cenário: Trabalhador guarda a apuração mensal dos pontos registrados
	Dado que os pontos registrados foram:
		| data/hora        | momento |
		| 27/11/2022 09:14 | Entrada |
		| 27/11/2022 11:30 | Saida   |
		| 27/11/2022 12:27 | Entrada |
		| 27/11/2022 18:03 | Saida   |
	Quando o trabalhador fechar a folha de ponto
	Então a folha de ponto deverá ser fechada
	E o tempo total apurado deverá ser '07:52'
	Mas o tempo total período anterior deverá ser nulo
		