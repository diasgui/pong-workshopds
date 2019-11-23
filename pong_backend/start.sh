#!/bin/bash

export MY_IP=$MY_IP

echo "=> Stopping older services"
docker-compose --file docker-compose.yaml down

echo "=> Starting databases"
docker-compose \
  --file docker-compose.yaml \
  --project-name=pong \
  up --no-recreate -d postgres

until docker exec pong_postgres pg_isready
  do echo "=> Waiting for Postgres..." && sleep 1
done

echo "=> Starting services"
docker-compose \
  --file docker-compose.yaml \
  --project-name=pong \
  up -d

