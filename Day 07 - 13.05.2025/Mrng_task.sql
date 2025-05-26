/* 13 May 2025 - Task
1. Try two concurrent updates to same row → see lock in action.
2. Write a query using SELECT...FOR UPDATE and check how it locks row.
3. Intentionally create a deadlock and observe PostgreSQL cancel one transaction.
4. Use pg_locks query to monitor active locks.
5. Explore about Lock Modes.
*/

-- 1. try two concurrent updates to the same row → see lock in action.
-- note: each transaction was executed in a different session

-- transaction a (session 1)
begin;
update actor 
set first_name = 'Pene' 
where actor_id = 1;
-- at this point, the row with actor_id = 1 is locked for update by transaction a.

-- transaction b (session 2)
begin;
update actor 
set first_name = 'Penelo' 
where actor_id = 1;
-- this transaction will now wait because the row is already locked by transaction a.


-- 2. write a query using select...for update and check how it locks the row.

-- transaction a (session 1)
begin;
-- lock the row for update.
select * from actor 
where actor_id = 1 
for update;

-- transaction b (session 2)
begin;
update actor 
set first_name = 'Penelo' 
where actor_id = 1;
-- this transaction will now wait because the row is already locked by transaction a.

-- transaction a (session 1)
update actor 
set first_name = 'Pene' 
where actor_id = 1;
commit;


-- 3. intentionally create a deadlock and observe postgresql cancel one transaction.
-- transaction a (session 1)
begin;
update actor 
set first_name = 'Pene' 
where actor_id = 1;
-- transaction a locks row with actor_id = 1.
-- now, transaction a attempts to lock another resource (e.g., actor_id = 2).
update actor 
set first_name = 'Pene A' 
where actor_id = 2;

-- transaction b (session 2)
begin;
update actor 
set first_name = 'Penelo' 
where actor_id = 2;
-- transaction b locks row with actor_id = 2.
-- now, transaction b attempts to lock another resource (e.g., actor_id = 1), which is already locked by transaction a.
update actor 
set first_name = 'Penelo B' 
where actor_id = 1;
-- this will create a deadlock situation that postgresql will detect and resolve by canceling one of the transactions.

-- 4. use pg_locks query to monitor active locks.
select * from pg_locks 


-- 5. explore lock modes.

-- ACCESS SHARE         
-- ROW SHARE            
-- EXCLUSIVE             
-- ACCESS EXCLUSIVE  
