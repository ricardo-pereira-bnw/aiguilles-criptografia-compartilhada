#!/bin/bash
#
# Acesso facilitado às linguagens PHP, C# e Delphi
#

dockerUp()
{
    docker-compose up -d
}

dockerDown()
{
    docker-compose down
}

if [ "$CURRENT_USER" == "root" ]; then
    exitError "Você não pode executar este comando como root!"
fi

#
# Certifica que o usuário local também exista dentro do conteiner
#
docker exec -it "$PHP_CONTAINER" useradd -ms /bin/bash "$CURRENT_USER" >/dev/null 2>&1

if [ "$1" == "generate" ]; then

    ROOT_PATH="$(pwd)"

    # BUILD

    # PHP
    cd $ROOT_PATH/php
    docker build -t phpapp .

    # C SHARP
    cd $ROOT_PATH/csharp
    docker build --pull -t dotnetapp .

    # DELPHI
    cd $ROOT_PATH/delphi
    docker pull radstudio/paserver

    # EXECUÇÃO

    # PHP
    cd $ROOT_PATH/php
    docker run -it --rm phpapp

    # C SHARP
    cd $ROOT_PATH/csharp
    docker run --rm dotnetapp

    # DELPHI
    cd $ROOT_PATH/delphi

fi

#
# ./bash php
# --------------------------------------------------------------------------------------------------
# Entra no shell do PHP
#
if [ "$2" == "php" ]; then
    docker exec -it --user $CURRENT_USER php-crypt bash
    exit 0
fi

#
# ./bash csharp
# --------------------------------------------------------------------------------------------------
# Entra no shell do C#
#
if [ "$2" == "csharp" ]; then
    docker exec -it --user $CURRENT_USER csharp-crypt bash
    exit 0
fi

#
# ./bash delphi
# --------------------------------------------------------------------------------------------------
# Entra no shell do Delphi
#
if [ "$2" == "delphi" ]; then
    docker exec -it --user $CURRENT_USER delphi-crypt bash
    exit 0
fi