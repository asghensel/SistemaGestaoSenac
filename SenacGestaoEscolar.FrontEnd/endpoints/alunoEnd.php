<?php 
#Obter_todos
$urlTodos = "https://localhost:7017/api/Aluno/Obter_Todos";

// Inicializa o cURL
$ch = curl_init($urlTodos);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false); // Ignora SSL local
curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, false);

// Executa e captura a resposta
$response = curl_exec($ch);
if ($response === false) {
    die("Erro cURL: " . curl_error($ch));
}
// Fecha a conexão
curl_close($ch);

// Decodifica JSON
$data = json_decode($response, true);
if (json_last_error() !== JSON_ERROR_NONE) {
    die("Erro ao decodificar JSON: " . json_last_error_msg());
}





?>