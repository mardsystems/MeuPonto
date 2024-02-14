# language: pt-br

Funcionalidade: Cadastro de Empregadores

Cenário: Trabalhador cadastra empregador
	#Dado que existe um contrato criado
	Quando o trabalhador iniciar um cadastro de empregador
	Então um empregador deverá ser criado
	#E o empregador deverá ser associado ao contrato

	Quando o trabalhador cadastrar o empregador como:
		| nome         |
		| Empregador A |
	Então o nome do empregador deverá ser 'Empregador A'
