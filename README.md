# Desafio - API Lista de Tarefas (To Do List)

A API Lista de Tarefas (To Do List) permite que sejam cadastradas tarefas simples, com um identificador, uma descrição, uma data de abertura, uma prioridade (alta, média e baixa) e um status (pendente e concluída), sendo possível se concluir uma tarefa posteriormente (atualizando seu status de pendente para concluída). Nessa API, tarefas podem ter sua descrição e prioridade editadas, além de tarefas poderem também ser completamente removidas.

Seu desafio aqui será construir esta API seguindo além da introdução acima, alguns requisitos de interação.

## Requisitos funcionais

* Os endpoints da API devem estar agrupados sob um mesmo prefixo de rota (ex.: api/tarefas).

* Deve ser possível se ter endpoints para criação de uma nova tarefa, listagem de tarefas, detalhamento de uma tarefa, atualização de informações de uma tarefa, conclusão de uma tarefa e deleção de uma tarefa.

* Em requisições que demandam o envio do identificador, ele deve ser enviado por URL/Path parameters (parâmetros de rota).
* Na lista de tarefas deve ser possível filtrar tarefas por prioridade e/ou status, utilizando Query/String parameters (parâmetros de consulta).
* A requisição de listagem de tarefas precisa atender a dois tipos consumidores, web e mobile:
    *  Na versão web, os objetos retornados devem conter todas as propriedades.
    *  Na versão mobile existe uma preocupação com consumo de dados, então os objetos devem ser retornados em uma versão reduzida, contendo apenas a descrição e o status.

* Deve haver um controle para que qualquer requisição que não atenda às rotas definidas, tenha um retorno 404.


## Requisitos não funcionais

* Não há banco de dados, o sistema deve ser todo "in-memory".
* A API deve operar o CRUD o mais próximo possível de RESTful, promovendo consistência e facilidade de uso.


