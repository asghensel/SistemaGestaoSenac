 
 <style>

     :root {
            --sidebar-bg: #4A4A4A;
            --main-bg: #F0F2F5;
        }

        body {
            background-color: var(--main-bg);
            font-family: 'Poppins', sans-serif;
        }

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
            opacity: 0; 
            animation: fadeIn 0.8s ease-out forwards;
        }

      
        .sidebar {
            position: fixed;
            top: 0;
            right: 0;
            height: 100vh;
            width: 280px;
            background-color: var(--sidebar-bg);
            padding: 1.5rem 1rem;
            color: #fff;
            display: flex;
            flex-direction: column;
            box-shadow: -5px 0 15px rgba(0,0,0,0.1);
            animation-delay: 0.2s; 
        }

        .sidebar .logo {
            max-width: 150px;
            margin-bottom: 2rem;
        }

        .sidebar .nav-link {
            color: #d1d1d1;
            font-size: 1.1rem;
            font-weight: 600;
            padding: 0.8rem 1rem;
            margin-bottom: 0.5rem;
            border-radius: 0.5rem;
            transition: background-color 0.3s, color 0.3s;
        }

        .sidebar .nav-link i {
            margin-right: 1rem;
            width: 20px;
            text-align: center;
        }

        .sidebar .nav-link:hover {
            background-color: rgba(255, 255, 255, 0.1);
            color: #fff;
        }

        .sidebar .nav-link.active {
            background-color: #fff;
            color: #333;
            box-shadow: 0 4px 8px rgba(0,0,0,0.2);
        }

        .sidebar .user-info {
            margin-top: auto;
            font-weight: 600;
        }
 </style>
        <script>
        const tocarSom = () => {
            const audio = new Audio(somAguia);
            audio.play();
        };
        </script>
 <nav class="sidebar fade-in-element">
        <div class="text-center">
            <img src="../midia/logoBoi.png" alt="Logo Boizão School" class="logo">
        </div>

        <ul class="nav flex-column">
            <li class="nav-item">
                <a class="nav-link " href="../visao/menu.php">
                    <i class="fas fa-home"></i> Início
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="../visao/professor.php">
                    <i class="fas fa-chalkboard-teacher"></i> Professor
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="../visao/aluno.php">
                    <i class="fas fa-user-graduate"></i> Aluno
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="../visao/curso.php">
                    <i class="fas fa-book"></i> Cursos
                </a>
            </li>
        </ul>

        <div class="user-info">
            <p style="margin-left: 50px"><i class="fas fa-user-circle me-2"></i> Bem-Vindo</p>
        </div>
    </nav>