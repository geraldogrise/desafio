# Prova

## Informações de Contato

**Nome:** Geraldo Grise Bacelar de Sousa  
**Endereço:** Rua Ministro Antônio Carlos Magalhães, 1328. Buraquinho, Lauro de Freitas  
**Telefone:** (71) 99377 - 6041  
**E-mail:** grise888@gmail.com / geraldogrise@hotmail.com  

---

## Questões

### 1) Diferença entre programação síncrona e assíncrona em Python?
Na programação síncrona, as tarefas são executadas de maneira sequencial, ou seja, uma tarefa só começa após a anterior finalizar. Já na programação assíncrona, é permitido a execução de múltiplas tarefas simultaneamente sem bloquear a execução do código principal.

### 2) O que são metaclasses em Python e como elas podem ser úteis?
Em Python, uma metaclasse é uma classe que determina como outras classes se comportam. Toda classe em Python é instância de uma metaclasse (por padrão, `type`). As metaclasses são úteis para modificar a criação de classes, como adicionar métodos ou validar atributos automaticamente.

### 3) Como funciona o garbage collector do Python e como gerenciar manualmente a memória?
O Garbage Collector (GC) do Python gerencia a memória automaticamente, limpando objetos sem referências. Ele usa contagem de referências e um coletor de ciclos para evitar vazamentos de memória.

### 4) Qual a diferença entre deepcopy e copy em Python?
- `copy`: Faz uma cópia rasa (shallow copy). Apenas as referências dos objetos aninhados são copiadas, não os valores.
- `deepcopy`: Faz uma cópia profunda (deep copy), duplicando todos os objetos recursivamente.

### 5) O que são decorators e como eles funcionam?
Decorators em Python são funções especiais que permitem modificar o comportamento de funções, métodos ou classes sem alterar seu código-fonte diretamente. Eles são amplamente utilizados para adicionar funcionalidades como logging, autenticação, caching e validação de entrada. Um decorator recebe uma função como argumento, adiciona alguma funcionalidade a ela e retorna uma nova função modificada.

### 6) O que é GIL (Global Interpreter Lock) e como afeta o multi-threading?
O GIL (Global Interpreter Lock) é um bloqueio que impede que múltiplas threads executem código Python puro ao mesmo tempo. Isso significa que Python não pode usar múltiplos threads para processamento paralelo real (apenas para tarefas I/O). Para paralelismo real, usa-se `multiprocessing`, que cria processos separados em vez de threads.

### 7) Implemente uma estrutura de dados LRUCache utilizando OrderedDict ou collections.deque.
[Código da implementação](cache.py)

### 8) Dado o seguinte DataFrame, escreva uma função para calcular a média salarial dos funcionários com idade superior a 30 anos.
[Código da implementação](pandas.py)

### 9) Implemente uma função assíncrona que faça requisições HTTP para três URLs diferentes em paralelo e retorne os conteúdos dessas páginas.
[Código da implementação](aiohttp.py)

### 10) Você foi contratado para otimizar um sistema de processamento de logs que está enfrentando problemas de performance.
O sistema recebe logs em tempo real e precisa armazená-los em um banco de dados PostgreSQL. Atualmente, o código processa cada linha de log e insere os dados individualmente, causando gargalos de performance. Reescreva a função abaixo utilizando boas práticas para otimizar o desempenho.
[Código da implementação](otimizacao.py)

