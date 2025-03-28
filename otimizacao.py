import psycopg2
from psycopg2 import pool


connection_pool = pool.SimpleConnectionPool(
    minconn=1,  
    maxconn=10, 
    dsn="dbname=test user=postgres password=secret"
)

def process_log(logs, batch_size=100):
    """Insere logs no banco de forma otimizada usando batch inserts."""
    if not logs:
        return
    
    conn = connection_pool.getconn()
    try:
        cursor = conn.cursor()

        query = "INSERT INTO logs (timestamp, level, message) VALUES %s"
        values = [(log['timestamp'], log['level'], log['message']) for log in logs]

        for i in range(0, len(values), batch_size):
            batch = values[i:i + batch_size]
            psycopg2.extras.execute_values(cursor, query, batch)

        conn.commit()
        cursor.close()
    
    except Exception as e:
        conn.rollback()  
        print(f"Erro ao processar logs: {e}")

    finally:
        connection_pool.putconn(conn)  
