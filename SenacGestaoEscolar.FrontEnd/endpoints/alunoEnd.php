<?php 
#Obter_todos
$urlTodos = "https://localhost:7017/api/Aluno/Obter_Todos";


$ch = curl_init($urlTodos);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false); 
curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, false);


$response = curl_exec($ch);
if ($response === false) {
    die("Erro cURL: " . curl_error($ch));
}
curl_close($ch);


$data = json_decode($response, true);
if (json_last_error() !== JSON_ERROR_NONE) {
    die("Erro ao decodificar JSON: " . json_last_error_msg());
}





?>