# Sistema de Gestao de Vendas

Este é um sistema de console desenvolvido em **C#** para gerenciar as vendas mensais de uma equipe comercial. O projeto foi construído com base em conceitos de Programação Orientada a Objetos (POO), seguindo um diagrama de classes específico.

## Funcionalidades

O sistema apresenta um menu interativo com as seguintes opções:

* **1. Cadastrar vendedor:** Permite adicionar novos vendedores à equipe (limite máximo de 10 vendedores).
* **2. Consultar vendedor:** Busca um vendedor pelo ID e exibe seu nome, total de vendas, comissão devida e a média de valor das vendas em cada dia trabalhado.
* **3. Excluir vendedor:** Remove um vendedor do sistema (a exclusão só é permitida se o vendedor não possuir nenhuma venda registrada).
* **4. Registrar venda:** Registra a quantidade de itens e o valor total vendido por um vendedor em um dia específico do mês (1 a 31).
* **5. Listar vendedores:** Exibe um relatório completo com todos os vendedores, suas respectivas vendas e comissões, finalizando com o total geral da empresa.

## Tecnologias e Estrutura

* **Linguagem:** C# (.NET)
* **Interface:** Console Application
* **Paradigma:** Programação Orientada a Objetos (POO)

A estrutura do código é dividida em três classes principais:
* `Venda`: Gerencia a quantidade e o valor das vendas, além de calcular o valor médio.
* `Vendedor`: Armazena os dados do funcionário, sua porcentagem de comissão e um vetor de até 31 dias para registrar as vendas diárias.
* `Vendedores`: Classe gerenciadora que controla o vetor de funcionários (limite de 10), valida regras de negócio e calcula os totais gerais da empresa.
