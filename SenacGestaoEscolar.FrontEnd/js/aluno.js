
const API_URL = "https://localhost:7017/api/Aluno";

document.addEventListener('DOMContentLoaded', () => {
   
    const tabelaCorpo = document.getElementById('tabela-alunos-corpo');
    const inputPesquisa = document.getElementById('pesquisar-aluno');
    const btnCadastrar = document.getElementById('btnCadastrar');

   
    const cadastrarModal = new bootstrap.Modal(document.getElementById('cadastrarModal'));
    const editarModal = new bootstrap.Modal(document.getElementById('editarModal'));

   
    let todosOsAlunos = []; 
-

    function renderizarTabela(alunos) {
        tabelaCorpo.innerHTML = ''; 

        if (alunos.length === 0) {
            tabelaCorpo.innerHTML = `<tr><td colspan="4" class="text-center">Nenhum aluno encontrado.</td></tr>`;
            return;
        }

        alunos.forEach(aluno => {
            const tr = `
                <tr>
                    <td>${aluno.id}</td>
                    <td>${aluno.nome}</td>
                    <td>${aluno.sobrenome}</td>
                    <td class="text-center table-actions">
                        <a href="aluno-detalhes.php?id=${aluno.id}" class="icon-info" title="Informações"><i class="fas fa-info-circle"></i></a>
                        <a href="#" class="icon-edit" title="Editar" data-id="${aluno.id}"><i class="fas fa-edit"></i></a>
                        <a href="#" class="icon-delete" title="Deletar" data-id="${aluno.id}"><i class="fas fa-trash-alt"></i></a>
                    </td>
                </tr>
            `;
            tabelaCorpo.insertAdjacentHTML('beforeend', tr);
        });
    }

    async function carregarAlunos() {
        try {
            const response = await fetch(`${API_URL}/Obter_Todos`);
            if (!response.ok) throw new Error(`Erro na API! Status: ${response.status}`);
            
            const alunosDaApi = await response.json();
            todosOsAlunos = alunosDaApi; 
            renderizarTabela(todosOsAlunos);
            
        } catch (error) {
            console.error("Falha ao carregar alunos:", error);
            tabelaCorpo.innerHTML = `<tr><td colspan="4" class="text-center text-danger">Erro ao carregar os dados. A API está rodando?</td></tr>`;
        }
    }

    async function cadastrarAluno() {
        const nome = document.getElementById('nome').value.trim();
        const sobrenome = document.getElementById('sobrenome').value.trim();
        const dataDeNascimento = document.getElementById('dataDeNascimento').value;
        const email = document.getElementById('email').value.trim();
        const telefone = document.getElementById('telefone').value.trim();

        if (!nome || !sobrenome || !email || !dataDeNascimento) {
            Swal.fire('Atenção!', 'Preencha os campos obrigatórios (Nome, Sobrenome, Data de Nascimento e Email).', 'warning');
            return;
        }

        const alunoData = { nome, sobrenome, dataDeNascimento, email, telefone };

        try {
            const response = await fetch(`${API_URL}/Cadastrar_Aluno`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(alunoData)
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.Mensagem || 'Erro desconhecido ao cadastrar');
            }
            
            Swal.fire('Sucesso!', 'Aluno cadastrado com sucesso!', 'success');
            cadastrarModal.hide();
            document.getElementById('formCadastrarAluno').reset();
            await carregarAlunos();
        } catch (error) {
            Swal.fire('Erro!', `Não foi possível cadastrar o aluno: ${error.message}`, 'error');
        }
    }

    async function abrirModalEdicao(id) {
        try {
            const response = await fetch(`${API_URL}/${id}/Obter_Aluno`);
            if (!response.ok) throw new Error('Aluno não encontrado.');
            
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
            const response = await fetch(`${API_URL}/${id}/Atualizar_Aluno`, {
                method: 'PATCH',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(dadosAtualizados)
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.Mensagem || 'Erro desconhecido ao atualizar');
            }
            
            Swal.fire('Sucesso!', 'Aluno atualizado com sucesso!', 'success');
            editarModal.hide();
            await carregarAlunos();
        } catch (error) {
            Swal.fire('Erro!', `Não foi possível atualizar o aluno: ${error.message}`, 'error');
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
                    const response = await fetch(`${API_URL}/${id}/Deletar_Aluno`, {
                        method: 'DELETE'
                    });
                    if (!response.ok) {
                        const errorData = await response.json();
                        throw new Error(errorData.Mensagem || 'Erro desconhecido ao deletar');
                    }
                    Swal.fire('Deletado!', 'O aluno foi deletado com sucesso.', 'success');
                    await carregarAlunos();
                } catch (error) {
                     Swal.fire('Erro!', `Não foi possível deletar o aluno: ${error.message}`, 'error');
                }
            }
        });
    }

    

    btnCadastrar.addEventListener('click', cadastrarAluno);
    document.getElementById('btnSalvarEdicao').addEventListener('click', salvarAlteracoes);

    tabelaCorpo.addEventListener('click', (event) => {
        const target = event.target.closest('a');
        if (!target) return;
        const id = target.dataset.id;
        if (target.classList.contains('icon-edit')) {
            event.preventDefault();
            abrirModalEdicao(id);
        } else if (target.classList.contains('icon-delete')) {
            event.preventDefault();
            deletarAluno(id);
        }
    });
    
    inputPesquisa.addEventListener('input', () => {
        const termoBusca = inputPesquisa.value.toLowerCase();
        const alunosFiltrados = todosOsAlunos.filter(aluno => {
            const nomeCompleto = `${aluno.nome} ${aluno.sobrenome}`.toLowerCase();
            return nomeCompleto.includes(termoBusca);
        });
        renderizarTabela(alunosFiltrados);
    });

    
    carregarAlunos();
});