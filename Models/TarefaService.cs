using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace GerenciadorTarefas.Models;

public class TarefaService
{
    private readonly string _arquivoJson = "tarefas.json";

    public async Task<List<Tarefa>> ObterTodos()
    {
        if (!File.Exists(_arquivoJson))
        {
            return new List<Tarefa>();
        }

        string json = await File.ReadAllTextAsync(_arquivoJson);
        return JsonSerializer.Deserialize<List<Tarefa>>(json) ?? new List<Tarefa>();
    }

    public async Task<Tarefa> Obter(int? id)
    {
        string json = await File.ReadAllTextAsync(_arquivoJson);
        var tarefas = JsonSerializer.Deserialize<List<Tarefa>>(json);
        List<Tarefa> registros = tarefas.Where(t => t.ID == id).ToList();
        return registros[0];
    }

    public async Task Adicionar(Tarefa tarefa)
    {
        var tarefas = await ObterTodos();
        int proximoId = tarefas.Any() ? tarefas.Max(t => t.ID) + 1 : 1;
        tarefa.ID = proximoId;
        tarefas.Add(tarefa);
        await Salvar(tarefas);
    }

    public async Task Atualizar(int id, Tarefa tarefa)
    {
        string json = await File.ReadAllTextAsync(_arquivoJson);
        var tarefas = JsonSerializer.Deserialize<List<Tarefa>>(json);

        var tarefaEncontrada = tarefas.FirstOrDefault(t => t.ID == id);

        if (tarefaEncontrada != null)
        {
            tarefaEncontrada.Titulo = tarefa.Titulo;
            tarefaEncontrada.Descricao = tarefa.Descricao;
            tarefaEncontrada.Status = tarefa.Status;

            string novoJson = JsonSerializer.Serialize(tarefas, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync("tarefas.json", novoJson);
        }
    }

    public async Task Deletar(int? id)
    {
        string json = await File.ReadAllTextAsync("tarefas.json");
        var tarefas = JsonSerializer.Deserialize<List<Tarefa>>(json);

        tarefas.RemoveAll(t => t.ID == id);

        string novoJson = JsonSerializer.Serialize(tarefas, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync("tarefas.json", novoJson);
    }

    private async Task Salvar(List<Tarefa> tarefas)
    {
        string json = JsonSerializer.Serialize(tarefas, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_arquivoJson, json);
    }
}
