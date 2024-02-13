# language: pt-br

Funcionalidade: Gestão Contratos

Caso de Uso: Abrir Contrato

Cenário: Trabalhador abre um contrato para um novo empregador
	Quando o trabalhador iniciar uma abertura de contrato
	Então um contrato deverá ser criado

	Quando o trabahador cadastrar o empregador 'Empregador A'
	E o trabalhador abrir o contrato como:
		| nome       | ativo | empregador   | domingo  | segunda  | terça    | quarta   | quinta   | sexta    | sábado   |
		| Contrato A | True  | Empregador A | 00:00:00 | 08:00:00 | 08:00:00 | 08:00:00 | 08:00:00 | 08:00:00 | 00:00:00 |
	Então o nome do contrato deverá ser 'Contrato A'
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
	
Cenário: Trabalhador altera um contrato para corrigir um erro de digitação no nome
	Dado que o trabalhador tem um contrato cadastrado com o nome 'Marcello'
	E que o trabalhador identifica na lista o contrato cadastrado
	E que o nome do trabalhador é 'Marcelo'
	Quando o trabalhador editar o contrato
	Então o contrato deverá ser editado
	E o nome do contrato deverá ser 'Marcelo'

Regra: Um novo contrato deve ser ativo

Cenário: Trabalhador inicia um novo contrato ativo
	Quando o trabalhador iniciar uma abertura de contrato
	Então um contrato deverá ser criado
	E o contrato deverá ser ativo

Regra: Um novo contrato deve ter uma jornada de trabalho prevista

Cenário: Trabalhador inicia um novo contrato com uma jornada de trabalho prevista
	Quando o trabalhador iniciar uma abertura de contrato
	Então um contrato deverá ser criado
	E o contrato deverá prever a seguinte jornada de trabalho semanal:
		| dia semana | tempo    |
		| Sunday     | 00:00:00 |
		| Monday     | 08:00:00 |
		| Tuesday    | 08:00:00 |
		| Wednesday  | 08:00:00 |
		| Thursday   | 08:00:00 |
		| Friday     | 08:00:00 |
		| Saturday   | 00:00:00 |

Regra: Um contrato pode ser feito com um empregador

Cenário: Trabalhador abre um contrato feito com um novo empregador
	Dado que existe uma abertura de contrato em andamento
	E o trabahador cadastrar o empregador 'Empregador A'
	E o trabalhador informar que o contrato foi feito com o 'Empregador A'
	Quando o trabalhador abrir o contrato
	Então o empregador do contrato deverá ser 'Empregador A'

Cenário: Trabalhador abre um contrato para um empregador já cadastrado (feito com um empregador existente)
	Dado que existe um empregador cadastrado com o nome 'Empregador B'
	Quando o trabalhador iniciar uma abertura de contrato
	E o trabalhador informar que o contrato foi feito com o 'Empregador B'
	E o trabalhador abrir o contrato
	Então o empregador do contrato deverá ser 'Empregador B'

Regra: O nome do contrato deve ter pelo menos 2 caracteres

@basic
Cenário: Trabalhador abre um contrato com nome maior que 1 caractere
	Dado que existe uma abertura de contrato em andamento
	Quando o trabalhador abrir o contrato como
		| nome       |
		| Contrato A |
	Então o contrato deverá ser aberto como esperado

@basic
Cenário: Trabalhador altera um contrato com nome maior que 1 caractere
	Dado que existe um contrato qualquer
	E que existe uma alteração desse contrato em andamento
	Quando o trabalhador alterar esse contrato para
		| nome       |
		| Contrato A |
	Então o contrato deverá ser alterado como esperado

@basic
Cenário: Trabalhador tenta abrir um contrato com nome menor que 2 caracteres
	Dado que existe uma abertura de contrato em andamento
	Quando o trabalhador tentar abrir um contrato como
		| nome |
		| A    |
	Então a tentativa de abrir o contrato deverá falhar com um erro "'Nome' deve ser maior ou igual a 2 caracteres."

