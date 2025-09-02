<!DOCTYPE html>
<html lang="pt-br">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Gestão de Professores</title>
    <link rel="stylesheet" href="../css/style.css">
</head>
<body>
    <header>
        <div class="container">
            <h1>Gestão de Professores</h1>
            <nav>
                <ul>
                    <li><a href="../index.php">Início</a></li>
                    <li><a href="professores.php">Professores</a></li>
                    <li><a href="alunos.php">Alunos</a></li>
                    <li><a href="cursos.php">Cursos</a></li>
                </ul>
            </nav>
        </div>
    </header>

    <main class="container">
        <section class="form-container">
            <h2>Adicionar/Editar Professor</h2>
            <form id="form-professor">
                <input type="hidden" id="professor-id">
                <div class="form-group">
                    <label for="nome">Nome:</label>
                    <input type="text" id="nome" required>
                </div>
                <div class="form-group">
                    <label for="email">Email:</label>
                    <input type="email" id="email" required>
                </div>
                <button type="submit" class="btn btn-primary" id="btn-salvar">Salvar Professor</button>
            </form>
        </section>
        
        <section class="table-container">
            <h2>Lista de Professores</h2>
            <table id="professores-tabela">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Nome</th>
                        <th>Email</th>
                        <th>Ações</th>
                    </tr>
                </thead>
                <tbody>
                    </tbody>
            </table>
        </section>
    </main>
    
    <script type="module" src="../js/professores.js"></script>
</body>
</html>