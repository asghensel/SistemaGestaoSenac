<?php
$alunoId = $_GET['id'] ?? 0;


if ($alunoId <= 0) {
    die("Erro: ID do aluno não fornecido ou inválido.");
}


$url = "https://localhost:7017/api/Aluno/" . $alunoId . "/Obter_Aluno";

$ch = curl_init($url);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, false);

$response = curl_exec($ch);

if ($response === false) {
    die("Erro ao conectar na API: " . curl_error($ch));
}

curl_close($ch);

$aluno = json_decode($response, true);

if (json_last_error() !== JSON_ERROR_NONE || $aluno === null || isset($aluno['status'])) {
     die("Erro: Aluno com ID " . htmlspecialchars($alunoId) . " não foi encontrado ou a API retornou um erro.");
}

$pageTitle = 'Detalhes de ' . htmlspecialchars($aluno['nome']);
include('../includes/header.php');
include('../includes/sidebar.php');
?>

<main class="main-content">
    <div class="page-header-container">
        <div class="page-header">
            <h1>Currículo do Aluno</h1>
        </div>
    </div>
    
    <div class="curriculo-container" style="width: 100%; max-width: 800px; padding: 2rem; background: #fff; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1);">
        <h2><?php echo htmlspecialchars($aluno['nome'] . ' ' . $aluno['sobrenome']); ?></h2>
        <p><strong>ID do Aluno:</strong> <?php echo htmlspecialchars($aluno['id']); ?></p>
        <hr>
        <h4>Informações de Contato</h4>
        <p><strong>Email:</strong> <?php echo htmlspecialchars($aluno['email']); ?></p>
        <p><strong>Telefone:</strong> <?php echo htmlspecialchars($aluno['telefone'] ?? 'Não informado'); ?></p>
        <hr>
        <h4>Detalhes Acadêmicos</h4>
        <p><strong>Data de Nascimento:</strong> <?php echo date('d/m/Y', strtotime($aluno['dataDeNascimento'])); ?></p>
        <p><strong>Data da Matrícula:</strong> <?php echo date('d/m/Y', strtotime($aluno['dataMatricula'])); ?></p>
        <p><strong>Status:</strong> <?php echo $aluno['ativo'] ? '<span class="badge bg-success">Ativo</span>' : '<span class="badge bg-danger">Inativo</span>'; ?></p>
    </div>
</main>

<?php
    include('../includes/footer.php');
?>