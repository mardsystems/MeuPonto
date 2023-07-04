# language: pt-br

Funcionalidade: Gestao Folhas
	
Regra: Perfil qualifica a folha de ponto

Cenário: Trabalhador abre uma folha de ponto usando seu único perfil
	Dado que o trabalhador tem um perfil cadastrado com o nome 'Marcelo - Ateliex'
	Dado que o trabalhador qualifica a folha com o perfil 'Marcelo - Ateliex'
	Quando o trabalhador abrir uma folha de ponto
	Então uma folha de ponto deverá ser aberta
	E o perfil da folha de ponto deverá deverá ser 'Marcelo - Ateliex'

Regra: Folha de ponto tem periodicidade mensal

Cenário: Trabalhador abre uma folha ponto para o mês de novembro de 2022
	Dado que o trabalhador tem um perfil cadastrado
	E que o trabalhador deseja apurar a folha de ponto da competência '2022/11'
	Quando o trabalhador abrir uma folha de ponto
	Então uma folha de ponto deverá ser aberta
	E o status da folha de ponto deverá ser 'Aberta'
	E a folha de ponto deverá ter '30' dias
	#TODO: Verificar se o trecho abaixo deve ser verificado aqui ou não.
	Mas a folha de ponto não deverá ter tempo total apurado
	E a folha de ponto não deverá ter tempo total período anterior
	E a folha de ponto não deverá ter uma observação

Regra: Folha de ponto com observação
	
Cenário: Trabalhador abre uma folha de ponto anotando que deve confirmar os feriados do mês
	Dado que o trabalhador tem um perfil cadastrado
	E que o trabalhador anota a seguinte observação na folha de ponto:
		"""
		Verificar se a última sexta-feira do mês vai ser feriado.
		"""
	Quando o trabalhador abrir uma folha de ponto
	Então uma folha de ponto deverá ser aberta
	E a folha de ponto deverá ter uma observação

Regra: Para toda entrada deverá existir uma saída

Esquema do Cenário: Trabalhador registra a entrada e a saída do expediente
	Dado que o trabalhador tem um perfil cadastrado com a seguinte jornada de trabalho semanal prevista:
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

Regra: O fechamento deve ser representado por um status

Cenário: Trabalhador confirma que uma folha de ponto aberta foi fechada
	Dado que o trabalhador tem uma folha de ponto aberta na competência '2022/11'
	E que o ano/mês é '2022/11'
	Quando o trabalhador fechar a folha de ponto
	Então a folha de ponto deverá ser fechada
	E o status da folha de ponto deverá ser 'Fechada'

Regra: Guarda da apuração

Cenário: Trabalhador guarda a apuração mensal dos pontos registrados
	Dado que o trabalhador tem uma folha de ponto aberta na competência '2022/11'
	E que o ano/mês é '2022/11'
	E que os pontos registrados foram:
		| data/hora        | momento |
		| 27/11/2022 09:14 | Entrada |
		| 27/11/2022 11:30 | Saida   |
		| 27/11/2022 12:27 | Entrada |
		| 27/11/2022 18:03 | Saida   |
	Quando o trabalhador fechar a folha de ponto
	Então a folha de ponto deverá ser fechada
	E o tempo total apurado deverá ser '07:52'
	Mas o tempo total período anterior deverá ser nulo
		