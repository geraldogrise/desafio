docker compose up -d
docker network create monitoring 
docker network connect monitoring sqlserver
docker network connect monitoring prometheus
docker network connect monitoring grafana
docker network connect monitoring zookeeper
docker network connect monitoring kafka
docker network connect monitoring redis
docker network connect monitoring kafka-ui

docker run -dit --name user_api --network monitoring -p 9099:8080 desafio-carrefour_user_webapi
docker run -dit --name lancamento_api --network monitoring -p 19099:8080 desafio-carrefour_lancamento_webapi
docker run -dit --name consolidado_api --network monitoring -p 29099:8080 desafio-carrefour_consolidado_webapi

docker network connect monitoring user_api
docker network connect monitoring lancamento_api
docker network connect monitoring consolidado_api