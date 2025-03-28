import pandas as pd


data = {
    'id': [1, 2, 3, 4, 5],
    'nome': ['Alice', 'Bob', 'Carlos', 'Daniel', 'Eva'],
    'idade': [25, 30, 35, 40, 45],
    'salario': [5000, 7000, 8000, 10000, 12000]
}

df = pd.DataFrame(data)


def media_salarial_mais_de_30(df):
    filtrado = df[df['idade'] > 30]  
    return filtrado['salario'].mean() 


media = media_salarial_mais_de_30(df)
print(f"Média salarial dos funcionários com mais de 30 anos: {media}")
