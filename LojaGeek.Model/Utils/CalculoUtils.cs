using LojaGeek.Model.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.Utils
{
    public class CalculoUtils
    {
        public static double CalcularPrecoReal(double precoFabrica)
        {
            double pis = (1.65/100)*precoFabrica;
            double cofins = (7.60/100)*precoFabrica;
            double icms = (12/100)*precoFabrica;
            double ipi = (15/100)*precoFabrica;
            double impostos = pis + cofins + icms + ipi;

            double lucro = (20 / 100)*precoFabrica;
            double despesasFixas = (3 / 100)*precoFabrica;

            double transporte = (1 / 100)*precoFabrica;
            double logisticaArmazenamento = (2 / 100)*precoFabrica;
            double comissao = (0.3 / 100) * precoFabrica;
            double despesasBancarias = (0.3 / 100) * precoFabrica;
            double inadimplencia = (0.5 / 100) * precoFabrica;
            double propaganda = (0.5 / 100) * precoFabrica;
            double servidores = (0.5 / 100) * precoFabrica;
            double RMA = (0.5 / 100) * precoFabrica;
            double despesasVariaveis = transporte + logisticaArmazenamento + comissao + despesasBancarias + inadimplencia + propaganda + servidores + RMA;

            double precoFinal = precoFabrica + impostos + lucro + despesasFixas + despesasVariaveis;
            precoFinal = Math.Round(precoFinal, 2);
            return precoFinal;
        }

        public static double NovoPrecoAoAtualizarEstoque(Produto produtoNovo, Produto produtoAntigo)
        {
            double novoPrecoFinal = CalcularPrecoReal(produtoNovo.Preco);
            double antigoPrecoFinal = produtoAntigo.Preco;

            int quantidadeNova = produtoNovo.Estoque;
            int quantidadeAntiga = produtoAntigo.Estoque;
            int quantidadeTotal = quantidadeNova + quantidadeAntiga;

            double antigoPrecoTotalFinal = antigoPrecoFinal * quantidadeAntiga;
            double novoPrecoTotalFinal = novoPrecoFinal * quantidadeNova;
            double precoTotal = antigoPrecoTotalFinal + novoPrecoTotalFinal;

            double novoPreco = precoTotal / quantidadeTotal;

            novoPreco = Math.Round(novoPreco, 2);
            return novoPreco;
        }
    }
}
