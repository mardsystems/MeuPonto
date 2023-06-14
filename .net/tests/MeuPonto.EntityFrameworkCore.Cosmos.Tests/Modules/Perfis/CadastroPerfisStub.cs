namespace MeuPonto.Modules.Perfis;

public static class CadastroPerfisStub
{
    public static Perfil ObtemPerfil()
    {
        var perfil = new Perfil
        {
            Nome = "Test user",
            Matricula = "0001",
            Id = Guid.NewGuid(),
            PartitionKey = "Test user",
            CreationDate = DateTime.Now,
            JornadaTrabalhoSemanalPrevista = new JornadaTrabalhoSemanal
            {
                Semana = new List<JornadaTrabalhoDiaria>(new[]{
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Monday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Tuesday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Wednesday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Thursday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Friday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Saturday,
                        Tempo = new TimeSpan(0,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Sunday,
                        Tempo = new TimeSpan(0,0,0)
                    }
                })
            }
        };

        return perfil;
    }

    public static Perfil ObtemPerfilComNome(string nome)
    {
        var perfil = new Perfil
        {
            Nome = nome,
            Matricula = "0001",
            Id = Guid.NewGuid(),
            PartitionKey = "Test user",
            CreationDate = DateTime.Now,
            JornadaTrabalhoSemanalPrevista = new JornadaTrabalhoSemanal
            {
                Semana = new List<JornadaTrabalhoDiaria>(new[]{
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Monday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Tuesday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Wednesday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Thursday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Friday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Saturday,
                        Tempo = new TimeSpan(0,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Sunday,
                        Tempo = new TimeSpan(0,0,0)
                    },
                })
            }
        };

        return perfil;
    }

    public static Perfil ObtemPerfilComMatricula(string matricula)
    {
        var perfil = new Perfil
        {
            Nome = "Test user",
            Matricula = matricula,
            Id = Guid.NewGuid(),
            PartitionKey = "Test user",
            CreationDate = DateTime.Now,
            JornadaTrabalhoSemanalPrevista = new JornadaTrabalhoSemanal
            {
                Semana = new List<JornadaTrabalhoDiaria>(new[]{
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Monday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Tuesday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Wednesday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Thursday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Friday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Saturday,
                        Tempo = new TimeSpan(0,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Sunday,
                        Tempo = new TimeSpan(0,0,0)
                    },
                })
            }
        };

        return perfil;
    }

    public static Perfil ObtemPerfilComJornadaTrabalhoSemanalPrevistaDe(IEnumerable<(DayOfWeek diaSemana, TimeSpan tempo)> semana)
    {
        var perfil = new Perfil
        {
            Nome = "Test user",
            Matricula = "0001",
            Id = Guid.NewGuid(),
            PartitionKey = "Test user",
            CreationDate = DateTime.Now
        };

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var jornadaTrabalhoDiaria = semana.SingleOrDefault(x => x.diaSemana == dayOfWeek);

            var i = (int)dayOfWeek;

            if (jornadaTrabalhoDiaria == default)
            {
                perfil.JornadaTrabalhoSemanalPrevista.Semana.Add(new JornadaTrabalhoDiaria
                {
                    DiaSemana = dayOfWeek,
                    Tempo = new TimeSpan(0, 0, 0)
                });
            }
            else
            {
                perfil.JornadaTrabalhoSemanalPrevista.Semana.Add(new JornadaTrabalhoDiaria
                {
                    DiaSemana = dayOfWeek,
                    Tempo = jornadaTrabalhoDiaria.tempo
                });
            }
        }

        //_db.Perfis.Add(perfil);
        //await _db.SaveChangesAsync();

        ////

        //var document = await _angleSharp.GetDocumentAsync("/");

        ////

        //var perfisAnchor = (IHtmlAnchorElement)document.QuerySelector("a.perfis");

        //perfisAnchor.Should().NotBeNull("a tela inicial deve ter um link para os perfis");

        //_scenario["perfisAnchor"] = perfisAnchor;

        ////

        //var marcacaoPontoAnchor = (IHtmlAnchorElement)document.QuerySelector("a.marcacao.ponto");

        //marcacaoPontoAnchor.Should().NotBeNull("a tela inicial deve ter um link de marcação de ponto");

        //_scenario["marcacaoPontoAnchor"] = marcacaoPontoAnchor;

        ////

        //var aberturaFolhaPontoAnchor = (IHtmlAnchorElement)document.QuerySelector("a.abertura.folha.ponto");

        //aberturaFolhaPontoAnchor.Should().NotBeNull("a tela inicial deve ter um link de abertura de folha de ponto");

        //_scenario["aberturaFolhaPontoAnchor"] = aberturaFolhaPontoAnchor;

        return perfil;
    }
}
