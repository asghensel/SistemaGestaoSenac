
const API_URL = "https://localhost:7017/api/Professor";

document.addEventListener('DOMContentLoaded', () => {
  
    const tabelaCorpo = document.getElementById('tabela-professores-corpo');
    const inputPesquisa = document.getElementById('pesquisar-professor');
    const paginacaoContainer = document.getElementById('paginacao-container');
    const btnCadastrar = document.getElementById('btnCadastrarProfessor');
    
   
    const cadastrarModal = new bootstrap.Modal(document.getElementById('cadastrarProfessorModal'));
    const editarModal = new bootstrap.Modal(document.getElementById('editarProfessorModal'));


    let todosOsProfessores = [];
    let paginaAtual = 1;
    const limitePorPagina = 10;

    

    function renderizarTabela(professores) {
        tabelaCorpo.innerHTML = '';
        if (professores.length === 0) {
            tabelaCorpo.innerHTML = `<tr><td colspan="4" class="text-center">Nenhum professor encontrado.</td></tr>`;
            return;
        }
        professores.forEach(professor => {
            const tr = `
                <tr>
                    <td>${professor.id}</td>
                    <td>${professor.nome}</td>
                    <td>${professor.sobrenome}</td>
                    <td class="text-center table-actions">
                        <a href="professor-detalhes.php?id=${professor.id}" class="icon-info" title="Informações">
                            <i class="fas fa-info-circle"></i>
                        </a>
                        <a href="#" class="icon-edit" title="Editar" data-id="${professor.id}"><i class="fas fa-edit"></i></a>
                        <a href="#" class="icon-delete" title="Deletar" data-id="${professor.id}"><i class="fas fa-trash-alt"></i></a>
                    </td>
                </tr>
            `;
            tabelaCorpo.insertAdjacentHTML('beforeend', tr);
        });
    }

    async function carregarProfessores(pagina = 1) {
        try {
            const response = await fetch(`${API_URL}/Obter_Todos?pagina=${pagina}&limite=${limitePorPagina}`);
            if (!response.ok) throw new Error('Falha ao carregar dados da API.');
            
            const resultado = await response.json();
            todosOsProfessores = resultado.professores || resultado; 
            renderizarTabela(todosOsProfessores);
            renderizarPaginacao(resultado.paginaAtual || 1, resultado.totalDePaginas || 1);
        } catch (error) {
            console.error("Falha ao carregar professores:", error);
            tabelaCorpo.innerHTML = `<tr><td colspan="4" class="text-center text-danger">Erro ao carregar dados. A API está rodando?</td></tr>`;
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

    async function cadastrarProfessor() {
        const nome = document.getElementById('nome').value.trim();
        const sobrenome = document.getElementById('sobrenome').value.trim();
        const dataNascimento = document.getElementById('dataNascimento').value;
        const email = document.getElementById('email').value.trim();
        const telefone = document.getElementById('telefone').value.trim();
        const formacao = document.getElementById('formacao').value.trim();
        const dataContratacao = document.getElementById('dataContratacao').value;

        const professorData = { nome, sobrenome, dataNascimento, email, telefone, formacao, dataContratacao, ativo: true };

        try {
            const response = await fetch(`${API_URL}/Cadastrar_Professor`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(professorData)
            });
            if (!response.ok) {
                const err = await response.json();
                throw new Error(err.Mensagem || 'Erro desconhecido');
            }
            Swal.fire('Sucesso!', 'Professor cadastrado!', 'success');
            cadastrarModal.hide();
            document.getElementById('formCadastrarProfessor').reset();
            await carregarProfessores();
        } catch (error) {
            Swal.fire('Erro!', `Não foi possível cadastrar: ${error.message}`, 'error');
        }
    }

    
  
    async function abrirModalEdicao(id) {
        try {
            const response = await fetch(`${API_URL}/${id}/Obter_Professor`);
            if (!response.ok) {
                 const err = await response.json();
                 throw new Error(err.Mensagem || 'Professor não encontrado');
            }
            const professor = await response.json();

            document.getElementById('editProfessorId').value = professor.id;
            document.getElementById('editEmail').value = professor.email;
            document.getElementById('editTelefone').value = professor.telefone;
            document.getElementById('editFormacao').value = professor.formacao;
            document.getElementById('editAtivo').value = professor.ativo;
            editarModal.show();
        } catch (error) {
            Swal.fire('Erro!', `Não foi possível carregar os dados: ${error.message}`, 'error');
        }
    }

   
    async function salvarAlteracoes() {
        const id = document.getElementById('editProfessorId').value;
        const email = document.getElementById('editEmail').value.trim();
        const telefone = document.getElementById('editTelefone').value.trim();
        const formacao = document.getElementById('editFormacao').value.trim();
        const ativo = document.getElementById('editAtivo').value === 'true';

        const dados = { email, telefone, formacao, ativo };

        try {
            const response = await fetch(`${API_URL}/${id}/Atualizar_Professor`, {
                method: 'PATCH',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(dados)
            });
            if (!response.ok) {
                const err = await response.json();
                throw new Error(err.Mensagem || 'Erro desconhecido');
            }
            Swal.fire('Sucesso!', 'Professor atualizado!', 'success');
            editarModal.hide();
            await carregarProfessores(paginaAtual);
        } catch (error) {
            Swal.fire('Erro!', `Não foi possível atualizar: ${error.message}`, 'error');
        }
    }

    
    function deletarProfessor(id) {
        Swal.fire({
            title: 'Você tem certeza?', text: "Esta ação não pode ser revertida!", icon: 'warning',
            showCancelButton: true, confirmButtonColor: '#3085d6', cancelButtonColor: '#d33',
            confirmButtonText: 'Sim, deletar!', cancelButtonText: 'Cancelar'
        }).then(async (result) => {
            if (result.isConfirmed) {
                try {
                    const response = await fetch(`${API_URL}/${id}/Deletar_Professor`, { method: 'DELETE' });
                    
                    if (!response.ok) {
                        try {
                            const err = await response.json();
                            throw new Error(err.Mensagem || 'A resposta do servidor não contém um erro detalhado.');
                        } catch (jsonError) {
                            throw new Error('Erro desconhecido. O servidor respondeu com um status de erro.');
                        }
                    }

                    Swal.fire('Deletado!', 'O professor foi deletado com sucesso.', 'success');
                    await carregarProfessores(paginaAtual);
                } catch (error) {
                     Swal.fire('Erro!', `Não foi possível deletar: ${error.message}`, 'error');
                }
            }
        });
    }

    
    btnCadastrar.addEventListener('click', cadastrarProfessor);
    
    document.getElementById('btnSalvarEdicaoProfessor').addEventListener('click', salvarAlteracoes);

    inputPesquisa.addEventListener('input', () => {
        const termo = inputPesquisa.value.toLowerCase();
        const filtrados = todosOsProfessores.filter(p => `${p.nome} ${p.sobrenome}`.toLowerCase().includes(termo));
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
            deletarProfessor(id);
        }
    });

    paginacaoContainer.addEventListener('click', (event) => {
        event.preventDefault();
        if (event.target.tagName === 'A' && event.target.dataset.pagina) {
            const novaPagina = parseInt(event.target.dataset.pagina);
            if (novaPagina !== paginaAtual) {
                paginaAtual = novaPagina;
                carregarProfessores(paginaAtual);
            }
        }
    });


 
    carregarProfessores(paginaAtual);
});