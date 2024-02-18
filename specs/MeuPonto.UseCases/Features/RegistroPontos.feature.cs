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
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Trabalhador marca os pontos de entrada e saída do expediente")]
        [Xunit.TraitAttribute("FeatureTitle", "Registro Pontos")]
        [Xunit.TraitAttribute("Description", "Trabalhador marca os pontos de entrada e saída do expediente")]
        [Xunit.TraitAttribute("Category", "main")]
        [Xunit.InlineDataAttribute("27/11/2022 09:14", "Marcelo - Ateliex", "Entrada", new string[0])]
        [Xunit.InlineDataAttribute("27/11/2022 18:05", "Marcelo - Ateliex", "Saida", new string[0])]
        public void TrabalhadorMarcaOsPontosDeEntradaESaidaDoExpediente(string dataHora, string contrato, string momentoId, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "main"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("data/hora", dataHora);
            argumentsOfScenario.Add("contrato", contrato);
            argumentsOfScenario.Add("momento id", momentoId);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador marca os pontos de entrada e saída do expediente", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 8
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 9
 testRunner.Given(string.Format("que a data/hora do relógio é \'{0}\'", dataHora), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 10
 testRunner.And(string.Format("que existe um contrato aberto \'{0}\'", contrato), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 11
 testRunner.When("o trabalhador iniciar uma marcação de ponto", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 12
 testRunner.Then("um ponto deverá ser criado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
                TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                            "data/hora",
                            "contrato",
                            "momento id"});
                table18.AddRow(new string[] {
                            string.Format("{0}", dataHora),
                            string.Format("{0}", contrato),
                            string.Format("{0}", momentoId)});
#line 13
 testRunner.When("o trabalhador marcar o ponto como:", ((string)(null)), table18, "Quando ");
#line hidden
#line 16
 testRunner.Then(string.Format("a data do ponto deverá ser \'{0}\'", dataHora), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 17
 testRunner.And(string.Format("o ponto deverá ser qualificado pelo contrato \'{0}\'", contrato), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 18
 testRunner.And(string.Format("o momento do ponto deverá ser de \'{0}\'", momentoId), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 19
 testRunner.But("o ponto não deverá indicar que foi uma pausa", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Mas ");
#line hidden
#line 20
 testRunner.And("o ponto não deverá indicar que foi estimado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 21
 testRunner.And("o ponto não deverá ter uma observação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador qualifica um ponto com um contrato")]
        [Xunit.TraitAttribute("FeatureTitle", "Registro Pontos")]
        [Xunit.TraitAttribute("Description", "Trabalhador qualifica um ponto com um contrato")]
        [Xunit.TraitAttribute("Category", "invariant")]
        public void TrabalhadorQualificaUmPontoComUmContrato()
        {
            string[] tagsOfScenario = new string[] {
                    "invariant"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador qualifica um ponto com um contrato", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 31
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 32
 testRunner.Given("que existe um contrato aberto \'Marcelo - Ateliex\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 33
 testRunner.And("que existe uma marcacao de ponto em andamento", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
                TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                            "contrato"});
                table19.AddRow(new string[] {
                            "Marcelo - Ateliex"});
#line 34
 testRunner.When("o trabalhador marcar o ponto como:", ((string)(null)), table19, "Quando ");
#line hidden
#line 37
 testRunner.Then("o ponto deverá ser registrado como esperado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador deixa de qualificar um ponto com um contrato")]
        [Xunit.TraitAttribute("FeatureTitle", "Registro Pontos")]
        [Xunit.TraitAttribute("Description", "Trabalhador deixa de qualificar um ponto com um contrato")]
        [Xunit.TraitAttribute("Category", "invariant")]
        public void TrabalhadorDeixaDeQualificarUmPontoComUmContrato()
        {
            string[] tagsOfScenario = new string[] {
                    "invariant"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador deixa de qualificar um ponto com um contrato", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 40
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 41
 testRunner.Given("que existe um contrato aberto \'Marcelo - Ateliex\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 42
 testRunner.And("que existe uma marcacao de ponto em andamento", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
                TechTalk.SpecFlow.Table table20 = new TechTalk.SpecFlow.Table(new string[] {
                            "contrato"});
                table20.AddRow(new string[] {
                            "<null>"});
#line 43
 testRunner.When("o trabalhador tentar marcar o ponto como:", ((string)(null)), table20, "Quando ");
#line hidden
#line 46
 testRunner.Then("a tentativa de marcar o ponto deverá falhar com um erro \"\'Contrato\' deve ser info" +
                        "rmado.\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Trabalhador marca os pontos de pausa do expediente")]
        [Xunit.TraitAttribute("FeatureTitle", "Registro Pontos")]
        [Xunit.TraitAttribute("Description", "Trabalhador marca os pontos de pausa do expediente")]
        [Xunit.TraitAttribute("Category", "alter")]
        [Xunit.InlineDataAttribute("Marcelo - Ateliex", "Saida", "Almoco", new string[0])]
        [Xunit.InlineDataAttribute("Marcelo - Ateliex", "Entrada", "Almoco", new string[0])]
        public void TrabalhadorMarcaOsPontosDePausaDoExpediente(string contrato, string momentoId, string pausaId, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "alter"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("contrato", contrato);
            argumentsOfScenario.Add("momento id", momentoId);
            argumentsOfScenario.Add("pausa id", pausaId);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador marca os pontos de pausa do expediente", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 51
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 52
 testRunner.Given(string.Format("que existe um contrato aberto \'{0}\'", contrato), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 53
 testRunner.When("o trabalhador iniciar uma marcação de ponto", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
                TechTalk.SpecFlow.Table table21 = new TechTalk.SpecFlow.Table(new string[] {
                            "contrato",
                            "momento id",
                            "pausa id"});
                table21.AddRow(new string[] {
                            string.Format("{0}", contrato),
                            string.Format("{0}", momentoId),
                            string.Format("{0}", pausaId)});
#line 54
 testRunner.And("o trabalhador marcar o ponto como:", ((string)(null)), table21, "E ");
#line hidden
#line 58
 testRunner.Then(string.Format("o momento do ponto deverá ser de \'{0}\'", momentoId), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 59
 testRunner.And(string.Format("a pausa do ponto deverá ser \'{0}\'", pausaId), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador marca o ponto justificando porque chegou atrasado")]
        [Xunit.TraitAttribute("FeatureTitle", "Registro Pontos")]
        [Xunit.TraitAttribute("Description", "Trabalhador marca o ponto justificando porque chegou atrasado")]
        [Xunit.TraitAttribute("Category", "alter")]
        public void TrabalhadorMarcaOPontoJustificandoPorqueChegouAtrasado()
        {
            string[] tagsOfScenario = new string[] {
                    "alter"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador marca o ponto justificando porque chegou atrasado", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 69
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 70
 testRunner.When("o trabalhador iniciar uma marcação de ponto", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 71
 testRunner.And("o trabalhador marcar o ponto com a seguinte observação:", "Hoje o trânsito estava lento.", ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 75
 testRunner.Then("a observação do ponto deverá ser:", "Hoje o trânsito estava lento.", ((TechTalk.SpecFlow.Table)(null)), "Então ");
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
