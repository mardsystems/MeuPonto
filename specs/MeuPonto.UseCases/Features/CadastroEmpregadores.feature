# language: pt-br

Funcionalidade: Cadastro de Empregadores

Regra: Um contrato pode ser feito com um empregador

@main
Cenário: Trabalhador abre um contrato feito com um novo empregador
	Dado que existe uma abertura de contrato em andamento 'Contrato com Novo Empregador'
	Quando o trabalhador iniciar um cadastro de empregador
	Então um empregador deverá ser criado
	Quando o trabalhador cadastrar o empregador como:
		| nome            |
		| Empregador Novo |
	Então o nome do empregador deverá ser 'Empregador Novo'
	Quando o trabalhador abrir o contrato como:
		| empregador      |
		| Empregador Novo |
	Então o empregador 'Empregador Novo' deverá ser associado ao contrato

@alter
Cenário: Trabalhador abre um contrato feito com um empregador existente
	Dado que existe um empregador cadastrado 'Empregador Existente'
	E que existe uma abertura de contrato em andamento 'Contrato com Empregador Existente'
	Quando o trabalhador abrir o contrato como:
		| empregador           |
		| Empregador Existente |
	Então o contrato deverá ser aberto como esperado
