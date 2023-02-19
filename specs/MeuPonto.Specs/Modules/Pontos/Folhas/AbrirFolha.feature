# language: pt-br

Funcionalidade: Abrir Folha
	Para apurar o tempo total trabalhado
	Enquanto trabalhador
	Eu quero abrir uma folha de ponto
	
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
