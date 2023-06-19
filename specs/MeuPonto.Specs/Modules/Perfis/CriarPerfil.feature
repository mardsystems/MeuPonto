# language: pt-br

Funcionalidade: Criar Perfil
	Para identificar o meu vínculo com o empregador
	E saber o tempo total previsto de trabalho na minha folha de ponto
	Enquanto trabalhador
	Eu quero criar um perfil

Regra: Identificação do vínculo do trabalhador com o empregador

Cenário: Trabalhador cria um perfil para identificar seu vínculo com o empregador
	Dado que o trabalhador não tem nenhum perfil cadastrado
	E que o melhor nome que denota o vínculo entre o trabalhador e o empregador é 'Marcelo - Ateliex'
	Quando o trabalhador criar um perfil
	Então um perfil deverá ser cadastrado
	E o nome do perfil deverá ser 'Marcelo - Ateliex'

Cenário: Trabalhador cria um perfil para identificar seu novo vínculo com o empregador
	Dado que o trabalhador já tem um perfil cadastrado
	E que o melhor nome que denota o novo vínculo entre o trabalhador e o empregador é 'Marcelo - Ateliex - Consultor'
	Quando o trabalhador criar um perfil
	Então um perfil deverá ser cadastrado
	E o nome do perfil deverá ser 'Marcelo - Ateliex - Consultor'

Regra: Tempo Total = Tempo Monday + Tempo Tuesday + Tempo Wednesday + Tempo Thursday + Tempo Friday + Tempo Saturday + Tempo Sunday

Cenário: Trabalhador cria um perfil com uma jornada de trabalho prevista de 40 horas semanais
	Dado que o trabalhador não tem nenhum perfil cadastrado
	E que o horário de trabalho é de 'Monday' a 'Friday' das '09:00' às '18:00' com '01:00' de almoço
	Quando o trabalhador criar um perfil
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

Cenário: Trabalhador cria um perfil com uma jornada de trabalho prevista de 44 horas semanais (incluindo sábado)
	Dado que o trabalhador não tem nenhum perfil cadastrado
	E que o horário de trabalho é de 'Monday' a 'Friday' das '09:00' às '18:00' com '01:00' de almoço
	E que o horário de trabalho de 'Saturday' é das '08:00' às '12:00'
	Quando o trabalhador criar um perfil
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
