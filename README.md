# PalavrimAPI
<p align="center">
    <img src="wwwroot/8545842.png" alt="Logo do Palavrim" width="200" />
</p>

API RESTful para manipulação e consulta de palavras em português, ideal para jogos, validação e geração de palavras. Desenvolvida em ASP.NET Core 8.0.

## Visão Geral
A PalavrimAPI fornece endpoints para:
- Obter o tamanho padrão das palavras
- Consultar a palavra do dia
- Gerar palavras aleatórias
- Validar palavras
- Buscar por prefixo ou sufixo
- Listar todas as palavras do dicionário

O dicionário utilizado está em `PalavrimWordList/portuguese.txt` e pode ser customizado conforme a necessidade.

## Endpoints

| Método | Rota                        | Descrição                                               |
|--------|-----------------------------|---------------------------------------------------------|
| GET    | `/tamanho`                  | Retorna o tamanho padrão das palavras                   |
| GET    | `/palavra-do-dia`           | Retorna a palavra do dia                                |
| GET    | `/palavra-aleatoria`        | Retorna uma palavra aleatória                           |
| GET    | `/valida/{palavra}`         | Valida se a palavra existe no dicionário (true/false)   |
| GET    | `/prefixo/{prefixo}`        | Lista palavras que começam com o prefixo informado      |
| GET    | `/sufixo/{sufixo}`          | Lista palavras que terminam com o sufixo informado      |
| GET    | `/todas`                    | Lista todas as palavras do dicionário                   |

## Exemplos de Uso

### Obter tamanho da palavra
```http
GET /tamanho
```
Resposta:
```json
5
```

### Palavra do dia
```http
GET /palavra-do-dia
```
Resposta:
```json
"magia"
```

### Buscar por prefixo
```http
GET /prefixo/ma
```
Resposta:
```json
["magia", "mago", ...]
```

### Validar palavra
```http
GET /valida/magia
```
Resposta:
```json
true
```

## Executando Localmente

1. **Pré-requisitos:**
   - [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

2. **Clone o repositório:**
   ```bash
   git clone <url-do-repositorio>
   cd PalavrimAPI
   ```

3. **Execute a aplicação:**
   ```bash
   dotnet run
   ```
   A API estará disponível em `http://localhost:5134` (ou porta configurada).

## Deploy com Docker

1. **Build da imagem:**
   ```bash
   docker build -t palavrimapi .
   ```
2. **Execute o container:**
   ```bash
   docker run -d -p 8080:8080 palavrimapi
   ```

## Customização do Dicionário
- O arquivo `PalavrimWordList/portuguese.txt` contém uma palavra por linha, sem acentos e com o tamanho definido no serviço (padrão: 5 letras).

## Licença
Este projeto está licenciado sob a licença MIT. Veja o arquivo LICENSE.txt para mais detalhes.
