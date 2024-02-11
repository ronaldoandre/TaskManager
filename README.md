# API Task Manager

---

Sistema de garenciamento de tarefas.

Funcionalidades:

- Criar novo projeto.
- Obter os projetos de um usuario.
- Remover um projeto.
- Adicionar nova tarefa do projeto.
- Remover uma tarefa do projeto.
- Editar uma tarefa do projeto.
- Obter as tarefas do projeto.
- Adicionar comentario as tarefas.
- Editar um comentario.
- Remover um comentario.
- Obter comentarios de uma tarefa.
- Relatório sobre a conclusão das tarafes por cada usuario.

## Iniciando

---

Para inicia o serviço você pode utilizar o Docker Compose basta rodar os comandos abaixo na pasta raiz do projeto:

- Este comando ira gerar a imagem docker do projeto com o nome taskmanager:

```powershell
docker build -t taskmanager .
```

- Este comando ira baixar a imagem do MySQL e inicioar os serviços, tanto do MySQL quanto do Task Manager:

```powershell
docker compose up -d --build
```

## Testes

Para verificar a cobertura de testes podera utilizar o Coverage Report, executando os scrips nos arquivos "coverage-report.ps1" ou "coverage-report.sh".

## Swagger

A aplicação gera um documentação a partir do Swagger que é possivel acessar pelo endpoint:

```powershell
http://localhost:5011/swagger/index.html
```

## Debug

Em casos de iniciar o projeto em depuração deve ser iniciado o banco de dados MySQL utilizando o comando a seguir:

```powershell
docker pull mysql
docker run --name mysql -e MYSQL_ROOT_PASSWORD=TestTaskManager -d -p 3306:3306 mysql
```


## Observação

Em cada chamada ao serviço é necessário informar um header, conforme abaixo:

| Nome   | Descrição                      |
| :------| :----------------------------- |
| userId | Identificador único do Usuário |


## Refinamento

Perguntaria ao PO do projeto qual seria a media de usuarios no sistema, para dimencionar melhorias como paginação no retorno de alguns dados, adicionaria algum servico de monitoranmento e logs como o Elastic APM e Serilog para envio dos logs ao ElasticSearch.

## Final

Este projeto pode ser escalado com diversas instancias, usando um balanceador de carga para o direcionamento das chamadas, dividindo assim a carga dos servidores, para esse processo é ideal que se tenha configurado um sistema de entrega continua, com pipelines automatizadas.

## Referências

---

- [.NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
