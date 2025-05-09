/*

Cursor-Based Questions (5)
1. Write a cursor that loops through all films and prints titles longer than 120 minutes.

2. Create a cursor that iterates through all customers and counts how many rentals each made.

3. Using a cursor, update rental rates: Increase rental rate by $1 for films with less than 5 rentals.

4. Create a function using a cursor that collects titles of all films from a particular category.

5. Loop through all stores and count how many distinct films are available in each store using a cursor.

Trigger-Based Questions (5)
1. Write a trigger that logs whenever a new customer is inserted.

2. Create a trigger that prevents inserting a payment of amount 0.

3. Set up a trigger to automatically set last_update on the film table before update.

4. Create a trigger to log changes in the inventory table (insert/delete).

5. Write a trigger that ensures a rental can’t be made for a customer who owes more than $50.

Transaction-Based Questions (5)
1. Write a transaction that inserts a customer and an initial rental in one atomic operation.

2. Simulate a failure in a multi-step transaction (update film + insert into inventory) and roll back.

3. Create a transaction that transfers an inventory item from one store to another.

4. Demonstrate SAVEPOINT and ROLLBACK TO SAVEPOINT by updating payment amounts, then undoing one.

5. Write a transaction that deletes a customer and all associated rentals and payments, ensuring atomicity.
   Procedure: get_overdue_rentals() that selects relevant columns.

*/

-- Write a cursor that loops through all films and prints titles longer than 120 minutes.

do $$
declare
    curFilms cursor for
    select title, length
    from film;
    
    filmTitle varchar(255);
    filmLength int;
begin
    open curFilms;
    fetch next from curFilms into filmTitle, filmLength;

    while found loop
        if filmLength > 120 then
            raise notice 'Title: %', filmTitle;
        end if;

        fetch next from curFilms into filmTitle, filmLength;
    end loop;

    close curFilms;
end $$;

-- Create a cursor that iterates through all customers and counts how many rentals each made.


do $$
declare
    customerRec record;
    rentalCount int;
begin
    for customerRec in select customerId from customer loop
        select count(*) into rentalCount
        from rental
        where rental.customerId = customerRec.customerId;
        
        raise notice 'Customer ID: %, Rental Count: %', customerRec.customerId, rentalCount;
    end loop;
end $$;


-- Using a cursor, update rental rates: Increase rental rate by $1 for films with less than 5 rentals.

do $$
declare
    filmRec record;
begin
    for filmRec in
        select film.filmId
        from film
        left join inventory on film.filmId = inventory.filmId
        left join rental on inventory.inventoryId = rental.inventoryId
        group by film.filmId
        having count(rental.rentalId) < 5
    loop
        update film
        set rentalRate = rentalRate + 1
        where film.filmId = filmRec.filmId;
    end loop;
end $$;


-- Create a function using a cursor that collects titles of all films from a particular category.

create or replace function getFilmsByCategory(catName text)
returns table(title text) as $$
declare
    filmRec record;
    catId integer;
begin
    select categoryId into catId from category where name = catName;
    for filmRec in 
        select f.title 
        from film f
        join filmCategory fc on f.filmId = fc.filmId
        where fc.categoryId = catId
    loop
        title := filmRec.title;
        return next;
    end loop;
end;
$$ language plpgsql;


--Loop through all stores and count how many distinct films are available in each store using a cursor.

do $$
declare
    storeRec record;
    filmCount integer;
begin
    for storeRec in select storeId from store loop
        select count(distinct filmId) into filmCount from inventory where storeId = storeRec.storeId;
        raise notice 'Store ID: %, Film Count: %', storeRec.storeId, filmCount;
    end loop;
end $$;

-- Triggers

-- Write a trigger that logs whenever a new customer is inserted.

create table customerLog (
    logId serial primary key,
    customerId int,
    logDate timestamp default now()
);

create or replace function logNewCustomer()
returns trigger as $$
begin
    insert into customerLog (customerId) values (new.customerId);
    return new;
end;
$$ language plpgsql;

create trigger trgLogNewCustomer
after insert on customer
for each row execute function logNewCustomer();

-- Create a trigger that prevents inserting a payment of amount 0.


create or replace function preventZeroPayment()
returns trigger as $$
begin
    if new.amount = 0 then
        raise exception 'Payment amount cannot be zero';
    end if;
    return new;
end;
$$ language plpgsql;

create trigger trgPreventZeroPayment
before insert on payment
for each row execute function preventZeroPayment();

-- Set up a trigger to automatically set last_update on the film table before update.

alter table film add column lastUpdate timestamp default now();

create or replace function updateLastUpdate()
returns trigger as $$
begin
    new.lastUpdate = now();
    return new;
end;
$$ language plpgsql;

create trigger trgUpdateLastUpdate
before update on film
for each row execute function updateLastUpdate();


-- Create a trigger to log changes in the inventory table (insert/delete).


create table inventoryLog (
    logId serial primary key,
    inventoryId int,
    actionType text,
    logDate timestamp default now()
);

create or replace function logInventoryChanges()
returns trigger as $$
begin
    if tg_op = 'INSERT' then
        insert into inventoryLog (inventoryId, actionType) values (new.inventoryId, 'INSERT');
    elsif tg_op = 'DELETE' then
        insert into inventoryLog (inventoryId, actionType) values (old.inventoryId, 'DELETE');
    end if;
    return null;
end;
$$ language plpgsql;

create trigger trgLogInventoryInsert
after insert on inventory
for each row execute function logInventoryChanges();

create trigger trgLogInventoryDelete
after delete on inventory
for each row execute function logInventoryChanges();

-- Write a trigger that ensures a rental can’t be made for a customer who owes more than $50.

create or replace function checkCustomerBalance()
returns trigger as $$
declare
    totalDue numeric;
begin
    select coalesce(sum(amount), 0) into totalDue from payment where customerId = new.customerId;
    if totalDue < -50 then
        raise exception 'Customer owes more than $50. Rental not allowed.';
    end if;
    return new;
end;
$$ language plpgsql;

create trigger trgCheckCustomerBalance
before insert on rental
for each row execute function checkCustomerBalance();

-- Transaction
-- Write a transaction that inserts a customer and an initial rental in one atomic operation.

begin;

insert into customer (storeId, firstName, lastName, email, addressId, activeBool, createDate, active)
values (1, 'John', 'Doe', 'johndoe@example.com', 1, true, now(), 1)
returning customerId into temp table newCustomer;

insert into rental (rentalDate, inventoryId, customerId, staffId)
values (now(), 1, (select customerId from newCustomer), 1);

commit;


-- Simulate a failure in a multi-step transaction (update film + insert into inventory) and roll back.

begin;

update film set rentalRate = rentalRate + 2 where filmId = 1;

-- simulated failure
do $$ begin
    raise exception 'Simulated failure!';
end $$;

insert into inventory (filmId, storeId) values (1, 1);

rollback;


-- Create a transaction that transfers an inventory item from one store to another.

begin;

update inventory
set storeId = 2
where inventoryId = 1;

commit;


-- Demonstrate SAVEPOINT and ROLLBACK TO SAVEPOINT by updating payment amounts, then undoing one.

begin;

savepoint sp1;

update payment set amount = amount + 5 where paymentId = 1;

savepoint sp2;

update payment set amount = amount + 10 where paymentId = 2;

rollback to savepoint sp2;

commit;


-- Write a transaction that deletes a customer and all associated rentals and payments, ensuring atomicity.

begin;

delete from payment where customerId = 1;
delete from rental where customerId = 1;
delete from customer where customerId = 1;

commit;








