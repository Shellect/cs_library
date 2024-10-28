FROM node:18-alpine AS build
WORKDIR /src
RUN chown node:node ./
USER node
COPY package*.json ./
RUN npm ci && npm cache clean --force
COPY ./ ./
RUN npm run build

FROM  nginx:mainline-alpine3.20-slim AS client
COPY --from=build /src/dist/browser /usr/share/nginx/html

FROM node:18-alpine AS final
WORKDIR /app
COPY --from=build /src/dist .
CMD ["node", "server/server.mjs"]