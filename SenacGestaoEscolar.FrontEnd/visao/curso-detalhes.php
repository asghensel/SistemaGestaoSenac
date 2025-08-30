<?php
$cursoId = $_GET['id'] ?? 0;
if ($cursoId <= 0) die("Erro: ID do curso inválido.");


$url = "https://localhost:7017/api/Curso/" . $cursoId . "/Obter_Curso";
$ch = curl_init($url);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
$response = curl_exec($ch);
curl_close($ch);
$curso = json_decode($response, true);

if (!$curso || isset($curso['status'])) {
     die("Erro: Curso com ID " . htmlspecialchars($cursoId) . " não foi encontrado.");
}

$pageTitle = 'Detalhes de ' . htmlspecialchars($curso['nome']);
include('../includes/header.php');
include('../includes/sidebar.php');
?>

<main class="main-content">
    <div class="page-header-container">
        <div class="page-header">
            <h1>Detalhes do Curso</h1>
        </div>
    </div>
    
    <div class="curriculo-container" style="width: 100%; max-width: 800px; padding: 2rem; background: #fff; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1);">
        <h2><?php echo htmlspecialchars($curso['nome']); ?></h2>
        <p><strong>ID do Curso:</strong> <?php echo htmlspecialchars($curso['id']); ?></p>
        <hr>
        <h4>Descrição</h4>
        <p><?php echo htmlspecialchars($curso['descricao']); ?></p>
        <hr>
        <h4>Detalhes</h4>
        <p><strong>Categoria:</strong> <?php echo htmlspecialchars($curso['categoria']); ?></p>
        <p><strong>Carga Horária:</strong> <?php echo htmlspecialchars($curso['cargaHoraria']); ?> horas</p>
        <p><strong>Valor:</strong> R$ <?php echo number_format($curso['valor'], 2, ',', '.'); ?></p>
        <p><strong>Professor ID:</strong> <?php echo htmlspecialchars($curso['professorId']); ?></p>
        <p><strong>Status:</strong> <?php echo $curso['ativo'] ? '<span class="badge bg-success">Ativo</span>' : '<span class="badge bg-danger">Inativo</span>'; ?></p>
    </div>
</main>

<?php
    include('../includes/footer.php');
?>