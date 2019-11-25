# Backend (Linux Ubuntu)

## 1 - Instalar requisitos
No terminal, digite:
```
sudo apt-get install docker docker.io docker-compose pgcli redis-tools elixir
```

## 2 - Baixar primeiros arquivos
Crie uma pasta no seu computador onde vamos criar o backend (por exemplo `mkdir ~/Documentos/pong/pong_backend`). Nela, baixe os arquivos:
* docker-compose.yaml
* Makefile
* start.sh

Se sua vesãon do docker-compose for menor que 3, corrija o `docker-compose.yaml` para usar a versao 2.

## 3 - Subir a imagem docker
```
sudo ./start.sh
```

## 4 - Conectar no Database
Vamos testar a criação de uma tabela
```
sudo docker exec -it pong_postgres psql -U pong_user -d pong
```
Dentro do postgre
```
create TABLE test (
    id integer,
    name varchar(40)
);
```
Vamos apagar
```
drop table test;
```
Para sair:
```
\q
```
Vamos testar o redis
```
sudo docker exec -it pong_redis redis-cli
```

## 5 - Let's MIX
Primeiro, vamos instalar a versão atualizada do Elixir, rodando um a um os comandos a baixo:
```
wget https://packages.erlang-solutions.com/erlang-solutions_2.0_all.deb && sudo dpkg -i erlang-solutions_2.0_all.deb
sudo apt-get update
sudo apt-get install elixir
```
Vamos instalar o phoenix usando o mix:
```
mix local.hex
mix archive.install hex phx_new 1.4.11
```

## 6 - Let's create the project
Vamos utilizar o boilerplate para subir o projeto inteiro:
```
mix phx.new pong_backend --database postgres --no-html --no-webpack --verbose
mv docker-compose.yaml ./pong_backend/
mv Makefile ./pong_backend/
mv start.sh ./pong_backend/
cd ./pong_backend
mix deps.get
mix phx.routes
```

## 7 - Configurando nosso servidor
Vamos editar as configuraçōes, abra o arquivo `config/dev.exs`:
```
# Configure your database
config :pong_backend, PongBackend.Repo,
    username: "pong_user",
    password: "",
    database: "pong",
    hostname: "localhost",
    show_sensitive_data_on_connection_error: true,
    pool_size: 10
```
E rodar:
```
mix phx.server
```
Devemos ter um servidor rodando agora :-D

## 8 - Modelos
Vamos criar o modelo de um player
```
mix phx.gen.json Accounts Player players name:string wins:integer losses:integer
```
Para gerar o modelo no nosso banco de dados, precisamos rodar:
```
mix ecto.generate
```

E ver como ficou nosso BD:
```
sudo docker exec -it pong_postgres psql -U pong_user -d pong
\d
drop table players;
drop table players_id_seq;
drop table scheme_migrations;
\q
```

Vamos editar nossa migracao, para corrigir nosso banco de dados. Edite o arquivo `priv/repo/migrations/20191123201459_create_players.exs` (o nome do seu arquivo será diferente)
```
    defmodule PongBackend.Repo.Migrations.CreatePlayers do
  1   use Ecto.Migration
  2 
  3   def change do
  4     create table(:players, primary_key: false) do
  5       add :id, :uuid, primary_key: true
  6       add :name, :string
  7       add :wins, :integer
  8       add :losses, :integer
  9 
 10       timestamps()
 11     end
 12 
 13   end
 14 end

```