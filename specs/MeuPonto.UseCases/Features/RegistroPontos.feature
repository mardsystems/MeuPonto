# language: pt-br

Funcionalidade: Registro Pontos

Regra: Contrato qualifica o ponto

Cenário: Trabalhador marca o ponto usando seu único contrato
	Dado que existe um contrato aberto 'Marcelo - Ateliex'
	E que o trabalhador qualifica o ponto com o contrato 'Marcelo - Ateliex'
	Quando o trabalhador marcar o ponto
	Então o ponto deverá ser marcado
	E o contrato do ponto deverá deverá ser 'Marcelo - Ateliex'
	
Regra: Registro do momento de entrada e saída do expediente

Cenário: Trabalhador marca o ponto de entrada do expediente
	Dado que a data/hora do relógio é '27/11/2022 09:14'
	E que é o momento de 'Entrada' do expediente
	Quando o trabalhador marcar o ponto
	Então o ponto deverá ser marcado
	E a data do ponto deverá ser '27/11/2022 09:14'
	E o momento do ponto deverá ser de 'Entrada'
	#TODO: Verificar se o trecho abaixo deve ser verificado aqui ou não.
	E o ponto deverá indicar que não é almoço
	E o ponto deverá indicar que não foi estimado
	Mas o ponto não deverá ter uma observação

Cenário: Trabalhador marca o ponto de saída do expediente
	Dado que a data/hora do relógio é '27/11/2022 18:05'
	E que é o momento de 'Saida' do expediente
	Quando o trabalhador marcar o ponto
	Então o ponto deverá ser marcado
	E a data do ponto deverá ser '27/11/2022 18:05'
	E o momento do ponto deverá ser de 'Saida'
	#TODO: Verificar se o trecho abaixo deve ser verificado aqui ou não.
	E o ponto deverá indicar que não é almoço
	E o ponto deverá indicar que não foi estimado
	Mas o ponto não deverá ter uma observação

Regra: Indicação de saída e retorno do almoço

Cenário: Trabalhador marca o ponto de saída do expediente para o almoço
	Dado que é o momento de 'Saida' do expediente para o almoço
	Quando o trabalhador marcar o ponto
	Então o ponto deverá ser marcado
	E o momento do ponto deverá ser de 'Saida'
	E o ponto deverá indicar que é almoço

Cenário: Trabalhador marca o ponto de entrada do expediente da volta do almoço
	Dado que é o momento de 'Entrada' do expediente da volta do almoço
	Quando o trabalhador marcar o ponto
	Então o ponto deverá ser marcado
	E o momento do ponto deverá ser de 'Entrada'
	E o ponto deverá indicar que é almoço

Regra: Ponto com observação
	
Cenário: Trabalhador marca o ponto justificando porque chegou atrasado
	Dado que o trabalhador anota a seguinte observação no ponto:
		"""
		Hoje o trânsito estava lento.
		"""
	Quando o trabalhador marcar o ponto
	Então o ponto deverá ser marcado
	E o ponto deverá ter uma observação
