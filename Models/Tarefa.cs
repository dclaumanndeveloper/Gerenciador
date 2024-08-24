using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.Models;

public class Tarefa
{
    [Key]
    public int ID{get;set;}

    [Display(Name="Título")]
    public string Titulo{get;set;}
    [Display(Name="Descrição")]
    public string Descricao{get;set;}
    [Display(Name="Status da tarefa")]
    public string Status{get;set;}


}
