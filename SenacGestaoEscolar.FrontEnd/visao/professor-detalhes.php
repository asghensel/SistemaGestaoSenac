<?php

$professorId = $_GET['id'] ?? 0;

if ($professorId <= 0) {
    die("Erro: ID do professor não fornecido ou inválido.");
}

$url = "https://localhost:7017/api/Professor/" . $professorId . "/Obter_Professor";


$ch = curl_init($url);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, false);
$response = curl_exec($ch);

if ($response === false) {
    die("Erro ao conectar na API: " . curl_error($ch));
}
curl_close($ch);

$professor = json_decode($response, true);

if (json_last_error() !== JSON_ERROR_NONE || $professor === null || isset($professor['status'])) {
     die("Erro: Professor com ID " . htmlspecialchars($professorId) . " não foi encontrado ou a API retornou um erro.");
}

$pageTitle = 'Detalhes de ' . htmlspecialchars($professor['nome']);
include('../includes/header.php');
include('../includes/sidebar.php');
?>
<style>
    


</style>
<main class="main-content">
    <div class="page-header-container">
        <div class="page-header">
            <h1>Currículo do Professor</h1>
        </div>
    </div>
    
    <div class="curriculo-container" style="width: 100%; max-width: 800px; padding: 2rem; background: #fff; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1);">
        <h2><?php echo htmlspecialchars($professor['nome'] . ' ' . $professor['sobrenome']); ?></h2>
        <p><strong>ID do Professor:</strong> <?php echo htmlspecialchars($professor['id']); ?></p>
        <hr>
        <h4>Informações de Contato</h4>
        <p><strong>Email:</strong> <?php echo htmlspecialchars($professor['email']); ?></p>
        <p><strong>Telefone:</strong> <?php echo htmlspecialchars($professor['telefone'] ?? 'Não informado'); ?></p>
        <hr>
        <h4>Detalhes Profissionais</h4>
        <p><strong>Data de Nascimento:</strong> <?php echo date('d/m/Y', strtotime($professor['dataNascimento'])); ?></p>
        <p><strong>Data da Contratação:</strong> <?php echo date('d/m/Y', strtotime($professor['dataContratacao'])); ?></p>
        <p><strong>Formação:</strong> <?php echo htmlspecialchars($professor['formacao']); ?></p>
        <p><strong>Status:</strong> <?php echo $professor['ativo'] ? '<span class="badge bg-success">Ativo</span>' : '<span class="badge bg-danger">Inativo</span>'; ?></p>
    </div>
</main>

<?php
    include('../includes/footer.php'); 
?>