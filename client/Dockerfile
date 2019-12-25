FROM node:alpine as builder
COPY . /src/
WORKDIR /src/
RUN npm install
RUN npm run build

FROM nginx:alpine
RUN rm /etc/nginx/conf.d/default.conf
COPY --from=builder /src/dist /src/docker-entrypoint.sh /site/
COPY --from=builder /src/site.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
WORKDIR /site
ENTRYPOINT [ "./docker-entrypoint.sh" ]