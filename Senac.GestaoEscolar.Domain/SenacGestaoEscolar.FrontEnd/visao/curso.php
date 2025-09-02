<?php
    $pageTitle = 'Cursos';
    $activePage = 'cursos';
    include('../includes/header.php');
    include('../includes/sidebar.php');
?>
    
<main class="main-content">
    <div class="page-header-container">
        <div class="page-header fade-in-element">
            <h1 class="display-6">Painel De Cursos</h1>
            <p class="lead mb-0">Cursos Disponíveis na Plataforma</p>
        </div>
    </div>
    
    <div class="search-bar mb-4 fade-in-element">
        <i class="fas fa-search"></i>
        <input type="text" id="pesquisar-curso" class="form-control form-control-lg" placeholder="Pesquisar Curso...">
        <button type="button" class="btn btn-primary btn-cadastrar" data-bs-toggle="modal" data-bs-target="#cadastrarCursoModal">
            Cadastrar Curso
        </button>
    </div>

    <div class="modal fade" id="cadastrarCursoModal" tabindex="-1" aria-labelledby="cadastrarCursoModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="cadastrarCursoModalLabel">Cadastrar Novo Curso</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form id="formCadastrarCurso">
                        <div class="mb-3">
                            <label for="nome" class="form-label">Nome do Curso</label>
                            <input type="text" class="form-control" id="nome" required>
                        </div>
                        <div class="mb-3">
                            <label for="descricao" class="form-label">Descrição</label>
                            <textarea class="form-control" id="descricao" rows="3" required></textarea>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="cargaHoraria" class="form-label">Carga Horária (h)</label>
                                <input type="number" class="form-control" id="cargaHoraria" required>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="valor" class="form-label">Valor (R$)</label>
                                <input type="number" step="0.01" class="form-control" id="valor" required>
                            </div>
                        </div>
                        <div class="mb-3">
                            <label for="categoria" class="form-label">Categoria</label>
                            <select id="categoria" class="form-select" required>
                                <option value="" disabled selected>Selecione...</option>
                                <option value="Básico">Básico</option>
                                <option value="Intermediário">Intermediário</option>
                                <option value="Avançado">Avançado</option>
                            </select>
                        </div>
                         <div class="mb-3">
                            <label for="professorId" class="form-label">Professor Responsável</label>
                            <select id="professorId" class="form-select" required>
                                <option value="">Carregando professores...</option>
                            </select>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Fechar</button>
                    <button type="button" class="btn btn-primary" id="btnCadastrarCurso">Cadastrar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="editarCursoModal" tabindex="-1" aria-labelledby="editarCursoModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editarCursoModalLabel">Editar Curso</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form id="formEditarCurso">
                        <input type="hidden" id="editCursoId">
                        <div class="mb-3">
                            <label for="editDescricao" class="form-label">Descrição</label>
                            <textarea class="form-control" id="editDescricao" rows="3" required></textarea>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="editCargaHoraria" class="form-label">Carga Horária (h)</label>
                                <input type="number" class="form-control" id="editCargaHoraria" required>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="editValor" class="form-label">Valor (R$)</label>
                                <input type="number" step="0.01" class="form-control" id="editValor" required>
                            </div>
                        </div>
                        <div class="mb-3">
                            <label for="editCategoria" class="form-label">Categoria</label>
                            <select id="editCategoria" class="form-select" required>
                                <option value="Básico">Básico</option>
                                <option value="Intermediário">Intermediário</option>
                                <option value="Avançado">Avançado</option>
                            </select>
                        </div>
                         <div class="mb-3">
                            <label for="editProfessorId" class="form-label">Professor Responsável</label>
                            <select id="editProfessorId" class="form-select" required>
                                <option value="">Carregando professores...</option>
                            </select>
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
                    <button type="button" class="btn btn-primary" id="btnSalvarEdicaoCurso">Salvar Alterações</button>
                </div>
            </div>
        </div>
    </div>

    <div class="table-responsive fade-in-element">
        <table class="table table-striped table-hover align-middle">
            <thead class="table-dark">
                <tr>
                    <th>ID</th>
                    <th>Nome do Curso</th>
                    <th class="text-center">Carga Horária</th>
                    <th class="text-center">Ações</th>
                </tr>
            </thead>
            <tbody id="tabela-cursos-corpo">
                <tr><td colspan="4" class="text-center">Carregando dados...</td></tr>
            </tbody>
        </table>
    </div>
    <nav id="paginacao-container" aria-label="Navegação da página"></nav>
</main>

<?php include('../includes/footer-curso.php'); ?>