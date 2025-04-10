CREATE TABLE TBL_LANCAMENTO(
    LAN_ID UNIQUEIDENTIFIER  DEFAULT NEWID(),
    DT_LANCAMENTO DATETIME NOT NULL DEFAULT GETDATE(),
    TP_LANCAMENTO VARCHAR(10) CHECK (TP_LANCAMENTO IN ('DEBITO', 'CREDITO')) NOT NULL,
    VLR_LANCAMENTO DECIMAL(18,2) NOT NULL CHECK (VLR_LANCAMENTO > 0),
    DES_LANCAMENTO NVARCHAR(255),
    CAT_LANCAMENTO NVARCHAR(100),
    CREATED_AT DATETIME DEFAULT GETDATE(),
	UPDATED_AT DATETIME NULL,
	CONSTRAINT PK_TBL_LANCAMENTO PRIMARY KEY (LAN_ID)
);
