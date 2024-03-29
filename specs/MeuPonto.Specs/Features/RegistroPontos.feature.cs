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
    public partial class RegistroPontosFeature : object, Xunit.IClassFixture<RegistroPontosFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "RegistroPontos.feature"
#line hidden
        
        public RegistroPontosFeature(RegistroPontosFeature.FixtureData fixtureData, MeuPonto_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt-br"), "Features", "Registro Pontos", null, ProgrammingLanguage.CSharp, featureTags);
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
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador marca o ponto usando seu único contrato")]
        [Xunit.TraitAttribute("FeatureTitle", "Registro Pontos")]
        [Xunit.TraitAttribute("Description", "Trabalhador marca o ponto usando seu único contrato")]
        public void TrabalhadorMarcaOPontoUsandoSeuUnicoContrato()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador marca o ponto usando seu único contrato", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 7
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
 testRunner.Given("que o trabalhador tem um contrato cadastrado com o nome \'Marcelo - Ateliex\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 9
 testRunner.Given("que o trabalhador qualifica o ponto com o contrato \'Marcelo - Ateliex\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 10
 testRunner.When("o trabalhador marcar o ponto", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 11
 testRunner.Then("o ponto deverá ser marcado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 12
 testRunner.And("o contrato do ponto deverá deverá ser \'Marcelo - Ateliex\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador marca o ponto de entrada do expediente")]
        [Xunit.TraitAttribute("FeatureTitle", "Registro Pontos")]
        [Xunit.TraitAttribute("Description", "Trabalhador marca o ponto de entrada do expediente")]
        public void TrabalhadorMarcaOPontoDeEntradaDoExpediente()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador marca o ponto de entrada do expediente", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 16
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 17
 testRunner.Given("que a data/hora do relógio é \'27/11/2022 09:14\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 18
 testRunner.And("que é o momento de \'Entrada\' do expediente", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 19
 testRunner.When("o trabalhador marcar o ponto", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 20
 testRunner.Then("o ponto deverá ser marcado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 21
 testRunner.And("a data do ponto deverá ser \'27/11/2022 09:14\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 22
 testRunner.And("o momento do ponto deverá ser de \'Entrada\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 24
 testRunner.And("o ponto deverá indicar que não é almoço", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 25
 testRunner.And("o ponto deverá indicar que não foi estimado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 26
 testRunner.But("o ponto não deverá ter uma observação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Mas ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador marca o ponto de saída do expediente")]
        [Xunit.TraitAttribute("FeatureTitle", "Registro Pontos")]
        [Xunit.TraitAttribute("Description", "Trabalhador marca o ponto de saída do expediente")]
        public void TrabalhadorMarcaOPontoDeSaidaDoExpediente()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador marca o ponto de saída do expediente", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 28
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 29
 testRunner.Given("que a data/hora do relógio é \'27/11/2022 18:05\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 30
 testRunner.And("que é o momento de \'Saida\' do expediente", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 31
 testRunner.When("o trabalhador marcar o ponto", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 32
 testRunner.Then("o ponto deverá ser marcado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 33
 testRunner.And("a data do ponto deverá ser \'27/11/2022 18:05\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 34
 testRunner.And("o momento do ponto deverá ser de \'Saida\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 36
 testRunner.And("o ponto deverá indicar que não é almoço", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 37
 testRunner.And("o ponto deverá indicar que não foi estimado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 38
 testRunner.But("o ponto não deverá ter uma observação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Mas ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador marca o ponto de saída do expediente para o almoço")]
        [Xunit.TraitAttribute("FeatureTitle", "Registro Pontos")]
        [Xunit.TraitAttribute("Description", "Trabalhador marca o ponto de saída do expediente para o almoço")]
        public void TrabalhadorMarcaOPontoDeSaidaDoExpedienteParaOAlmoco()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador marca o ponto de saída do expediente para o almoço", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 42
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 43
 testRunner.Given("que é o momento de \'Saida\' do expediente para o almoço", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 44
 testRunner.When("o trabalhador marcar o ponto", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 45
 testRunner.Then("o ponto deverá ser marcado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 46
 testRunner.And("o momento do ponto deverá ser de \'Saida\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 47
 testRunner.And("o ponto deverá indicar que é almoço", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador marca o ponto de entrada do expediente da volta do almoço")]
        [Xunit.TraitAttribute("FeatureTitle", "Registro Pontos")]
        [Xunit.TraitAttribute("Description", "Trabalhador marca o ponto de entrada do expediente da volta do almoço")]
        public void TrabalhadorMarcaOPontoDeEntradaDoExpedienteDaVoltaDoAlmoco()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador marca o ponto de entrada do expediente da volta do almoço", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 49
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 50
 testRunner.Given("que é o momento de \'Entrada\' do expediente da volta do almoço", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 51
 testRunner.When("o trabalhador marcar o ponto", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 52
 testRunner.Then("o ponto deverá ser marcado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 53
 testRunner.And("o momento do ponto deverá ser de \'Entrada\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 54
 testRunner.And("o ponto deverá indicar que é almoço", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador marca o ponto justificando porque chegou atrasado")]
        [Xunit.TraitAttribute("FeatureTitle", "Registro Pontos")]
        [Xunit.TraitAttribute("Description", "Trabalhador marca o ponto justificando porque chegou atrasado")]
        public void TrabalhadorMarcaOPontoJustificandoPorqueChegouAtrasado()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador marca o ponto justificando porque chegou atrasado", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 58
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 59
 testRunner.Given("que o trabalhador anota a seguinte observação no ponto:", "Hoje o trânsito estava lento.", ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 63
 testRunner.When("o trabalhador marcar o ponto", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 64
 testRunner.Then("o ponto deverá ser marcado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 65
 testRunner.And("o ponto deverá ter uma observação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
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
                RegistroPontosFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                RegistroPontosFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
