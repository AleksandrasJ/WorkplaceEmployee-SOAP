FROM node:19.7.0-alpine

WORKDIR /app

COPY ./package*.json /app/

COPY . .

RUN npm install

CMD [ "npm", "start" ]