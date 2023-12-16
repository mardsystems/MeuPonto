# language: pt-br

Funcionalidade: Backup Comprovantes

Regra: Escaneamento de comprovante de ponto

@formulated
Cenário: Sistema reconhece a data/hora no comprovante de ponto
	Dado que o trabalhador tem um comprovante de ponto com a data '17/02/2023 17:07'
	Quando o trabalhador escanear o comprovante de ponto
	Então a data do ponto do comprovante deverá ser '17/02/2023 17:07'
	
Regra: Preservação de comprovante de ponto
	
@formulated
Cenário: Trabalhador guarda o comprovante de ponto
	Dado que o trabalhador escaneou um comprovante de ponto com a data '17/02/2023 17:07'
	Quando o trabalhador guardar o comprovante de ponto
	Então o comprovante de ponto deverá ser guardado
	E a data do ponto do comprovante deverá ser '17/02/2023 17:07'
