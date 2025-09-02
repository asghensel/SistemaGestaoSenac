const API_BASE_URL = 'https://localhost:7001/api';

async function apiRequest(endpoint, options = {}) {
    const url = `${API_BASE_URL}/${endpoint}`;
    
    const token = localStorage.getItem('authToken');
    
    const headers = { ...options.headers };
    if (token) {
        headers['Authorization'] = `Bearer ${token}`;
    }

    if (options.body) {
        headers['Content-Type'] = 'application/json';
    }

    try {
        const response = await fetch(url, { ...options, headers });
        
        if (!response.ok) {
            const errorData = await response.json().catch(() => ({ Mensagem: response.statusText }));
            throw new Error(errorData.Mensagem || `Erro na requisição: ${response.status}`);
        }
        
        if (response.status === 204 || options.method === 'DELETE' || (options.method === 'PUT' && response.status === 200)) {
            return true;
        }

        return await response.json();

    } catch (error) {
        console.error("Ocorreu um erro:", error.message);
        throw error;
    }
}

export { apiRequest };