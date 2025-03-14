# Base.Exchange.OrderAccumulator.WebApi

Este repositório contém a API **Base.Exchange.OrderAccumulator.WebApi**, que é responsável por acumular e gerenciar ordens financeiras de ações.

## Pré-requisitos

Antes de executar a aplicação, certifique-se de que você tem os seguintes pré-requisitos:

- **MongoDB**: A base de dados usada é o MongoDB.
- **.NET Core SDK**: Para executar a API Web.

## Configuração da Base de Dados

Para iniciar a base de dados **Base.Exchange** e criar as coleções necessárias, você pode executar os seguintes scripts no MongoDB.

### Passos para Criar a Base de Dados

1. Acesse o seu MongoDB e conecte-se à instância desejada.
2. Execute o script abaixo para criar a base de dados `base_exchange` e a coleção `OrderSingle`, se ainda não existirem.

### Script MongoDB

```js
use base_exchange;

if (!db.getCollectionNames().includes("OrderSingle")) {
    db.createCollection("OrderSingle");
}

db.OrderSingle.insertOne({
    "Symbol": "VALE3",
    "FinancialExposure": 500.00,
    "FinancialExposureLimit": 30000.00
});

db.OrderSingle.insertOne({
    "Symbol": "PETR4",
    "FinancialExposure": 1100.00,
    "FinancialExposureLimit": 20000.00
});

db.OrderSingle.insertOne({
    "Symbol": "VIIA4",
    "FinancialExposure": 1000.00,
    "FinancialExposureLimit": 50000.00
});

db.OrderSingle.createIndex({ "Symbol": 1 }, { unique: true });

db.OrderSingle.find({})
   .projection({})
   .sort({})
   .limit(0)
