FROM oven/bun:latest

WORKDIR /client

COPY ./package.json ./bun.lock ./
RUN bun install

COPY ./ /client

EXPOSE 5173

CMD ["bun", "run", "dev"]
