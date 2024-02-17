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
    public partial class GestaoContratosFeature : object, Xunit.IClassFixture<GestaoContratosFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "GestaoContratos.feature"
#line hidden
        
        public GestaoContratosFeature(GestaoContratosFeature.FixtureData fixtureData, MeuPonto_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt-br"), "Features", "Gestão Contratos", null, ProgrammingLanguage.CSharp, featureTags);
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
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador abre um contrato")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador abre um contrato")]
        [Xunit.TraitAttribute("Category", "main")]
        public void TrabalhadorAbreUmContrato()
        {
            string[] tagsOfScenario = new string[] {
                    "main"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador abre um contrato", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
 testRunner.When("o trabalhador iniciar uma abertura de contrato", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 10
 testRunner.Then("um contrato deverá ser criado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                            "nome",
                            "ativo",
                            "domingo",
                            "segunda",
                            "terça",
                            "quarta",
                            "quinta",
                            "sexta",
                            "sábado"});
                table4.AddRow(new string[] {
                            "Contrato A",
                            "True",
                            "00:00:00",
                            "08:00:00",
                            "08:00:00",
                            "08:00:00",
                            "08:00:00",
                            "08:00:00",
                            "00:00:00"});
#line 11
 testRunner.When("o trabalhador abrir o contrato como:", ((string)(null)), table4, "Quando ");
#line hidden
#line 14
 testRunner.Then("o nome do contrato deverá ser \'Contrato A\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 15
 testRunner.And("o contrato deverá ser ativo", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "dia semana",
                            "tempo"});
                table5.AddRow(new string[] {
                            "Sunday",
                            "00:00:00"});
                table5.AddRow(new string[] {
                            "Monday",
                            "08:00:00"});
                table5.AddRow(new string[] {
                            "Tuesday",
                            "08:00:00"});
                table5.AddRow(new string[] {
                            "Wednesday",
                            "08:00:00"});
                table5.AddRow(new string[] {
                            "Thursday",
                            "08:00:00"});
                table5.AddRow(new string[] {
                            "Friday",
                            "08:00:00"});
                table5.AddRow(new string[] {
                            "Saturday",
                            "00:00:00"});
#line 16
 testRunner.And("a jornada de trabalho semanal prevista no contrato deverá ser:", ((string)(null)), table5, "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador altera um contrato para corrigir um erro de digitação no nome")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador altera um contrato para corrigir um erro de digitação no nome")]
        [Xunit.TraitAttribute("Category", "main")]
        public void TrabalhadorAlteraUmContratoParaCorrigirUmErroDeDigitacaoNoNome()
        {
            string[] tagsOfScenario = new string[] {
                    "main"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador altera um contrato para corrigir um erro de digitação no nome", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 29
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 30
 testRunner.Given("que existe um contrato aberto \'Marcello - Particular\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 31
 testRunner.When("o trabalhador iniciar uma edição de contrato", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "nome"});
                table6.AddRow(new string[] {
                            "Marcelo - Particular"});
#line 32
 testRunner.And("o trabalhador alterar esse contrato para", ((string)(null)), table6, "E ");
#line hidden
#line 35
 testRunner.Then("o nome do contrato deverá ser \'Marcelo - Particular\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador inicia um novo contrato ativo")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador inicia um novo contrato ativo")]
        [Xunit.TraitAttribute("Category", "invariant")]
        public void TrabalhadorIniciaUmNovoContratoAtivo()
        {
            string[] tagsOfScenario = new string[] {
                    "invariant"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador inicia um novo contrato ativo", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
 testRunner.When("o trabalhador iniciar uma abertura de contrato", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 42
 testRunner.Then("um contrato deverá ser criado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 43
 testRunner.And("o contrato deverá ser ativo", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador inicia um novo contrato com uma jornada de trabalho prevista")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador inicia um novo contrato com uma jornada de trabalho prevista")]
        [Xunit.TraitAttribute("Category", "invariant")]
        public void TrabalhadorIniciaUmNovoContratoComUmaJornadaDeTrabalhoPrevista()
        {
            string[] tagsOfScenario = new string[] {
                    "invariant"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador inicia um novo contrato com uma jornada de trabalho prevista", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 48
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 49
 testRunner.When("o trabalhador iniciar uma abertura de contrato", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 50
 testRunner.Then("um contrato deverá ser criado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "dia semana",
                            "tempo"});
                table7.AddRow(new string[] {
                            "Sunday",
                            "00:00:00"});
                table7.AddRow(new string[] {
                            "Monday",
                            "08:00:00"});
                table7.AddRow(new string[] {
                            "Tuesday",
                            "08:00:00"});
                table7.AddRow(new string[] {
                            "Wednesday",
                            "08:00:00"});
                table7.AddRow(new string[] {
                            "Thursday",
                            "08:00:00"});
                table7.AddRow(new string[] {
                            "Friday",
                            "08:00:00"});
                table7.AddRow(new string[] {
                            "Saturday",
                            "00:00:00"});
#line 51
 testRunner.And("o contrato deverá prever a seguinte jornada de trabalho semanal:", ((string)(null)), table7, "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador abre um contrato com nome maior que 2 caractere")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador abre um contrato com nome maior que 2 caractere")]
        [Xunit.TraitAttribute("Category", "invariant")]
        [Xunit.TraitAttribute("Category", "basic")]
        public void TrabalhadorAbreUmContratoComNomeMaiorQue2Caractere()
        {
            string[] tagsOfScenario = new string[] {
                    "invariant",
                    "basic"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador abre um contrato com nome maior que 2 caractere", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 64
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 65
 testRunner.Given("que existe uma abertura de contrato em andamento", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
                TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                            "nome"});
                table8.AddRow(new string[] {
                            "Contrato A"});
#line 66
 testRunner.When("o trabalhador abrir o contrato como:", ((string)(null)), table8, "Quando ");
#line hidden
#line 69
 testRunner.Then("o contrato deverá ser aberto como esperado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador altera um contrato com nome maior que 2 caractere")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador altera um contrato com nome maior que 2 caractere")]
        [Xunit.TraitAttribute("Category", "invariant")]
        [Xunit.TraitAttribute("Category", "basic")]
        public void TrabalhadorAlteraUmContratoComNomeMaiorQue2Caractere()
        {
            string[] tagsOfScenario = new string[] {
                    "invariant",
                    "basic"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador altera um contrato com nome maior que 2 caractere", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 72
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 73
 testRunner.Given("que existe um contrato aberto \'Contrato Feito\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 74
 testRunner.And("que existe uma alteração desse contrato em andamento \'Contrato Feito\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                            "nome"});
                table9.AddRow(new string[] {
                            "Contrato A"});
#line 75
 testRunner.When("o trabalhador alterar esse contrato para", ((string)(null)), table9, "Quando ");
#line hidden
#line 78
 testRunner.Then("o contrato deverá ser alterado como esperado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador tenta abrir um contrato com nome menor que 3 caracteres")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador tenta abrir um contrato com nome menor que 3 caracteres")]
        [Xunit.TraitAttribute("Category", "invariant")]
        [Xunit.TraitAttribute("Category", "exception")]
        [Xunit.TraitAttribute("Category", "basic")]
        public void TrabalhadorTentaAbrirUmContratoComNomeMenorQue3Caracteres()
        {
            string[] tagsOfScenario = new string[] {
                    "invariant",
                    "exception",
                    "basic"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador tenta abrir um contrato com nome menor que 3 caracteres", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 81
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 82
 testRunner.Given("que existe uma abertura de contrato em andamento", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
                TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                            "nome"});
                table10.AddRow(new string[] {
                            "A"});
#line 83
 testRunner.When("o trabalhador tentar abrir um contrato como", ((string)(null)), table10, "Quando ");
#line hidden
#line 86
 testRunner.Then("a tentativa de abrir o contrato deverá falhar com um erro \"\'Nome\' deve ser maior " +
                        "ou igual a 3 caracteres.\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador tenta alterar um contrato com nome menor que 3 caracteres")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador tenta alterar um contrato com nome menor que 3 caracteres")]
        [Xunit.TraitAttribute("Category", "invariant")]
        [Xunit.TraitAttribute("Category", "exception")]
        [Xunit.TraitAttribute("Category", "basic")]
        public void TrabalhadorTentaAlterarUmContratoComNomeMenorQue3Caracteres()
        {
            string[] tagsOfScenario = new string[] {
                    "invariant",
                    "exception",
                    "basic"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador tenta alterar um contrato com nome menor que 3 caracteres", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 89
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 90
 testRunner.Given("que existe um contrato aberto \'Contrato Feito\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 91
 testRunner.And("que existe uma alteração desse contrato em andamento \'Contrato Feito\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
                TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                            "nome"});
                table11.AddRow(new string[] {
                            "B"});
#line 92
 testRunner.When("o trabalhador tentar alterar esse contrato para", ((string)(null)), table11, "Quando ");
#line hidden
#line 95
 testRunner.Then("a tentativa de alterar o contrato deverá falhar com um erro \"\'Nome\' deve ser maio" +
                        "r ou igual a 3 caracteres.\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador abre um contrato com nome menor que 36 caracteres")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador abre um contrato com nome menor que 36 caracteres")]
        [Xunit.TraitAttribute("Category", "invariant")]
        [Xunit.TraitAttribute("Category", "basic")]
        public void TrabalhadorAbreUmContratoComNomeMenorQue36Caracteres()
        {
            string[] tagsOfScenario = new string[] {
                    "invariant",
                    "basic"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador abre um contrato com nome menor que 36 caracteres", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 100
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 101
 testRunner.Given("que existe uma abertura de contrato em andamento", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
                TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                            "nome"});
                table12.AddRow(new string[] {
                            "Contrato A"});
#line 102
 testRunner.When("o trabalhador abrir o contrato como:", ((string)(null)), table12, "Quando ");
#line hidden
#line 105
 testRunner.Then("o contrato deverá ser aberto como esperado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador tenta abrir um contrato com nome maior que 35 caracteres")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador tenta abrir um contrato com nome maior que 35 caracteres")]
        [Xunit.TraitAttribute("Category", "invariant")]
        [Xunit.TraitAttribute("Category", "exception")]
        [Xunit.TraitAttribute("Category", "basic")]
        public void TrabalhadorTentaAbrirUmContratoComNomeMaiorQue35Caracteres()
        {
            string[] tagsOfScenario = new string[] {
                    "invariant",
                    "exception",
                    "basic"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador tenta abrir um contrato com nome maior que 35 caracteres", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 108
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 109
 testRunner.Given("que existe uma abertura de contrato em andamento", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
                TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                            "nome"});
                table13.AddRow(new string[] {
                            "Contrato de Trabalho Feito com uma Empresa do Ramo da Industria Farmacêutica do E" +
                                "stado do Rio de Janeiro"});
#line 110
 testRunner.When("o trabalhador tentar abrir um contrato como", ((string)(null)), table13, "Quando ");
#line hidden
#line 113
 testRunner.Then("a tentativa de abrir o contrato deverá falhar com um erro \"\'Nome\' deve ser menor " +
                        "ou igual a 35 caracteres.\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador abre um contrato com uma jornada de trabalho prevista de 40 horas sem" +
            "anais")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador abre um contrato com uma jornada de trabalho prevista de 40 horas sem" +
            "anais")]
        [Xunit.TraitAttribute("Category", "invariant")]
        public void TrabalhadorAbreUmContratoComUmaJornadaDeTrabalhoPrevistaDe40HorasSemanais()
        {
            string[] tagsOfScenario = new string[] {
                    "invariant"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador abre um contrato com uma jornada de trabalho prevista de 40 horas sem" +
                    "anais", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 118
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 119
 testRunner.Given("que a jornada de trabalho semanal é de \'Monday\' a \'Friday\' das \'09:00\' às \'18:00\'" +
                        " com \'01:00\' de almoço", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 120
 testRunner.When("o trabalhador abrir um contrato", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
                TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                            "dia semana",
                            "tempo"});
                table14.AddRow(new string[] {
                            "Sunday",
                            "00:00:00"});
                table14.AddRow(new string[] {
                            "Monday",
                            "08:00:00"});
                table14.AddRow(new string[] {
                            "Tuesday",
                            "08:00:00"});
                table14.AddRow(new string[] {
                            "Wednesday",
                            "08:00:00"});
                table14.AddRow(new string[] {
                            "Thursday",
                            "08:00:00"});
                table14.AddRow(new string[] {
                            "Friday",
                            "08:00:00"});
                table14.AddRow(new string[] {
                            "Saturday",
                            "00:00:00"});
#line 121
 testRunner.Then("a jornada de trabalho semanal prevista no contrato deverá ser:", ((string)(null)), table14, "Então ");
#line hidden
#line 130
 testRunner.And("o tempo total da jornada de trabalho semanal prevista no contrato deverá ser \'1.1" +
                        "6:00\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador abre um contrato com uma jornada de trabalho prevista de 44 horas sem" +
            "anais (incluindo sábado)")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador abre um contrato com uma jornada de trabalho prevista de 44 horas sem" +
            "anais (incluindo sábado)")]
        [Xunit.TraitAttribute("Category", "invariant")]
        public void TrabalhadorAbreUmContratoComUmaJornadaDeTrabalhoPrevistaDe44HorasSemanaisIncluindoSabado()
        {
            string[] tagsOfScenario = new string[] {
                    "invariant"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador abre um contrato com uma jornada de trabalho prevista de 44 horas sem" +
                    "anais (incluindo sábado)", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 133
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 134
 testRunner.Given("que a jornada de trabalho semanal é de \'Monday\' a \'Friday\' das \'09:00\' às \'18:00\'" +
                        " com \'01:00\' de almoço", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 135
 testRunner.And("que a jornada de trabalho de \'Saturday\' é das \'08:00\' às \'12:00\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 136
 testRunner.When("o trabalhador abrir um contrato", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
                TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                            "dia semana",
                            "tempo"});
                table15.AddRow(new string[] {
                            "Sunday",
                            "00:00:00"});
                table15.AddRow(new string[] {
                            "Monday",
                            "08:00:00"});
                table15.AddRow(new string[] {
                            "Tuesday",
                            "08:00:00"});
                table15.AddRow(new string[] {
                            "Wednesday",
                            "08:00:00"});
                table15.AddRow(new string[] {
                            "Thursday",
                            "08:00:00"});
                table15.AddRow(new string[] {
                            "Friday",
                            "08:00:00"});
                table15.AddRow(new string[] {
                            "Saturday",
                            "04:00:00"});
#line 137
 testRunner.Then("a jornada de trabalho semanal prevista no contrato deverá ser:", ((string)(null)), table15, "Então ");
#line hidden
#line 146
 testRunner.And("o tempo total da jornada de trabalho semanal prevista no contrato deverá ser \'1.2" +
                        "0:00\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador exclui um contrato que não era necessário")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador exclui um contrato que não era necessário")]
        [Xunit.TraitAttribute("Category", "main")]
        public void TrabalhadorExcluiUmContratoQueNaoEraNecessario()
        {
            string[] tagsOfScenario = new string[] {
                    "main"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador exclui um contrato que não era necessário", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 151
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 152
 testRunner.Given("que existe um contrato aberto \'Marcelo - Ateliex\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 153
 testRunner.When("o trabalhador excluir o contrato", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 154
 testRunner.Then("o contrato deverá ser excluído", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Trabalhador tenta excluir excluir um contrato com ponto(s) marcado(s)")]
        [Xunit.TraitAttribute("FeatureTitle", "Gestão Contratos")]
        [Xunit.TraitAttribute("Description", "Trabalhador tenta excluir excluir um contrato com ponto(s) marcado(s)")]
        [Xunit.TraitAttribute("Category", "exception")]
        [Xunit.TraitAttribute("Category", "wip")]
        public void TrabalhadorTentaExcluirExcluirUmContratoComPontoSMarcadoS()
        {
            string[] tagsOfScenario = new string[] {
                    "exception",
                    "wip"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Trabalhador tenta excluir excluir um contrato com ponto(s) marcado(s)", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 159
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 160
 testRunner.Given("que existe um contrato aberto \'Marcelo - Ateliex\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 162
 testRunner.When("o trabalhador excluir o contrato", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 163
 testRunner.Then("o contrato não deverá ser excluído", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
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
                GestaoContratosFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                GestaoContratosFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
