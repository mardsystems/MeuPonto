# language: pt-br

Funcionalidade: Editar Perfil
	Para corrigir erros de digitação
	Enquanto trabalhador
	Eu quero editar um perfil

Cenário: Trabalhador edita um perfil para corrigir um erro de digitação na matrícula
	Dado que o trabalhador tem um perfil cadastrado com a matrícula '0001'
	E que o trabalhador identifica na lista o perfil cadastrado
	E que a matrícula do trabalhador é '0002'
	Quando o trabalhador editar o perfil
	Então o perfil deverá ser editado
	E a matrícula do perfil deverá ser '0002'
