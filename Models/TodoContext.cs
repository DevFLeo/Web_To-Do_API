﻿using Microsoft.EntityFrameworkCore;

namespace TodoApiPortfolio.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) 
        {
        }
        public DbSet<Tarefa> Tarefas { get; set; } = null!;
    }
}