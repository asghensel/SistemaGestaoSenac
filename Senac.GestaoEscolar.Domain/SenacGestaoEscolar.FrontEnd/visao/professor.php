<?php
    $pageTitle = 'Professores';
    $activePage = 'professores';
    include('../includes/header.php');
    include('../includes/sidebar.php');
?>
    
<main class="main-content">
    
    <div class="page-header-container">
        <div class="page-header fade-in-element">
            <h1 class="display-6">Painel De Professores</h1>
            <p class="lead mb-0">Corpo Docente</p>
        </div>
    </div>
    
    <div class="search-bar mb-4 fade-in-element">
        <i class="fas fa-search"></i>
        <input type="text" id="pesquisar-professor" class="form-control form-control-lg" placeholder="Pesquisar Professor...">
        <button type="button" class="btn btn-primary btn-cadastrar" data-bs-toggle="modal" data-bs-target="#cadastrarProfessorModal">
            Cadastrar Professor
        </button>
    </div>

    <div class="modal fade" id="cadastrarProfessorModal" tabindex="-1" aria-labelledby="cadastrarProfessorModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="cadastrarProfessorModalLabel">Cadastrar Professor</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form id="formCadastrarProfessor">
                        <div class="mb-3">
                            <label for="nome" class="form-label">Nome</label>
                            <input type="text" class="form-control" id="nome" required>
                        </div>
                        <div class="mb-3">
                            <label for="sobrenome" class="form-label">Sobrenome</label>
                            <input type="text" class="form-control" id="sobrenome" required>
                        </div>
                        <div class="mb-3">
                            <label for="dataNascimento" class="form-label">Data de Nascimento</label>
                            <input type="date" class="form-control" id="dataNascimento" required>
                        </div>
                        <div class="mb-3">
                            <label for="email" class="form-label">Email</label>
                            <input type="email" class="form-control" id="email" required>
                        </div>
                        <div class="mb-3">
                            <label for="telefone" class="form-label">Telefone</label>
                            <input type="text" class="form-control" id="telefone">
                        </div>
                        <div class="mb-3">
                            <label for="formacao" class="form-label">Formação</label>
                            <input type="text" class="form-control" id="formacao" placeholder="Ex: Doutorado" required>
                        </div>
                         <div class="mb-3">
                            <label for="dataContratacao" class="form-label">Data de Contratação</label>
                            <input type="date" class="form-control" id="dataContratacao" required>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Fechar</button>
                    <button type="button" class="btn btn-primary" id="btnCadastrarProfessor">Cadastrar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="editarProfessorModal" tabindex="-1" aria-labelledby="editarProfessorModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editarProfessorModalLabel">Editar Professor</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form id="formEditarProfessor">
                        <input type="hidden" id="editProfessorId">
                        <div class="mb-3">
                            <label for="editEmail" class="form-label">Email</label>
                            <input type="email" class="form-control" id="editEmail" required>
                        </div>
                        <div class="mb-3">
                            <label for="editTelefone" class="form-label">Telefone</label>
                            <input type="text" class="form-control" id="editTelefone">
                        </div>
                        <div class="mb-3">
                            <label for="editFormacao" class="form-label">Formação</label>
                            <input type="text" class="form-control" id="editFormacao" required>
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
                    <button type="button" class="btn btn-primary" id="btnSalvarEdicaoProfessor">Salvar Alterações</button>
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
            <tbody id="tabela-professores-corpo">
                <tr><td colspan="4" class="text-center">Carregando dados...</td></tr>
            </tbody>
        </table>
    </div>
    <nav id="paginacao-container" aria-label="Navegação da página"></nav>
</main>

<?php include('../includes/footer-professor.php'); ?>