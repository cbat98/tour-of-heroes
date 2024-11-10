#!/bin/sh

set -e

docker stop tour-of-heroes-ui
docker build -t heroes-ui .
docker run -dit --rm --name tour-of-heroes-ui -p 8080:80 heroes-ui
