const API_CURSO_URL = "https://localhost:7017/api/Curso";
const API_PROFESSOR_URL = "https://localhost:7017/api/Professor";

document.addEventListener('DOMContentLoaded', () => {
    // --- SELEÇÃO DE ELEMENTOS ---
    const tabelaCorpo = document.getElementById('tabela-cursos-corpo');
    const inputPesquisa = document.getElementById('pesquisar-curso');
    const paginacaoContainer = document.getElementById('paginacao-container');
    
    const cadastrarModalEl = document.getElementById('cadastrarCursoModal');
    const cadastrarModal = new bootstrap.Modal(cadastrarModalEl);
    const editarModalEl = document.getElementById('editarCursoModal');
    const editarModal = new bootstrap.Modal(editarModalEl);

    // --- VARIÁVEIS DE ESTADO ---
    let todosOsCursos = [];
    let paginaAtual = 1;
    const limitePorPagina = 10;

    // --- FUNÇÕES ---

    function renderizarTabela(cursos) {
        tabelaCorpo.innerHTML = '';
        if (cursos.length === 0) {
            tabelaCorpo.innerHTML = `<tr><td colspan="4" class="text-center">Nenhum curso encontrado.</td></tr>`;
            return;
        }
        cursos.forEach(curso => {
            const tr = `
                <tr>
                    <td>${curso.id}</td>
                    <td>${curso.nome}</td>
                    <td class="text-center">${curso.cargaHoraria}h</td>
                    <td class="text-center table-actions">
                        <a href="curso-detalhes.php?id=${curso.id}" class="icon-info" title="Informações"><i class="fas fa-info-circle"></i></a>
                        <a href="#" class="icon-edit" title="Editar" data-id="${curso.id}"><i class="fas fa-edit"></i></a>
                        <a href="#" class="icon-delete" title="Deletar" data-id="${curso.id}"><i class="fas fa-trash-alt"></i></a>
                    </td>
                </tr>
            `;
            tabelaCorpo.insertAdjacentHTML('beforeend', tr);
        });
    }

    async function carregarCursos(pagina = 1) {
        try {
            const response = await fetch(`${API_CURSO_URL}/Obter_Todos?pagina=${pagina}&limite=${limitePorPagina}`);
            if (!response.ok) {
                const errorData = await response.json().catch(() => null);
                throw new Error(errorData?.Mensagem || `A API respondeu com status ${response.status}`);
            }
            const resultado = await response.json();
            
            todosOsCursos = resultado.cursos || [];
            renderizarTabela(todosOsCursos);
            renderizarPaginacao(resultado.paginaAtual || 1, resultado.totalDePaginas || 1);
        } catch (error) {
            console.error("Falha ao carregar cursos:", error);
            tabelaCorpo.innerHTML = `<tr><td colspan="4" class="text-center text-danger">Erro ao carregar dados: ${error.message}</td></tr>`;
        }
    }

    function renderizarPaginacao(pagAtual, totalPaginas) {
        paginacaoContainer.innerHTML = '';
        if (totalPaginas <= 1) return;
        const ul = document.createElement('ul');
        ul.className = 'pagination justify-content-center';
        for (let i = 1; i <= totalPaginas; i++) {
            const li = document.createElement('li');
            li.className = `page-item ${i === pagAtual ? 'active' : ''}`;
            const a = document.createElement('a');
            a.className = 'page-link'; a.href = '#'; a.textContent = i; a.dataset.pagina = i;
            li.appendChild(a);
            ul.appendChild(li);
        }
        paginacaoContainer.appendChild(ul);
    }
    
    async function carregarProfessoresNoModal(selectElementId, selectedProfessorId = null) {
        const selectProfessor = document.getElementById(selectElementId);
        try {
            const response = await fetch(`${API_PROFESSOR_URL}/Obter_Todos?limite=1000`);
            if (!response.ok) throw new Error('Falha ao carregar professores.');
            const resultado = await response.json();
            const professores = resultado.professores || resultado;

            selectProfessor.innerHTML = '<option value="">Selecione um professor</option>';
            professores.forEach(prof => {
                const option = document.createElement('option');
                option.value = prof.id;
                option.textContent = `${prof.nome} ${prof.sobrenome}`;
                if (prof.id == selectedProfessorId) { // Usar '==' para comparar string com número
                    option.selected = true;
                }
                selectProfessor.appendChild(option);
            });
        } catch (error) {
            console.error(error);
            selectProfessor.innerHTML = '<option value="">Erro ao carregar professores</option>';
        }
    }

    async function cadastrarCurso() {
        const nome = document.getElementById('nome').value;
        const descricao = document.getElementById('descricao').value;
        const cargaHoraria = parseInt(document.getElementById('cargaHoraria').value);
        const valor = parseFloat(document.getElementById('valor').value);
        const categoria = document.getElementById('categoria').value;
        const professorId = document.getElementById('professorId').value;
        
        if (!nome || !cargaHoraria || !valor || !categoria || !professorId) {
             Swal.fire('Atenção!', 'Todos os campos são obrigatórios.', 'warning');
             return;
        }

        const cursoData = { nome, descricao, cargaHoraria, valor, categoria, professorId };
        
        try {
            const response = await fetch(`${API_CURSO_URL}/Cadastrar_Curso`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(cursoData)
            });
            if (!response.ok) {
                const err = await response.json().catch(()=>null); 
                throw new Error(err?.Mensagem || 'Erro desconhecido');
            }
            Swal.fire('Sucesso!', 'Curso cadastrado com sucesso!', 'success');
            cadastrarModal.hide();
            document.getElementById('formCadastrarCurso').reset();
            await carregarCursos();
        } catch (error) {
            Swal.fire('Erro!', `Não foi possível cadastrar o curso: ${error.message}`, 'error');
        }
    }

    async function abrirModalEdicao(id) {
        try {
            const response = await fetch(`${API_CURSO_URL}/${id}/Obter_Curso`);
            if (!response.ok) {
                 const err = await response.json().catch(()=>null);
                 throw new Error(err?.Mensagem || 'Curso não encontrado');
            }
            const curso = await response.json();
            
            await carregarProfessoresNoModal('editProfessorId', curso.professorId);
            
            document.getElementById('editCursoId').value = curso.id;
            document.getElementById('editDescricao').value = curso.descricao;
            document.getElementById('editCargaHoraria').value = curso.cargaHoraria;
            document.getElementById('editValor').value = curso.valor;
            document.getElementById('editCategoria').value = curso.categoria;
            document.getElementById('editProfessorId').value = curso.professorId;
            document.getElementById('editAtivo').value = curso.ativo;
            
            editarModal.show();
        } catch (error) {
            Swal.fire('Erro!', `Não foi possível carregar os dados do curso: ${error.message}`, 'error');
        }
    }

    async function salvarAlteracoes() {
        const id = document.getElementById('editCursoId').value;
        const descricao = document.getElementById('editDescricao').value;
        const cargaHoraria = parseInt(document.getElementById('editCargaHoraria').value);
        const valor = parseFloat(document.getElementById('editValor').value);
        const categoria = document.getElementById('editCategoria').value;
        const professorId = document.getElementById('editProfessorId').value;
        const ativo = document.getElementById('editAtivo').value === 'true';

        const dados = { descricao, cargaHoraria, valor, categoria, professorId, ativo };

        try {
            const response = await fetch(`${API_CURSO_URL}/${id}/Atualizar_Curso`, {
                method: 'PATCH',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(dados)
            });
            if (!response.ok) {
                const err = await response.json().catch(()=>null); 
                throw new Error(err?.Mensagem || 'Erro desconhecido');
            }
            Swal.fire('Sucesso!', 'Curso atualizado com sucesso!', 'success');
            editarModal.hide();
            await carregarCursos(paginaAtual);
        } catch (error) {
            Swal.fire('Erro!', `Não foi possível atualizar o curso: ${error.message}`, 'error');
        }
    }

    function deletarCurso(id) {
    Swal.fire({
        title: 'Você tem certeza?',
        text: "Esta ação não pode ser revertida!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, deletar!',
        cancelButtonText: 'Cancelar'
    }).then(async (result) => {
        if (result.isConfirmed) {
            try {
                // CORREÇÃO APLICADA AQUI: 'Deletar Curso' -> 'Deletar_Curso'
                const response = await fetch(`${API_CURSO_URL}/${id}/Deletar_Curso`, { method: 'DELETE' });

                if (!response.ok) {
                    const err = await response.json().catch(()=>null);
                    throw new Error(err?.Mensagem || 'Erro desconhecido');
                }
                Swal.fire('Deletado!', 'O curso foi deletado com sucesso.', 'success');
                await carregarCursos(paginaAtual);
            } catch (error) {
                Swal.fire('Erro!', `Não foi possível deletar: ${error.message}`, 'error');
            }
        }
    });
}

    // --- EVENT LISTENERS ---
    document.getElementById('btnCadastrarCurso').addEventListener('click', cadastrarCurso);
    document.getElementById('btnSalvarEdicaoCurso').addEventListener('click', salvarAlteracoes);
    cadastrarModalEl.addEventListener('show.bs.modal', () => carregarProfessoresNoModal('professorId'));
    
    inputPesquisa.addEventListener('input', () => {
        const termo = inputPesquisa.value.toLowerCase();
        const filtrados = todosOsCursos.filter(c => c.nome.toLowerCase().includes(termo));
        renderizarTabela(filtrados);
    });

    tabelaCorpo.addEventListener('click', (event) => {
        const target = event.target.closest('a');
        if (!target) return;
        const id = target.dataset.id;
        if (target.classList.contains('icon-edit')) {
            event.preventDefault();
            abrirModalEdicao(id);
        } else if (target.classList.contains('icon-delete')) {
            event.preventDefault();
            deletarCurso(id);
        }
    });

    paginacaoContainer.addEventListener('click', (event) => {
        event.preventDefault();
        if (event.target.tagName === 'A' && event.target.dataset.pagina) {
            const novaPagina = parseInt(event.target.dataset.pagina);
            if (novaPagina !== paginaAtual) {
                paginaAtual = novaPagina;
                carregarCursos(paginaAtual);
            }
        }
    });

    // --- CARGA INICIAL ---
    carregarCursos(paginaAtual);
});