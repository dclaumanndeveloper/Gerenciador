using System.Collections.Generic;
using System.IO;
using GerenciadorTarefas.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace GerenciadorTarefas.Models;
public class TarefaService
    {
        private readonly string _arquivoJson = "tarefas.json";
        public List<Tarefa> ObterTodos()
        {
            if (!File.Exists(_arquivoJson))
            {
                return new List<Tarefa>();
            }

            string json = File.ReadAllText(_arquivoJson);
            return JsonConvert.DeserializeObject<List<Tarefa>>(json) ?? new List<Tarefa>();
        }
        public Tarefa Obter(int? id){
           string json = File.ReadAllText(_arquivoJson);
           var tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(json);
           List<Tarefa> registros  = tarefas.Where(t => t.ID == id).ToList();
           return registros[0];
        }
        public void Adicionar(Tarefa Tarefa)
        {
            var tarefas = ObterTodos();
            int proximoId = tarefas.Any() ? tarefas.Max(t => t.ID) + 1 : 1;
            Tarefa.ID = proximoId;
            tarefas.Add(Tarefa);
            Salvar(tarefas);
        }
        public void Atualizar(int id, Tarefa tarefa){
            string json = File.ReadAllText(_arquivoJson);
            var tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(json);

            var tarefaEncontrada = tarefas.FirstOrDefault(t => t.ID == id);

        if (tarefaEncontrada != null)
        {
            tarefaEncontrada.Titulo = tarefa.Titulo;
            tarefaEncontrada.Descricao = tarefa.Descricao;
            tarefaEncontrada.Status = tarefa.Status;

            string novoJson = JsonConvert.SerializeObject(tarefas, Formatting.Indented);
            File.WriteAllText("tarefas.json", novoJson);
        }
        }
         public void Deletar(int? id){
  
        string json = File.ReadAllText("tarefas.json");
        var tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(json);

        tarefas.RemoveAll(t => t.ID == id);

        string novoJson = JsonConvert.SerializeObject(tarefas, Formatting.Indented);
        File.WriteAllText("tarefas.json", novoJson);
    }
   private void Salvar(List<Tarefa> Tarefas)
        {
            string json = JsonConvert.SerializeObject(Tarefas, Formatting.Indented);
            File.WriteAllText(_arquivoJson, json);
        }
   
    }