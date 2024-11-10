# syntax=docker/dockerfile:1.7-labs

FROM node:18.20 AS build

WORKDIR /app

COPY package.json package-lock.json .

RUN npm ci --force

COPY --exclude=./nginx.conf.template . .

RUN npm run build

FROM nginx:1.27 AS run

WORKDIR /usr/share/nginx/html

ENV NGINX_PORT=80
ENV NGINX_HOST=localhost

COPY --from=build /app/dist/tour-of-heroes /usr/share/nginx/html
COPY ./nginx.conf.template /etc/nginx/templates/
