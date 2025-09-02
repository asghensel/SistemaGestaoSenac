const API_ALUNO_URL = "https://localhost:7017/api/Aluno";
const API_CURSO_URL = "https://localhost:7017/api/Curso";
const API_MATRICULA_URL = "https://localhost:7017/api/Matricula";

document.addEventListener('DOMContentLoaded', () => {
    // --- SELEÇÃO DE ELEMENTOS ---
    const tabelaCorpo = document.getElementById('tabela-alunos-corpo');
    const inputPesquisa = document.getElementById('pesquisar-aluno');
    const paginacaoContainer = document.getElementById('paginacao-container-aluno');
    
    const cadastrarModal = new bootstrap.Modal(document.getElementById('cadastrarAlunoModal'));
    const editarModal = new bootstrap.Modal(document.getElementById('editarAlunoModal'));
    const matricularModal = new bootstrap.Modal(document.getElementById('matricularModal'));

    // --- VARIÁVEIS DE ESTADO ---
    let todosOsAlunos = []; // Guarda TODOS os alunos buscados da API
    let alunosFiltrados = []; // Guarda os alunos após a busca do usuário
    let paginaAtual = 1;
    const limitePorPagina = 10;

    /**
     * Renderiza a tabela com a página atual dos alunos filtrados.
     */
    function renderizarTabela(alunos) {
        tabelaCorpo.innerHTML = '';
        if (alunos.length === 0) {
             tabelaCorpo.innerHTML = `<tr><td colspan="4" class="text-center">Nenhum aluno encontrado.</td></tr>`;
             return;
        }

        // Lógica de paginação no cliente: calcula qual "fatia" da lista mostrar
        const inicio = (paginaAtual - 1) * limitePorPagina;
        const fim = inicio + limitePorPagina;
        const alunosDaPagina = alunos.slice(inicio, fim);

        alunosDaPagina.forEach(aluno => {
            const nomeCompleto = `${aluno.nome} ${aluno.sobrenome}`;
            const tr = `
                <tr>
                    <td>${aluno.id}</td>
                    <td>${aluno.nome}</td>
                    <td>${aluno.sobrenome}</td>
                    <td class="text-center table-actions">
                        <a href="#" class="icon-matricula" title="Matricular em Curso" data-id="${aluno.id}" data-nome="${nomeCompleto}"><i class="fas fa-graduation-cap"></i></a>
                        <a href="aluno-detalhes.php?id=${aluno.id}" class="icon-info" title="Informações"><i class="fas fa-info-circle"></i></a>
                        <a href="#" class="icon-edit" title="Editar" data-id="${aluno.id}"><i class="fas fa-edit"></i></a>
                        <a href="#" class="icon-delete" title="Deletar" data-id="${aluno.id}"><i class="fas fa-trash-alt"></i></a>
                    </td>
                </tr>
            `;
            tabelaCorpo.insertAdjacentHTML('beforeend', tr);
        });
    }
    
    /**
     * Carrega a lista COMPLETA de alunos da API.
     */
    async function carregarAlunos() {
        try {
            const response = await fetch(`${API_ALUNO_URL}/Obter_Todos`); // Busca todos os alunos
            if (!response.ok) {
                const errorData = await response.json().catch(() => null);
                throw new Error(errorData?.Mensagem || `A API respondeu com status ${response.status}`);
            }
            
            // A resposta é um array direto de alunos
            todosOsAlunos = await response.json(); 
            alunosFiltrados = todosOsAlunos; // Lista filtrada começa igual à lista completa
            
            paginaAtual = 1;
            renderizarPagina();

        } catch (error) {
            console.error("Falha ao carregar alunos:", error);
            tabelaCorpo.innerHTML = `<tr><td colspan="4" class="text-center text-danger">Erro ao carregar dados: ${error.message}</td></tr>`;
        }
    }

    /**
     * Renderiza os botões de paginação com base no total de itens.
     */
    function renderizarPaginacao(totalDeItens) {
        paginacaoContainer.innerHTML = '';
        const totalDePaginas = Math.ceil(totalDeItens / limitePorPagina);

        if (totalDePaginas <= 1) return;

        const ul = document.createElement('ul');
        ul.className = 'pagination justify-content-center';
        for (let i = 1; i <= totalDePaginas; i++) {
            const li = document.createElement('li');
            li.className = `page-item ${i === paginaAtual ? 'active' : ''}`;
            const a = document.createElement('a');
            a.className = 'page-link'; a.href = '#'; a.textContent = i; a.dataset.pagina = i;
            li.appendChild(a);
            ul.appendChild(li);
        }
        paginacaoContainer.appendChild(ul);
    }

    /**
     * Função central que atualiza a tabela e a paginação.
     */
    function renderizarPagina() {
        renderizarTabela(alunosFiltrados);
        renderizarPaginacao(alunosFiltrados.length);
    }

    async function cadastrarAluno() {
        const nome = document.getElementById('nome').value.trim();
        const sobrenome = document.getElementById('sobrenome').value.trim();
        const dataDeNascimento = document.getElementById('dataDeNascimento').value;
        const email = document.getElementById('email').value.trim();
        const telefone = document.getElementById('telefone').value.trim();

        if (!nome || !sobrenome || !email || !dataDeNascimento) {
            Swal.fire('Atenção!', 'Preencha os campos obrigatórios.', 'warning');
            return;
        }
        const alunoData = { nome, sobrenome, dataDeNascimento, email, telefone };
        try {
            const response = await fetch(`${API_ALUNO_URL}/Cadastrar_Aluno`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(alunoData)
            });
            if (!response.ok) {
                const err = await response.json().catch(()=>null);
                throw new Error(err?.Mensagem || 'Erro desconhecido');
            }
            Swal.fire('Sucesso!', 'Aluno cadastrado com sucesso!', 'success');
            cadastrarModal.hide();
            document.getElementById('formCadastrarAluno').reset();
            await carregarAlunos(); // Recarrega a lista completa
        } catch (error) {
            Swal.fire('Erro!', `Não foi possível cadastrar: ${error.message}`, 'error');
        }
    }

    async function abrirModalEdicao(id) {
        try {
            const response = await fetch(`${API_ALUNO_URL}/${id}/Obter_Aluno`);
            if (!response.ok) {
                 const err = await response.json().catch(()=>null);
                 throw new Error(err?.Mensagem || 'Aluno não encontrado');
            }
            const aluno = await response.json();
            
            document.getElementById('editAlunoId').value = aluno.id;
            document.getElementById('editEmail').value = aluno.email;
            document.getElementById('editTelefone').value = aluno.telefone;
            document.getElementById('editAtivo').value = aluno.ativo;
            
            editarModal.show();
        } catch (error) {
            Swal.fire('Erro!', `Não foi possível carregar os dados do aluno: ${error.message}`, 'error');
        }
    }

    async function salvarAlteracoes() {
        const id = document.getElementById('editAlunoId').value;
        const email = document.getElementById('editEmail').value.trim();
        const telefone = document.getElementById('editTelefone').value.trim();
        const ativo = document.getElementById('editAtivo').value === 'true';

        const dadosAtualizados = { email, telefone, ativo };

        try {
            const response = await fetch(`${API_ALUNO_URL}/${id}/Atualizar_Aluno`, {
                method: 'PATCH',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(dadosAtualizados)
            });
            if (!response.ok) {
                const err = await response.json().catch(()=>null);
                throw new Error(err?.Mensagem || 'Erro desconhecido');
            }
            Swal.fire('Sucesso!', 'Aluno atualizado com sucesso!', 'success');
            editarModal.hide();
            await carregarAlunos();
        } catch (error) {
            Swal.fire('Erro!', `Não foi possível atualizar: ${error.message}`, 'error');
        }
    }

    function deletarAluno(id) {
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
                    const response = await fetch(`${API_ALUNO_URL}/${id}/Deletar_Aluno`, { method: 'DELETE' });
                    if (!response.ok) {
                        const err = await response.json().catch(()=>null);
                        throw new Error(err?.Mensagem || 'Erro desconhecido');
                    }
                    Swal.fire('Deletado!', 'O aluno foi deletado.', 'success');
                    await carregarAlunos();
                } catch (error) {
                    Swal.fire('Erro!', `Não foi possível deletar: ${error.message}`, 'error');
                }
            }
        });
    }
    
    async function abrirModalMatricula(alunoId, alunoNome) {
        document.getElementById('matricularModalLabel').textContent = `Matricular ${alunoNome}`;
        document.getElementById('matricularAlunoId').value = alunoId;
        const listaCursosAtuaisEl = document.getElementById('listaCursosAtuais');
        const selectCursoEl = document.getElementById('selectCurso');
        listaCursosAtuaisEl.innerHTML = '<li class="list-group-item">Carregando...</li>';
        selectCursoEl.innerHTML = '<option>Carregando...</option>';
        matricularModal.show();
        try {
            const [cursosResponse, matriculasResponse] = await Promise.all([
                fetch(`${API_CURSO_URL}/Obter_Todos`),
                fetch(`${API_MATRICULA_URL}/Aluno/${alunoId}`)
            ]);
            if (!cursosResponse.ok || !matriculasResponse.ok) throw new Error('Falha ao buscar dados para matrícula.');
            const todosOsCursos = await cursosResponse.json();
            const cursosMatriculados = await matriculasResponse.json();
            const idsCursosMatriculados = cursosMatriculados.map(m => m.cursoId);
            listaCursosAtuaisEl.innerHTML = '';
            const cursosAtuaisNomes = todosOsCursos.filter(c => idsCursosMatriculados.includes(c.id));
            if (cursosAtuaisNomes.length > 0) {
                cursosAtuaisNomes.forEach(curso => {
                    listaCursosAtuaisEl.insertAdjacentHTML('beforeend', `<li class="list-group-item">${curso.nome}</li>`);
                });
            } else {
                listaCursosAtuaisEl.innerHTML = '<li class="list-group-item">Nenhuma matrícula encontrada.</li>';
            }
            selectCursoEl.innerHTML = '<option value="">Selecione para matricular</option>';
            const cursosDisponiveis = todosOsCursos.filter(c => !idsCursosMatriculados.includes(c.id));
            cursosDisponiveis.forEach(curso => {
                selectCursoEl.insertAdjacentHTML('beforeend', `<option value="${curso.id}">${curso.nome}</option>`);
            });
        } catch (error) {
            Swal.fire('Erro!', `Não foi possível carregar os dados: ${error.message}`, 'error');
            matricularModal.hide();
        }
    }
    
    async function matricularAluno() {
        const alunoId = document.getElementById('matricularAlunoId').value;
        const cursoId = document.getElementById('selectCurso').value;
        if (!cursoId) {
            Swal.fire('Atenção!', 'Por favor, selecione um curso.', 'warning');
            return;
        }
        try {
            const response = await fetch(`${API_MATRICULA_URL}`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ alunoId: parseInt(alunoId), cursoId: parseInt(cursoId) })
            });
            if (!response.ok) {
                const err = await response.json();
                throw new Error(err.Mensagem || 'Erro desconhecido');
            }
            Swal.fire('Sucesso!', 'Matrícula realizada com sucesso!', 'success');
            matricularModal.hide();
        } catch (error) {
            Swal.fire('Erro!', `Não foi possível realizar a matrícula: ${error.message}`, 'error');
        }
    }

    // --- EVENT LISTENERS ---
    
    document.getElementById('btnCadastrar').addEventListener('click', cadastrarAluno);
    document.getElementById('btnSalvarEdicao').addEventListener('click', salvarAlteracoes);
    document.getElementById('btnConfirmarMatricula').addEventListener('click', matricularAluno);

    inputPesquisa.addEventListener('input', () => {
        const termo = inputPesquisa.value.toLowerCase();
        alunosFiltrados = todosOsAlunos.filter(aluno => 
            `${aluno.nome} ${aluno.sobrenome}`.toLowerCase().includes(termo)
        );
        paginaAtual = 1;
        renderizarPagina();
    });

    tabelaCorpo.addEventListener('click', (event) => {
        const target = event.target.closest('a');
        if (!target) return;
        const id = target.dataset.id;
        const nome = target.dataset.nome;
        if (target.classList.contains('icon-matricula')) {
            event.preventDefault();
            abrirModalMatricula(id, nome);
        }
        else if (target.classList.contains('icon-edit')) {
            event.preventDefault();
            abrirModalEdicao(id);
        } else if (target.classList.contains('icon-delete')) {
            event.preventDefault();
            deletarAluno(id);
        }
    });
    
    paginacaoContainer.addEventListener('click', (event) => {
        event.preventDefault();
        if (event.target.tagName === 'A' && event.target.dataset.pagina) {
            paginaAtual = parseInt(event.target.dataset.pagina);
            renderizarPagina();
        }
    });

    // --- CARGA INICIAL ---
    carregarAlunos();
});