FROM node:18-alpine AS base
WORKDIR /app

FROM base AS restore
COPY package*.json ./
RUN npm install

FROM restore AS build
COPY . .
RUN npm run build

FROM base AS final
COPY --from=build /app/dist .
CMD ["node", "/app/server/server.mjs"]

FROM nginx:mainline-alpine3.20-slim AS webserver
COPY --from=build /app/dist/browser /usr/share/nginx/html

FROM restore AS develop
CMD ["npm", "run", "serve"]