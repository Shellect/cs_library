FROM node:18-alpine AS build
WORKDIR /src
COPY ./package.json .
RUN npm install
COPY . .
RUN npm run build

FROM  nginx:mainline-alpine3.20-slim AS client
COPY --from=build /src/dist/browser /usr/share/nginx/html

FROM node:18-alpine as final
WORKDIR /app
COPY --from=build /src/dist .
CMD ["node", "server/server.mjs"]