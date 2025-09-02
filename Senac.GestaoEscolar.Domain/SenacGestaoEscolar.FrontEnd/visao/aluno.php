<?php
    $pageTitle = 'Alunos';
    $activePage = 'alunos';
    include('../includes/header.php');
    include('../includes/sidebar.php');
?>
    
<main class="main-content">
    
    <div class="page-header-container">
        <div class="page-header fade-in-element">
            <h1 class="display-6">Painel De Alunos</h1>
            <p class="lead mb-0">Alunos Cursando</p>
        </div>
    </div>
    
    <div class="search-bar mb-4 fade-in-element">
        <i class="fas fa-search"></i>
        <input type="text" id="pesquisar-aluno" class="form-control form-control-lg" placeholder="Pesquisar Aluno...">
        <button type="button" class="btn btn-primary btn-cadastrar" data-bs-toggle="modal" data-bs-target="#cadastrarModal">
            Cadastrar Aluno
        </button>
    </div>

    <div class="modal fade" id="cadastrarModal" tabindex="-1" aria-labelledby="cadastrarModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="cadastrarModalLabel">Cadastrar Aluno</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form id="formCadastrarAluno">
                        <div class="mb-3">
                            <label for="nome" class="form-label">Nome</label>
                            <input type="text" class="form-control" id="nome" required>
                        </div>
                        <div class="mb-3">
                            <label for="sobrenome" class="form-label">Sobrenome</label>
                            <input type="text" class="form-control" id="sobrenome" required>
                        </div>
                        <div class="mb-3">
                            <label for="dataDeNascimento" class="form-label">Data de Nascimento</label>
                            <input type="date" class="form-control" id="dataDeNascimento" required>
                        </div>
                        <div class="mb-3">
                            <label for="email" class="form-label">Email</label>
                            <input type="email" class="form-control" id="email" required>
                        </div>
                        <div class="mb-3">
                            <label for="telefone" class="form-label">Telefone</label>
                            <input type="text" class="form-control" id="telefone">
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Fechar</button>
                    <button type="button" class="btn btn-primary" id="btnCadastrar">Cadastrar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="editarModal" tabindex="-1" aria-labelledby="editarModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editarModalLabel">Editar Aluno</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form id="formEditarAluno">
                        <input type="hidden" id="editAlunoId">
                        <div class="mb-3">
                            <label for="editEmail" class="form-label">Email</label>
                            <input type="email" class="form-control" id="editEmail" required>
                        </div>
                        <div class="mb-3">
                            <label for="editTelefone" class="form-label">Telefone</label>
                            <input type="text" class="form-control" id="editTelefone">
                        </div>
                        <div class="mb-3">
                            <label for="editAtivo" class="form-label">Status</label>
                            <select class="form-select" id="editAtivo" required>
                                <option value="true">Ativo</option>
                                <option value="false">Inativo</option>
                            </select>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-primary" id="btnSalvarEdicao">Salvar Alterações</button>
                </div>
            </div>
        </div>
    </div>

    <div class="table-responsive fade-in-element">
        <table class="table table-striped table-hover align-middle">
            <thead class="table-dark">
                <tr>
                    <th>ID</th>
                    <th>Nome</th>
                    <th>Sobrenome</th>
                    <th class="text-center">Ações</th>
                </tr>
            </thead>
            <tbody id="tabela-alunos-corpo">
                <tr><td colspan="4" class="text-center">Carregando dados...</td></tr>
            </tbody>
        </table>
    </div>
</main>

<?php
    include('../includes/footer.php');
?>