# JPV_Crud

# Para utilizar o projeto faça as configurações seguintes

- Execute o arquivo script.sql localizado na pasta Scripts-db na raiz da solução para criar o banco.

- Execute o arquivo dotnet_ef_update_database que está na pasta CRUD.Site, assim ele irá rodar as migrations do projeto e criar a tabela com suas configurações.

- Ao abrir a solução, clique com o botão direito na solução e ir até Propriedades. Selecione a opção para iniciar mais de um projeto, selecione o projeto CRUD.API e o projeto CRUD.Site para subir as duas aplicações.

- caso tenha algum problema ao rodar o database update, certifique-se de que o EF Tools está na versão 7.0.8 (rode o comando dotnet tool install --global dotnet-ef no terminal do desenvolvedor para atualizar).
