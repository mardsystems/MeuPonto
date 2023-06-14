# language: pt-br

Funcionalidade: Detalhar Perfil
	Para verificar os dados digitados
	Enquanto trabalhador
	Eu quero detalhar um perfil

Cenário: Trabalhador detalha um perfil para verificar o nome
	Dado que o trabalhador tem um perfil cadastrado com o nome 'Marcelo - Ateliex'
	E que o trabalhador identifica na lista o perfil cadastrado
	Quando o trabalhador detalhar o perfil
	Então o perfil deverá ser detalhado
	E o nome do perfil deverá ser 'Marcelo - Ateliex'

Cenário: Trabalhador detalha um perfil para verificar a matrícula
	Dado que o trabalhador tem um perfil cadastrado com a matrícula '0001'
	E que o trabalhador identifica na lista o perfil cadastrado
	Quando o trabalhador detalhar o perfil
	Então o perfil deverá ser detalhado
	E a matrícula do perfil deverá ser '0001'
