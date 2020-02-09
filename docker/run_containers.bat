docker run -d -p 9200:9200/tcp -p 9300:9300/tcp -e "discovery.type=single-node" elasticsearch:7.5.2
docker run -d -p 5672:5672/tcp --hostname my-rabbit --name some-rabbit rabbitmq:latest
docker run -d -p 27017:27017/tcp mongo:latest