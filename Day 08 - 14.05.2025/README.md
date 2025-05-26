
# Task - 14/05/2025
Task Description is in the **Task.txt** file

## Creat and setup servers

- Created primary server
  ```bash
  initdb -D "D:\Replication_Sample\pri"
  ```
- Modified the primary server port as 5433
- Started the primary server and created the role
  ```bash
  pg_ctl -D D:\Replication_Sample\pri -o "-p 5433" -l D:\Replication_Sample\pri\logfile start

  psql -p 5433 -d postgres -c "CREATE ROLE replicator with REPLICATION LOGIN PASSWORD 'repl_pass';"
  ```
- Created Secondary server(standby server) using replicator from primary server(5433)
  ```bash
  pg_basebackup -D D:\Replication_Sample\sec -Fp -Xs -P -R -h 127.0.0.1 -U replicator -p 5433
  ```
- Modified the Secondary server port as 5435
- Connected both the servers to the postgres database
  ```bash
  psql -p 5433 -d postgres 

  psql -p 5435 -d postgres 
  ```

### 1. Primary Server Setup (Port 5433)

#### Starting the PostgreSQL Service
- Started PostgreSQL on the primary server with:
  ```bash
  pg_ctl -D D:\Replication_Sample\pri -o "-p 5433" -l D:\Replication_Sample\pri\logfile start
  ```
  Initializes the PostgreSQL server with port `5433` and logs into the specified file.

#### Table Creation
- Created the `rental_log` table on the primary server to store rental information:
  ```sql
  CREATE TABLE rental_log (
      log_id SERIAL PRIMARY KEY,
      rental_time TIMESTAMP,
      customer_id INT,
      film_id INT,
      amount NUMERIC,
      logged_on TIMESTAMP DEFAULT CURRENT_TIMESTAMP
  );
  ```

#### Stored Procedure to Insert Rental Log
- Created a stored procedure `sp_add_rental_log` to insert rental data into the `rental_log` table:
  ```sql
  CREATE OR REPLACE PROCEDURE sp_add_rental_log(
      p_customer_id INT,
      p_film_id INT,
      p_amount NUMERIC
  )
  LANGUAGE plpgsql
  AS $$
  BEGIN
      INSERT INTO rental_log (rental_time, customer_id, film_id, amount)
      VALUES (CURRENT_TIMESTAMP, p_customer_id, p_film_id, p_amount);
  EXCEPTION WHEN OTHERS THEN
      RAISE NOTICE 'Error occurred: %', SQLERRM;
  END;
  $$;
  ```
- Executed the procedure with an example call:
  ```sql
  CALL sp_add_rental_log(1, 100, 4.99);
  ```

#### Table for Logging Updates
- Created a new table `rental_log_updation` to log changes to the `rental_log` table:
  ```sql
  CREATE TABLE rental_log_updation (
      id SERIAL PRIMARY KEY,
      column_name TEXT,
      old_value TEXT,
      new_value TEXT
  );
  ```

#### Function to Track Column Changes
- Created a trigger function `fn_log_column_changes` to track changes made to the `rental_log` table:
  ```sql
  CREATE OR REPLACE FUNCTION fn_log_column_changes()
  RETURNS TRIGGER
  LANGUAGE plpgsql
  AS $$
  BEGIN
      IF OLD.rental_time IS DISTINCT FROM NEW.rental_time THEN
          INSERT INTO rental_log_updation (column_name, old_value, new_value)
          VALUES ('rental_time', OLD.rental_time::TEXT, NEW.rental_time::TEXT);
      END IF;
  
      IF OLD.customer_id IS DISTINCT FROM NEW.customer_id THEN
          INSERT INTO rental_log_updation (column_name, old_value, new_value)
          VALUES ('customer_id', OLD.customer_id::TEXT, NEW.customer_id::TEXT);
      END IF;
  
      IF OLD.film_id IS DISTINCT FROM NEW.film_id THEN
          INSERT INTO rental_log_updation (column_name, old_value, new_value)
          VALUES ('film_id', OLD.film_id::TEXT, NEW.film_id::TEXT);
      END IF;
  
      IF OLD.amount IS DISTINCT FROM NEW.amount THEN
          INSERT INTO rental_log_updation (column_name, old_value, new_value)
          VALUES ('amount', OLD.amount::TEXT, NEW.amount::TEXT);
      END IF;
  
      RETURN NEW;
  END;
  $$;
  ```

#### Trigger for Update Logging
- Created a trigger `trg_log_column_changes` to invoke the function `fn_log_column_changes` on any update to `rental_log`:
  ```sql
  CREATE TRIGGER trg_log_column_changes
  AFTER UPDATE ON rental_log
  FOR EACH ROW
  EXECUTE FUNCTION fn_log_column_changes();
  ```

---

### 2. Secondary Server Setup (Port 5435)

#### Starting the PostgreSQL Service
- Started PostgreSQL on the secondary server with:
  ```bash
  pg_ctl -D D:\Replication_Sample\sec -o "-p 5435" -l D:\Replication_Sample\sec\logfile start
  ```
  This initializes the PostgreSQL server on port `5435` and logs it to the specified file.

#### Replication of Data
- Ensured replication is working by querying the `rental_log` table on the secondary server:
  ```sql
  SELECT * FROM rental_log ORDER BY log_id DESC LIMIT 1;
  ```
  The result returned the recently inserted log:
  ```plaintext
  log_id |        rental_time         | customer_id | film_id | amount |         logged_on
  --------+----------------------------+-------------+---------+--------+----------------------------
      1 | 2025-05-14 15:00:17.106544 |           1 |     100 |   4.99 | 2025-05-14 15:00:17.106544
  (1 row)
  ```

#### Update Verification
- After updating the rental record in the primary server (`UPDATE rental_log SET amount = 7.99, film_id = 202 WHERE log_id = 1;`), the changes were reflected on the secondary server:
  ```sql
  SELECT * FROM rental_log ORDER BY log_id;
  ```
  The updated record was confirmed:
  ```plaintext
  log_id |        rental_time         | customer_id | film_id | amount |         logged_on
  --------+----------------------------+-------------+---------+--------+----------------------------
      1 | 2025-05-14 15:00:17.106544 |           1 |     202 |   7.99 | 2025-05-14 15:00:17.106544
  (1 row)
  ```

#### Tracking Column Changes
- The column update was logged correctly in the `rental_log_updation` table on the secondary server:
  ```sql
  SELECT * FROM rental_log_updation ORDER BY id DESC;
  ```
  Output:
  ```plaintext
  id | column_name | old_value | new_value
  ----+-------------+-----------+-----------
   2 | amount      | 4.99      | 7.99
   1 | film_id     | 100       | 202
  (2 rows)
  ```

---