document.addEventListener('DOMContentLoaded', () => {
    const wordSizeSpan = document.getElementById('word-size');
    const dailyWordSpan = document.getElementById('daily-word');
    const prefixInput = document.getElementById('prefix-input');
    const searchPrefixBtn = document.getElementById('search-prefix');
    const prefixResultsUl = document.getElementById('prefix-results');
    const suffixInput = document.getElementById('suffix-input');
    const searchSuffixBtn = document.getElementById('search-suffix');
    const suffixResultsUl = document.getElementById('suffix-results');
    const generateRandomWordBtn = document.getElementById('generate-random-word');
    const randomWordSpan = document.getElementById('random-word');

    //HML
    /*const API_BASE_URL = 'http://localhost:5134'*/
    //PRD 
    const API_BASE_URL = 'https://palavrimapi.onrender.com/';

    // Função para buscar e exibir o tamanho da palavra
    async function fetchWordSize() {
        try {
            const response = await fetch(`${API_BASE_URL}/tamanho`);
            const size = await response.json();
            wordSizeSpan.textContent = size;
        } catch (error) {
            console.error('Erro ao buscar tamanho da palavra:', error);
            wordSizeSpan.textContent = 'Erro';
        }
    }

    // Função para buscar e exibir a palavra do dia
    async function fetchDailyWord() {
        try {
            const response = await fetch(`${API_BASE_URL}/palavra-do-dia`);
            const word = await response.json();
            dailyWordSpan.textContent = word;
        } catch (error) {
            console.error('Erro ao buscar palavra do dia:', error);
            dailyWordSpan.textContent = 'Erro';
        }
    }

    // Função para gerar palavra aleatória
    async function generateRandomWord() {
        try {
            const response = await fetch(`${API_BASE_URL}/palavra-aleatoria`);
            const word = await response.json();
            randomWordSpan.textContent = word;
        } catch (error) {
            console.error('Erro ao gerar palavra aleatória:', error);
            randomWordSpan.textContent = 'Erro';
        }
    }

    // Função para buscar palavras por prefixo
    async function searchByPrefix() {
        const prefix = prefixInput.value.trim();
        if (prefix) {
            try {
                const response = await fetch(`${API_BASE_URL}/prefixo/${prefix}`);
                const words = await response.json();
                displayResults(words, prefixResultsUl);
            } catch (error) {
                console.error('Erro ao buscar por prefixo:', error);
                prefixResultsUl.innerHTML = '<li>Erro ao buscar palavras.</li>';
            }
        } else {
            prefixResultsUl.innerHTML = '';
        }
    }

    // Função para buscar palavras por sufixo
    async function searchBySuffix() {
        const suffix = suffixInput.value.trim();
        if (suffix) {
            try {
                const response = await fetch(`${API_BASE_URL}/sufixo/${suffix}`);
                const words = await response.json();
                displayResults(words, suffixResultsUl);
            } catch (error) {
                console.error('Erro ao buscar por sufixo:', error);
                suffixResultsUl.innerHTML = '<li>Erro ao buscar palavras.</li>';
            }
        } else {
            suffixResultsUl.innerHTML = '';
        }
    }

    // Função auxiliar para exibir os resultados da busca
    function displayResults(words, ulElement) {
        ulElement.innerHTML = '';
        if (words.length > 0) {
            words.forEach(word => {
                const li = document.createElement('li');
                li.textContent = word;
                ulElement.appendChild(li);
            });
        } else {
            ulElement.innerHTML = '<li>Nenhuma palavra encontrada.</li>';
        }
    }

    // Event Listeners
    searchPrefixBtn.addEventListener('click', searchByPrefix);
    prefixInput.addEventListener('keypress', (e) => {
        if (e.key === 'Enter') {
            searchByPrefix();
        }
    });

    searchSuffixBtn.addEventListener('click', searchBySuffix);
    suffixInput.addEventListener('keypress', (e) => {
        if (e.key === 'Enter') {
            searchBySuffix();
        }
    });

    generateRandomWordBtn.addEventListener('click', generateRandomWord);

    // Carregar informações iniciais
    fetchWordSize();
    fetchDailyWord();
}); 