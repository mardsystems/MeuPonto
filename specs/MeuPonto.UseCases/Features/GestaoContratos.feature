# language: pt-br

Funcionalidade: Gestão Contratos

O sistema deverá fornecer para o trabalhador a capacidade de gerenciar seus contratos.

Abrir Contrato

O sistema deverá fornecer para o trabalhador a capacidade de abrir um contrato.

1. Trabalhador solicita abertura de contrato
2. Sistema apresenta um contrato novo
3. Trabalhador abre o contrato (nome, ativo, segunda, terça, …) (E2)
4. Sistema registra o contrato

O sistema deverá fornecer para o trabalhador a capacidade de alterar um contrato

O sistema deverá fornecer para o trabalhador a capacidade de encerrar um contrato

O sistema deverá fornecer para o trabalhador a capacidade de excluir um contrato

Regra: Um contrato pode ser aberto

@main
Cenário: [Abrir Contrato] Trabalhador abre um contrato
	Quando o trabalhador solicitar a abertura de um contrato
	Então o sistema deverá apresentar um contrato novo
	E o contrato deverá ser ativo
	E o contrato deverá prever a seguinte jornada de trabalho semanal:
		| dia semana | tempo    |
		| Sunday     | 00:00:00 |
		| Monday     | 08:00:00 |
		| Tuesday    | 08:00:00 |
		| Wednesday  | 08:00:00 |
		| Thursday   | 08:00:00 |
		| Friday     | 08:00:00 |
		| Saturday   | 00:00:00 |
	Quando o trabalhador abrir o contrato como:
		| nome       | ativo | domingo  | segunda  | terça    | quarta   | quinta   | sexta    | sábado   |
		| Contrato A | True  | 00:00:00 | 08:00:00 | 08:00:00 | 08:00:00 | 08:00:00 | 08:00:00 | 00:00:00 |
	Então o sistema deverá registrar o contrato como esperado

Regra: Um contrato pode ser alterado

@main
Cenário: [Alterar Contrato] Trabalhador altera um contrato para corrigir um erro de digitação no nome
	Dado que existe um contrato aberto 'Marcello - Particular'
	Quando o trabalhador solicitar a edição do contrato 'Marcello - Particular'
	E o trabalhador alterar o contrato para
		| nome                 |
		| Marcelo - Particular |
	Então o sistema deverá alterar o contrato como esperado
	#Então o nome do contrato deverá ser 'Marcelo - Particular'

Regra: O nome do contrato deve ter pelo menos 3 caracteres

@secondary
Cenário: [Abrir Contrato] Trabalhador abre um contrato com nome maior que 2 caractere
	Dado que existe uma abertura de contrato em andamento
	Quando o trabalhador abrir o contrato como:
		| nome       |
		| Contrato B |
	Então o sistema deverá registrar o contrato como esperado

@secondary
Cenário: [Alterar Contrato] Trabalhador altera um contrato com nome maior que 2 caractere
	Dado que existe um contrato aberto 'Contrato Feito'
	E que existe uma edição do contrato 'Contrato Feito' em andamento
	Quando o trabalhador alterar o contrato para
		| nome       |
		| Contrato B |
	Então o sistema deverá alterar o contrato como esperado

@exception
Cenário: [Abrir Contrato] Trabalhador tenta abrir um contrato com nome menor que 3 caracteres
	Dado que existe uma abertura de contrato em andamento
	Quando o trabalhador tentar abrir o contrato como
		| nome |
		| A    |
	Então a tentativa de abrir o contrato deverá falhar com um erro "'Nome' deve ser maior ou igual a 3 caracteres."

@exception
Cenário: [Alterar Contrato] Trabalhador tenta alterar um contrato com nome menor que 3 caracteres
	Dado que existe um contrato aberto 'Contrato Feito'
	E que existe uma edição do contrato 'Contrato Feito' em andamento
	Quando o trabalhador tentar alterar o contrato para
		| nome |
		| B    |
	Então a tentativa de alterar o contrato deverá falhar com um erro "'Nome' deve ser maior ou igual a 3 caracteres."

Regra: O nome do contrato deve ter no máximo 35 caracteres

