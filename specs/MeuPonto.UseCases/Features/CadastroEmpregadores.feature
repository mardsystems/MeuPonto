# language: pt-br

Funcionalidade: Cadastro de Empregadores

Cenário: Trabalhador cadastra empregador
	Dado que existe um contrato criado
	Quando o trabalhador iniciar um cadastro de empregador
	Então um trabalhador deverá ser criado
	E o trabalhador deverá ser associado ao contrato

	Quando o trabalhador cadastrar o empregador como:
		| nome         |
		| Empregador A |
	Então o empregador deverá ser cadastrado como esperado
	E o nome do empregador deverá ser 'Empregador A'
