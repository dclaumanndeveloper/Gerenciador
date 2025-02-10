using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GerenciadorTarefas.Models;
using System.IO;
using Newtonsoft.Json;

namespace GerenciadorTarefas.Controllers
{
    public class TarefasController : Controller
    {
        int id = 0;
        private readonly TarefaService _tarefaService;
        public TarefasController(TarefaService tarefaService){
            _tarefaService = tarefaService;
        }
        // GET: Tarefas
        public async Task<IActionResult> Index()
        {
             return View(await Task.Run(() => _tarefaService.ObterTodos()));
        }

        // GET: Tarefas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await Task.Run(() => _tarefaService.Obter(id));
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // GET: Tarefas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tarefas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarefa tarefa)
        {
            tarefa.ID = id+1;
            await Task.Run(() => _tarefaService.Adicionar(tarefa));
            return RedirectToAction("Index");
        }

        // GET: Tarefas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await Task.Run(() => _tarefaService.Obter(id));
            if (tarefa == null)
            {
                return NotFound();
            }
          
            return View(tarefa);
        }

        // POST: Tarefas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tarefa tarefa)
        {
            if (id != tarefa.ID)
            {
                return NotFound();
            }

            await Task.Run(() => _tarefaService.Atualizar(id, tarefa));
            return RedirectToAction("Index");
        }
     
        // GET: Tarefas/Delete/5
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await Task.Run(() => _tarefaService.Obter(id));
            if (tarefa == null)
            {
                return NotFound();
            }
          
            return View(tarefa);
        }
        // GET: Tarefas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await Task.Run(() => _tarefaService.Deletar(id));
            return RedirectToAction("Index");
        }
    }
}
