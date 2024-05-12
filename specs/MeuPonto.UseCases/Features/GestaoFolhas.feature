# language: pt-br

Funcionalidade: Gestão Folhas

O sistema deverá fornecer para o trabalhador a capacidade de gerenciar suas folhas.
	
Regra: Contrato qualifica a folha de ponto

@wip
Cenário: [Abrir Folha] Trabalhador abre uma folha de ponto usando seu único contrato
	Dado que existe um contrato aberto 'Marcelo - Ateliex'
	E que o trabalhador qualifica a folha com o contrato 'Marcelo - Ateliex'
	Quando o trabalhador abrir uma folha de ponto
	Então uma folha de ponto deverá ser aberta
	E o contrato da folha de ponto deverá deverá ser 'Marcelo - Ateliex'

Regra: Folha de ponto tem periodicidade mensal

@wip
Cenário: [Abrir Folha] Trabalhador abre uma folha ponto para o mês de novembro de 2022
	Dado que existe um contrato aberto
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

@wip
Cenário: [Abrir Folha] Trabalhador abre uma folha de ponto anotando que deve confirmar os feriados do mês
	Dado que existe um contrato aberto
	E que o trabalhador anota a seguinte observação na folha de ponto:
		"""
		Verificar se a última sexta-feira do mês vai ser feriado.
		"""
	Quando o trabalhador abrir uma folha de ponto
	Então uma folha de ponto deverá ser aberta
	E a folha de ponto deverá ter uma observação

Regra: O fechamento deve ser representado por um status

@wip
Cenário: [Fechar Folha] Trabalhador confirma que uma folha de ponto aberta foi fechada
	Dado que o trabalhador tem uma folha de ponto aberta na competência '2022/11'
	E que o ano/mês é '2022/11'
	Quando o trabalhador fechar a folha de ponto
	Então a folha de ponto deverá ser fechada
	E o status da folha de ponto deverá ser 'Fechada'

Regra: Guarda da apuração

@wip
Cenário: [Fechar Folha] Trabalhador guarda a apuração mensal dos pontos registrados
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
		