# language: pt-br

Funcionalidade: Gestão Contratos

Regra: Identificação do vínculo do trabalhador com o empregador

Cenário: Trabalhador cria um contrato para identificar seu vínculo com o empregador
	Dado que o trabalhador não tem nenhum contrato cadastrado
	E que o melhor nome que denota o vínculo entre o trabalhador e o empregador é 'Marcelo - Ateliex'
	Quando o trabalhador criar um contrato
	Então um contrato deverá ser cadastrado
	E o nome do contrato deverá ser 'Marcelo - Ateliex'

Cenário: Trabalhador cria um contrato para identificar seu novo vínculo com o empregador
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
