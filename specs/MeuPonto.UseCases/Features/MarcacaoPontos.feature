# language: pt-br

Funcionalidade: Marcacao Pontos

O sistema deverá fornecer para o trabalhador a capacidade de marcar um ponto

Regra: Um ponto pode ser marcado

@main
Delineação do Cenário: [Marcar Ponto] Trabalhador marca os pontos de entrada e saída do expediente
	Dado que a data/hora do relógio é '<data/hora>'
	E que existe um contrato aberto '<contrato>'
	Quando o trabalhador solicitar uma marcação de ponto
	Então o sistema deverá apresentar um ponto novo
	Quando o trabalhador marcar o ponto como:
		| contrato   | momento id   |
		| <contrato> | <momento id> |
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

Regra: Um ponto pode indicar uma pausa

@alter
Delineação do Cenário: [Marcar Ponto] Trabalhador marca os pontos de pausa do expediente
	Dado que existe um contrato aberto '<contrato>'
	Quando o trabalhador solicitar uma marcação de ponto
	E o trabalhador marcar o ponto como:
		| contrato   | momento id   | pausa id   |
		| <contrato> | <momento id> | <pausa id> |
	#Então o ponto deverá ser marcado como esperado
	Então o momento do ponto deverá ser de '<momento id>'
	E a pausa do ponto deverá ser '<pausa id>'

Exemplos:
	| contrato          | momento id | pausa id |
	| Marcelo - Ateliex | Saida      | Almoco   |
	| Marcelo - Ateliex | Entrada    | Almoco   |

Regra: Um ponto pode ser marcado com uma observação

@alter
Cenário: [Marcar Ponto] Trabalhador marca o ponto justificando porque chegou atrasado
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