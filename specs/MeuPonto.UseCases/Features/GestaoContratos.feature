# language: pt-br

Funcionalidade: Gestão Contratos

Cenário: Trabalhador abre um contrato para um novo empregador
	Quando o trabalhador iniciar uma abertura de contrato
	Então o sistema deverá sugerir que o contrato está ativo
	E o sistema deverá listar os empregadores
	E o sistema deverá sugerir a seguinte jornada de trabalho semanal prevista em contrato:
		| dia semana | tempo    |
		| Sunday     | 00:00:00 |
		| Monday     | 08:00:00 |
		| Tuesday    | 08:00:00 |
		| Wednesday  | 08:00:00 |
		| Thursday   | 08:00:00 |
		| Friday     | 08:00:00 |
		| Saturday   | 00:00:00 |
	Quando o trabalhador informar que o nome do contrato é 'Contrato A'
	E o trabalhador informar que o contrato está ativo
	# Novo Empregador - Início
	E o trabahador cadastrar um novo empregador com o nome 'Empregador A'
	E o trabalhador informar que o contrato foi feito com o 'Empregador A'
	Então o sistema deverá listar os empregadores
	E o sistema deverá selecionar o empregador 'Empregador A'
	# Novo Empregador - Fim
	Quando o trabalhador informar que a jornada de trabalho semanal prevista no contrato é:
		| dia semana | tempo    |
		| Sunday     | 00:00:00 |
		| Monday     | 08:00:00 |
		| Tuesday    | 08:00:00 |
		| Wednesday  | 08:00:00 |
		| Thursday   | 08:00:00 |
		| Friday     | 08:00:00 |
		| Saturday   | 00:00:00 |
	E o trabalhador salvar a abertura de contrato
	Então o sistema deverá criar um contrato
	E o nome do contrato deverá ser 'Contrato A'
	E o contrato deverá ser ativo
	E o empregador do contrato deverá ser 'Empregador A'
	E a jornada de trabalho semanal prevista no contrato deverá ser:
		| dia semana | tempo    |
		| Sunday     | 00:00:00 |
		| Monday     | 08:00:00 |
		| Tuesday    | 08:00:00 |
		| Wednesday  | 08:00:00 |
		| Thursday   | 08:00:00 |
		| Friday     | 08:00:00 |
		| Saturday   | 00:00:00 |

Cenário: Trabalhador abre um contrato para um empregador já cadastrado
	Dado que existe um empregador cadastrado com o nome 'Empregador A'
	Quando o trabalhador iniciar uma abertura de contrato
	Então o sistema deverá listar os empregadores
	Quando o trabalhador informar que o nome do contrato é 'Contrato A'
	E o trabalhador informar que o contrato está ativo
	E o trabalhador informar que o contrato foi feito com o 'Empregador A'
	E o trabalhador informar que a jornada de trabalho semanal prevista no contrato é:
		| dia semana | tempo    |
		| Sunday     | 00:00:00 |
		| Monday     | 08:00:00 |
		| Tuesday    | 08:00:00 |
		| Wednesday  | 08:00:00 |
		| Thursday   | 08:00:00 |
		| Friday     | 08:00:00 |
		| Saturday   | 00:00:00 |
	E o trabalhador salvar a abertura de contrato
	Então o sistema deverá criar um contrato
	E o nome do contrato deverá ser 'Contrato A'
	E o contrato deverá ser ativo
	E o empregador do contrato deverá ser 'Empregador A'
	E a jornada de trabalho semanal prevista no contrato deverá ser:
		| dia semana | tempo    |
		| Sunday     | 00:00:00 |
		| Monday     | 08:00:00 |
		| Tuesday    | 08:00:00 |
		| Wednesday  | 08:00:00 |
		| Thursday   | 08:00:00 |
		| Friday     | 08:00:00 |
		| Saturday   | 00:00:00 |

Regra: Deve ser possível excluir um contrato
	
Cenário: Trabalhador exclui um contrato que não era necessário
	Dado que existe um contrato aberto com o nome 'Marcelo - Ateliex'
	E que o trabalhador identifica na lista o contrato cadastrado
	Quando o trabalhador excluir o contrato
	Então o sistema deverá excluir o contrato

#Regra: Se existir dados relacionados a um contrato então ele não pode ser excluído

@wip
Cenário: Trabalhador tenta excluir um contrato com ponto(s) registrado(s)
	Dado que existe um contrato aberto com o nome 'Marcelo - Ateliex'
	E que existe um ponto qualificado com o contrato 'Marcelo - Ateliex'
	E que o trabalhador identifica na lista o contrato cadastrado
	Quando o trabalhador tentar excluir o contrato
	Então o sistema deverá informar um mensagem ''