@basic
Cenário: Trabalhador tenta alterar um contrato com nome menor que 2 caracteres
	Dado que existe um contrato qualquer
	E que existe uma alteração desse contrato em andamento
	Quando o trabalhador tentar alterar esse contrato para
		| nome |
		| B    |
	Então a tentativa de alterar o contrato deverá falhar com um erro "'Nome' deve ser maior ou igual a 2 caracteres."

Regra: O nome do contrato deve ter no máximo 50 caracteres

@basic
Cenário: Trabalhador abre um contrato com nome menor que 51 caracteres
	Dado que existe uma abertura de contrato em andamento
	Quando o trabalhador abrir um contrato como
		| nome       |
		| Contrato A |
	Então o contrato deverá ser aberto como esperado

@basic
Cenário: Trabalhador tenta abrir um contrato com nome maior que 50 caracteres
	Dado que existe uma abertura de contrato em andamento
	Quando o trabalhador tentar abrir um contrato como
		| nome                                                                                                     |
		| Contrato de Trabalho Feito com uma Empresa do Ramo da Industria Farmacêutica do Estado do Rio de Janeiro |
	Então a tentativa de cadastrar o contrato deverá falhar com um erro "'Nome' deve ser menor ou igual a 50 caracteres."

Regra: Tempo Total = Tempo Monday + Tempo Tuesday + Tempo Wednesday + Tempo Thursday + Tempo Friday + Tempo Saturday + Tempo Sunday

Cenário: Trabalhador cria um contrato com uma jornada de trabalho prevista de 40 horas semanais
	Dado que o trabalhador não tem nenhum contrato cadastrado
	E que o horário de trabalho é de 'Monday' a 'Friday' das '09:00' às '18:00' com '01:00' de almoço
	Quando o trabalhador criar um contrato
	Então a jornada de trabalho semanal prevista deverá ser:
		| dia semana | tempo    |
		| Sunday     | 00:00:00 |
		| Monday     | 08:00:00 |
		| Tuesday    | 08:00:00 |
		| Wednesday  | 08:00:00 |
		| Thursday   | 08:00:00 |
		| Friday     | 08:00:00 |
		| Saturday   | 00:00:00 |
	E o tempo total da jornada de trabalho semanal prevista deverá ser '1.16:00'

Cenário: Trabalhador cria um contrato com uma jornada de trabalho prevista de 44 horas semanais (incluindo sábado)
	Dado que o trabalhador não tem nenhum contrato cadastrado
	E que o horário de trabalho é de 'Monday' a 'Friday' das '09:00' às '18:00' com '01:00' de almoço
	E que o horário de trabalho de 'Saturday' é das '08:00' às '12:00'
	Quando o trabalhador criar um contrato
	Então a jornada de trabalho semanal prevista deverá ser:
		| dia semana | tempo    |
		| Sunday     | 00:00:00 |
		| Monday     | 08:00:00 |
		| Tuesday    | 08:00:00 |
		| Wednesday  | 08:00:00 |
		| Thursday   | 08:00:00 |
		| Friday     | 08:00:00 |
		| Saturday   | 04:00:00 |
	E o tempo total da jornada de trabalho semanal prevista deverá ser '1.20:00'

Regra: Deve ser possível excluir um contrato
	
Cenário: Sucesso ao remover um contrato que não era necessário
	Dado que o trabalhador tem um contrato cadastrado com o nome 'Marcelo - Ateliex'
	E que o trabalhador identifica na lista o contrato cadastrado
	Quando o trabalhador excluir o contrato
	Então o contrato deverá ser excluído

Regra: Se existir dados relacionados a um contrato então ele não pode ser excluído

@wip
Cenário: Erro ao excluir um contrato com ponto(s) marcado(s)
	Dado que o trabalhador tem um contrato cadastrado com o nome 'Marcelo - Ateliex'
	E que o trabalhador qualifica o ponto com o contrato 'Marcelo - Ateliex'
	E que o trabalhador identifica na lista o contrato cadastrado
	Quando o trabalhador excluir o contrato
	Então o contrato não deverá ser excluído
