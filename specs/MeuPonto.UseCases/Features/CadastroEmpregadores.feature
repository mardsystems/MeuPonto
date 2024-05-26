# language: pt-br

Funcionalidade: Cadastro de Empregadores

O sistema deverá fornecer para o trabalhador a capacidade de cadastrar um empregador

O sistema deverá fornecer para o trabalhador a capacidade de abrir um contrato feito com um empregador novo

O sistema deverá fornecer para o trabalhador a capacidade de abrir um contrato feito com um empregador existente

Regra: Um empregador pode ser cadastrado

@main
Cenário: [Cadastrar Empregador] Trabalhador cadastra um empregador
	Quando o trabalhador solicitar o cadastro de um empregador
	Então o sistema deverá apresentar um empregador novo
	Quando o trabalhador cadastrar o empregador como:
		| nome         |
		| Empregador A |
	Então o sistema deverá registrar o empregador como esperado

Regra: Um contrato pode ser feito com um empregador novo

@extension
Cenário: [Abrir Contrato com Empregador Novo] Trabalhador abre um contrato feito com um empregador novo
	Dado que existe uma abertura de contrato em andamento
	Quando o trabalhador solicitar o cadastro de um empregador
	Então o sistema deverá apresentar um empregador novo
	Quando o trabalhador cadastrar o empregador como:
		| nome            |
		| Empregador Novo |
	#Então o sistema deverá registrar o empregador como esperado
	Quando o trabalhador abrir o contrato feito com um empregador como:
		| empregador      |
		| Empregador Novo |
	Então o sistema deverá registrar o contrato como esperado
	#Então o empregador 'Empregador Novo' deverá ser associado ao contrato

Regra: Um contrato pode ser feito com um empregador existente

@extension
Cenário: [Abrir Contrato com Empregador Existente] Trabalhador abre um contrato feito com um empregador existente
	Dado que existe um empregador cadastrado 'Empregador Existente'
	E que existe uma abertura de contrato em andamento
	Quando o trabalhador abrir o contrato feito com um empregador como:
		| empregador           |
		| Empregador Existente |
	Então o sistema deverá registrar o contrato como esperado
	#Então o empregador 'Empregador Existente' deverá ser associado ao contrato

Regra: Um contrato pode ser aberto a partir de um empregador

@main @wip
Cenário: [Abrir Contrato] Trabalhador abre um contrato a partir de um empregador
	Dado que existe um empregador cadastrado 'Empregador Existente'
	Quando o trabalhador solicitar a abertura de um contrato a partir de um empregador
	Então o sistema deverá apresentar um contrato novo
	#E o contrato deverá ser feito com o empregador 'Empregador Existente'
	Quando o trabalhador abrir o contrato como:
		| empregador           |
		| Empregador Existente |
	Então o sistema deverá registrar o contrato como esperado
	E o contrato deverá ser feito com o empregador 'Empregador Existente'

Regra: O nome do empregador deve ter pelo menos 3 caracteres

@secondary
Cenário: [Cadastrar Empregador] Trabalhador cadastra um empregador com nome maior que 2 caractere
	Dado que existe um cadastro de empregador em andamento
	Quando o trabalhador cadastrar o empregador como:
		| nome         |
		| Empregador B |
	Então o sistema deverá registrar o empregador como esperado
