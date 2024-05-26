﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace MeuPonto.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ExemplarFeature : object, Xunit.IClassFixture<ExemplarFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Exemplar.feature"
#line hidden
        
        public ExemplarFeature(ExemplarFeature.FixtureData fixtureData, MeuPonto_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt-br"), "Features", "Exemplar", @"Processar Venda

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
10. Cliente vai embora com recibo e mercadorias (se houver).", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="[Processar Venda] Caixa efetua nova venda")]
        [Xunit.TraitAttribute("FeatureTitle", "Exemplar")]
        [Xunit.TraitAttribute("Description", "[Processar Venda] Caixa efetua nova venda")]
        [Xunit.TraitAttribute("Category", "wip")]
        public void ProcessarVendaCaixaEfetuaNovaVenda()
        {
            string[] tagsOfScenario = new string[] {
                    "wip"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("[Processar Venda] Caixa efetua nova venda", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 20
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 21
 testRunner.When("o caixa começar uma nova venda", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 22
 testRunner.And("o caixa inserir o identificador do item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 23
 testRunner.Then("o sistema deverá registrar a linha de item da venda e apresentar uma descrição do" +
                        " item, seu preço e total parcial da venda", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 24
 testRunner.When("o caixa terminar a venda", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 25
 testRunner.Then("o sistema deverá apresentar o total com impostos calculados", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 26
 testRunner.When("o cliente pagar", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 27
 testRunner.Then("o sitema deverá tratar o pagamento", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 28
 testRunner.And("o sistema deverá registrar venda completada", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 29
 testRunner.And("o sistema deverá enviar informações de venda e pagamento para sistema externo de " +
                        "contabilidade", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 30
 testRunner.And("o sistema deverá enviar informações de venda e pagamento para sistema externo de " +
                        "estoque", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 31
 testRunner.And("o sistema deverá apresentar o recibo", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="[Processar Venda] Caixa efetua nova venda com sucesso")]
        [Xunit.TraitAttribute("FeatureTitle", "Exemplar")]
        [Xunit.TraitAttribute("Description", "[Processar Venda] Caixa efetua nova venda com sucesso")]
        [Xunit.TraitAttribute("Category", "wip")]
        public void ProcessarVendaCaixaEfetuaNovaVendaComSucesso()
        {
            string[] tagsOfScenario = new string[] {
                    "wip"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("[Processar Venda] Caixa efetua nova venda com sucesso", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 34
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 35
 testRunner.When("o caixa iniciar uma nova venda", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                            "id item",
                            "quantidade"});
                table9.AddRow(new string[] {
                            "B0002",
                            "10"});
                table9.AddRow(new string[] {
                            "A0001",
                            "5"});
                table9.AddRow(new string[] {
                            "C0007",
                            "2"});
#line 36
 testRunner.And("o caixa entrar um item como:", ((string)(null)), table9, "E ");
#line hidden
                TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                            "descrição",
                            "preço",
                            "total"});
                table10.AddRow(new string[] {
                            "Biscoito",
                            "R$ 1,99",
                            "19,90"});
                table10.AddRow(new string[] {
                            "Sabonete",
                            "R$ 3,50",
                            "17,50"});
                table10.AddRow(new string[] {
                            "Arroz (1 kg)",
                            "R$ 5,70",
                            "11,4"});
#line 41
 testRunner.Then("o sistema deverá registrar a linha de item da venda e apresentar uma descrição do" +
                        " item, seu preço e total parcial da venda", ((string)(null)), table10, "Então ");
#line hidden
#line 46
 testRunner.When("o caixa terminar a venda", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
                TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                            "total",
                            "impostos",
                            "total com impostos"});
                table11.AddRow(new string[] {
                            "R$ 48,8",
                            "R$ 4,88",
                            "R$ 53,68"});
#line 47
 testRunner.Then("o sistema deverá apresentar o total com impostos calculados", ((string)(null)), table11, "Então ");
#line hidden
#line 50
 testRunner.When("o cliente fazer o pagamento de \'R$ 53,68\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 51
 testRunner.Then("o sitema deverá tratar o pagamento", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 52
 testRunner.And("o sistema deverá registrar venda completada", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 53
 testRunner.And("o sistema deverá enviar informações de venda e pagamento para sistema externo de " +
                        "contabilidade", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 54
 testRunner.And("o sistema deverá enviar informações de venda e pagamento para sistema externo de " +
                        "estoque", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 55
 testRunner.And("o sistema deverá apresentar o recibo", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                ExemplarFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ExemplarFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
