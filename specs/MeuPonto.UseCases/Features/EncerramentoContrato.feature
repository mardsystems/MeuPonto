# language: pt-br

Funcionalidade: Encerramento Contrato

O sistema deverá fornecer para o trabalhador a capacidade de encerrar um contrato

Regra: Um contrato pode ser encerrado

@main @wip
Cenário: [Encerrar Contrato] Trabalhador encerra um contrato
	Dado que existe um contrato aberto 'Contrato A'
	Quando o trabalhador solicitar o encerramento do contrato 'Contrato A'
	E o trabalhador encerrar o contrato 'Contrato A'
	Então o contrato deverá ser encerrado
