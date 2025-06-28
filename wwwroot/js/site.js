console.log('Ficheiro site.js carregado com sucesso!');

const uri = 'https://localhost:7253/api/tarefas'; // Lembre-se de verificar a sua porta
let tarefas = [];

const themeKey = 'theme-preference';

// --- 1. ENTRADA ---
document.addEventListener('DOMContentLoaded', () => {

    // --- LÓGICA DO TEMA ---
    const themeToggleBtn = document.getElementById('theme-toggle');
    applyTheme(getTheme());
    themeToggleBtn.addEventListener('click', toggleTheme);

    // --- LÓGICA DAS TAREFAS ---
    const botaoAdicionar = document.getElementById('btn-adicionar');
    botaoAdicionar.addEventListener('click', addTarefa);
    getTarefas();
});


// --- FUNÇÕES DO TEMA ---
function getTheme() {
    return localStorage.getItem(themeKey) || 'dark'; 
}

// Função para aplicar o tema na página e atualizar o ícone
function applyTheme(theme) {
    document.documentElement.setAttribute('data-bs-theme', theme);
    const themeIcon = document.getElementById('theme-icon');
    if (theme === 'dark') {
        themeIcon.className = 'bi bi-sun';
    } else {
        themeIcon.className = 'bi bi-moon-fill';
    }
}

// Função para alternar o tema quando o botão é clicado
function toggleTheme() {
    const currentTheme = getTheme();
    const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
    localStorage.setItem(themeKey, newTheme);
    applyTheme(newTheme); // Aplica o novo tema na página
}


// --- 2. FUNÇÕES DAS TAREFA ---

async function getTarefas() {
    try {
        const response = await fetch(uri);
        if (!response.ok) throw new Error('Erro ao obter tarefas.');
        tarefas = await response.json();
        displayTarefas();
    } catch (error) {
        console.error('Erro ao buscar tarefas:', error);
    }
}

async function addTarefa() {
    const inputNovaTarefa = document.getElementById('input-nova-tarefa');
    const tituloDaTarefa = inputNovaTarefa.value.trim();
    if (!tituloDaTarefa) {
        alert('Por favor, escreva o título da tarefa.');
        return;
    }
    const novaTarefa = { titulo: tituloDaTarefa, prioridade: 1 };
    try {
        const response = await fetch(uri, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(novaTarefa)
        });
        if (!response.ok) throw new Error('Erro ao adicionar a tarefa.');
        inputNovaTarefa.value = '';
        getTarefas();
    } catch (error) {
        console.error('Erro ao adicionar tarefa:', error);
    }
}

async function deleteTarefa(id) {
    if (!confirm('Tem a certeza de que quer apagar esta tarefa?')) return;
    try {
        const response = await fetch(`${uri}/${id}`, { method: 'DELETE' });
        if (!response.ok) throw new Error('Erro ao apagar a tarefa.');
        tarefas = tarefas.filter(t => t.id !== id);
        displayTarefas();
    } catch (error) {
        console.error('Erro ao apagar tarefa:', error);
    }
}

async function toggleStatus(id) {
    const tarefa = tarefas.find(t => t.id === id);
    if (!tarefa) return;
    tarefa.estaConcluida = !tarefa.estaConcluida;
    try {
        const response = await fetch(`${uri}/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(tarefa)
        });
        if (!response.ok) throw new Error('Erro ao atualizar o estado da tarefa.');
        displayTarefas();
    } catch (error) {
        console.error('Erro ao alterar o estado da tarefa:', error);
        tarefa.estaConcluida = !tarefa.estaConcluida; // Reverte se der erro
    }
}

function displayTarefas() {
    const listaDeTarefasElemento = document.getElementById('lista-de-tarefas');
    listaDeTarefasElemento.innerHTML = '';
    tarefas.forEach(tarefa => {
        let li = document.createElement('li');
        li.className = 'list-group-item d-flex justify-content-between align-items-center';
        li.innerHTML = `
            <div>
                <input class="form-check-input me-2" type="checkbox" onchange="toggleStatus(${tarefa.id})" ${tarefa.estaConcluida ? 'checked' : ''}>
                <span class="${tarefa.estaConcluida ? 'text-decoration-line-through' : ''}">${tarefa.titulo}</span>
            </div>
            <div>
                <span class="badge ${getPriorityBadgeClass(tarefa.prioridade)} me-2">${getPriorityText(tarefa.prioridade)}</span>
                <button class="btn btn-sm btn-outline-danger" onclick="deleteTarefa(${tarefa.id})">
                    <i class="bi bi-trash"></i>
                </button>
            </div>
        `;
        listaDeTarefasElemento.appendChild(li);
    });
}

function getPriorityText(prioridade) {
    switch (prioridade) {
        case 0: return 'Baixa';
        case 1: return 'Média';
        case 2: return 'Alta';
        default: return 'Desconhecida';
    }
}

function getPriorityBadgeClass(prioridade) {
    switch (prioridade) {
        case 0: return 'bg-success';
        case 1: return 'bg-warning text-dark';
        case 2: return 'bg-danger';
        default: return 'bg-secondary';
    }
}