# language: pt-br

Funcionalidade: Backup Comprovantes
	
O sistema deverá fornecer para o trabalhador a capacidade de guardar seus comprovantes.

O sistema deverá fornecer para o trabalhador a capacidade de guardar um comprovante

Regra: Um ponto pode ser legitimado por um comprovante

@extension @wip
Cenário: [Registrar Ponto com um Comprovante] Trabalhador registra um ponto com um comprovante
	Dado que existe um contrato aberto
	E que existe um registro de ponto em andamento
	Quando o trabalhador solicitar o backup de um comprovante
	Então o sistema deverá apresentar as opções de backup de um comprovante
	Quando o trabalhador escanear a imagem do comprovante com a data '17/02/2023 17:07'
	Então o sistema deverá processar a imagem do comprovante
	E a data do ponto deverá ser '17/02/2023 17:07'
	#E a data do ponto do comprovante deverá ser '17/02/2023 17:07'
	Quando o trabalhador registrar o ponto como:
		| data/hora        |
		| 17/02/2023 17:07 |
	Então o sistema deverá registrar o ponto como esperado
	E o comprovante '17/02/2023 17:07' deverá ser associado ao ponto

@formulated @wip
Cenário: [Registrar Ponto com um Comprovante] Sistema reconhece a data/hora no comprovante de ponto
	Dado que o trabalhador tem um comprovante de ponto com a data '17/02/2023 17:07'
	Quando o trabalhador escanear o comprovante de ponto
	Então o sistema deverá processar a imagem do comprovante
	E a data do ponto do comprovante deverá ser '17/02/2023 17:07'
	
Regra: Um comprovante pode ser guardado
	
@formulated @wip
Cenário: [Guardar Comprovante] Trabalhador guarda o comprovante de ponto
	Dado que o trabalhador escaneou um comprovante de ponto com a data '17/02/2023 17:07'
	Quando o trabalhador guardar o comprovante de ponto
	Então o sistema deverá registrar o comprovante de ponto
	E a data do ponto do comprovante deverá ser '17/02/2023 17:07'
