# language: pt-br

Funcionalidade: Processamento de Venda

Processar Venda

1. Cliente chega à saída do PDV com bens ou serviços para adquirir.
2. Caixa começa uma nova venda.
3. Caixa insere o identificador do item.
4. Sistema registra a linha de item da venda e apresenta uma descrição do item, seu preço e total parcial da venda. Preço calculado segundo um conjunto de regras de preços.
Caixa repete os passos 3 e 4 até que indique ter terminado.
5. Sistema apresenta o total com impostos calculados.
6. Caixa informa total ao Cliente e solicita pagamento.
7. Cliente paga e Sistema trata pagamento.
8. Sistema registra venda completada e envia informações de venda e pagamento para Sistema externo de contabilidade (para contabilidade e comissões) e para Sistema de Estoque (para atualizar o estoque).
9. Sistema apresenta recibo.
10. Cliente vai embora com recibo e mercadorias (se houver).

@wip
Cenário: [Processar Venda] Caixa efetua nova venda
	Quando o caixa começar uma nova venda
	E o caixa inserir o identificador do item
	Então o sistema deverá registrar a linha de item da venda e apresentar uma descrição do item, seu preço e total parcial da venda
	Quando o caixa terminar a venda
	Então o sistema deverá apresentar o total com impostos calculados
	Quando o cliente pagar
	Então o sitema deverá tratar o pagamento
	E o sistema deverá registrar venda completada
	E o sistema deverá enviar informações de venda e pagamento para sistema externo de contabilidade
	E o sistema deverá enviar informações de venda e pagamento para sistema externo de estoque
	E o sistema deverá apresentar o recibo

@wip	
Cenário: [Processar Venda] Caixa efetua nova venda com sucesso
	Quando o caixa iniciar uma nova venda
	E o caixa entrar um item como:
		| id item | quantidade |
		| B0002   | 10         |
		| A0001   | 5          |
		| C0007   | 2          |
	Então o sistema deverá registrar a linha de item da venda e apresentar uma descrição do item, seu preço e total parcial da venda
		| descrição    | preço   | total |
		| Biscoito     | R$ 1,99 | 19,90 |
		| Sabonete     | R$ 3,50 | 17,50 |
		| Arroz (1 kg) | R$ 5,70 | 11,4  |
	Quando o caixa terminar a venda
	Então o sistema deverá apresentar o total com impostos calculados
		| total   | impostos | total com impostos |
		| R$ 48,8 | R$ 4,88  | R$ 53,68           |
	Quando o cliente fazer o pagamento de 'R$ 53,68'
	Então o sitema deverá tratar o pagamento
	E o sistema deverá registrar venda completada
	E o sistema deverá enviar informações de venda e pagamento para sistema externo de contabilidade
	E o sistema deverá enviar informações de venda e pagamento para sistema externo de estoque
	E o sistema deverá apresentar o recibo