using ApiAlunos.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiAlunos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private List<Aluno> PegarDados()
        {
            // Criar uma lista de Alunos vazia para receber os dados do arquivo, desserializar o arquivo c:\temp\alunos.json para essa lista
            List<Aluno> listaAlunos = new();
            try
            {
                string dadosArquivo = System.IO.File.ReadAllText("c:\\temp\\alunos.json");
                listaAlunos = System.Text.Json.JsonSerializer.Deserialize<List<Aluno>>(dadosArquivo);
            }
            catch
            {
                // Ignorar erros e retornar lista vazia
            }
            return listaAlunos;
        }

        [HttpGet]
        public IEnumerable<Aluno> GetLista()
        {
            return PegarDados();
        }

        [HttpPost]
        public IActionResult CadastrarAluno(Aluno aluno)
        {
            List<Aluno> listaAlunos = PegarDados();

            // Adicionar o novo aluno na lista de alunos 
            listaAlunos.Add(aluno);

            SalvarDados(listaAlunos);

            return Ok();
        }

        private static void SalvarDados(List<Aluno> listaAlunos)
        {
            // Serializar a lista de alunos e salvar o arquivo c:\temp\alunos.json
            string dadosSerializados = System.Text.Json.JsonSerializer.Serialize(listaAlunos);

            // Salvar o arquivo c:\temp\alunos.json com os dados serializados    
            System.IO.File.WriteAllText("c:\\temp\\alunos.json", dadosSerializados);
        }
    }
}