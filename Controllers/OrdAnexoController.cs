using Microsoft.AspNetCore.Mvc;
using SisEngeman.Data;
using SisEngeman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;

namespace SisEngeman.Controllers
{
    public class OrdAnexoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrdAnexoController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int codigoOS)
        {
            bool sucesso = true;
            string mensagem = "Download executado com sucesso.";
            List<OrdAnexoModel> anexos = _db.OrdAnexo.Where(x => x.codOrd == codigoOS).ToList();

            if (anexos.Count == 0)
            {
                mensagem = "OS não encontrada!";
                sucesso = false;
            }
            else if (anexos.Count == 1)
            {
                var item = anexos.First();
                try
                {
                    byte[] fileData = item.anexo;
                    string fileName = item.descricao.Substring(0, item.descricao.Length - 4) + ".zip";
                    return File(fileData, "application/zip", fileName);
                }
                catch (Exception ex)
                {
                    sucesso = false;
                    mensagem = $"Erro ao preparar o arquivo {item.descricao} para download: {ex.Message}";
                }
            }
            else
            {
                // Se houver mais de um anexo, zipar todos os arquivos em um único arquivo zip
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var arquivoZip = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                        {
                            foreach (var item in anexos)
                            {
                                var zipEntry = arquivoZip.CreateEntry(item.descricao.Substring(0, item.descricao.Length - 4) + ".zip", CompressionLevel.Fastest);
                                using (var zipEntryStream = zipEntry.Open())
                                {
                                    zipEntryStream.Write(item.anexo, 0, item.anexo.Length);
                                }
                            }
                        }

                        return File(memoryStream.ToArray(), "application/zip", "anexosOS" + "_" + codigoOS + ".zip");
                    }
                }
                catch (Exception ex)
                {
                    sucesso = false;
                    mensagem = $"Erro ao preparar os arquivos para download: {ex.Message}";
                }
            }

            // Usar TempData para passar a mensagem para a View
            TempData["Mensagem"] = mensagem;
            TempData["Sucesso"] = sucesso;

            // Redirecionar para uma página onde o alerta será exibido
            return RedirectToAction("MensagemResul");
        }

        public IActionResult MensagemResul()
        {
            return View();
        }

    }
}
