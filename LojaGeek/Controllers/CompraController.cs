using Danzor.Print;
using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace LojaGeek.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult ComprasDoliente()
        {
            Cliente clienteLogado = (Cliente)Session["usuario"];
            var compras = DbFactory.Instance.CompraRespository.GetAllByClient(clienteLogado);
            return View(compras);
        }

        public ActionResult Compras()
        {
            var compras = DbFactory.Instance.CompraRespository.FindAll();
            return View(compras);
        }

        public ActionResult GerarNfe(Guid id)
        {
            Compra compra = DbFactory.Instance.CompraRespository.FindById(id);

            var relativePath = "D:\\Projetos\\LojaGeek\\LojaGeek\\NFE's\\" + compra.Id + ".xml";
            //var absolutePath = HttpContext.Server.MapPath(relativePath);
            if (!System.IO.File.Exists(relativePath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("D:\\Projetos\\LojaGeek\\LojaGeek\\NFE's\\NFEPadrao.xml");

                var data = DateTime.Now;

                XmlNode dtemi = xmlDoc.SelectSingleNode("//*[@id='data_emissao']");
                dtemi.InnerText = data.ToString("yyyy-MM-dd HH:mm:ss");

                XmlNode dtsaida = xmlDoc.SelectSingleNode("//*[@id='data_saida']");
                dtemi.InnerText = data.ToString("yyyy-MM-dd HH:mm:ss");

                XmlNode cpf = xmlDoc.SelectSingleNode("//*[@id='cpfCliente']");
                cpf.InnerText = compra.Cliente.Cpf;

                XmlNode nome = xmlDoc.SelectSingleNode("//*[@id='nomeCliente']");
                nome.InnerText = compra.Cliente.Nome + " "+ compra.Cliente.Sobrenome;

                XmlNode rua = xmlDoc.SelectSingleNode("//*[@id='ruaEnderecoCliente']");
                rua.InnerText = compra.EnderecoEntrega.Logradouro;

                XmlNode num = xmlDoc.SelectSingleNode("//*[@id='noEnderecoCliente']");
                num.InnerText = compra.EnderecoEntrega.Numero.ToString();

                XmlNode bairro = xmlDoc.SelectSingleNode("//*[@id='bairroEnderecoCliente']");
                bairro.InnerText = compra.EnderecoEntrega.Bairro;

                XmlNode cidade = xmlDoc.SelectSingleNode("//*[@id='cidadeEnderecoCliente']");
                cidade.InnerText = compra.EnderecoEntrega.Cidade;

                XmlNode uf = xmlDoc.SelectSingleNode("//*[@id='ufEnderecoCliente']");
                uf.InnerText = compra.EnderecoEntrega.Uf;

                XmlNode cep = xmlDoc.SelectSingleNode("//*[@id='cepEnderecoCliente']");
                cep.InnerText = compra.EnderecoEntrega.Cep;

                XmlNodeList node = xmlDoc.GetElementsByTagName("infNFe");
                XmlNode infNfe = node[0];

                XmlNode dest = xmlDoc.SelectSingleNode("//*[@id='dest']");

                var i = 0;
                var qtdTotalProduto = 0;
                var dTotalvICMS = 0.0;
                var totalPeso = 0.0;
                foreach (var produto in compra.Carrinho.Items)
                {
                    i++;
                    qtdTotalProduto += produto.Quantidade;

                    XmlNode det = xmlDoc.CreateElement("det", "http://www.portalfiscal.inf.br/nfe");
                    XmlAttribute detAttribute = xmlDoc.CreateAttribute("nItem");
                    detAttribute.Value = i.ToString();
                    det.Attributes.Append(detAttribute);
                    infNfe.InsertAfter(det,dest);

                    XmlNode prod = xmlDoc.CreateElement("prod", "http://www.portalfiscal.inf.br/nfe");
                    det.AppendChild(prod);

                    XmlNode cProd = xmlDoc.CreateElement("cProd", "http://www.portalfiscal.inf.br/nfe");
                    cProd.InnerText = produto.Id.ToString();
                    prod.AppendChild(cProd);

                    XmlNode cEAN = xmlDoc.CreateElement("cEAN", "http://www.portalfiscal.inf.br/nfe");
                    cEAN.InnerText = " ";
                    prod.AppendChild(cEAN);

                    XmlNode xProd = xmlDoc.CreateElement("xProd", "http://www.portalfiscal.inf.br/nfe");
                    xProd.InnerText = produto.Produto.Nome;
                    prod.AppendChild(xProd);

                    XmlNode NCM = xmlDoc.CreateElement("NCM", "http://www.portalfiscal.inf.br/nfe");
                    prod.AppendChild(NCM);

                    XmlNode CFOP = xmlDoc.CreateElement("CFOP", "http://www.portalfiscal.inf.br/nfe");
                    prod.AppendChild(CFOP);

                    XmlNode uCom = xmlDoc.CreateElement("uCom", "http://www.portalfiscal.inf.br/nfe");
                    uCom.InnerText = "UN";
                    prod.AppendChild(uCom);

                    XmlNode qCom = xmlDoc.CreateElement("qCom", "http://www.portalfiscal.inf.br/nfe");
                    qCom.InnerText = produto.Quantidade.ToString();
                    prod.AppendChild(qCom);

                    XmlNode vUnCom = xmlDoc.CreateElement("vUnCom", "http://www.portalfiscal.inf.br/nfe");
                    vUnCom.InnerText = produto.Produto.Preco.ToString();
                    prod.AppendChild(vUnCom);

                    XmlNode vProd = xmlDoc.CreateElement("vProd", "http://www.portalfiscal.inf.br/nfe");
                    vUnCom.InnerText = (produto.Produto.Preco * produto.Quantidade).ToString();
                    prod.AppendChild(vProd);

                    XmlNode cEANTrib = xmlDoc.CreateElement("cEANTrib", "http://www.portalfiscal.inf.br/nfe");
                    prod.AppendChild(cEANTrib);

                    XmlNode uTrib = xmlDoc.CreateElement("uTrib", "http://www.portalfiscal.inf.br/nfe");
                    prod.AppendChild(uTrib);

                    XmlNode qTrib = xmlDoc.CreateElement("qTrib", "http://www.portalfiscal.inf.br/nfe");
                    prod.AppendChild(qTrib);

                    XmlNode vUnTrib = xmlDoc.CreateElement("vUnTrib", "http://www.portalfiscal.inf.br/nfe");
                    prod.AppendChild(vUnTrib);

                    XmlNode indTot = xmlDoc.CreateElement("indTot", "http://www.portalfiscal.inf.br/nfe");
                    prod.AppendChild(indTot);

                    XmlNode med = xmlDoc.CreateElement("med", "http://www.portalfiscal.inf.br/nfe");
                    prod.AppendChild(med);

                    XmlNode nLote = xmlDoc.CreateElement("nLote", "http://www.portalfiscal.inf.br/nfe");
                    med.AppendChild(nLote);

                    XmlNode qLote = xmlDoc.CreateElement("qLote", "http://www.portalfiscal.inf.br/nfe");
                    med.AppendChild(qLote);

                    XmlNode dFab = xmlDoc.CreateElement("dFab", "http://www.portalfiscal.inf.br/nfe");
                    med.AppendChild(dFab);

                    XmlNode dVal = xmlDoc.CreateElement("dVal", "http://www.portalfiscal.inf.br/nfe");
                    med.AppendChild(dVal);

                    XmlNode vPMC = xmlDoc.CreateElement("vPMC", "http://www.portalfiscal.inf.br/nfe");
                    med.AppendChild(vPMC);

                    XmlNode imposto = xmlDoc.CreateElement("imposto", "http://www.portalfiscal.inf.br/nfe");
                    det.AppendChild(imposto);

                    XmlNode ICMS = xmlDoc.CreateElement("ICMS", "http://www.portalfiscal.inf.br/nfe");
                    imposto.AppendChild(ICMS);

                    XmlNode ICMS00 = xmlDoc.CreateElement("ICMS00", "http://www.portalfiscal.inf.br/nfe");
                    ICMS.AppendChild(ICMS00);

                    XmlNode orig = xmlDoc.CreateElement("orig", "http://www.portalfiscal.inf.br/nfe");
                    ICMS00.AppendChild(orig);

                    XmlNode CST = xmlDoc.CreateElement("CST", "http://www.portalfiscal.inf.br/nfe");
                    CST.InnerText = "0";
                    ICMS00.AppendChild(CST);

                    XmlNode modBC = xmlDoc.CreateElement("modBC", "http://www.portalfiscal.inf.br/nfe");
                    ICMS00.AppendChild(modBC);

                    XmlNode vBC = xmlDoc.CreateElement("vBC", "http://www.portalfiscal.inf.br/nfe");
                    vBC.InnerText = (produto.Produto.Preco * produto.Quantidade).ToString();
                    ICMS00.AppendChild(vBC);

                    XmlNode pICMS = xmlDoc.CreateElement("pICMS", "http://www.portalfiscal.inf.br/nfe");
                    pICMS.InnerText = "12.00";
                    ICMS00.AppendChild(CST);

                    XmlNode vICMS = xmlDoc.CreateElement("vICMS", "http://www.portalfiscal.inf.br/nfe");
                    vICMS.InnerText = (0.12* (produto.Produto.Preco * produto.Quantidade)).ToString();
                    ICMS00.AppendChild(vICMS);
                    dTotalvICMS += (0.12 * (produto.Produto.Preco * produto.Quantidade));

                    XmlNode PIS = xmlDoc.CreateElement("PIS", "http://www.portalfiscal.inf.br/nfe");
                    imposto.AppendChild(PIS);

                    XmlNode PISNT = xmlDoc.CreateElement("PISNT", "http://www.portalfiscal.inf.br/nfe");
                    PIS.AppendChild(PISNT);

                    XmlNode CST2 = xmlDoc.CreateElement("CST", "http://www.portalfiscal.inf.br/nfe");
                    CST2.InnerText = " ";
                    PISNT.AppendChild(CST2);

                    XmlNode COFINS = xmlDoc.CreateElement("COFINS", "http://www.portalfiscal.inf.br/nfe");
                    imposto.AppendChild(COFINS);

                    XmlNode COFINSNT = xmlDoc.CreateElement("COFINSNT", "http://www.portalfiscal.inf.br/nfe");
                    COFINS.AppendChild(COFINSNT);

                    XmlNode CST3 = xmlDoc.CreateElement("CST", "http://www.portalfiscal.inf.br/nfe");
                    CST3.InnerText = " ";
                    COFINSNT.AppendChild(CST3);

                    XmlNode infAdProd = xmlDoc.CreateElement("infAdProd", "http://www.portalfiscal.inf.br/nfe");
                    det.AppendChild(infAdProd);

                    totalPeso += produto.Produto.Peso;
                }

                XmlNode qVol = xmlDoc.SelectSingleNode("//*[@id='quantidade']");
                qVol.InnerText = qtdTotalProduto.ToString();

                XmlNode pesoL = xmlDoc.SelectSingleNode("//*[@id='pesoLiquido']");
                pesoL.InnerText = (totalPeso).ToString() + "g";

                XmlNode pesoB = xmlDoc.SelectSingleNode("//*[@id='pesoBruto']");
                pesoB.InnerText = ((qtdTotalProduto*50) + totalPeso).ToString()+"g";

                XmlNode obsCont = xmlDoc.SelectSingleNode("//*[@id='CodigoPedido']");
                obsCont.InnerText = compra.Id.ToString();

                XmlNode obsCont2 = xmlDoc.SelectSingleNode("//*[@id='CodigoPedido2']");
                obsCont2.InnerText = compra.Id.ToString();

                XmlNode dhRecbto = xmlDoc.SelectSingleNode("//*[@id='dhRecbto']");
                dhRecbto.InnerText = data.ToString("yyyy-MM-ddTHH:mm:ss");

                XmlNode totalvBC = xmlDoc.SelectSingleNode("//*[@id='vBC']");
                totalvBC.InnerText = (compra.Carrinho.ValorTotal).ToString();

                XmlNode tvProd = xmlDoc.SelectSingleNode("//*[@id='vProd']");
                tvProd.InnerText = (compra.Carrinho.ValorTotal).ToString();

                XmlNode TotalvICMS = xmlDoc.SelectSingleNode("//*[@id='vICMS']");
                TotalvICMS.InnerText = (dTotalvICMS).ToString();

                XmlNode vNF = xmlDoc.SelectSingleNode("//*[@id='vNF']");
                vNF.InnerText = (compra.Carrinho.ValorTotal + compra.Carrinho.ValorFrete).ToString();

                xmlDoc.Save(relativePath);
                compra.Nfe = "true";
                DbFactory.Instance.CompraRespository.SaveOrUpdate(compra);
            }
            return RedirectToAction("ExibirNfe", new { id=compra.Id});
        }

        public ActionResult ExibirNfe(Guid id)
        {
            var relativePath = "D:\\Projetos\\LojaGeek\\LojaGeek\\NFE's\\" + id + ".xml";
            //var absolutePath = HttpContext.Server.MapPath(relativePath);
            if (System.IO.File.Exists(relativePath))
            {
                var model = new DanzorPrintViewer(relativePath);
                return View("~/Views/Danfe/Danfe.cshtml", model);
            }
            else
            {
                return RedirectToAction("Compras");
            }
        }
    }
}