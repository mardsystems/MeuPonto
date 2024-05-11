# language: pt-br

Funcionalidade: Backup Comprovantes
	
Regra: Um ponto pode ser legitimado por um comprovante

Caso de Uso: Guardar Comprovante

@main @wip
Cenário: Trabalhador registra o ponto com um comprovante
	Dado que existe um contrato aberto
	E que existe um registro de ponto em andamento
	Quando o trabalhador iniciar um backup de comprovante
	Então um comprovante deverá ser criado
	Quando o trabalhador escanear o comprovante com a data '17/02/2023 17:07'
	Então a data do ponto do comprovante deverá ser '17/02/2023 17:07'
	Quando o trabalhador registrar o ponto como:
		| data/hora        |
		| 17/02/2023 17:07 |
	Então o comprovante '17/02/2023 17:07' deverá ser associado ao ponto

#Regra: Escaneamento de comprovante de ponto

@formulated @wip
Cenário: Sistema reconhece a data/hora no comprovante de ponto
	Dado que o trabalhador tem um comprovante de ponto com a data '17/02/2023 17:07'
	Quando o trabalhador escanear o comprovante de ponto
	Então a data do ponto do comprovante deverá ser '17/02/2023 17:07'
	
Regra: Um comprovante pode ser guardado
	
@formulated @wip
Cenário: Trabalhador guarda o comprovante de ponto
	Dado que o trabalhador escaneou um comprovante de ponto com a data '17/02/2023 17:07'
	Quando o trabalhador guardar o comprovante de ponto
	Então o comprovante de ponto deverá ser guardado
	E a data do ponto do comprovante deverá ser '17/02/2023 17:07'
