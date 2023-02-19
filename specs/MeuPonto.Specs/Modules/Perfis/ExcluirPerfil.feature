# language: pt-br

Funcionalidade: Excluir Perfil
	Para corrigir erros de cadastro
	Enquanto trabalhador
	Eu quero excluir um perfil

Cenário: Trabalhador identifica na lista de perfis um perfil que não era necessário
	Dado que o trabalhador tem um perfil cadastrado com a matrícula '0001'
	E que o trabalhador identifica na lista o perfil cadastrado
	Quando o trabalhador excluir o perfil
	Então o perfil deverá ser excluído
