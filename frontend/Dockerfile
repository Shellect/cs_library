FROM node:18-alpine AS build
WORKDIR /src
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:mainline-alpine3.20-slim AS webserver
COPY --from=build /src/dist/browser /usr/share/nginx/html

FROM node:18-alpine AS final
WORKDIR /app
COPY --from=build /src/dist .
CMD ["node", "/app/server/server.mjs"]