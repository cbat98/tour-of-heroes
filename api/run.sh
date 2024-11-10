#!/bin/sh

set -e

docker stop tour-of-heroes-api
docker build -t heroes-api .
docker run -dit --rm --name tour-of-heroes-api -p 8081:8080 heroes-api
