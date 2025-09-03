const API_CURSO_URL = "https://localhost:7017/api/Curso";
const API_PROFESSOR_URL = "https://localhost:7017/api/Professor";

document.addEventListener('DOMContentLoaded', () => {
   
    const tabelaCorpo = document.getElementById('tabela-cursos-corpo');
    const inputPesquisa = document.getElementById('pesquisar-curso');
    const paginacaoContainer = document.getElementById('paginacao-container');
    
    const cadastrarModalEl = document.getElementById('cadastrarCursoModal');
    const cadastrarModal = new bootstrap.Modal(cadastrarModalEl);
    const editarModalEl = document.getElementById('editarCursoModal');
    const editarModal = new bootstrap.Modal(editarModalEl);

    
    let todosOsCursos = [];
    let cursosFiltrados = [];
    let paginaAtual = 1;
    const limitePorPagina = 10;

   

    function renderizarTabela(cursos) {
        tabelaCorpo.innerHTML = '';
        if (cursos.length === 0) {
            tabelaCorpo.innerHTML = `<tr><td colspan="4" class="text-center">Nenhum curso encontrado.</td></tr>`;
            return;
        }

        const inicio = (paginaAtual - 1) * limitePorPagina;
        const fim = inicio + limitePorPagina;
        const cursosDaPagina = cursos.slice(inicio, fim);

        cursosDaPagina.forEach(curso => {
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
            cursosFiltrados = todosOsCursos;
            
            renderizarPagina();
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
    
    function renderizarPagina() {
        renderizarTabela(cursosFiltrados);
        
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
                if (prof.id == selectedProfessorId) {
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

    


    document.getElementById('btnCadastrarCurso').addEventListener('click', cadastrarCurso);
    
    cadastrarModalEl.addEventListener('show.bs.modal', () => carregarProfessoresNoModal('professorId'));
    
    inputPesquisa.addEventListener('input', () => {
        const termo = inputPesquisa.value.toLowerCase();
      
        const cursosVisiveis = todosOsCursos.filter(c => c.nome.toLowerCase().includes(termo));
        renderizarTabela(cursosVisiveis);
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

   
    carregarCursos(paginaAtual);
});