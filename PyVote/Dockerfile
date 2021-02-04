FROM python:3.9-alpine

RUN apk add py3-pip

RUN pip install flask redis

WORKDIR /app
COPY . /app

EXPOSE 83
ENTRYPOINT [ "python", "app.py" ]