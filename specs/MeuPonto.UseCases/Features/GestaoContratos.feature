﻿# language: pt-br

Funcionalidade: Gestão Contratos

Regra: Um contrato pode ser aberto

@main
Cenário: Trabalhador abre um contrato
	Quando o trabalhador iniciar uma abertura de contrato
	Então um contrato deverá ser criado
	Quando o trabalhador abrir o contrato como:
		| nome       | ativo | domingo  | segunda  | terça    | quarta   | quinta   | sexta    | sábado   |
		| Contrato A | True  | 00:00:00 | 08:00:00 | 08:00:00 | 08:00:00 | 08:00:00 | 08:00:00 | 00:00:00 |
	Então o nome do contrato deverá ser 'Contrato A'
	E o contrato deverá ser ativo
	E a jornada de trabalho semanal prevista no contrato deverá ser:
		| dia semana | tempo    |
		| Sunday     | 00:00:00 |
		| Monday     | 08:00:00 |
		| Tuesday    | 08:00:00 |
		| Wednesday  | 08:00:00 |
		| Thursday   | 08:00:00 |
		| Friday     | 08:00:00 |
		| Saturday   | 00:00:00 |

Regra: Um contrato pode ser alterado

@main
Cenário: Trabalhador altera um contrato para corrigir um erro de digitação no nome
	Dado que existe um contrato aberto 'Marcello - Particular'
	Quando o trabalhador iniciar uma edição de contrato
	E o trabalhador alterar esse contrato para
		| nome                 |
		| Marcelo - Particular |
	Então o nome do contrato deverá ser 'Marcelo - Particular'

Regra: Um novo contrato deve ser ativo

@invariant
Cenário: Trabalhador inicia um novo contrato ativo
	Quando o trabalhador iniciar uma abertura de contrato
	Então um contrato deverá ser criado
	E o contrato deverá ser ativo

Regra: Um novo contrato deve ter uma jornada de trabalho prevista

@invariant
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

Regra: O nome do contrato deve ter pelo menos 3 caracteres

@invariant @basic
Cenário: Trabalhador abre um contrato com nome maior que 2 caractere
	Dado que existe uma abertura de contrato em andamento
	Quando o trabalhador abrir o contrato como:
		| nome       |
		| Contrato A |
	Então o contrato deverá ser aberto como esperado

@invariant @basic
Cenário: Trabalhador altera um contrato com nome maior que 2 caractere
	Dado que existe um contrato aberto 'Contrato Feito'
	E que existe uma alteração desse contrato em andamento 'Contrato Feito'
	Quando o trabalhador alterar esse contrato para
		| nome       |
		| Contrato A |
	Então o contrato deverá ser alterado como esperado

@invariant @exception @basic
Cenário: Trabalhador tenta abrir um contrato com nome menor que 3 caracteres
	Dado que existe uma abertura de contrato em andamento
	Quando o trabalhador tentar abrir um contrato como
		| nome |
		| A    |
	Então a tentativa de abrir o contrato deverá falhar com um erro "'Nome' deve ser maior ou igual a 3 caracteres."

@invariant @exception @basic
Cenário: Trabalhador tenta alterar um contrato com nome menor que 3 caracteres
	Dado que existe um contrato aberto 'Contrato Feito'
	E que existe uma alteração desse contrato em andamento 'Contrato Feito'
	Quando o trabalhador tentar alterar esse contrato para
		| nome |
		| B    |
	Então a tentativa de alterar o contrato deverá falhar com um erro "'Nome' deve ser maior ou igual a 3 caracteres."

Regra: O nome do contrato deve ter no máximo 35 caracteres

@invariant @basic
Cenário: Trabalhador abre um contrato com nome menor que 36 caracteres
	Dado que existe uma abertura de contrato em andamento
	Quando o trabalhador abrir o contrato como:
		| nome       |
		| Contrato A |
	Então o contrato deverá ser aberto como esperado

@invariant @exception @basic
Cenário: Trabalhador tenta abrir um contrato com nome maior que 35 caracteres
	Dado que existe uma abertura de contrato em andamento
	Quando o trabalhador tentar abrir um contrato como
		| nome                                                                                                     |
		| Contrato de Trabalho Feito com uma Empresa do Ramo da Industria Farmacêutica do Estado do Rio de Janeiro |
	Então a tentativa de abrir o contrato deverá falhar com um erro "'Nome' deve ser menor ou igual a 35 caracteres."

Regra: Tempo Total = Tempo Monday + Tempo Tuesday + Tempo Wednesday + Tempo Thursday + Tempo Friday + Tempo Saturday + Tempo Sunday

@invariant
Cenário: Trabalhador abre um contrato com uma jornada de trabalho prevista de 40 horas semanais
	Dado que a jornada de trabalho semanal é de 'Monday' a 'Friday' das '09:00' às '18:00' com '01:00' de almoço
	Quando o trabalhador abrir um contrato
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

@invariant
Cenário: Trabalhador abre um contrato com uma jornada de trabalho prevista de 44 horas semanais (incluindo sábado)
	Dado que a jornada de trabalho semanal é de 'Monday' a 'Friday' das '09:00' às '18:00' com '01:00' de almoço
	E que a jornada de trabalho de 'Saturday' é das '08:00' às '12:00'
	Quando o trabalhador abrir um contrato
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

Regra: Um contrato pode ser excluído

@main
Cenário: Trabalhador exclui um contrato que não era necessário
	Dado que existe um contrato aberto 'Marcelo - Ateliex'
	Quando o trabalhador excluir o contrato
	Então o contrato deverá ser excluído

Regra: Se existir dados relacionados a um contrato então ele não pode ser excluído

@exception @wip
Cenário: Trabalhador tenta excluir excluir um contrato com ponto(s) marcado(s)
	Dado que existe um contrato aberto 'Marcelo - Ateliex'
	#E que o trabalhador qualifica o ponto com o contrato 'Marcelo - Ateliex'
	Quando o trabalhador excluir o contrato
	Então o contrato não deverá ser excluído
