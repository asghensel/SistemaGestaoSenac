<?php include("../includes/assets.php") ?>
<!DOCTYPE html>
<html lang="pt-br">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Boizão School - Início</title>
    
    

    <style>
        :root {
            --sidebar-bg: #4A4A4A;
            --main-bg: #F0F2F5;
            --card-professores: #F3B63A;
            --card-alunos: #4A8D8B;
            --card-cursos: #3A4A8A;
        }

        body {
            background-color: var(--main-bg);
            font-family: 'Poppins', sans-serif;
        }

        /* --- Animação de Fade-in --- */
        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(15px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .fade-in-element {
            opacity: 0; /* Começa invisível */
            animation: fadeIn 0.8s ease-out forwards;
        }

        /* --- Barra Lateral (Sidebar) --- */
            
        /* --- Conteúdo Principal --- */
        .main-content {
         
            padding: 2rem 3rem;
        }

        .main-header {
            animation-delay: 0.4s;
        }

        .main-header img {
             max-width: 400px;
             margin-bottom: 10px!important;
        }

        .content-cards .row {
            animation-delay: 0.6s;
        }
        
        .info-card {
            background-color: #fff;
            padding: 1.5rem;
            border-radius: 0.75rem;
            border: 1px solid #e0e0e0;
            animation-delay: 0.5s;
        }

        .custom-card {
            border: solid 2px black;
            padding: 2rem 1.5rem;
            border-radius: 1.25rem;
            color: #fff;
            text-align: center;
            box-shadow: 0 8px 15px rgba(0,0,0,0.1);
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

        a{
            text-decoration: none;
        }
        
        .custom-card:hover {
            transform: translateY(-10px);
            box-shadow: 0 12px 25px rgba(0,0,0,0.2);
        }

        .custom-card h3 {
            font-size: 1.8rem;
            font-weight: 700;
        }
        
       
        .card-professores { background-color: var(--card-professores); }
        .card-alunos { background-color: var(--card-alunos); }
        .card-cursos { background-color: var(--card-cursos); }
        
        /* --- Responsividade --- */
        @media (max-width: 992px) {
            .sidebar {
                position: static;
                width: 100%;
                height: auto;
                margin-bottom: 2rem;
            }
            .main-content {
                margin-right: 0;
            }
        }

    </style>
</head>
<body>

 

    <main class="main-content">
        <header class="text-center main-header fade-in-element">
            <img src="../midia/logoBoi.png" id="logoInicio">
            <br>
            <hr style="border: none;  height: 5px;  background-color: black;"></hr>
            <br>
            
        </header>
        

        <section class="info-card mb-5 fade-in-element">
            <p class="lead" style="font-size:24px;">
                Na Boizão School, acreditamos que aprender é transformar. Com ensino de qualidade, valores sólidos e um ambiente acolhedor, preparamos nossos alunos para os desafios do futuro.
    </p>
    <p class="lead" style="font-size:24px;">
            🌱 Educar é semear o amanhã.
            </p>
        </section>

        <section class="content-cards">
            <div class="row fade-in-element">
                <div class="col-lg-4 col-md-6 mb-4">
                    <a href="./professor.php">
                    <div class="custom-card card-professores">
                        <img src="https://media-public.canva.com/nvK_c/MAGNJEnvK_c/1/t.png" alt="Professores" class="img-fluid mb-3">
                        <h3>Professores</h3>
                    </div>
                    </a>
                </div>
                <div class="col-lg-4 col-md-6 mb-4">
                    <a href="./aluno.php">
                    <div class="custom-card card-alunos">
                         <img src="https://media-public.canva.com/ODXu0/MAF4XWODXu0/1/t.png" alt="Alunos" class="img-fluid mb-3">
                        <h3>Alunos</h3>
                    </div>
                    </a>
                </div>
                <div class="col-lg-4 col-md-6 mb-4">
                    <a href="./curso.php">
                    <div class="custom-card card-cursos">
                         <img src="https://media-public.canva.com/lMCUM/MAE9qFlMCUM/1/t.png" alt="Cursos" class="img-fluid mb-3">
                        <h3>Cursos</h3>
                    </div>
                    </a>
                </div>
            </div>
        </section>

    </main>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>