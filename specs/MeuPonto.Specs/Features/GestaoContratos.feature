# language: pt-br

Funcionalidade: Gestão Contratos

Regra: O nome do contrato deve ter pelo menos 2 caracteres

@basic
Cenário: Trabalhador abre um contrato com nome maior que 1 caractere
	Dado que o trabalhador quer abrir um contrato
	Quando o trabalhador abrir um contrato como
		| nome       |
		| Contrato A |
	Então o contrato deverá ser criado como esperado

@basic
Cenário: Trabalhador altera um contrato com nome maior que 1 caractere
	Dado que existe um contrato qualquer
	E que o trabalhador quer alterar esse contrato
	Quando o trabalhador alterar esse contrato para
		| nome       |
		| Contrato A |
	Então o contrato deverá ser alterado como esperado

@basic
Cenário: Trabalhador tenta abrir um contrato com nome menor que 2 caracteres
	Dado que o trabalhador quer abrir um contrato
	Quando o trabalhador tentar abrir um contrato como
		| nome |
		| A    |
	Então a tentativa de abrir o contrato deverá falhar com um erro "'Nome' deve ser maior ou igual a 2 caracteres."

@basic
Cenário: Trabalhador tenta alterar um contrato com nome menor que 2 caracteres
	Dado que o trabalhador quer alterar um contrato
	E que existe um contrato qualquer
	Quando o trabalhador tentar alterar esse contrato para
		| nome |
		| B    |
	Então a tentativa de alterar o contrato deverá falhar com um erro "'Nome' deve ser maior ou igual a 2 caracteres."

Regra: O nome do contrato deve ter no máximo 50 caracteres

@basic
Cenário: Trabalhador abre um contrato com nome menor que 51 caracteres
	Dado que o trabalhador quer abrir um contrato
	Quando o trabalhador abrir um contrato como
		| nome       |
		| Contrato A |
	Então o contrato deverá ser criado como esperado

@basic
Cenário: Trabalhador tenta abrir um contrato com nome maior que 50 caracteres
	Dado que o trabalhador quer abrir um contrato
	Quando o trabalhador tentar abrir um contrato como
		| nome                                                                                                     |
		| Contrato de Trabalho Feito com uma Empresa do Ramo da Industria Farmacêutica do Estado do Rio de Janeiro |
	Então a tentativa de cadastrar o contrato deverá falhar com um erro "'Nome' deve ser menor ou igual a 50 caracteres."










Regra: Um contrato pode ser feito com um empregador

Cenário: Trabalhador abre um contrato feito com um novo empregador
	Dado que o trabalhador quer abrir um contrato feito com um novo empregador
	Quando o trabalhador abrir um contrato feito com o empregador 'Empregador A'
	Então um contrato deverá ser criado para o empregador 'Empregador A'
	E o empregador do contrato deverá ser 'Empregador A'

Cenário: Trabalhador abre um contrato feito com um empregador existente
	Dado que o trabalhador já tem um contrato cadastrado
	E que o melhor nome que denota o novo vínculo entre o trabalhador e o empregador é 'Marcelo - Ateliex - Consultor'
	Quando o trabalhador criar um contrato
	Então um contrato deverá ser cadastrado
	E o nome do contrato deverá ser 'Marcelo - Ateliex - Consultor'

Cenário: Trabalhador edita um contrato para corrigir um erro de digitação no nome
	Dado que o trabalhador tem um contrato cadastrado com o nome 'Marcello'
	E que o trabalhador identifica na lista o contrato cadastrado
	E que o nome do trabalhador é 'Marcelo'
	Quando o trabalhador editar o contrato
	Então o contrato deverá ser editado
	E o nome do contrato deverá ser 'Marcelo'

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
