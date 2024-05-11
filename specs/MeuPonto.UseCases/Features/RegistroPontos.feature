# language: pt-br

Funcionalidade: Registro Pontos
	
Regra: Um ponto pode ser registrado

Caso de Uso: Registrar Ponto

@main @wip
Delineação do Cenário: Trabalhador registra os pontos de entrada e saída do expediente
	Dado que a data/hora do relógio é '<data/hora>'
	E que existe um contrato aberto '<contrato>'
	Quando o trabalhador iniciar um registro de ponto
	Então um ponto deverá ser criado
	Quando o trabalhador registrar o ponto como:
		| data/hora   | contrato   | momento id   |
		| <data/hora> | <contrato> | <momento id> |
	Então a data do ponto deverá ser '<data/hora>'
	E o ponto deverá ser qualificado pelo contrato '<contrato>'
	E o momento do ponto deverá ser de '<momento id>'
	Mas o ponto não deverá indicar que foi uma pausa
	E o ponto não deverá indicar que foi estimado
	E o ponto não deverá ter uma observação

Exemplos:
	| data/hora        | contrato          | momento id |
	| 27/11/2022 09:14 | Marcelo - Ateliex | Entrada    |
	| 27/11/2022 18:05 | Marcelo - Ateliex | Saida      |
	
Regra: Um ponto pode ser marcado

Caso de Uso: Marcar Ponto

@main
Delineação do Cenário: Trabalhador marca os pontos de entrada e saída do expediente
	Dado que a data/hora do relógio é '<data/hora>'
	E que existe um contrato aberto '<contrato>'
	Quando o trabalhador iniciar uma marcação de ponto
	Então um ponto deverá ser criado
	Quando o trabalhador marcar o ponto como:
		| data/hora   | contrato   | momento id   |
		| <data/hora> | <contrato> | <momento id> |
	Então a data do ponto deverá ser '<data/hora>'
	E o ponto deverá ser qualificado pelo contrato '<contrato>'
	E o momento do ponto deverá ser de '<momento id>'
	Mas o ponto não deverá indicar que foi uma pausa
	E o ponto não deverá indicar que foi estimado
	E o ponto não deverá ter uma observação

Exemplos:
	| data/hora        | contrato          | momento id |
	| 27/11/2022 09:14 | Marcelo - Ateliex | Entrada    |
	| 27/11/2022 18:05 | Marcelo - Ateliex | Saida      |

Regra: Um ponto deve ser qualificado por um contrato

@invariant
Cenário: Trabalhador qualifica um ponto com um contrato
	Dado que existe um contrato aberto 'Marcelo - Ateliex'
	E que existe uma marcação de ponto em andamento
	Quando o trabalhador marcar o ponto como:
		| contrato          |
		| Marcelo - Ateliex |
	Então o ponto deverá ser registrado como esperado

@invariant @wip
Cenário: Trabalhador deixa de qualificar um ponto com um contrato
	Dado que existe um contrato aberto 'Marcelo - Ateliex'
	E que existe um registro de ponto em andamento
	Quando o trabalhador registrar o ponto como:
		| contrato |
		| <null>   |
	Então a tentativa de marcar o ponto deverá falhar com um erro "'Contrato' deve ser informado."

Regra: Um ponto pode indicar uma pausa

Caso de Uso: Marcar Ponto

@alter
Delineação do Cenário: Trabalhador marca os pontos de pausa do expediente
	Dado que existe um contrato aberto '<contrato>'
	Quando o trabalhador iniciar uma marcação de ponto
	E o trabalhador marcar o ponto como:
		| contrato   | momento id   | pausa id   |
		| <contrato> | <momento id> | <pausa id> |
	#Então o ponto deverá ser registrado como esperado
	Então o momento do ponto deverá ser de '<momento id>'
	E a pausa do ponto deverá ser '<pausa id>'

Exemplos:
	| contrato          | momento id | pausa id |
	| Marcelo - Ateliex | Saida      | Almoco   |
	| Marcelo - Ateliex | Entrada    | Almoco   |

Regra: Um ponto pode ser registrado com uma observação

Caso de Uso: Registrar Ponto

@alter @wip
Cenário: Trabalhador registra o ponto justificando porque chegou atrasado
	Dado que existe um contrato aberto
	E que existe um registro de ponto em andamento
	Quando o trabalhador registrar o ponto com a seguinte observação:
		"""
		Hoje o trânsito estava lento.
		"""
	Então a observação do ponto deverá ser:
		"""
		Hoje o trânsito estava lento.
		"""


Regra: Um ponto pode ser marcado com uma observação

Caso de Uso: Marcar Ponto

@alter
Cenário: Trabalhador marca o ponto justificando porque chegou atrasado
	Dado que existe um contrato aberto
	E que existe uma marcação de ponto em andamento
	Quando o trabalhador marcar o ponto com a seguinte observação:
		"""
		Hoje o trânsito estava lento.
		"""
	Então a observação do ponto deverá ser:
		"""
		Hoje o trânsito estava lento.
		"""

