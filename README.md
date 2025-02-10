📌 Backend – Sistema de Itens Perdidos e Achados
O backend do sistema foi desenvolvido em ASP.NET e utiliza PostgreSQL como base de dados para armazenar e gerir os itens perdidos e achados, bem como as informações dos utilizadores. Ele fornece uma API RESTful que permite a comunicação com o frontend de forma segura e eficiente.

🔹 Tecnologias Utilizadas
ASP.NET – Framework para desenvolvimento da API.
PostgreSQL – Base de dados relacional para armazenar os itens e utilizadores.
Entity Framework Core – ORM utilizado para interação com o banco de dados.
JWT (JSON Web Token) – Implementação da autenticação por email e senha.
Swagger – Documentação da API para facilitar a integração.
CORS – Configuração para permitir acesso ao frontend.
🔹 Comunicação com o Frontend
O backend expõe endpoints RESTful que podem ser consumidos pelo frontend desenvolvido em Kotlin com Jetpack Compose. A comunicação ocorre via HTTP, utilizando o formato JSON para envio e recebimento de dados. O frontend utiliza Retrofit para consumir os serviços da API, realizando operações como:

Autenticação: Login e registo de utilizadores.
Gestão de Itens: Criação, atualização, listagem e remoção de itens.
Favoritos: Gerir a lista de itens favoritos dos utilizadores.
Com essa arquitetura, o sistema garante escalabilidade, segurança e uma experiência integrada entre frontend e backend. 
