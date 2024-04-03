```sql
CREATE TABLE Prodotto(
prodottoID INT PRIMARY KEY IDENTITY(1,1),
codice VARCHAR(250) DEFAULT NEWID(),
nome VARCHAR(250) NOT NULL,
descrizione TEXT,
prezzo DECIMAL(10,2) NOT NULL,
quantita INT NOT NULL,
categoria VARCHAR(250) NOT NULL,
dataCreazione DATE DEFAULT CURRENT_TIMESTAMP,
UNIQUE (nome,categoria)
);

```
