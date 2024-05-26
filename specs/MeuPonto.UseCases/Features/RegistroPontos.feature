# language: pt-br

Funcionalidade: Registro Pontos

O sistema deverá fornecer para o trabalhador a capacidade de registrar seus pontos.

O sistema deverá fornecer para o trabalhador a capacidade de registrar um ponto

Regra: Um ponto pode ser registrado

@main
Delineação do Cenário: [Registrar Ponto] Trabalhador registra os pontos de entrada e saída do expediente
	Dado que a data/hora do relógio é '<data/hora>'
	E que existe um contrato aberto '<contrato>'
	Quando o trabalhador solicitar um registro de ponto
	Então o sistema deverá apresentar um ponto novo
	Quando o trabalhador registrar o ponto como:
		| data/hora   | contrato   | momento id   |
		| <data/hora> | <contrato> | <momento id> |
	Então o sistema deverá registrar o ponto como esperado
	E a data do ponto deverá ser '<data/hora>'
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

@alter
Cenário: [Registrar Ponto] Trabalhador qualifica um ponto com um contrato
	Dado que existe um contrato aberto 'Marcelo - Ateliex'
	E que existe um registro de ponto em andamento
	Quando o trabalhador registrar o ponto como:
		| contrato          |
		| Marcelo - Ateliex |
	Então o ponto deverá ser registrado como esperado

@alter @wip
Cenário: [Registrar Ponto] Trabalhador deixa de qualificar um ponto com um contrato
	Dado que existe um contrato aberto 'Marcelo - Ateliex'
	E que existe um registro de ponto em andamento
	Quando o trabalhador tentar registrar o ponto como:
		| contrato |
		| <null>   |
	Então a tentativa de registrar o ponto deverá falhar com um erro "'Contrato' deve ser informado."

Regra: Um ponto pode indicar uma pausa

@alter
Delineação do Cenário: [Registrar Ponto] Trabalhador registra os pontos de pausa do expediente
	Dado que existe um contrato aberto '<contrato>'
	Quando o trabalhador solicitar um registro de ponto
	E o trabalhador registrar o ponto como:
		| data/hora   | contrato   | momento id   | pausa id   |
		| <data/hora> | <contrato> | <momento id> | <pausa id> |
	#Então o ponto deverá ser registrado como esperado
	Então o momento do ponto deverá ser de '<momento id>'
	E a pausa do ponto deverá ser '<pausa id>'

Exemplos:
	| data/hora        | contrato          | momento id | pausa id |
	| 27/11/2022 12:07 | Marcelo - Ateliex | Saida      | Almoco   |
	| 27/11/2022 13:05 | Marcelo - Ateliex | Entrada    | Almoco   |

Regra: Um ponto pode ser registrado com uma observação

@alter
Cenário: [Registrar Ponto] Trabalhador registra o ponto justificando porque chegou atrasado
	Dado que existe um contrato aberto
	E que existe um registro de ponto em andamento
	Quando o trabalhador registrar o ponto com a seguinte observação:
		"""
		Hoje o trânsito estava lento.
		"""
	#Então o sistema deverá registrar o ponto?
	Então o ponto deverá ser registrado
	E a observação do ponto deverá ser:
		"""
		Hoje o trânsito estava lento.
		"""
