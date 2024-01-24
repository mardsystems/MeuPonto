# language: pt-br

Funcionalidade: Cadastro de Empregadores

Cenário: x
	Quando o trabalhador iniciar o cadastro de empregador
	Então o sistema deverá carregar o cadastro de empregador
	Quando o trabalhador informar que o nome do empregador é 'Empregador A'
	E o trabalhador salvar o cadastro do empregador
	Então o sistema deverá listar os empregadores
	E o sistema deverá selecionar o empregador 'Empregador A'


