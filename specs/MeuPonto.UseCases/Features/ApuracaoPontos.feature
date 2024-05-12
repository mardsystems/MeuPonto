# language: pt-br

Funcionalidade: Apuração Pontos

O sistema deverá fornecer para o trabalhador a capacidade de apurar seus pontos.

Regra: Para toda entrada deverá existir uma saída

@wip
Esquema do Cenário: [Apurar Pontos] Trabalhador registra a entrada e a saída do expediente
	Dado que existe um contrato aberto com a seguinte jornada de trabalho semanal prevista:
		| dia semana | tempo    |
		| Sunday     | 00:00:00 |
		| Monday     | 08:00:00 |
		| Tuesday    | 08:00:00 |
		| Wednesday  | 08:00:00 |
		| Thursday   | 08:00:00 |
		| Friday     | 08:00:00 |
		| Saturday   | 00:00:00 |
	E que o trabalhador registrou a entrada no expediente às '<entrada>'
	E que o trabalhador registrou a saída no expediente às '<saída>'
	E que o trabalhador tem uma folha de ponto aberta na competência '2022/11'
	Quando o trabalhador apurar a folha de ponto
	Então o tempo total apurado da folha de ponto deverá ser de '<apurado>'

Exemplos:
	| entrada          | saída            | apurado |
	| 27/11/2022 09:14 | 27/11/2022 11:30 | 02:16   |
	| 27/11/2022 12:27 | 27/11/2022 18:03 | 05:36   |
