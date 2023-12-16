namespace MeuPonto.Pages.Shared;

public class PaginationModel
{
    public int PaginaAtual { get; set; }

    public bool PrimeiroItemPagina { get; set; }

    public bool ItemPaginaContinuacaoInicial { get; set; }

    public bool ItemPaginaInicial { get; set; }

    public int? PaginaInicial { get; set; }

    public bool ItemPaginaMedio { get; set; }

    public int? PaginaMedia { get; set; }

    public bool ItemPaginaFinal { get; set; }

    public int? PaginaFinal { get; set; }

    public bool ItemPaginaContinuacaoFinal { get; set; }

    public bool UltimoItemPagina { get; set; }

    public int? TamanhoPagina { get; set; }

    public int TotalPaginas { get; set; }

    public PaginationModel(int totalRegistros, int paginaAtual)
    {
        PaginaAtual = paginaAtual;

        TamanhoPagina = TamanhoPagina ?? 10;

        int totalInteiroPaginas = totalRegistros / TamanhoPagina.Value;

        int restoPaginas = totalRegistros % TamanhoPagina.Value;

        if (restoPaginas > 0)
        {
            TotalPaginas = totalInteiroPaginas + 1;
        }
        else
        {
            TotalPaginas = totalInteiroPaginas;
        }

        if (TotalPaginas == 0)
        {

        }
        else if (TotalPaginas == 1)
        {


        }
        else if (TotalPaginas == 2)
        {
            PrimeiroItemPagina = false;

            ItemPaginaContinuacaoInicial = false;

            ItemPaginaInicial = true;

            PaginaInicial = 1;

            ItemPaginaMedio = false;

            PaginaMedia = null;

            ItemPaginaFinal = true;

            PaginaFinal = 2;

            ItemPaginaContinuacaoFinal = false;

            UltimoItemPagina = false;
        }
        else if (TotalPaginas == 3)
        {
            PrimeiroItemPagina = false;

            ItemPaginaContinuacaoInicial = false;

            ItemPaginaInicial = true;

            PaginaInicial = 1;

            ItemPaginaMedio = true;

            PaginaMedia = 2;

            ItemPaginaFinal = true;

            PaginaFinal = 3;

            ItemPaginaContinuacaoFinal = false;

            UltimoItemPagina = false;
        }
        else
        {
            PrimeiroItemPagina = true;

            ItemPaginaInicial = true;

            ItemPaginaMedio = true;

            if (PaginaAtual > 2)
            {
                if (PaginaAtual > TotalPaginas - 2)
                {
                    ItemPaginaContinuacaoInicial = true;

                    ItemPaginaContinuacaoFinal = false;

                    PaginaInicial = TotalPaginas - 2;

                    PaginaMedia = TotalPaginas - 1;

                    PaginaFinal = TotalPaginas;
                }
                else
                {
                    ItemPaginaContinuacaoInicial = true;

                    ItemPaginaContinuacaoFinal = true;

                    PaginaInicial = PaginaAtual - 1;

                    PaginaMedia = PaginaAtual;

                    PaginaFinal = PaginaAtual + 1;
                }
            }
            else
            {
                ItemPaginaContinuacaoInicial = false;

                ItemPaginaContinuacaoFinal = true;

                PaginaInicial = 1;

                PaginaMedia = 2;

                PaginaFinal = 3;
            }

            ItemPaginaFinal = true;

            UltimoItemPagina = true;
        }
    }
}