@secondary
Cenário: [Abrir Contrato] Trabalhador abre um contrato com nome menor que 36 caracteres
	Dado que existe uma abertura de contrato em andamento
	Quando o trabalhador abrir o contrato como:
		| nome       |
		| Contrato B |
	Então o sistema deverá registrar o contrato como esperado

@exception
Cenário: [Abrir Contrato] Trabalhador tenta abrir um contrato com nome maior que 35 caracteres
	Dado que existe uma abertura de contrato em andamento
	Quando o trabalhador tentar abrir o contrato como
		| nome                                                                                                     |
		| Contrato de Trabalho Feito com uma Empresa do Ramo da Industria Farmacêutica do Estado do Rio de Janeiro |
	Então a tentativa de abrir o contrato deverá falhar com um erro "'Nome' deve ser menor ou igual a 35 caracteres."

Regra: Tempo Total = Tempo Monday + Tempo Tuesday + Tempo Wednesday + Tempo Thursday + Tempo Friday + Tempo Saturday + Tempo Sunday

@secondary
Cenário: [Abrir Contrato] Trabalhador abre um contrato com uma jornada de trabalho prevista de 40 horas semanais
	Dado que existe uma abertura de contrato em andamento
	E que a jornada de trabalho semanal é de 'Monday' a 'Friday' das '09:00' às '18:00' com '01:00' de almoço
	Mas que não tem jornada de trabalho no 'Saturday' e no 'Sunday'
	Quando o trabalhador abrir o contrato
	Então a jornada de trabalho semanal prevista no contrato deverá ser:
		| dia semana | tempo    |
		| Sunday     | 00:00:00 |
		| Monday     | 08:00:00 |
		| Tuesday    | 08:00:00 |
		| Wednesday  | 08:00:00 |
		| Thursday   | 08:00:00 |
		| Friday     | 08:00:00 |
		| Saturday   | 00:00:00 |
	E o tempo total da jornada de trabalho semanal prevista no contrato deverá ser '1.16:00'

@secondary
Cenário: [Abrir Contrato] Trabalhador abre um contrato com uma jornada de trabalho prevista de 44 horas semanais (incluindo sábado)
	Dado que existe uma abertura de contrato em andamento
	E que a jornada de trabalho semanal é de 'Monday' a 'Friday' das '09:00' às '18:00' com '01:00' de almoço
	E que a jornada de trabalho de 'Saturday' é das '08:00' às '12:00'
	Mas que não tem jornada de trabalho no 'Sunday'
	Quando o trabalhador abrir o contrato
	Então a jornada de trabalho semanal prevista no contrato deverá ser:
		| dia semana | tempo    |
		| Sunday     | 00:00:00 |
		| Monday     | 08:00:00 |
		| Tuesday    | 08:00:00 |
		| Wednesday  | 08:00:00 |
		| Thursday   | 08:00:00 |
		| Friday     | 08:00:00 |
		| Saturday   | 04:00:00 |
	E o tempo total da jornada de trabalho semanal prevista no contrato deverá ser '1.20:00'

Regra: Um contrato pode ser encerrado

@main @wip
Cenário: [Encerrar Contrato] Trabalhador encerra um contrato
	Dado que existe um contrato aberto 'Contrato A'
	Quando o trabalhador solicitar o encerramento do contrato 'Contrato A'
	E o trabalhador encerrar o contrato 'Contrato A'
	Então o contrato deverá ser encerrado

Regra: Um contrato pode ser excluído

@main
Cenário: [Excluir Contrato] Trabalhador exclui um contrato que não era necessário
	Dado que existe um contrato aberto 'Contrato A'
	Quando o trabalhador solicitar a exclusão do contrato 'Contrato A'
	#E o trabalhador excluir o contrato 'Contrato A'
	E o trabalhador excluir esse contrato
	Então o contrato deverá ser excluído

Regra: Se existir dados relacionados a um contrato então ele não pode ser excluído

@exception @wip
Cenário: [Excluir Contrato] Trabalhador tenta excluir excluir um contrato com ponto(s) marcado(s)
	Dado que existe um contrato aberto 'Contrato C'
	E que existe um ponto qualificado com o contrato 'Contrato C'
	Quando o trabalhador solicitar a exclusão do contrato 'Contrato C'
	#E o trabalhador excluir o contrato 'Contrato C'
	E o trabalhador excluir esse contrato
	Então o contrato não deverá ser excluído
