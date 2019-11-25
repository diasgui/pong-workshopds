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
```
sudo docker exec -it pong_postgres psql -U pong_user -d pong
```
